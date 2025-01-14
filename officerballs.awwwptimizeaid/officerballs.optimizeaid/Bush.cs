using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;

namespace OptimizeAid;

public class BushMod : IScriptMod {
    public bool ShouldRun(string path) => path == "res://Scenes/Map/Props/bush_particle_detect.gdc";

    // returns a list of tokens for the new script, with the input being the original script's tokens
    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens) {

        var waiter = new MultiTokenWaiter([
            t => t is IdentifierToken {Name: "body"},
            t => t.Type is TokenType.ParenthesisClose,
            t => t.Type is TokenType.Colon,
        ]);

        // loop through all tokens in the script
        foreach (var token in tokens) {
            if (waiter.Check(token)) {

                yield return token;

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfReturn);


            } else {
                // return the original token
                yield return token;
            }
        }
    }
}
