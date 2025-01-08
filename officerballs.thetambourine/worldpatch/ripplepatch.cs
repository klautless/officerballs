using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;
using System.Collections.Generic;

namespace BallsWorldPatch;

public class RipplePatch : IScriptMod
{
    public bool ShouldRun(string path) => path == "res://Scenes/Entities/FishSpawn/fish_spawn.gdc";

    // returns a list of tokens for the new script, with the input being the original script's tokens
    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens)
    {
        // wait for any newline after any reference to "_ready"
        var waiter = new MultiTokenWaiter([
            t => t is IdentifierToken {Name: "disabled"},
            t => t.Type is TokenType.OpAssign,
            t => t is ConstantToken {Value: BoolVariant {Value:true}}
        ]);

        // loop through all tokens in the script
        foreach (var token in tokens)
        {
            if (waiter.Check(token))
            {
                // found our match, return the original newline

                yield return token;
                yield return new Token(TokenType.OpAnd);
                yield return new ConstantToken(new BoolVariant(false));

            }
            else
            {
                // return the original token
                yield return token;
            }
        }
    }
}
