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
            t => t is IdentifierToken {Name:"dive_distance"},
            t => t.Type is TokenType.OpMul,
            t => t is IdentifierToken {Name:"dive_bonus"},
            t => t.Type is TokenType.Newline,
        ]);


        var spineyes = new MultiTokenWaiter([
            t => t.Type is TokenType.CfIf,
            t => t is IdentifierToken {Name: "direction"},
            t => t.Type is TokenType.OpNotEqual,
            t => t.Type is TokenType.BuiltInType,
            t => t.Type is TokenType.Period,
            t => t is IdentifierToken {Name: "ZERO"},
        ]);

        var spinassist = new MultiTokenWaiter([
            t => t is IdentifierToken {Name: "y"},
            t => t.Type is TokenType.ParenthesisClose,
            t => t.Type is TokenType.Newline,
            t => t is IdentifierToken {Name: "rotation"},
            t => t.Type is TokenType.Period,
            t => t is IdentifierToken {Name: "y"},
            t => t.Type is TokenType.OpAssign,
            t => t.Type is TokenType.BuiltInFunc,
            t => t.Type is TokenType.ParenthesisOpen,
            t => t is IdentifierToken {Name: "rotation"},
            t => t.Type is TokenType.Period,
            t => t is IdentifierToken {Name: "y"},
            t => t.Type is TokenType.Comma,
            t => t is IdentifierToken {Name: "rot_help"},
            t => t.Type is TokenType.Period,
            t => t is IdentifierToken {Name: "rotation"},
            t => t.Type is TokenType.Period,
            t => t is IdentifierToken {Name: "y"},
            t => t.Type is TokenType.Comma,
            t => t is ConstantToken {Value: RealVariant{Value:0.2}},
            t => t.Type is TokenType.ParenthesisClose,
        ]);

        var shutoffvalve = new MultiTokenWaiter([
            t => t.Type is TokenType.PrFunction,
            t => t is IdentifierToken {Name: "_process_timers"},
            t => t.Type is TokenType.ParenthesisOpen,
            t => t.Type is TokenType.ParenthesisClose,
            t => t.Type is TokenType.Colon,
            t => t.Type is TokenType.Newline,

        ]);

        var busyhide = new MultiTokenWaiter([
            t => t is IdentifierToken {Name: "animation_data"},
            t => t.Type is TokenType.BracketOpen,
            t => t is ConstantToken{Value:StringVariant{Value:"busy"}},
            t => t.Type is TokenType.BracketClose,
            t => t.Type is TokenType.OpAssign,
            //t => t is IdentifierToken {Name: "busy"},

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
            if (declared.Check(token))
            {
                yield return token;
                yield return new Token(TokenType.Newline);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("spinnies");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(false));
                yield return new Token(TokenType.Newline);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("spinang");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Newline);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("spinfactor");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Newline);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("helishut");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(false));
                yield return new Token(TokenType.Newline);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("busyblock");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(true));
                yield return new Token(TokenType.Newline);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("nyan_zoomlock");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(false));
            }
            else if (busyhide.Check(token))
            {
                yield return token;
                yield return new IdentifierToken("busyblock");
                yield return new Token(TokenType.OpAnd);
            }
            else if (spineyes.Check(token))
            {
                yield return token;

                yield return new Token(TokenType.OpAnd);
                yield return new Token(TokenType.OpNot);
                yield return new IdentifierToken("spinnies");

            }
            else if(shutoffvalve.Check(token))
            {
                yield return token;
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("helishut");
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("spinang");
                yield return new Token(TokenType.OpGreater);
                yield return new ConstantToken(new IntVariant(100));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 3);
                yield return new IdentifierToken("spinang");
                yield return new Token(TokenType.OpAssignSub);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.CfElif);
                yield return new IdentifierToken("spinang");
                yield return new Token(TokenType.OpGreater);
                yield return new ConstantToken(new IntVariant(50));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 3);
                yield return new IdentifierToken("spinang");
                yield return new Token(TokenType.OpAssignSub);
                yield return new ConstantToken(new RealVariant(0.5));
                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.CfElif);
                yield return new IdentifierToken("spinang");
                yield return new Token(TokenType.OpGreater);
                yield return new ConstantToken(new IntVariant(25));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 3);
                yield return new IdentifierToken("spinang");
                yield return new Token(TokenType.OpAssignSub);
                yield return new ConstantToken(new RealVariant(0.25));
                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.CfElif);
                yield return new IdentifierToken("spinang");
                yield return new Token(TokenType.OpGreater);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 3);
                yield return new IdentifierToken("spinang");
                yield return new Token(TokenType.OpAssignSub);
                yield return new ConstantToken(new RealVariant(0.125));
                yield return new Token(TokenType.Newline, 2);

                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("spinang");
                yield return new Token(TokenType.OpLess);
                yield return new ConstantToken(new IntVariant(-100));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 3);
                yield return new IdentifierToken("spinang");
                yield return new Token(TokenType.OpAssignAdd);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.CfElif);
                yield return new IdentifierToken("spinang");
                yield return new Token(TokenType.OpLess);
                yield return new ConstantToken(new IntVariant(-50));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 3);
                yield return new IdentifierToken("spinang");
                yield return new Token(TokenType.OpAssignAdd);
                yield return new ConstantToken(new RealVariant(0.5));
                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.CfElif);
                yield return new IdentifierToken("spinang");
                yield return new Token(TokenType.OpLess);
                yield return new ConstantToken(new IntVariant(-25));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 3);
                yield return new IdentifierToken("spinang");
                yield return new Token(TokenType.OpAssignAdd);
                yield return new ConstantToken(new RealVariant(0.25));
                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.CfElif);
                yield return new IdentifierToken("spinang");
                yield return new Token(TokenType.OpLess);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 3);
                yield return new IdentifierToken("spinang");
                yield return new Token(TokenType.OpAssignAdd);
                yield return new ConstantToken(new RealVariant(0.125));
                yield return new Token(TokenType.Newline, 2);

                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("thestring");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.BuiltInFunc, 62); //str
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.BuiltInFunc, 17); // abs
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("spinang");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpAdd);
                yield return new ConstantToken(new StringVariant(" spinnies per hour."));
                yield return new Token(TokenType.Newline, 2);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("emit_signal");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("_help_update"));
                yield return new Token(TokenType.Comma);
                yield return new IdentifierToken("thestring");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("spinang");
                yield return new Token(TokenType.OpEqual);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 3);
                yield return new IdentifierToken("spinfactor");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.BuiltInFunc, 43);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("rotation");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("y");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Newline, 3);
                yield return new IdentifierToken("spinnies");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(false));
                yield return new Token(TokenType.Newline, 3);
                yield return new IdentifierToken("helishut");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(false));
                yield return new Token(TokenType.Newline, 1);

            }
            else if(spinassist.Check(token))
            { 
                yield return token;
                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfElif);
                yield return new IdentifierToken("spinnies");
                yield return new Token(TokenType.OpAnd);
                yield return new IdentifierToken("spinang");
                yield return new Token(TokenType.OpNotEqual);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 2);
                yield return new IdentifierToken("spinfactor");
                yield return new Token(TokenType.OpAssignSub);
                yield return new IdentifierToken("spinang");
                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("spinfactor");
                yield return new Token(TokenType.OpLess);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("spinfactor");
                yield return new Token(TokenType.OpAssignAdd);
                yield return new ConstantToken(new IntVariant(360));
                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("spinfactor");
                yield return new Token(TokenType.OpGreater);
                yield return new ConstantToken(new IntVariant(360));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("spinfactor");
                yield return new Token(TokenType.OpAssignSub);
                yield return new ConstantToken(new IntVariant(360));
                yield return new Token(TokenType.Newline, 2);
                yield return new IdentifierToken("rotation");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("y");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.BuiltInFunc, 31); //lerp
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("rotation");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("y");
                yield return new Token(TokenType.Comma);
                yield return new Token(TokenType.BuiltInFunc, 43); //deg2rad
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("spinfactor");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new RealVariant(0.2));
                yield return new Token(TokenType.ParenthesisClose);
                //built in 43
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
                yield return token;
                yield return new IdentifierToken("dive_vec");
                yield return new Token(TokenType.OpAssign);
                yield return new IdentifierToken("direction");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("normalized");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpMul);
                yield return new IdentifierToken("dive_distance");
                yield return new Token(TokenType.OpMul);
                yield return new IdentifierToken("dive_bonus");
                yield return new Token(TokenType.Newline, 3);
            }
            else
            {
                // return the original token
                yield return token;
            }
        }
    }
}
