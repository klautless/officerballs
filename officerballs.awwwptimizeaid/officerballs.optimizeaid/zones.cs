using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;

namespace OptimizeAid;

public class Zones : IScriptMod {
    public bool ShouldRun(string path) => path == "res://Scenes/Map/Zones/zone.gdc";

    // returns a list of tokens for the new script, with the input being the original script's tokens
    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens) {

        var waiter = new MultiTokenWaiter([
            t => t is IdentifierToken {Name: "intro_text"},
            t => t.Type is TokenType.OpAssign,
            t => t is ConstantToken {Value: StringVariant {Value: ""}}
        ]);

        // loop through all tokens in the script
        foreach (var token in tokens) {
            if (waiter.Check(token)) {

                yield return token;

                yield return new Token(TokenType.Newline);
                yield return new Token(TokenType.PrFunction);
                yield return new IdentifierToken("_ready");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.OpNot);
                yield return new Token(TokenType.Self);
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("get_name");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("lake_zone"));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CfReturn);

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.Dollar);
                yield return new IdentifierToken("particles");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("queue_free");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.Dollar);
                yield return new IdentifierToken("sonud_ambient_zones");
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
