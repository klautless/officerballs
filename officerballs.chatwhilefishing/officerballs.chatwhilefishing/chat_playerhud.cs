using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;

namespace OfficerballsChatFishing;

public class PlayerHUDChat : IScriptMod {
    public bool ShouldRun(string path) => path == "res://Scenes/HUD/playerhud.gdc";

    // returns a list of tokens for the new script, with the input being the original script's tokens
    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens) {
        // wait for any newline after any reference to "_ready"
        var allowChatWhileFishing = new MultiTokenWaiter([
            t => t is IdentifierToken {Name: "queue_free"},
            t => t.Type is TokenType.ParenthesisOpen,
            t => t.Type is TokenType.ParenthesisClose,
            t => t.Type is TokenType.Newline,
            t => t.Type is TokenType.CfElse,
            t => t.Type is TokenType.Colon,
            t => t.Type is TokenType.Newline,
        ]);

        var moreOfThat = new MultiTokenWaiter([

            t => t is IdentifierToken {Name: "OptionsMenu"},
            t => t.Type is TokenType.Period,
            t => t is IdentifierToken {Name: "open"},
            t => t.Type is TokenType.Colon,


        ]);

        // loop through all tokens in the script
        foreach (var token in tokens) {
            if (allowChatWhileFishing.Check(token)) {
                // found our match, return the original newline
                yield return token;
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("coconut_oil");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(false));

                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("popups");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("size");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpGreater);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 3);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.OpNot);
                yield return new IdentifierToken("popups");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("has");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("MINIGAME_fishing3"));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("coconut_oil");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(true));

                yield return new Token(TokenType.Newline, 2);
                yield return new IdentifierToken("player");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("busy");
                yield return new Token(TokenType.OpAssign);
                yield return new IdentifierToken("menu");
                yield return new Token(TokenType.OpNotEqual);
                yield return new IdentifierToken("MENUS");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("DEFAULT");
                yield return new Token(TokenType.OpOr);
                yield return new IdentifierToken("using_chat");
                yield return new Token(TokenType.OpOr);
                yield return new IdentifierToken("coconut_oil");

                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.CfIf);
                yield return new ConstantToken(new BoolVariant(false));
                yield return new Token(TokenType.Colon);

            } else if (moreOfThat.Check(token)) {

                yield return token;
                yield return new Token(TokenType.CfPass);

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("popups");
                yield return new Token(TokenType.OpEqual);
                yield return new Token(TokenType.BracketOpen);
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpOr);
                yield return new IdentifierToken("popups");
                yield return new Token(TokenType.OpEqual);
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("MINIGAME_fishing3"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpAnd);
                yield return new Token(TokenType.OpNot);
                yield return new IdentifierToken("OptionsMenu");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("open");
                yield return new Token(TokenType.Colon);

            } else {
                yield return token;
            }
        }
    }
}
