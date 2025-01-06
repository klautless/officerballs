using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;

namespace BallsWorldPatch;

public class WorldInstanceMod : IScriptMod {
    public bool ShouldRun(string path) => path == "res://Scenes/World/world.gdc";

    // returns a list of tokens for the new script, with the input being the original script's tokens
    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens) {
        // wait for any newline after any reference to "_ready"
        var waiter = new MultiTokenWaiter([
            t => t is IdentifierToken {Name: "BANK_DATA"},
            t => t.Type is TokenType.ParenthesisOpen,
            t => t is ConstantToken {Value: IntVariant {Value: 1}},
            t => t.Type is TokenType.ParenthesisClose,
        ]);

        // loop through all tokens in the script
        foreach (var token in tokens) {
            if (waiter.Check(token)) {
                // found our match, return the original newline
                
                yield return new ConstantToken(new BoolVariant(false));
                
            } else {
                // return the original token
                yield return token;
            }
        }
    }
}
