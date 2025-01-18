using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;

namespace OptimizeAid;

public class WorldMod : IScriptMod {
    public bool ShouldRun(string path) => path == "res://Scenes/World/world.gdc";

    // returns a list of tokens for the new script, with the input being the original script's tokens
    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens) {

        var waiter = new MultiTokenWaiter([
            t => t is IdentifierToken {Name: "_on_music_check_timeout"},
            t => t.Type is TokenType.ParenthesisOpen,
            t => t.Type is TokenType.ParenthesisClose,
            t => t.Type is TokenType.Colon,
        ]);

        var insurance = new MultiTokenWaiter([

            t => t is IdentifierToken {Name: "STEAM_LOBBY_ID"},
            t => t.Type is TokenType.OpGreater,
            t => t is ConstantToken {Value: IntVariant {Value: 0}},
            t => t.Type is TokenType.Colon,

        ]);
        var trysomething = new MultiTokenWaiter([

            t => t is ConstantToken {Value: IntVariant {Value: 60}}

        ]);
        // loop through all tokens in the script
        foreach (var token in tokens) {
            if (waiter.Check(token)) {

                yield return token;

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfReturn);

            } else if (insurance.Check(token)){

                yield return token;

                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("actor_type");
                yield return new Token(TokenType.OpEqual);
                yield return new ConstantToken(new StringVariant("fish_trap"));
                yield return new Token(TokenType.OpOr);
                yield return new IdentifierToken("actor_type");
                yield return new Token(TokenType.OpEqual);
                yield return new ConstantToken(new StringVariant("fish_trap_ocean"));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CfReturn);

            } else if (trysomething.Check(token)){ 
                
                yield return token;

                //yield return new Token(TokenType.Newline, 2);
                //yield return new Token(TokenType.Dollar);
                //yield return new IdentifierToken("ping_update");
                //yield return new Token(TokenType.Period);
                //yield return new IdentifierToken("wait_time");
                //yield return new Token(TokenType.OpAssign);
                //yield return new ConstantToken(new IntVariant(30));

            } else {
                // return the original token
                yield return token;
            }
        }
    }
}
