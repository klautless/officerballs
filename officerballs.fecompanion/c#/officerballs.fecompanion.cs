using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;

namespace FeCompanion;

public class CompanionMod : IScriptMod {
    public bool ShouldRun(string path) => path == "res://Scenes/HUD/playerhud.gdc";

    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens) {

        var addVar = new MultiTokenWaiter([
            t => t.Type is TokenType.PrVar,
            t => t is IdentifierToken {Name: "current_tab"},
            t => t.Type is TokenType.OpAssign,
            t => t is ConstantToken {Value: IntVariant {Value: 0}}
        ]);

        var onReady = new MultiTokenWaiter([
            t => t is IdentifierToken {Name: "_ready"},
            t => t.Type is TokenType.ParenthesisOpen,
            t => t.Type is TokenType.ParenthesisClose,
            t => t.Type is TokenType.Colon,
        ]);

        foreach (var token in tokens) {
            if (addVar.Check(token)) {

                yield return token;

                yield return new Token(TokenType.Newline);

                yield return new Token(TokenType.PrOnready);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("fecompanion");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.Dollar);
                yield return new ConstantToken(new StringVariant("/root/officerballsfecompanion"));

            } else if (onReady.Check(token)) {

                yield return token;

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.Dollar);
                yield return new IdentifierToken("main");
                yield return new Token(TokenType.OpDiv);
                yield return new IdentifierToken("in_game");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("add_child");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("fecompanion");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("femenu");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("instance");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.ParenthesisClose);


            } else {

                yield return token;
            }
        }
    }
}
public class DataMod : IScriptMod
{
    public bool ShouldRun(string path) => path == "res://Scenes/Singletons/playerdata.gdc";

    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens)
    {

        var nerfNotifications = new MultiTokenWaiter([
            t => t is IdentifierToken {Name: "_send_notification"},
            t => t.Type is TokenType.ParenthesisOpen,
            t => t is IdentifierToken {Name: "text"},
            t => t.Type is TokenType.Comma,
            t => t is IdentifierToken {Name: "type"},
            t => t.Type is TokenType.OpAssign,
            t => t is ConstantToken {Value: IntVariant {Value: 0}},
            t => t.Type is TokenType.ParenthesisClose,
            t => t.Type is TokenType.Colon,
        ]);

        foreach (var token in tokens)
        {
            if (nerfNotifications.Check(token))
            {

                yield return token;

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("text");
                yield return new Token(TokenType.OpEqual);
                yield return new ConstantToken(new StringVariant("letter sent!"));
                yield return new Token(TokenType.OpOr);
                yield return new IdentifierToken("text");
                yield return new Token(TokenType.OpEqual);
                yield return new ConstantToken(new StringVariant("letter accepted!"));
                yield return new Token(TokenType.OpOr);
                yield return new IdentifierToken("text");
                yield return new Token(TokenType.OpEqual);
                yield return new ConstantToken(new StringVariant("letter recieved!"));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpAnd);
                yield return new IdentifierToken("get_node");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("/root/officerballsfecompanion"));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("config");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("HideLetterNotifications");
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CfReturn);

            } else {

                yield return token;
            }
        }
    }
}
