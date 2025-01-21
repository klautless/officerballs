using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;

namespace OfficerballsChatFishing;

public class Fishing3Chat : IScriptMod {
    public bool ShouldRun(string path) => path == "res://Scenes/Minigames/Fishing3/fishing3.gdc";

    // returns a list of tokens for the new script, with the input being the original script's tokens
    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens) {
        // wait for any newline after any reference to "_ready"
        var removeBG = new MultiTokenWaiter([
            t => t is ConstantToken {Value: IntVariant {Value: 1080}},
            t => t.Type is TokenType.ParenthesisClose
        ]);

        var reposition = new MultiTokenWaiter([

            t => t is IdentifierToken {Name: "offset_active"},
            t => t.Type is TokenType.Colon,
            t => t is IdentifierToken {Name: "main_offset"},
            t => t.Type is TokenType.OpAssign,
            t => t is ConstantToken {Value: IntVariant {Value: 0}},

        ]);
        // loop through all tokens in the script
        foreach (var token in tokens) {
            if (removeBG.Check(token)) {
                // found our match, return the original newline
                yield return token;

                // then add our own code
                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.Dollar);
                yield return new IdentifierToken("bars");
                yield return new Token(TokenType.OpDiv);
                yield return new IdentifierToken("ColorRect");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("queue_free");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 1);
                yield return new IdentifierToken("main");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("rect_position");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("y");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(220));


            } else if (reposition.Check(token)){

                yield return token;
                yield return new Token(TokenType.OpAdd);
                yield return new ConstantToken(new IntVariant(320));
            } else {
                // return the original token
                yield return token;
            }
        }
    }
}
