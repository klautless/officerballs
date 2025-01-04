using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;

namespace Nyan;

public class NyanMod : IScriptMod
{
    public bool ShouldRun(string path) => path == "res://Scenes/Entities/Player/player.gdc";

    // returns a list of tokens for the new script, with the input being the original script's tokens
    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens)
    {

        var declared = new MultiTokenWaiter([
            t => t.Type is TokenType.PrVar,
            t => t is IdentifierToken {Name: "camera_zoom"},
            t => t.Type is TokenType.OpAssign,
            t => t is ConstantToken {Value: RealVariant{ Value: 5.0}},
            ]);

        var divecorrection = new MultiTokenWaiter([
            t => t.Type is TokenType.ParenthesisClose,
            t => t.Type is TokenType.Newline,
            t => t is IdentifierToken {Name: "snapped"},
            t => t.Type is TokenType.OpAssign,
            t => t is ConstantToken {Value: BoolVariant{Value: false}},
            t => t.Type is TokenType.Newline,
            t => t is IdentifierToken {Name: "diving"},
            t => t.Type is TokenType.OpAssign,
            t => t is ConstantToken {Value: BoolVariant{Value: true}},
            t => t.Type is TokenType.Newline,
            t => t is IdentifierToken {Name: "dive_vec"},
            t => t.Type is TokenType.OpAssign,
            t => t.Type is TokenType.OpIn,
            t => t is IdentifierToken {Name: "transform"},
            t => t.Type is TokenType.Period,
            t => t is IdentifierToken {Name: "basis"},
            t => t.Type is TokenType.Period,
            t => t is IdentifierToken {Name: "z"},
            ]);

        var spineyes = new MultiTokenWaiter([
            t => t.Type is TokenType.CfIf,
            t => t is IdentifierToken {Name: "direction"},
            t => t.Type is TokenType.OpNotEqual,
            t => t.Type is TokenType.BuiltInType,
            t => t.Type is TokenType.Period,
            t => t is IdentifierToken {Name: "ZERO"},
        ]);

        var zoomerin = new MultiTokenWaiter([
            t => t.Type is TokenType.Colon,
            t => t.Type is TokenType.Newline,
            t => t is IdentifierToken {Name: "camera_zoom"},
            ]);

        var zoomerout = new MultiTokenWaiter([
            t => t.Type is TokenType.Colon,
            t => t.Type is TokenType.Newline,
            t => t is IdentifierToken {Name: "camera_zoom"},
            ]);

        // loop through all tokens in the script
        foreach (var token in tokens)
        {
            if (spineyes.Check(token))
            {
                yield return token;

                yield return new Token(TokenType.OpAnd);
                yield return new Token(TokenType.OpNot);
                yield return new IdentifierToken("spinnies");

                // don't forget another newline!
                //yield return token;
            }
            else if (declared.Check(token))
            {
                yield return token;
                yield return new Token(TokenType.Newline);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("spinnies");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(false));
                yield return new Token(TokenType.Newline);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("nyan_zoomlock");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(false));
            }
            else if (zoomerin.Check(token))
            {
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.OpNot);
                yield return new IdentifierToken("nyan_zoomlock");
                yield return new Token(TokenType.Colon);
                yield return token;

            }
            else if (zoomerout.Check(token))
            {
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.OpNot);
                yield return new IdentifierToken("nyan_zoomlock");
                yield return new Token(TokenType.Colon);
                yield return token;

            }
            else if (divecorrection.Check(token))
            {
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Newline);
                yield return new IdentifierToken("snapped");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(false));
                yield return new Token(TokenType.Newline);
                yield return new IdentifierToken("diving");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(true));
                yield return new Token(TokenType.Newline);
                yield return new IdentifierToken("dive_vec");
                yield return new Token(TokenType.OpAssign);
                yield return new IdentifierToken("direction");
            }
            else
            {
                // return the original token
                yield return token;
            }
        }
    }
}
