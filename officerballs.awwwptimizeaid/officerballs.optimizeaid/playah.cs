using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;

namespace OptimizeAid;

public class ParticleMod : IScriptMod {
    public bool ShouldRun(string path) => path == "res://Scenes/Entities/Player/player.gdc";

    // returns a list of tokens for the new script, with the input being the original script's tokens
    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens) {

        var waiter = new MultiTokenWaiter([
            t => t is IdentifierToken {Name: "global"},
            t => t.Type is TokenType.ParenthesisClose,
            t => t.Type is TokenType.Colon,
        ]);

        // loop through all tokens in the script
        foreach (var token in tokens) {
            if (waiter.Check(token)) {

                yield return token;

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("id");
                yield return new Token(TokenType.OpNotEqual);
                yield return new ConstantToken(new StringVariant("kiss"));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CfReturn);


            } else {
                // return the original token
                yield return token;
            }
        }
    }
}
