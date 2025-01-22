using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;

namespace OfficerBallsBuffLib;

public class BufflibFXBox : IScriptMod {
    public bool ShouldRun(string path) => path == "res://Scenes/HUD/StatusEffectbox/statusfxbox.gdc";

    // returns a list of tokens for the new script, with the input being the original script's tokens
    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens) {

        var addAccurateTimers = new MultiTokenWaiter([

            t => t is IdentifierToken {Name: "tier"},
            t => t.Type is TokenType.Comma,
            t => t is IdentifierToken {Name: "time"},

        ]);

        var checkTheTime = new MultiTokenWaiter([

            t => t is IdentifierToken {Name: "tier"},
            t => t.Type is TokenType.OpGreater,
            t => t is ConstantToken {Value: IntVariant {Value: 0}},
            t => t.Type is TokenType.Newline,
            t => t.Type is TokenType.Newline,

        ]);

        foreach (var token in tokens) {
            if (addAccurateTimers.Check(token)) {

                yield return token;
                yield return new Token(TokenType.Comma);
                yield return new IdentifierToken("williamwonka");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(false));

            } else if (checkTheTime.Check(token)) {
            
                yield return token;
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("williamwonka");
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Dollar);
                yield return new IdentifierToken("TooltipNode");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("body");
                yield return new Token(TokenType.OpAssign);
                yield return new IdentifierToken("desc");
                yield return new Token(TokenType.OpAdd);
                yield return new ConstantToken(new StringVariant("\n"));
                yield return new Token(TokenType.OpAdd);
                yield return new Token(TokenType.BuiltInFunc, 62); // str
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.BuiltInFunc, 14); // floor
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("time");
                yield return new Token(TokenType.OpDiv);
                yield return new ConstantToken(new RealVariant(60.0));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpAdd);
                yield return new ConstantToken(new StringVariant(" min"));

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfElse);
                yield return new Token(TokenType.Colon);

            } else {

                yield return token;
            }
        }
    }
}
