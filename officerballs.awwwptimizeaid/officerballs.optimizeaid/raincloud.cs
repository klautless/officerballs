using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;

namespace OptimizeAid;

public class RainMod : IScriptMod {
    public bool ShouldRun(string path) => path == "res://Scenes/Entities/RainCloud/raincloud.gdc";

    // returns a list of tokens for the new script, with the input being the original script's tokens
    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens) {

        var waiter = new MultiTokenWaiter([
            t => t is IdentifierToken {Name: "_ready"},
            t => t.Type is TokenType.ParenthesisOpen,
            t => t.Type is TokenType.ParenthesisClose,
            t => t.Type is TokenType.Colon,
        ]);

        // loop through all tokens in the script
        foreach (var token in tokens) {
            if (waiter.Check(token)) {

                yield return token;

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.Dollar);
                yield return new IdentifierToken("Particles");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("queue_free");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.Dollar);
                yield return new IdentifierToken("Particles_sheet");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("queue_free");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);


            } else {
                // return the original token
                yield return token;
            }
        }
    }
}
