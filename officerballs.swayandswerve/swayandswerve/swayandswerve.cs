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

        var waiter2 = new MultiTokenWaiter([
            t => t is IdentifierToken {Name: "in_air"},
            t => t.Type is TokenType.OpAssign,
            t => t is ConstantToken {Value: BoolVariant { Value: true }},
        ]);

        var declares = new MultiTokenWaiter([
            t => t is ConstantToken {Value: StringVariant{Value:"npc title here"}},
        ]);

        // loop through all tokens in the script
        foreach (var token in tokens) {
            if (declares.Check(token)) {

                yield return token;

                yield return new Token(TokenType.Newline);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("sway");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new RealVariant(0.0));

                yield return new Token(TokenType.Newline);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("swaying");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(false));

                yield return new Token(TokenType.Newline);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("swayflipflop");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(false));

            } else if (waiter.Check(token)) {
                // found our match, return the original newline
                //yield return token;

                // then add our own code
                yield return new Token(TokenType.BuiltInFunc, (uint?)BuiltinFunction.TextPrint);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("Hello from the SwerveMod patch!"));
                yield return new Token(TokenType.ParenthesisClose);

                // don't forget another newline!
                //yield return token;

            } else if (waiter2.Check(token)) {

                yield return token;

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("swaying");
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("swayflipflop");
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 3);
                yield return new IdentifierToken("sway");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.BuiltInFunc, 30);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("sway");
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new IntVariant(-15));
                yield return new Token(TokenType.Comma);
                yield return new IdentifierToken("delta");
                yield return new Token(TokenType.OpMul);
                yield return new ConstantToken(new IntVariant(6));
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 3);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("sway");
                yield return new Token(TokenType.OpLessEqual);
                yield return new ConstantToken(new RealVariant(-14.8));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("swayflipflop");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(false));

                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.CfElse);
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 3);
                yield return new IdentifierToken("sway");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.BuiltInFunc, 30);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("sway");
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new RealVariant(17.5));
                yield return new Token(TokenType.Comma);
                yield return new IdentifierToken("delta");
                yield return new Token(TokenType.OpMul);
                yield return new ConstantToken(new IntVariant(6));
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 3);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("sway");
                yield return new Token(TokenType.OpGreaterEqual);
                yield return new ConstantToken(new RealVariant(17.3));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("swayflipflop");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(true));

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.BuiltInFunc, 17);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("spinang");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpEqual);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("sway");
                yield return new Token(TokenType.OpLess);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.OpOr);
                yield return new IdentifierToken("sway");
                yield return new Token(TokenType.OpGreater);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 3);
                yield return new IdentifierToken("rotation");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("z");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.BuiltInFunc, 31);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("rotation");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("z");
                yield return new Token(TokenType.Comma);
                yield return new Token(TokenType.BuiltInFunc, 43);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("sway");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Comma);
                yield return new IdentifierToken("delta");
                yield return new Token(TokenType.OpMul);
                yield return new ConstantToken(new IntVariant(8));
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.CfElse);
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 3);
                yield return new IdentifierToken("rotation");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("z");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.BuiltInFunc, 31);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("rotation");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("z");
                yield return new Token(TokenType.Comma);
                yield return new Token(TokenType.BuiltInFunc, 43);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Comma);
                yield return new IdentifierToken("delta");
                yield return new Token(TokenType.OpMul);
                yield return new ConstantToken(new IntVariant(2));
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.OpNot);
                yield return new IdentifierToken("swaying");
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 3);
                yield return new IdentifierToken("sway");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.BuiltInFunc, 30);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("sway");
                yield return new Token(TokenType.Comma);
                yield return new Token(TokenType.BuiltInFunc, 43);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Comma);
                yield return new IdentifierToken("delta");
                yield return new Token(TokenType.OpMul);
                yield return new ConstantToken(new IntVariant(8));
                yield return new Token(TokenType.ParenthesisClose);

            } else {
                // return the original token
                yield return token;
            }
        }
    }
}
