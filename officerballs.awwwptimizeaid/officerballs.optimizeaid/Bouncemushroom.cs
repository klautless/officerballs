using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;

namespace OptimizeAid;

public class MushroomMod : IScriptMod {
    public bool ShouldRun(string path) => path == "res://Scenes/Map/Props/mushroom_bounce.gdc";

    // returns a list of tokens for the new script, with the input being the original script's tokens
    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens) {

        var waiter = new MultiTokenWaiter([
            t => t is ConstantToken {Value: RealVariant {Value: 0.5}},
            t => t.Type is TokenType.ParenthesisClose,
        ]);

        var eof = new MultiTokenWaiter([

            t => t.Type is TokenType.Eof

        ]);

        // loop through all tokens in the script
        foreach (var token in tokens) {
            if (waiter.Check(token)) {

                yield return token;

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfReturn);


            } else if (eof.Check(token)) {

                yield return new Token(TokenType.Newline);
                yield return new Token(TokenType.PrFunction);
                yield return new IdentifierToken("_ready");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.Dollar);
                yield return new IdentifierToken("Particles");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("queue_free");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.Dollar);
                yield return new IdentifierToken("bounce_emit");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("queue_free");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);

                yield return token;

            } else { 
                yield return token;
            }
        }
    }
}
