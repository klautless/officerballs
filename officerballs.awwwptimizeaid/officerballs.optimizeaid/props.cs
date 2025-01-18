using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;

namespace OptimizeAid;

public class PropMod : IScriptMod {
    public bool ShouldRun(string path) => path == "res://Scenes/Entities/Props/prop.gdc";

    // returns a list of tokens for the new script, with the input being the original script's tokens
    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens) {

        var waiter = new MultiTokenWaiter([
            t => t.Type is TokenType.BuiltInType,
            t => t.Type is TokenType.Period,
            t => t is IdentifierToken {Name: "ZERO"},
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
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.Self);
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("get_name");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("campfire"));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.Dollar);
                yield return new IdentifierToken("Particles");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("queue_free");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.Dollar);
                yield return new IdentifierToken("smoke");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("queue_free");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.Self);
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("get_name");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("mushroom_1"));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.Dollar);
                yield return new IdentifierToken("Particles");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("queue_free");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.Dollar);
                yield return new IdentifierToken("Area");
                yield return new Token(TokenType.OpDiv);
                yield return new IdentifierToken("bounce_emit");
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
