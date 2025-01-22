using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;

namespace OfficerBallsBuffLib;

public class BufflibMinigame : IScriptMod {
    public bool ShouldRun(string path) => path == "res://Scenes/Minigames/Fishing3/fishing3.gdc";


    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens) {

        var addVar = new MultiTokenWaiter([

            t => t is IdentifierToken {Name: "challenge_stars_active"},
            t => t.Type is TokenType.OpAssign,
            t => t is ConstantToken {Value: BoolVariant {Value: false}},
        ]);

        var matchPlayer = new MultiTokenWaiter([

            t => t is IdentifierToken {Name: "_ready"},
            t => t.Type is TokenType.ParenthesisOpen,
            t => t.Type is TokenType.ParenthesisClose,
            t => t.Type is TokenType.Colon,

        ]);

        var buff_cantlosefish = new MultiTokenWaiter([

            t => t.Type is TokenType.ParenthesisClose,
            t => t.Type is TokenType.Newline,
            t => t.Type is TokenType.Newline,
            t => t.Type is TokenType.Newline,
            t => t.Type is TokenType.CfIf,
            t => t is IdentifierToken {Name: "active"},
            t => t.Type is TokenType.Colon,
            t => t.Type is TokenType.Newline,

        ]);

        var buff_haste = new MultiTokenWaiter([

            t => t is ConstantToken {Value: StringVariant{Value:"quick"}},

        ]);

        var buff_timestretch = new MultiTokenWaiter([

            t => t is ConstantToken {Value: RealVariant {Value: 0.001}},
            t => t.Type is TokenType.ParenthesisClose,
            t => t.Type is TokenType.Comma,
            t => t is ConstantToken {Value: RealVariant {Value: 0.1}},
            t => t.Type is TokenType.ParenthesisClose,

        ]);

        var buff_clickreduce = new MultiTokenWaiter([

            t => t.Type is TokenType.OpMul,
            t => t is ConstantToken {Value: IntVariant {Value: 2}},
            t => t.Type is TokenType.ParenthesisClose,

        ]);

        var buff_clickmultiply = new MultiTokenWaiter([

            t => t.Type is TokenType.OpAssignSub,
            t => t is IdentifierToken {Name: "params"},
            t => t.Type is TokenType.BracketOpen,
            t => t is ConstantToken {Value: StringVariant {Value: "damage"}},
            t => t.Type is TokenType.BracketClose,

        ]);

        var buff_gatereduce = new MultiTokenWaiter([

            t => t.Type is TokenType.Comma,
            t => t is ConstantToken {Value: IntVariant {Value: 6}},
            t => t.Type is TokenType.ParenthesisClose,

        ]);

        var boon_slowbite = new MultiTokenWaiter([

            t => t is ConstantToken {Value: RealVariant {Value: 2.0}},
            t => t.Type is TokenType.Comma,
            t => t is ConstantToken {Value: RealVariant {Value: 3.0}},
            t => t.Type is TokenType.ParenthesisClose,

        ]);

        foreach (var token in tokens) {
            if (addVar.Check(token)) {

                yield return token;

                yield return new Token(TokenType.Newline);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("plactor");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new NilVariant());

            } else if (matchPlayer.Check(token)) {

                yield return token;

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfFor);
                yield return new IdentifierToken("flactor");
                yield return new Token(TokenType.OpIn);
                yield return new IdentifierToken("get_tree");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("get_nodes_in_group");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("controlled_player"));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("plactor");
                yield return new Token(TokenType.OpAssign);
                yield return new IdentifierToken("flactor");

            } else if (buff_cantlosefish.Check(token)) {

                yield return token;
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("plactor");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("ob_buffs");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("buff_cantlosefish"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpLessEqual);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Colon);

            } else if (buff_haste.Check(token)) {

                yield return token;
                yield return new Token(TokenType.OpOr);
                yield return new IdentifierToken("plactor");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("ob_buffs");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("buff_haste"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpGreater);
                yield return new ConstantToken(new IntVariant(0));

            } else if (buff_timestretch.Check(token)) {

                yield return token;

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("plactor");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("ob_buffs");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("buff_timestretch"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpGreater);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("bad_speed");
                yield return new Token(TokenType.OpAssignMul);
                yield return new ConstantToken(new RealVariant(0.5));

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfElif);
                yield return new IdentifierToken("plactor");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("ob_boons");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("boon_redcreep"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpGreater);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("bad_speed");
                yield return new Token(TokenType.OpAssignMul);
                yield return new ConstantToken(new IntVariant(2));

            } else if (buff_clickreduce.Check(token)) {

                yield return token;

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("plactor");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("ob_buffs");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("buff_clickreduce"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpGreater);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("total_hp");
                yield return new Token(TokenType.OpAssignMul);
                yield return new ConstantToken(new RealVariant(0.5));

            } else if (buff_clickmultiply.Check(token)) {

                yield return token;

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("plactor");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("ob_buffs");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("buff_clickmultiply"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpGreater);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("ys");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("health");
                yield return new Token(TokenType.OpAssignSub);
                yield return new IdentifierToken("params");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("damage"));
                yield return new Token(TokenType.BracketClose);

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfElif);
                yield return new IdentifierToken("plactor");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("ob_boons");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("boon_weakening"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpGreater);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("ys");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("health");
                yield return new Token(TokenType.OpAssignAdd);
                yield return new Token(TokenType.BuiltInFunc, 14);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("params");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("damage"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpDiv);
                yield return new ConstantToken(new IntVariant(2));
                yield return new Token(TokenType.ParenthesisClose);

            } else if (buff_gatereduce.Check(token)) {

                yield return token;

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("plactor");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("ob_buffs");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("buff_gatereduce"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpGreater);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("yank_spots");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.BuiltInFunc, 53); //clamp
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("yank_spots");
                yield return new Token(TokenType.OpDiv);
                yield return new ConstantToken(new IntVariant(2));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new IntVariant(3));
                yield return new Token(TokenType.ParenthesisClose);

            } else if (boon_slowbite.Check(token)) {

                yield return token;

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("plactor");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("ob_boons");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("boon_slowbite"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpGreater);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("fish_timer");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("wait_time");
                yield return new Token(TokenType.OpAssignAdd);
                yield return new Token(TokenType.BuiltInFunc, 40);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new RealVariant(2));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new RealVariant(3));
                yield return new Token(TokenType.ParenthesisClose);

            } else {
                yield return token;
            }
        }
    }
}
