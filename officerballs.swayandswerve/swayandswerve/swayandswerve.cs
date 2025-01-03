using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;

namespace Swerve;

public class SwerveMod : IScriptMod {
    public bool ShouldRun(string path) => path == "res://Scenes/Entities/Player/player.gdc";

    // returns a list of tokens for the new script, with the input being the original script's tokens
    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens) {
        // wait for any newline after any reference to "_ready"
        var waiter = new MultiTokenWaiter([
            t => t.Type is TokenType.CfIf,
            t => t is IdentifierToken {Name: "diving"},
            t => t.Type is TokenType.Colon,
            t => t is IdentifierToken {Name: "speed_mult"},
            t => t.Type is TokenType.OpAssign,
            t => t is ConstantToken {Value: RealVariant { Value: 0.0 }},
        ]);

        // loop through all tokens in the script
        foreach (var token in tokens) {
            if (waiter.Check(token)) {
                // found our match, return the original newline
                //yield return token;

                // then add our own code
                yield return new Token(TokenType.BuiltInFunc, (uint?) BuiltinFunction.TextPrint);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("Hello from the SwerveMod patch!"));
                yield return new Token(TokenType.ParenthesisClose);

                // don't forget another newline!
                //yield return token;
            } else {
                // return the original token
                yield return token;
            }
        }
    }
}
