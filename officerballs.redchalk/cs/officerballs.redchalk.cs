using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;

namespace RedChalk;

public class RedChalkMod : IScriptMod {
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
                yield return new IdentifierToken("redchalk");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.Dollar);
                yield return new ConstantToken(new StringVariant("/root/officerballsredchalk"));

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
                yield return new IdentifierToken("redchalk");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("redmenu");
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
