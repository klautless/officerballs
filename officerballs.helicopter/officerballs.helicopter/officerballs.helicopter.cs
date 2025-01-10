using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;
using System.Numerics;

namespace Helicopter;

public class HelicopterMod : IScriptMod
{
    public bool ShouldRun(string path) => path == "res://Scenes/Entities/Player/player.gdc";

    // returns a list of tokens for the new script, with the input being the original script's tokens
    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens)
    {
        // wait for any newline after any reference to "_ready"

        var addvariables = new MultiTokenWaiter([
            t => t is ConstantToken {Value: StringVariant{Value:"npc title here"}},
        ]);

        var addanimation1 = new MultiTokenWaiter([
            t => t.Type is TokenType.OpAnd,
            t => t.Type is TokenType.OpNot,
            t => t is IdentifierToken {Name: "emoting"},
            t => t.Type is TokenType.Newline,

        ]);

        var addanimation2 = new MultiTokenWaiter([

            t => t.Type is TokenType.OpAssign,
            t => t is IdentifierToken {Name: "sitting"},
            t => t.Type is TokenType.Newline,

        ]);

        var addanimationcore = new MultiTokenWaiter([

            t => t.Type is TokenType.OpAssign,
            t => t is IdentifierToken {Name: "drunk_tier"},

        ]);

        var mover1 = new MultiTokenWaiter([

            t => t.Type is TokenType.OpAnd,
            t => t is IdentifierToken {Name: "in_air"},

        ]);

        var mover2 = new MultiTokenWaiter([

            t => t is IdentifierToken {Name: "in_air"},
            t => t.Type is TokenType.OpAssign,
            t => t is ConstantToken {Value: BoolVariant {Value: true}},

        ]);

        var mover3 = new MultiTokenWaiter([

            t => t is IdentifierToken {Name: "is_on_floor"},
            t => t.Type is TokenType.ParenthesisOpen,
            t => t.Type is TokenType.ParenthesisClose,
            t => t.Type is TokenType.OpAnd,
            t => t is IdentifierToken {Name: "ignore_snap"},
            t => t.Type is TokenType.OpLessEqual,
            t => t is ConstantToken {Value: IntVariant {Value: 0 }},

        ]);

        var mover4 = new MultiTokenWaiter([

            t => t is IdentifierToken {Name: "snapped"},
            t => t.Type is TokenType.OpAnd,
            t => t is IdentifierToken {Name: "ignore_snap"},
            t => t.Type is TokenType.OpLessEqual,
            t => t is ConstantToken {Value: IntVariant {Value: 0 }},

        ]);

        var mover5 = new MultiTokenWaiter([

            t => t is IdentifierToken {Name: "ZERO"},
            t => t.Type is TokenType.Newline,
            t => t.Type is TokenType.CfIf,
            t => t is IdentifierToken {Name: "is_on_floor"},
            t => t.Type is TokenType.ParenthesisOpen,
            t => t.Type is TokenType.ParenthesisClose,

        ]);

        var mover6 = new MultiTokenWaiter([

            t => t is IdentifierToken {Name: "jump_bonus"},
            t => t.Type is TokenType.Comma,
            t => t is ConstantToken {Value: IntVariant{Value:0}},
            t => t.Type is TokenType.ParenthesisClose,

        ]);

        var mover7 = new MultiTokenWaiter([

            t => t is IdentifierToken {Name: "delta"},
            t => t.Type is TokenType.OpMul,
            t => t is IdentifierToken {Name: "_accel"},
            t => t.Type is TokenType.ParenthesisClose,
            t => t.Type is TokenType.Newline,

        ]);

        var mover8 = new MultiTokenWaiter([

            t => t is IdentifierToken {Name: "snap_vec"},
            t => t.Type is TokenType.Comma,
            t => t.Type is TokenType.BuiltInType,
            t => t.Type is TokenType.Period,
            t => t is IdentifierToken {Name: "UP"},
            t => t.Type is TokenType.ParenthesisClose,
            t => t.Type is TokenType.Newline,

        ]);

        var mover9 = new MultiTokenWaiter([

            t => t is IdentifierToken {Name: "snapped"},
            t => t.Type is TokenType.OpAssign,
            t => t is ConstantToken {Value: BoolVariant{Value: false }},
            t => t.Type is TokenType.Newline,
            t => t.Type is TokenType.CfElse,


        ]);

        var addhotkey = new MultiTokenWaiter([

            t => t is IdentifierToken {Name: "MOUSE_MODE_VISIBLE"},
            t => t.Type is TokenType.ParenthesisClose,
            t => t.Type is TokenType.Newline,
            t => t.Type is TokenType.CfReturn,


        ]);

        // loop through all tokens in the script
        foreach (var token in tokens)
        {
            if (addvariables.Check(token))
            {

                yield return token;

                yield return new Token(TokenType.Newline);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("transit");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(false));

                yield return new Token(TokenType.Newline);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("rollOver");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(false));

                yield return new Token(TokenType.Newline);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("rollOverLatch");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(false));

                yield return new Token(TokenType.Newline);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("spinlatch");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(false));

                yield return new Token(TokenType.Newline);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("latchtimer");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new RealVariant(0.0));

                yield return new Token(TokenType.Newline);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("gravFlop");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(false));

                yield return new Token(TokenType.Newline);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("gravCount");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new RealVariant(-1.0));

            }
            else if (addanimation1.Check(token))
            {

                yield return token;

                yield return new Token(TokenType.CfIf);
                yield return new ConstantToken(new BoolVariant(false));
                yield return new Token(TokenType.Colon);

            }
            else if (addanimation2.Check(token))
            {

                yield return token;

                yield return new Token(TokenType.CfIf);
                yield return new ConstantToken(new BoolVariant(false));
                yield return new Token(TokenType.Colon);

            }
            else if (addanimationcore.Check(token))
            {

                yield return token;

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("rollOver");
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 2);
                yield return new IdentifierToken("animation_data");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("sitting"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.BuiltInFunc, 30);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("animation_data");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("sitting"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new RealVariant(0.2));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new RealVariant(0.04));
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 2);
                yield return new IdentifierToken("animation_data");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("land"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.BuiltInFunc, 30);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("animation_data");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("land"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new RealVariant(0.7));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new RealVariant(0.04));
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.OpNot);
                yield return new IdentifierToken("rollOverLatch");
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 3);
                yield return new IdentifierToken("transit");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(true));

                yield return new Token(TokenType.Newline, 3);
                yield return new Token(TokenType.PrYield);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("get_tree");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("create_timer");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new RealVariant(0.5));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("timeout"));
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 3);
                yield return new IdentifierToken("rollOverLatch");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(true));

                yield return new Token(TokenType.Newline, 3);
                yield return new IdentifierToken("transit");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(false));

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.OpNot);
                yield return new IdentifierToken("rollOver");
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 2);
                yield return new IdentifierToken("animation_data");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("sitting"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.BuiltInFunc, 30);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("animation_data");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("sitting"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.Comma);
                yield return new IdentifierToken("sitting");
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new RealVariant(0.04));
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 2);
                yield return new IdentifierToken("animation_data");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("land"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.BuiltInFunc, 30);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("animation_data");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("land"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new RealVariant(0.0));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new RealVariant(0.04));
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("rollOverLatch");
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 3);
                yield return new IdentifierToken("transit");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(true));

                yield return new Token(TokenType.Newline, 3);
                yield return new IdentifierToken("spinlatch");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(false));

                yield return new Token(TokenType.Newline, 3);
                yield return new Token(TokenType.PrYield);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("get_tree");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("create_timer");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new RealVariant(0.5));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("timeout"));
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 3);
                yield return new IdentifierToken("rollOverLatch");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(false));

                yield return new Token(TokenType.Newline, 3);
                yield return new IdentifierToken("transit");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(false));

            }
            else if (mover2.Check(token))
            {

                yield return token;
                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.OpNot);
                yield return new IdentifierToken("transit");
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.BuiltInFunc, 17); //abs
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("spinang");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpGreater);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.OpAnd);
                yield return new Token(TokenType.BuiltInFunc, 17); //abs
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("spinang");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpLess);
                yield return new ConstantToken(new IntVariant(14));
                yield return new Token(TokenType.OpAnd);
                yield return new IdentifierToken("spinnies");
                yield return new Token(TokenType.OpAnd);
                yield return new Token(TokenType.OpNot);
                yield return new IdentifierToken("rollOver");
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 3);
                yield return new IdentifierToken("rotation");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("z");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.BuiltInFunc, 31); //lerp
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("rotation");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("z");
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new IntVariant(5));
                yield return new Token(TokenType.OpMul);
                yield return new Token(TokenType.BuiltInFunc, 43); //deg2rad
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.OpSub);
                yield return new IdentifierToken("spinang");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Comma);
                yield return new IdentifierToken("delta");
                yield return new Token(TokenType.OpMul);
                yield return new ConstantToken(new IntVariant(2));
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 3);
                yield return new IdentifierToken("rotation");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("x");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.BuiltInFunc, 31); //lerp
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("rotation");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("x");
                yield return new Token(TokenType.Comma);
                yield return new Token(TokenType.BuiltInFunc, 43); //deg2rad
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Comma);
                yield return new IdentifierToken("delta");
                yield return new Token(TokenType.OpMul);
                yield return new ConstantToken(new IntVariant(2));
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.CfElif);
                yield return new Token(TokenType.BuiltInFunc, 17); //abs
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("spinang");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpGreaterEqual);
                yield return new ConstantToken(new IntVariant(14));
                yield return new Token(TokenType.OpAnd);
                yield return new Token(TokenType.BuiltInFunc, 17); //abs
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("spinang");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpLess);
                yield return new ConstantToken(new IntVariant(50));
                yield return new Token(TokenType.OpAnd);
                yield return new Token(TokenType.OpNot);
                yield return new IdentifierToken("rollOver");
                yield return new Token(TokenType.OpAnd);
                yield return new IdentifierToken("spinnies");
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 3);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("spinang");
                yield return new Token(TokenType.OpGreater);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("rotation");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("z");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.BuiltInFunc, 31); //lerp
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("rotation");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("z");
                yield return new Token(TokenType.Comma);
                yield return new Token(TokenType.BuiltInFunc, 43); //deg2rad
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new IntVariant(-70));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Comma);
                yield return new IdentifierToken("delta");
                yield return new Token(TokenType.OpMul);
                yield return new ConstantToken(new IntVariant(2));
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 3);
                yield return new Token(TokenType.CfElse);
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("rotation");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("z");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.BuiltInFunc, 31); //lerp
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("rotation");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("z");
                yield return new Token(TokenType.Comma);
                yield return new Token(TokenType.BuiltInFunc, 43); //deg2rad
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new IntVariant(70));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Comma);
                yield return new IdentifierToken("delta");
                yield return new Token(TokenType.OpMul);
                yield return new ConstantToken(new IntVariant(2));
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 3);
                yield return new IdentifierToken("rotation");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("x");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.BuiltInFunc, 31); //lerp
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("rotation");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("x");
                yield return new Token(TokenType.Comma);
                yield return new Token(TokenType.BuiltInFunc, 43); //deg2rad
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
                yield return new Token(TokenType.BuiltInFunc, 17); //abs
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("spinang");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpGreaterEqual);
                yield return new ConstantToken(new IntVariant(50));
                yield return new Token(TokenType.OpAnd);
                yield return new IdentifierToken("spinnies");
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 3);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("spinang");
                yield return new Token(TokenType.OpGreater);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 4);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.OpNot);
                yield return new IdentifierToken("rollOver");
                yield return new Token(TokenType.OpAnd);
                yield return new Token(TokenType.BuiltInFunc, 17); //abs
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("spinang");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpLess);
                yield return new ConstantToken(new IntVariant(130));
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 5);
                yield return new IdentifierToken("rotation");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("z");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.BuiltInFunc, 31); //lerp
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("rotation");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("z");
                yield return new Token(TokenType.Comma);
                yield return new Token(TokenType.BuiltInFunc, 43); //deg2rad
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new RealVariant(0.875));
                yield return new Token(TokenType.OpMul);
                yield return new IdentifierToken("spinang");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpSub);
                yield return new ConstantToken(new RealVariant(113.75));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Comma);
                yield return new IdentifierToken("delta");
                yield return new Token(TokenType.OpMul);
                yield return new ConstantToken(new IntVariant(2));
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 5);
                yield return new IdentifierToken("rotation");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("x");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.BuiltInFunc, 31); //lerp
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("rotation");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("x");
                yield return new Token(TokenType.Comma);
                yield return new Token(TokenType.BuiltInFunc, 43); //deg2rad
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.OpSub);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new RealVariant(0.9375));
                yield return new Token(TokenType.OpMul);
                yield return new IdentifierToken("spinang");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpAdd);
                yield return new ConstantToken(new RealVariant(46.875));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Comma);
                yield return new IdentifierToken("delta");
                yield return new Token(TokenType.OpMul);
                yield return new ConstantToken(new IntVariant(2));
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 4);
                yield return new Token(TokenType.CfElif);
                yield return new Token(TokenType.OpNot);
                yield return new IdentifierToken("rollOver");
                yield return new Token(TokenType.OpAnd);
                yield return new Token(TokenType.BuiltInFunc, 17); //abs
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("spinang");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpGreaterEqual);
                yield return new ConstantToken(new IntVariant(130));
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 5);
                yield return new IdentifierToken("rotation");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("z");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.BuiltInFunc, 31); //lerp
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("rotation");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("z");
                yield return new Token(TokenType.Comma);
                yield return new Token(TokenType.BuiltInFunc, 43); //deg2rad
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Comma);
                yield return new IdentifierToken("delta");
                yield return new Token(TokenType.OpMul);
                yield return new ConstantToken(new IntVariant(2));
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 5);
                yield return new IdentifierToken("rotation");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("x");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.BuiltInFunc, 31); //lerp
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("rotation");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("x");
                yield return new Token(TokenType.Comma);
                yield return new Token(TokenType.BuiltInFunc, 43); //deg2rad
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new IntVariant(-75));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Comma);
                yield return new IdentifierToken("delta");
                yield return new Token(TokenType.OpMul);
                yield return new ConstantToken(new IntVariant(2));
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 3);
                yield return new Token(TokenType.CfElse);
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 4);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.OpNot);
                yield return new IdentifierToken("rollOver");
                yield return new Token(TokenType.OpAnd);
                yield return new Token(TokenType.BuiltInFunc, 17); //abs
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("spinang");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpLess);
                yield return new ConstantToken(new IntVariant(130));
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 5);
                yield return new IdentifierToken("rotation");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("z");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.BuiltInFunc, 31); //lerp
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("rotation");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("z");
                yield return new Token(TokenType.Comma);
                yield return new Token(TokenType.BuiltInFunc, 43); //deg2rad
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new RealVariant(0.875));
                yield return new Token(TokenType.OpMul);
                yield return new IdentifierToken("spinang");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpAdd);
                yield return new ConstantToken(new RealVariant(113.75));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Comma);
                yield return new IdentifierToken("delta");
                yield return new Token(TokenType.OpMul);
                yield return new ConstantToken(new IntVariant(2));
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 5);
                yield return new IdentifierToken("rotation");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("x");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.BuiltInFunc, 31); //lerp
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("rotation");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("x");
                yield return new Token(TokenType.Comma);
                yield return new Token(TokenType.BuiltInFunc, 43); //deg2rad
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new RealVariant(0.9375));
                yield return new Token(TokenType.OpMul);
                yield return new IdentifierToken("spinang");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpAdd);
                yield return new ConstantToken(new RealVariant(46.875));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Comma);
                yield return new IdentifierToken("delta");
                yield return new Token(TokenType.OpMul);
                yield return new ConstantToken(new IntVariant(2));
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 4);
                yield return new Token(TokenType.CfElif);
                yield return new Token(TokenType.OpNot);
                yield return new IdentifierToken("rollOver");
                yield return new Token(TokenType.OpAnd);
                yield return new Token(TokenType.BuiltInFunc, 17); //abs
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("spinang");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpGreaterEqual);
                yield return new ConstantToken(new IntVariant(130));
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 5);
                yield return new IdentifierToken("rotation");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("z");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.BuiltInFunc, 31); //lerp
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("rotation");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("z");
                yield return new Token(TokenType.Comma);
                yield return new Token(TokenType.BuiltInFunc, 43); //deg2rad
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Comma);
                yield return new IdentifierToken("delta");
                yield return new Token(TokenType.OpMul);
                yield return new ConstantToken(new IntVariant(2));
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 5);
                yield return new IdentifierToken("rotation");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("x");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.BuiltInFunc, 31); //lerp
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("rotation");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("x");
                yield return new Token(TokenType.Comma);
                yield return new Token(TokenType.BuiltInFunc, 43); //deg2rad
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new IntVariant(-75));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Comma);
                yield return new IdentifierToken("delta");
                yield return new Token(TokenType.OpMul);
                yield return new ConstantToken(new IntVariant(2));
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 3);
                yield return new IdentifierToken("snap_vec");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.BuiltInType, 7); //vector3
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("ZERO");

                yield return new Token(TokenType.Newline, 3);
                yield return new IdentifierToken("snapped");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(false));

                yield return new Token(TokenType.Newline, 3);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("factor");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new RealVariant(1026.22));
                yield return new Token(TokenType.OpDiv);
                yield return new Token(TokenType.BuiltInFunc, 17); //abs
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("spinang");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpAdd);
                yield return new ConstantToken(new RealVariant(6.12));

                yield return new Token(TokenType.Newline, 3);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.OpNot);
                yield return new IdentifierToken("spinlatch");
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 4);
                yield return new IdentifierToken("request_jump");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(true));

                yield return new Token(TokenType.Newline, 3);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("spinlatch");
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 4);
                yield return new IdentifierToken("latchtimer");
                yield return new Token(TokenType.OpAssignAdd);
                yield return new ConstantToken(new IntVariant(1));

                yield return new Token(TokenType.Newline, 4);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("latchtimer");
                yield return new Token(TokenType.OpGreater);
                yield return new IdentifierToken("factor");
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 5);
                yield return new IdentifierToken("spinlatch");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(false));

                yield return new Token(TokenType.Newline, 5);
                yield return new IdentifierToken("latchtimer");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(0));

                yield return new Token(TokenType.Newline, 3);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.OpNot);
                yield return new IdentifierToken("gravFlop");
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 4);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("gravCount");
                yield return new Token(TokenType.OpLess);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 5);
                yield return new IdentifierToken("gravCount");
                yield return new Token(TokenType.OpAssignAdd);
                yield return new ConstantToken(new RealVariant(0.1));

                yield return new Token(TokenType.Newline, 4);
                yield return new Token(TokenType.CfElse);
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 5);
                yield return new IdentifierToken("gravFlop");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.OpNot);
                yield return new IdentifierToken("gravFlop");

                yield return new Token(TokenType.Newline, 4);
                yield return new IdentifierToken("gravity_vec");
                yield return new Token(TokenType.OpAssignAdd);
                yield return new Token(TokenType.BuiltInType, 7); //Vector3
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Comma);
                yield return new IdentifierToken("gravFlop");
                yield return new Token(TokenType.OpMul);
                yield return new ConstantToken(new IntVariant(12));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpMul);
                yield return new IdentifierToken("delta");

                yield return new Token(TokenType.Newline, 3);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("gravFlop");
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 4);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("gravCount");
                yield return new Token(TokenType.OpGreater);
                yield return new ConstantToken(new IntVariant(-2));
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 5);
                yield return new IdentifierToken("gravCount");
                yield return new Token(TokenType.OpAssignSub);
                yield return new ConstantToken(new RealVariant(0.1));

                yield return new Token(TokenType.Newline, 4);
                yield return new Token(TokenType.CfElse);
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 5);
                yield return new IdentifierToken("gravFlop");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.OpNot);
                yield return new IdentifierToken("gravFlop");

                yield return new Token(TokenType.Newline, 4);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("grav");
                yield return new Token(TokenType.OpAssign);
                yield return new IdentifierToken("GRAVITY");
                yield return new Token(TokenType.OpMul);
                yield return new Token(TokenType.BuiltInType, 7); //Vector3
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("DOWN");
                yield return new Token(TokenType.OpMul);
                yield return new IdentifierToken("delta");

                yield return new Token(TokenType.Newline, 4);
                yield return new IdentifierToken("gravity_vec");
                yield return new Token(TokenType.OpAssignAdd);
                yield return new IdentifierToken("grav");

                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.CfElif);
                yield return new IdentifierToken("is_on_floor");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpAnd);
                yield return new ConstantToken(new BoolVariant(true));
                yield return new Token(TokenType.OpAnd);
                yield return new IdentifierToken("ignore_snap");
                yield return new Token(TokenType.OpLessEqual);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 3);
                yield return new IdentifierToken("y_slow");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new RealVariant(0.2));

                yield return new Token(TokenType.Newline, 3);
                yield return new IdentifierToken("gravity_vec");
                yield return new Token(TokenType.OpAssign);
                yield return new IdentifierToken("get_floor_normal");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpMul);
                yield return new Token(TokenType.OpSub);
                yield return new ConstantToken(new RealVariant(1.0));

                yield return new Token(TokenType.Newline, 3);
                yield return new IdentifierToken("snapped");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(true));

                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.CfElif);
                yield return new IdentifierToken("snapped");
                yield return new Token(TokenType.OpAnd);
                yield return new ConstantToken(new BoolVariant(true));
                yield return new Token(TokenType.OpAnd);
                yield return new IdentifierToken("ignore_snap");
                yield return new Token(TokenType.OpLessEqual);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 3);
                yield return new IdentifierToken("gravity_vec");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.BuiltInType, 7); //Vector3
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("ZERO");

                yield return new Token(TokenType.Newline, 3);
                yield return new IdentifierToken("snapped");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(false));
                yield return new Token(TokenType.OpAnd);
                yield return new ConstantToken(new BoolVariant(false));

                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.CfElse);
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 3);
                yield return new IdentifierToken("snap_vec");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.BuiltInType, 7); //Vector3
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("ZERO");

                yield return new Token(TokenType.Newline, 3);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("grav");
                yield return new Token(TokenType.OpAssign);
                yield return new IdentifierToken("GRAVITY");
                yield return new Token(TokenType.OpMul);
                yield return new Token(TokenType.BuiltInType, 7); //Vector3
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("DOWN");
                yield return new Token(TokenType.OpMul);
                yield return new IdentifierToken("delta");

                yield return new Token(TokenType.Newline, 3);
                yield return new IdentifierToken("gravity_vec");
                yield return new Token(TokenType.OpAssignAdd);
                yield return new IdentifierToken("grav");

            }
            else if (mover3.Check(token))
            {

                yield return token;
                yield return new Token(TokenType.OpAnd);
                yield return new ConstantToken(new BoolVariant(false));

            }
            else if (mover4.Check(token))
            {

                yield return token;
                yield return new Token(TokenType.OpAnd);
                yield return new ConstantToken(new BoolVariant(false));

            }
            else if (mover5.Check(token))
            {

                yield return token;
                yield return new Token(TokenType.OpOr);
                yield return new Token(TokenType.OpNot);
                yield return new IdentifierToken("spinlatch");
                yield return new Token(TokenType.OpOr);
                yield return new IdentifierToken("transit");

            }
            else if (mover6.Check(token))
            {

                yield return token;

                yield return new Token(TokenType.Newline, 3);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("test");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.BuiltInFunc, 17);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("spinang");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpGreaterEqual);
                yield return new ConstantToken(new IntVariant(50));
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 3);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("test");
                yield return new Token(TokenType.OpAnd);
                yield return new Token(TokenType.BuiltInFunc, 17);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("spinang");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpLessEqual);
                yield return new ConstantToken(new IntVariant(130));
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 4);
                yield return new IdentifierToken("gravity_vec");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.BuiltInType, 7);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Comma);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new RealVariant(0.0195));
                yield return new Token(TokenType.OpMul);
                yield return new Token(TokenType.BuiltInFunc, 17);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("spinang");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpSub);
                yield return new ConstantToken(new RealVariant(0.4744));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 3);
                yield return new Token(TokenType.CfElif);
                yield return new Token(TokenType.BuiltInFunc, 17);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("spinang");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpGreater);
                yield return new ConstantToken(new IntVariant(130));
                yield return new Token(TokenType.OpAnd);
                yield return new Token(TokenType.BuiltInFunc, 17);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("spinang");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpLessEqual);
                yield return new ConstantToken(new IntVariant(170));
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 4);
                yield return new IdentifierToken("gravity_vec");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.BuiltInType, 7);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Comma);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new RealVariant(0.02353));
                yield return new Token(TokenType.OpMul);
                yield return new Token(TokenType.BuiltInFunc, 17);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("spinang");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpSub);
                yield return new ConstantToken(new RealVariant(0.9983));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 3);
                yield return new Token(TokenType.CfElif);
                yield return new Token(TokenType.BuiltInFunc, 17);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("spinang");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpGreater);
                yield return new ConstantToken(new IntVariant(170));
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 4);
                yield return new IdentifierToken("gravity_vec");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.BuiltInType, 7);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Comma);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new RealVariant(0.03846));
                yield return new Token(TokenType.OpMul);
                yield return new Token(TokenType.BuiltInFunc, 17);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("spinang");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpSub);
                yield return new ConstantToken(new RealVariant(3.3582));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 3);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("transit");
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("gravity_vec");
                yield return new Token(TokenType.OpAssign);
                yield return new IdentifierToken("gravity_vec");
                yield return new Token(TokenType.OpMul);
                yield return new Token(TokenType.BuiltInType, 7);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new RealVariant(0.09));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 3);
                yield return new IdentifierToken("spinlatch");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(true));

            }
            else if (mover7.Check(token))
            {

                yield return token;
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.OpNot);
                yield return new IdentifierToken("transit");
                yield return new Token(TokenType.Colon);


            }
            else if (mover8.Check(token))
            {

                yield return token;
                yield return new Token(TokenType.CfElse);
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 2);
                yield return new IdentifierToken("move_and_slide");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("gravity_vec");
                yield return new Token(TokenType.Comma);
                yield return new Token(TokenType.BuiltInType, 7);
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("UP");
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.OpNot);
                yield return new IdentifierToken("rollOverLatch");
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 3);
                yield return new IdentifierToken("rotation");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("x");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.BuiltInFunc, 31);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("rotation");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("x");
                yield return new Token(TokenType.Comma);
                yield return new Token(TokenType.BuiltInFunc, 43);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new IntVariant(75));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Comma);
                yield return new IdentifierToken("delta");
                yield return new Token(TokenType.OpMul);
                yield return new ConstantToken(new RealVariant(7.5));
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.CfElse);
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 3);
                yield return new IdentifierToken("rotation");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("x");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.BuiltInFunc, 31);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("rotation");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("x");
                yield return new Token(TokenType.Comma);
                yield return new Token(TokenType.BuiltInFunc, 43);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Comma);
                yield return new IdentifierToken("delta");
                yield return new Token(TokenType.OpMul);
                yield return new ConstantToken(new RealVariant(7.5));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Newline, 1);

            }
            else if (mover9.Check(token))
            {

                yield return token;
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CfIf);
                yield return new ConstantToken(new BoolVariant(false));

            }
            else if (addhotkey.Check(token))
            {

                yield return token;

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("held_item");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("id");
                yield return new Token(TokenType.OpEqual);
                yield return new ConstantToken(new StringVariant("chalk_special"));
                yield return new Token(TokenType.OpAnd);
                yield return new IdentifierToken("Input");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("is_action_pressed");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("alt_primary"));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpAnd);
                yield return new Token(TokenType.OpNot);
                yield return new IdentifierToken("transit");
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 2);
                yield return new IdentifierToken("rollOver");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.OpNot);
                yield return new IdentifierToken("rollOver");

            }
            else
            {
                yield return token;
            }
        }
    }
}
