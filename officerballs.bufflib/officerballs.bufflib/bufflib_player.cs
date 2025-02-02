using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;

namespace OfficerBallsBuffLib;

public class BufflibPlayer : IScriptMod {
    public bool ShouldRun(string path) => path == "res://Scenes/Entities/Player/player.gdc";

    // returns a list of tokens for the new script, with the input being the original script's tokens
    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens) {
       
        var addvariables = new MultiTokenWaiter([
            t => t is ConstantToken {Value: StringVariant{Value:"npc title here"}},
        ]);

        var addfunctions = new MultiTokenWaiter([

            t => t is ConstantToken {Value: StringVariant{Value:"mushroom"}},
            t => t.Type is TokenType.BracketClose,
            t => t.Type is TokenType.OpAssign,
            t => t is ConstantToken {Value: BoolVariant{Value:true}},

        ]);

        var processBuffTimers = new MultiTokenWaiter([
            t => t is IdentifierToken {Name:"_process_timers"},
            t => t.Type is TokenType.ParenthesisOpen,
            t => t.Type is TokenType.ParenthesisClose,
            t => t.Type is TokenType.Colon,
        ]);

        var rollsizer = new MultiTokenWaiter([

            t => t.Type is TokenType.CfFor,
            t => t is IdentifierToken {Name:"i"},
            t => t.Type is TokenType.OpIn,
            t => t is ConstantToken {Value: IntVariant {Value: 3}},
            t => t.Type is TokenType.Colon,
            t => t.Type is TokenType.Newline,

        ]);

        var roller = new MultiTokenWaiter([

            t => t.Type is TokenType.OpAssign,
            t => t is ConstantToken {Value: StringVariant{Value:"none"}},

        ]);

        var buff_small = new MultiTokenWaiter([

            t => t.Type is TokenType.OpEqual,
            t => t is ConstantToken {Value: StringVariant{Value:"small"}},

        ]);

        var buff_large = new MultiTokenWaiter([

            t => t.Type is TokenType.OpEqual,
            t => t is ConstantToken {Value: StringVariant{Value:"large"}},

        ]);

        var buff_rarity = new MultiTokenWaiter([

            t => t is ConstantToken {Value: StringVariant{Value:"large"}},
            t => t.Type is TokenType.Newline,
            t => t.Type is TokenType.CfIf,
            t => t is IdentifierToken {Name:"rod_cast_data"},
            t => t.Type is TokenType.OpEqual,
            t => t is ConstantToken {Value: StringVariant{Value:"gold"}},

        ]);

        var buff_quality = new MultiTokenWaiter([

            t => t.Type is TokenType.OpEqual,
            t => t is ConstantToken {Value: StringVariant{Value:"sparkling"}},

        ]);

        var buff_double1 = new MultiTokenWaiter([

            t => t.Type is TokenType.PrVar,
            t => t is IdentifierToken {Name:"bonus_text"},
            t => t.Type is TokenType.OpAssign,
            t => t.Type is TokenType.BracketOpen,
            t => t.Type is TokenType.BracketClose,
            t => t.Type is TokenType.Newline,
            t => t.Type is TokenType.Newline,
            t => t.Type is TokenType.CfIf,

        ]);

        var buff_double2 = new MultiTokenWaiter([

            t => t is ConstantToken {Value: StringVariant{Value:"double"}},
            t => t.Type is TokenType.OpAnd,
            t => t.Type is TokenType.BuiltInFunc,
            t => t.Type is TokenType.ParenthesisOpen,
            t => t.Type is TokenType.ParenthesisClose,
            t => t.Type is TokenType.OpLess,
            t => t is ConstantToken {Value: RealVariant {Value: 0.15}},

        ]);

        var buff_zones = new MultiTokenWaiter([

            t => t is IdentifierToken {Name:"zone"},
            t => t.Type is TokenType.Period,
            t => t is IdentifierToken {Name:"type_lock"},

        ]);

        var buff_rain = new MultiTokenWaiter([

            t => t.Type is TokenType.OpAssign,
            t => t is ConstantToken {Value: StringVariant{Value:"rain"}},

        ]);

        var buff_efficiency = new MultiTokenWaiter([

            t => t is ConstantToken {Value: StringVariant{Value:"shop_enter"}},
            t => t.Type is TokenType.ParenthesisClose,
            t => t.Type is TokenType.Newline,
            t => t.Type is TokenType.Newline,
            t => t.Type is TokenType.CfIf,

        ]);

        var buff_gamblefisher = new MultiTokenWaiter([

            t => t is IdentifierToken {Name:"tags"},
            t => t.Type is TokenType.OpAssign,
            t => t.Type is TokenType.BracketOpen,
            t => t.Type is TokenType.BracketClose,

        ]);

        var buff_lucky = new MultiTokenWaiter([

            t => t is IdentifierToken {Name:"failed_casts"},
            t => t.Type is TokenType.OpAssignAdd,
            t => t is ConstantToken {Value: RealVariant {Value: 0.05}},

        ]);

        var buff_fastbite = new MultiTokenWaiter([

            t => t is ConstantToken {Value: RealVariant {Value: 2}},
            t => t.Type is TokenType.Comma,
            t => t is ConstantToken {Value: RealVariant {Value: 3}},
            t => t.Type is TokenType.ParenthesisClose,

        ]);

        var boon_trash = new MultiTokenWaiter([

            t => t is IdentifierToken {Name:"treasure_mult"},
            t => t.Type is TokenType.OpAssign,
            t => t is ConstantToken {Value: RealVariant {Value: 2.0}},

        ]);

        var boon_slowness = new MultiTokenWaiter([

            t => t.Type is TokenType.OpAssign,
            t => t is IdentifierToken {Name:"slow_walk_speed"}

        ]);

        // loop through all tokens in the script
        foreach (var token in tokens) {
            if (addvariables.Check(token)) {
                // found our match, return the original newline
                yield return token;

                yield return new Token(TokenType.Newline);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("ob_buffs");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.CurlyBracketOpen);
                yield return new ConstantToken(new StringVariant("buff_lucky"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("buff_fastbite"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("buff_small"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("buff_large"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("buff_rarity"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("buff_quality"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("buff_double"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("buff_haste"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("buff_timestretch"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("buff_clickreduce"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("buff_clickmultiply"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("buff_gatereduce"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("buff_cantlosefish"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("buff_salty"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("buff_fresh"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("buff_alien"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("buff_void"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("buff_rain"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("buff_gamblefisher"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("buff_valuelift"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("buff_protection"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("buff_speedbuddies"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("buff_doublebuddies"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("buff_efficiency"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.CurlyBracketClose);

                yield return new Token(TokenType.Newline);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("ob_buffs_tier");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.CurlyBracketOpen);
                yield return new ConstantToken(new StringVariant("buff_lucky"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("buff_fastbite"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("buff_small"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("buff_large"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("buff_rarity"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("buff_quality"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("buff_double"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("buff_haste"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("buff_timestretch"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("buff_clickreduce"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("buff_clickmultiply"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("buff_gatereduce"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("buff_cantlosefish"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("buff_salty"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("buff_fresh"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("buff_alien"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("buff_void"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("buff_rain"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("buff_gamblefisher"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("buff_valuelift"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("buff_protection"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("buff_speedbuddies"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("buff_doublebuddies"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("buff_efficiency"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.CurlyBracketClose);

                yield return new Token(TokenType.Newline);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("ob_boons");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.CurlyBracketOpen);
                yield return new ConstantToken(new StringVariant("boon_invert"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("boon_redcreep"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("boon_weakening"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("boon_trash"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("boon_dizzy"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("boon_slowness"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("boon_slowbite"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("boon_confusion"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("boon_hityourself"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Comma);
                yield return new Token(TokenType.CurlyBracketClose);

                yield return new Token(TokenType.Newline);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("ob_boons_tier");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.CurlyBracketOpen);
                yield return new ConstantToken(new StringVariant("boon_invert"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("boon_redcreep"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("boon_weakening"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("boon_trash"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("boon_dizzy"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("boon_slowness"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("boon_slowbite"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("boon_confusion"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("boon_hityourself"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.Comma);
                yield return new Token(TokenType.CurlyBracketClose);

                yield return new Token(TokenType.Newline);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("ob_timer");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(0));

            } else if (addfunctions.Check(token)) {

                yield return token;

                yield return new Token(TokenType.Newline);
                yield return new Token(TokenType.PrFunction);
                yield return new IdentifierToken("_add_buff");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("inbuff");
                yield return new Token(TokenType.Comma);
                yield return new IdentifierToken("intime");
                yield return new Token(TokenType.Comma);
                yield return new IdentifierToken("intier");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 1);
                yield return new IdentifierToken("ob_buffs");
                yield return new Token(TokenType.BracketOpen);
                yield return new IdentifierToken("inbuff");
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpAssignAdd);
                yield return new IdentifierToken("intime");

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("intier");
                yield return new Token(TokenType.OpGreater);
                yield return new IdentifierToken("ob_buffs_tier");
                yield return new Token(TokenType.BracketOpen);
                yield return new IdentifierToken("inbuff");
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("ob_buffs_tier");
                yield return new Token(TokenType.BracketOpen);
                yield return new IdentifierToken("inbuff");
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpAssign);
                yield return new IdentifierToken("intier");

                yield return new Token(TokenType.Newline);
                yield return new Token(TokenType.PrFunction);
                yield return new IdentifierToken("_wipe_buff");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("inbuff");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 1);
                yield return new IdentifierToken("ob_buffs");
                yield return new Token(TokenType.BracketOpen);
                yield return new IdentifierToken("inbuff");
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(0));

                yield return new Token(TokenType.Newline);
                yield return new Token(TokenType.PrFunction);
                yield return new IdentifierToken("_wipe_all_buffs");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfFor);
                yield return new IdentifierToken("buff");
                yield return new Token(TokenType.OpIn);
                yield return new IdentifierToken("ob_buffs");
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 2);
                yield return new IdentifierToken("ob_buffs");
                yield return new Token(TokenType.BracketOpen);
                yield return new IdentifierToken("buff");
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(0));

                yield return new Token(TokenType.Newline);
                yield return new Token(TokenType.PrFunction);
                yield return new IdentifierToken("_add_boon");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("inboon");
                yield return new Token(TokenType.Comma);
                yield return new IdentifierToken("intime");
                yield return new Token(TokenType.Comma);
                yield return new IdentifierToken("intier");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 1);
                yield return new IdentifierToken("ob_boons");
                yield return new Token(TokenType.BracketOpen);
                yield return new IdentifierToken("inboon");
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpAssignAdd);
                yield return new IdentifierToken("intime");

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("intier");
                yield return new Token(TokenType.OpGreater);
                yield return new IdentifierToken("ob_boons_tier");
                yield return new Token(TokenType.BracketOpen);
                yield return new IdentifierToken("inboon");
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("ob_boons_tier");
                yield return new Token(TokenType.BracketOpen);
                yield return new IdentifierToken("inboon");
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpAssign);
                yield return new IdentifierToken("intier");

                yield return new Token(TokenType.Newline);
                yield return new Token(TokenType.PrFunction);
                yield return new IdentifierToken("_wipe_boon");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("inboon");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 1);
                yield return new IdentifierToken("ob_boons");
                yield return new Token(TokenType.BracketOpen);
                yield return new IdentifierToken("inboon");
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(0));

                yield return new Token(TokenType.Newline);
                yield return new Token(TokenType.PrFunction);
                yield return new IdentifierToken("_wipe_all_boons");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfFor);
                yield return new IdentifierToken("boon");
                yield return new Token(TokenType.OpIn);
                yield return new IdentifierToken("ob_boons");
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 2);
                yield return new IdentifierToken("ob_boons");
                yield return new Token(TokenType.BracketOpen);
                yield return new IdentifierToken("boon");
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(0));

                yield return new Token(TokenType.Newline);
                yield return new Token(TokenType.PrFunction);
                yield return new IdentifierToken("_random_buff_or_boon");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("bob_tier");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(1));

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("bob_tierrng");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.BuiltInFunc, 39);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("bob_tierrng");
                yield return new Token(TokenType.OpLessEqual);
                yield return new ConstantToken(new RealVariant(0.4));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("bob_tier");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(1));

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfElif);
                yield return new IdentifierToken("bob_tierrng");
                yield return new Token(TokenType.OpLessEqual);
                yield return new ConstantToken(new RealVariant(0.6));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("bob_tier");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(2));

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfElif);
                yield return new IdentifierToken("bob_tierrng");
                yield return new Token(TokenType.OpLessEqual);
                yield return new ConstantToken(new RealVariant(0.8));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("bob_tier");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(3));

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfElif);
                yield return new IdentifierToken("bob_tierrng");
                yield return new Token(TokenType.OpLessEqual);
                yield return new ConstantToken(new RealVariant(0.95));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("bob_tier");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(4));

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfElse);
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("bob_tier");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(5));

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("bob_timer");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(0));

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("bob_timerng");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.BuiltInFunc, 39);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("bob_timerng");
                yield return new Token(TokenType.OpLessEqual);
                yield return new ConstantToken(new RealVariant(0.3));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("bob_timer");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(5));

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfElif);
                yield return new IdentifierToken("bob_timerng");
                yield return new Token(TokenType.OpLessEqual);
                yield return new ConstantToken(new RealVariant(0.7));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("bob_timer");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(10));

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfElif);
                yield return new IdentifierToken("bob_timerng");
                yield return new Token(TokenType.OpLessEqual);
                yield return new ConstantToken(new RealVariant(0.9));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("bob_timer");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(15));

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfElse);
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("bob_timer");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(30));

                yield return new Token(TokenType.Newline, 1);
                yield return new IdentifierToken("bob_timer");
                yield return new Token(TokenType.OpAssignMul);
                yield return new ConstantToken(new IntVariant(60));

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.BuiltInFunc, 39);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpGreater);
                yield return new ConstantToken(new RealVariant(0.07));
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("buff_rng");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant(""));

                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("buff_number");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.BuiltInFunc, 38); // randi
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpMod);
                yield return new ConstantToken(new IntVariant(24));
                yield return new Token(TokenType.OpAdd);
                yield return new ConstantToken(new IntVariant(1));

                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.CfMatch);
                yield return new IdentifierToken("buff_number");
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("buff_rng");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant("buff_salty"));
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(2));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("buff_rng");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant("buff_fresh"));
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(3));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("buff_rng");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant("buff_rain"));
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(4));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("buff_rng");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant("buff_void"));
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(5));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("buff_rng");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant("buff_alien"));
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(6));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("buff_rng");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant("buff_small"));
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(7));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("buff_rng");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant("buff_large"));
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(8));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("buff_rng");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant("buff_quality"));
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(9));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("buff_rng");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant("buff_rarity"));
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(10));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("buff_rng");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant("buff_timestretch"));
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(11));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("buff_rng");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant("buff_cantlosefish"));
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(12));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("buff_rng");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant("buff_lucky"));
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(13));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("buff_rng");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant("buff_fastbite"));
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(14));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("buff_rng");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant("buff_double"));
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(15));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("buff_rng");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant("buff_haste"));
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(16));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("buff_rng");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant("buff_clickreduce"));
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(17));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("buff_rng");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant("buff_clickmultiply"));
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(18));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("buff_rng");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant("buff_gatereduce"));
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(19));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("buff_rng");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant("buff_valuelift"));
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(20));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("buff_rng");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant("buff_efficiency"));
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(21));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("buff_rng");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant("buff_speedbuddies"));
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(22));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("buff_rng");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant("buff_doublebuddies"));
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(23));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("buff_rng");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant("buff_protection"));
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(24));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("buff_rng");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant("buff_gamblefisher"));

                yield return new Token(TokenType.Newline, 2);
                yield return new IdentifierToken("_add_buff");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("buff_rng");
                yield return new Token(TokenType.Comma);
                yield return new IdentifierToken("bob_timer");
                yield return new Token(TokenType.Comma);
                yield return new IdentifierToken("bob_tier");
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfElse);
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("boon_rng");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant(""));

                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("boon_number");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.BuiltInFunc, 38); // randi
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpMod);
                yield return new ConstantToken(new IntVariant(5));
                yield return new Token(TokenType.OpAdd);
                yield return new ConstantToken(new IntVariant(1));

                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.CfMatch);
                yield return new IdentifierToken("boon_number");
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("boon_rng");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant("boon_redcreep"));
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(2));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("boon_rng");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant("boon_weakening"));
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(3));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("boon_rng");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant("boon_slowbite"));
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(4));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("boon_rng");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant("boon_trash"));
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(5));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("boon_rng");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant("boon_slowness"));

                yield return new Token(TokenType.Newline, 2);
                yield return new IdentifierToken("_add_boon");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("boon_rng");
                yield return new Token(TokenType.Comma);
                yield return new IdentifierToken("bob_timer");
                yield return new Token(TokenType.Comma);
                yield return new IdentifierToken("bob_tier");
                yield return new Token(TokenType.ParenthesisClose);


            } else if (processBuffTimers.Check(token)) {
            
                yield return token;

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("Time");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("get_unix_time_from_system");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpGreater);
                yield return new IdentifierToken("ob_timer");
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 2);
                yield return new IdentifierToken("ob_timer");
                yield return new Token(TokenType.OpAssign);
                yield return new IdentifierToken("Time");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("get_unix_time_from_system");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpAdd);
                yield return new ConstantToken(new IntVariant(1));

                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.CfFor);
                yield return new IdentifierToken("buff");
                yield return new Token(TokenType.OpIn);
                yield return new IdentifierToken("ob_buffs");
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 3);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("ob_buffs");
                yield return new Token(TokenType.BracketOpen);
                yield return new IdentifierToken("buff");
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpGreater);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 4);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("buff");
                yield return new Token(TokenType.OpEqual);
                yield return new ConstantToken(new StringVariant("buff_alien"));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 5);
                yield return new IdentifierToken("ob_buffs");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("buff_void"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Newline, 5);
                yield return new IdentifierToken("ob_buffs");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("buff_rain"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Newline, 5);
                yield return new IdentifierToken("ob_buffs");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("buff_fresh"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Newline, 5);
                yield return new IdentifierToken("ob_buffs");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("buff_salty"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(0));

                yield return new Token(TokenType.Newline, 4);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("buff");
                yield return new Token(TokenType.OpEqual);
                yield return new ConstantToken(new StringVariant("buff_void"));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 5);
                yield return new IdentifierToken("ob_buffs");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("buff_rain"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Newline, 5);
                yield return new IdentifierToken("ob_buffs");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("buff_fresh"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Newline, 5);
                yield return new IdentifierToken("ob_buffs");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("buff_salty"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(0));

                yield return new Token(TokenType.Newline, 4);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("buff");
                yield return new Token(TokenType.OpEqual);
                yield return new ConstantToken(new StringVariant("buff_rain"));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 5);
                yield return new IdentifierToken("ob_buffs");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("buff_fresh"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Newline, 5);
                yield return new IdentifierToken("ob_buffs");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("buff_salty"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(0));

                yield return new Token(TokenType.Newline, 4);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("buff");
                yield return new Token(TokenType.OpEqual);
                yield return new ConstantToken(new StringVariant("buff_fresh"));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("ob_buffs");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("buff_salty"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(0));

                yield return new Token(TokenType.Newline, 4);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("buff");
                yield return new Token(TokenType.OpEqual);
                yield return new ConstantToken(new StringVariant("buff_cantlosefish"));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 5);
                yield return new IdentifierToken("ob_buffs");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("buff_timestretch"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Newline, 5);
                yield return new IdentifierToken("ob_boons");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("boon_redcreep"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(0));

                yield return new Token(TokenType.Newline, 4);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("buff");
                yield return new Token(TokenType.OpEqual);
                yield return new ConstantToken(new StringVariant("buff_timestretch"));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("ob_boons");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("boon_redcreep"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(0));

                yield return new Token(TokenType.Newline, 4);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("buff");
                yield return new Token(TokenType.OpEqual);
                yield return new ConstantToken(new StringVariant("buff_clickreduce"));
                yield return new Token(TokenType.OpOr);
                yield return new IdentifierToken("buff");
                yield return new Token(TokenType.OpEqual);
                yield return new ConstantToken(new StringVariant("buff_clickmultiply"));
                yield return new Token(TokenType.OpOr);
                yield return new IdentifierToken("buff");
                yield return new Token(TokenType.OpEqual);
                yield return new ConstantToken(new StringVariant("buff_gatereduce"));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("ob_boons");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("boon_weakening"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(0));

                yield return new Token(TokenType.Newline, 4);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("buff");
                yield return new Token(TokenType.OpEqual);
                yield return new ConstantToken(new StringVariant("buff_rarity"));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 5);
                yield return new IdentifierToken("ob_buffs");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("buff_quality"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Newline, 5);
                yield return new IdentifierToken("ob_buffs");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("buff_large"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Newline, 5);
                yield return new IdentifierToken("ob_buffs");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("buff_small"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(0));

                yield return new Token(TokenType.Newline, 4);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("buff");
                yield return new Token(TokenType.OpEqual);
                yield return new ConstantToken(new StringVariant("buff_quality"));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 5);
                yield return new IdentifierToken("ob_buffs");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("buff_large"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Newline, 5);
                yield return new IdentifierToken("ob_buffs");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("buff_small"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(0));

                yield return new Token(TokenType.Newline, 4);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("buff");
                yield return new Token(TokenType.OpEqual);
                yield return new ConstantToken(new StringVariant("buff_large"));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 5);
                yield return new IdentifierToken("ob_buffs");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("buff_small"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(0));

                yield return new Token(TokenType.Newline, 4);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("buff");
                yield return new Token(TokenType.OpEqual);
                yield return new ConstantToken(new StringVariant("buff_fastbite"));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("ob_boons");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("boon_slowbite"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(0));

                yield return new Token(TokenType.Newline, 4);
                yield return new IdentifierToken("ob_buffs");
                yield return new Token(TokenType.BracketOpen);
                yield return new IdentifierToken("buff");
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.BuiltInFunc, 53); //clamp
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("ob_buffs");
                yield return new Token(TokenType.BracketOpen);
                yield return new IdentifierToken("buff");
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpSub);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new IntVariant(3600));
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 4);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("ob_buffs");
                yield return new Token(TokenType.BracketOpen);
                yield return new IdentifierToken("buff");
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpLessEqual);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.OpAnd);
                yield return new IdentifierToken("ob_buffs_tier");
                yield return new Token(TokenType.BracketOpen);
                yield return new IdentifierToken("buff");
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpGreater);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("ob_buffs_tier");
                yield return new Token(TokenType.BracketOpen);
                yield return new IdentifierToken("buff");
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(1));

                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.CfFor);
                yield return new IdentifierToken("boon");
                yield return new Token(TokenType.OpIn);
                yield return new IdentifierToken("ob_boons");
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 3);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("ob_boons");
                yield return new Token(TokenType.BracketOpen);
                yield return new IdentifierToken("boon");
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpGreater);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 4);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("ob_buffs");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("buff_protection"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpGreater);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("ob_boons");
                yield return new Token(TokenType.BracketOpen);
                yield return new IdentifierToken("boon");
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(0));

                yield return new Token(TokenType.Newline, 4);
                yield return new Token(TokenType.CfElse);
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("ob_boons");
                yield return new Token(TokenType.BracketOpen);
                yield return new IdentifierToken("boon");
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.BuiltInFunc, 53); //clamp
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("ob_boons");
                yield return new Token(TokenType.BracketOpen);
                yield return new IdentifierToken("boon");
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpSub);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new IntVariant(3600));
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 4);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("ob_boons");
                yield return new Token(TokenType.BracketOpen);
                yield return new IdentifierToken("boon");
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpLessEqual);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.OpAnd);
                yield return new IdentifierToken("ob_boons_tier");
                yield return new Token(TokenType.BracketOpen);
                yield return new IdentifierToken("boon");
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpGreater);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("ob_boons_tier");
                yield return new Token(TokenType.BracketOpen);
                yield return new IdentifierToken("boon");
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(1));

            } else if (buff_small.Check(token)){

                yield return token;
                yield return new Token(TokenType.OpOr);
                yield return new IdentifierToken("check_small");

            } else if (buff_large.Check(token)) {

                yield return token;
                yield return new Token(TokenType.OpOr);
                yield return new IdentifierToken("check_large");

            } else if (buff_rarity.Check(token)) {

                yield return token;
                yield return new Token(TokenType.OpOr);
                yield return new IdentifierToken("check_gold");

            } else if (buff_quality.Check(token)) {

                yield return token;
                yield return new Token(TokenType.OpOr);
                yield return new IdentifierToken("check_sparkling");

            } else if (buff_double1.Check(token)) {
            
                yield return token;
                yield return new ConstantToken(new BoolVariant(false));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CfPass);

                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("dubbable");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(false));

                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("ob_buffs");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("buff_double"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpGreater);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 3);
                yield return new Token(TokenType.CfMatch);
                yield return new IdentifierToken("ob_buffs_tier");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("buff_double"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 4);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.BuiltInFunc, 39);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpLess);
                yield return new ConstantToken(new RealVariant(0.05));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("dubbable");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(true));
                yield return new Token(TokenType.Newline, 4);
                yield return new ConstantToken(new IntVariant(2));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.BuiltInFunc, 39);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpLess);
                yield return new ConstantToken(new RealVariant(0.1));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("dubbable");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(true));
                yield return new Token(TokenType.Newline, 4);
                yield return new ConstantToken(new IntVariant(3));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.BuiltInFunc, 39);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpLess);
                yield return new ConstantToken(new RealVariant(0.15));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("dubbable");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(true));
                yield return new Token(TokenType.Newline, 4);
                yield return new ConstantToken(new IntVariant(4));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.BuiltInFunc, 39);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpLess);
                yield return new ConstantToken(new RealVariant(0.2));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("dubbable");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(true));
                yield return new Token(TokenType.Newline, 4);
                yield return new ConstantToken(new IntVariant(5));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.BuiltInFunc, 39);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpLess);
                yield return new ConstantToken(new RealVariant(0.25));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("dubbable");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(true));

                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.ParenthesisOpen);

            } else if (buff_double2.Check(token)) {

                yield return token;
                yield return new Token(TokenType.OpOr);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("dubbable");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.ParenthesisClose);

            } else if (buff_zones.Check(token)) {
                
                yield return token;

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("ob_buffs");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("buff_salty"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpGreater);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.CfMatch);
                yield return new IdentifierToken("ob_buffs_tier");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("buff_salty"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.BuiltInFunc, 39);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpLess);
                yield return new ConstantToken(new RealVariant(0.2));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("fish_type");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant("ocean"));
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(2));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.BuiltInFunc, 39);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpLess);
                yield return new ConstantToken(new RealVariant(0.4));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("fish_type");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant("ocean"));
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(3));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.BuiltInFunc, 39);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpLess);
                yield return new ConstantToken(new RealVariant(0.6));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("fish_type");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant("ocean"));
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(4));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.BuiltInFunc, 39);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpLess);
                yield return new ConstantToken(new RealVariant(0.8));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("fish_type");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant("ocean"));
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(5));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("fish_type");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant("ocean"));

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("ob_buffs");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("buff_fresh"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpGreater);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.CfMatch);
                yield return new IdentifierToken("ob_buffs_tier");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("buff_fresh"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.BuiltInFunc, 39);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpLess);
                yield return new ConstantToken(new RealVariant(0.2));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("fish_type");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant("lake"));
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(2));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.BuiltInFunc, 39);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpLess);
                yield return new ConstantToken(new RealVariant(0.4));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("fish_type");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant("lake"));
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(3));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.BuiltInFunc, 39);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpLess);
                yield return new ConstantToken(new RealVariant(0.6));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("fish_type");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant("lake"));
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(4));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.BuiltInFunc, 39);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpLess);
                yield return new ConstantToken(new RealVariant(0.8));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("fish_type");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant("lake"));
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(5));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("fish_type");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant("lake"));

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("ob_buffs");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("buff_alien"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpGreater);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.CfMatch);
                yield return new IdentifierToken("ob_buffs_tier");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("buff_alien"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.BuiltInFunc, 39);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpLess);
                yield return new ConstantToken(new RealVariant(0.05));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("fish_type");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant("alien"));
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(2));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.BuiltInFunc, 39);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpLess);
                yield return new ConstantToken(new RealVariant(0.1));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("fish_type");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant("alien"));
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(3));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.BuiltInFunc, 39);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpLess);
                yield return new ConstantToken(new RealVariant(0.15));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("fish_type");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant("alien"));
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(4));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.BuiltInFunc, 39);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpLess);
                yield return new ConstantToken(new RealVariant(0.2));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("fish_type");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant("alien"));
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(5));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.BuiltInFunc, 39);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpLess);
                yield return new ConstantToken(new RealVariant(0.25));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("fish_type");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant("alien"));

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("ob_buffs");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("buff_void"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpGreater);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.CfMatch);
                yield return new IdentifierToken("ob_buffs_tier");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("buff_void"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.BuiltInFunc, 39);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpLess);
                yield return new ConstantToken(new RealVariant(0.05));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("fish_type");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant("void"));
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(2));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.BuiltInFunc, 39);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpLess);
                yield return new ConstantToken(new RealVariant(0.1));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("fish_type");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant("void"));
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(3));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.BuiltInFunc, 39);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpLess);
                yield return new ConstantToken(new RealVariant(0.15));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("fish_type");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant("void"));
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(4));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.BuiltInFunc, 39);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpLess);
                yield return new ConstantToken(new RealVariant(0.2));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("fish_type");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant("void"));
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(5));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.BuiltInFunc, 39);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpLess);
                yield return new ConstantToken(new RealVariant(0.25));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("fish_type");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant("void"));

            } else if (buff_rain.Check(token)) {

                yield return token;

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfElif);
                yield return new IdentifierToken("ob_buffs");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("buff_rain"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpGreater);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.CfMatch);
                yield return new IdentifierToken("ob_buffs_tier");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("buff_rain"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.BuiltInFunc, 39);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpLess);
                yield return new ConstantToken(new RealVariant(0.066));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("fish_type");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant("rain"));
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(2));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.BuiltInFunc, 39);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpLess);
                yield return new ConstantToken(new RealVariant(0.1));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("fish_type");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant("rain"));
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(3));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.BuiltInFunc, 39);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpLess);
                yield return new ConstantToken(new RealVariant(0.15));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("fish_type");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant("rain"));
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(4));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.BuiltInFunc, 39);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpLess);
                yield return new ConstantToken(new RealVariant(0.2));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("fish_type");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant("rain"));
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(5));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.BuiltInFunc, 39);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpLess);
                yield return new ConstantToken(new RealVariant(0.33));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("fish_type");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant("rain"));


            } else if (buff_efficiency.Check(token)) {

                yield return token;
                yield return new ConstantToken(new BoolVariant(false));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CfPass);

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("baiconsume");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(true));

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("ob_buffs");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("buff_efficiency"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpGreater);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.CfMatch);
                yield return new IdentifierToken("ob_buffs_tier");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("buff_efficiency"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.BuiltInFunc, 39);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpLess);
                yield return new ConstantToken(new RealVariant(0.9));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("baiconsume");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(false));
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(2));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.BuiltInFunc, 39);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpLess);
                yield return new ConstantToken(new RealVariant(0.75));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("baiconsume");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(false));
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(3));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.BuiltInFunc, 39);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpLess);
                yield return new ConstantToken(new RealVariant(0.6));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("baiconsume");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(false));
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(4));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.BuiltInFunc, 39);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpLess);
                yield return new ConstantToken(new RealVariant(0.5));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("baiconsume");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(false));
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(5));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.BuiltInFunc, 39);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpLess);
                yield return new ConstantToken(new RealVariant(0.33));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("baiconsume");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(false));

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("baiconsume");
                yield return new Token(TokenType.OpAnd);

            } else if (buff_gamblefisher.Check(token)){
                
                yield return token;

                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("willywonkabutthole");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(false));

                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("ob_buffs");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("buff_gamblefisher"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpGreater);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 3);
                yield return new Token(TokenType.CfMatch);
                yield return new IdentifierToken("ob_buffs_tier");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("buff_gamblefisher"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 4);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.BuiltInFunc, 39); // randf
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpLess);
                yield return new ConstantToken(new RealVariant(0.02));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("willywonkabutthole");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(true));
                yield return new Token(TokenType.Newline, 4);
                yield return new ConstantToken(new IntVariant(2));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.BuiltInFunc, 39); // randf
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpLess);
                yield return new ConstantToken(new RealVariant(0.05));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("willywonkabutthole");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(true));
                yield return new Token(TokenType.Newline, 4);
                yield return new ConstantToken(new IntVariant(3));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.BuiltInFunc, 39); // randf
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpLess);
                yield return new ConstantToken(new RealVariant(0.07));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("willywonkabutthole");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(true));
                yield return new Token(TokenType.Newline, 4);
                yield return new ConstantToken(new IntVariant(4));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.BuiltInFunc, 39); // randf
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpLess);
                yield return new ConstantToken(new RealVariant(0.11));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("willywonkabutthole");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(true));
                yield return new Token(TokenType.Newline, 4);
                yield return new ConstantToken(new IntVariant(5));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.BuiltInFunc, 39); // randf
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpLess);
                yield return new ConstantToken(new RealVariant(0.13));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("willywonkabutthole");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(true));

                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("willywonkabutthole");
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 3);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("ticktype");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.BuiltInFunc, 38); // randi
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpMod);
                yield return new ConstantToken(new IntVariant(3));
                yield return new Token(TokenType.OpAdd);
                yield return new ConstantToken(new IntVariant(1));

                yield return new Token(TokenType.Newline, 3);
                yield return new Token(TokenType.CfMatch);
                yield return new IdentifierToken("ticktype");
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 4);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_add_item");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("scratch_off"));
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 4);
                yield return new ConstantToken(new IntVariant(2));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_add_item");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("scratch_off_2"));
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 4);
                yield return new ConstantToken(new IntVariant(3));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_add_item");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("scratch_off_3"));
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 3);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_send_notification");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("woah! you found a scratchie."));
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("bufflib");
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.OpAssign);
                yield return new IdentifierToken("get_node");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("/root/officerballsbufflib"));
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("bufflib");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("config");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("RandomBuffsFromFishing");
                yield return new Token(TokenType.OpAnd);
                yield return new Token(TokenType.BuiltInFunc, 39); // randf
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpLessEqual);
                yield return new ConstantToken(new RealVariant(0.334));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("_random_buff_or_boon");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);

            } else if (buff_lucky.Check(token)) {

                yield return token;

                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("ob_buffs");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("buff_lucky"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpGreater);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 3);
                yield return new Token(TokenType.CfMatch);
                yield return new IdentifierToken("ob_buffs_tier");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("buff_lucky"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 4);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("failed_casts");
                yield return new Token(TokenType.OpAssignAdd);
                yield return new ConstantToken(new RealVariant(0.08));
                yield return new Token(TokenType.Newline, 4);
                yield return new ConstantToken(new IntVariant(2));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("failed_casts");
                yield return new Token(TokenType.OpAssignAdd);
                yield return new ConstantToken(new RealVariant(0.11));
                yield return new Token(TokenType.Newline, 4);
                yield return new ConstantToken(new IntVariant(3));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("failed_casts");
                yield return new Token(TokenType.OpAssignAdd);
                yield return new ConstantToken(new RealVariant(0.14));
                yield return new Token(TokenType.Newline, 4);
                yield return new ConstantToken(new IntVariant(4));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("failed_casts");
                yield return new Token(TokenType.OpAssignAdd);
                yield return new ConstantToken(new RealVariant(0.17));
                yield return new Token(TokenType.Newline, 4);
                yield return new ConstantToken(new IntVariant(5));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("failed_casts");
                yield return new Token(TokenType.OpAssignAdd);
                yield return new ConstantToken(new RealVariant(0.20));


            } else if (buff_fastbite.Check(token)) {
              
                yield return token;

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("ob_buffs");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("buff_fastbite"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpGreater);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.CfMatch);
                yield return new IdentifierToken("ob_buffs_tier");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("buff_fastbite"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("fish_timer");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("wait_time");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.BuiltInFunc, 40);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new RealVariant(1.75));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new RealVariant(2.9));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(2));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("fish_timer");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("wait_time");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.BuiltInFunc, 40);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new RealVariant(1.75));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new RealVariant(2.8));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(3));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("fish_timer");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("wait_time");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.BuiltInFunc, 40);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new RealVariant(1.5));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new RealVariant(2.7));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(4));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("fish_timer");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("wait_time");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.BuiltInFunc, 40);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new RealVariant(1.25));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new RealVariant(2.6));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(5));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("fish_timer");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("wait_time");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.BuiltInFunc, 40);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new RealVariant(1.0));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new RealVariant(2.5));
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfElif);
                yield return new IdentifierToken("ob_boons");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("boon_slowbite"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpGreater);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.CfMatch);
                yield return new IdentifierToken("ob_boons_tier");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("boon_slowbite"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("fish_timer");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("wait_time");
                yield return new Token(TokenType.OpAssignAdd);
                yield return new Token(TokenType.BuiltInFunc, 40);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new RealVariant(0));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new RealVariant(0.5));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(2));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("fish_timer");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("wait_time");
                yield return new Token(TokenType.OpAssignAdd);
                yield return new Token(TokenType.BuiltInFunc, 40);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new RealVariant(0.5));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new RealVariant(1));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(3));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("fish_timer");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("wait_time");
                yield return new Token(TokenType.OpAssignAdd);
                yield return new Token(TokenType.BuiltInFunc, 40);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new RealVariant(1));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new RealVariant(2));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(4));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("fish_timer");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("wait_time");
                yield return new Token(TokenType.OpAssignAdd);
                yield return new Token(TokenType.BuiltInFunc, 40);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new RealVariant(1.5));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new RealVariant(3));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(5));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("fish_timer");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("wait_time");
                yield return new Token(TokenType.OpAssignAdd);
                yield return new Token(TokenType.BuiltInFunc, 40);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new RealVariant(2.5));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new RealVariant(3.5));
                yield return new Token(TokenType.ParenthesisClose);


            }
            else if (boon_trash.Check(token)) {

                yield return token;

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("ob_boons");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("boon_trash"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpGreater);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.CfMatch);
                yield return new IdentifierToken("ob_boons_tier");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("boon_trash"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("junk_mult");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new RealVariant(1.5));
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(2));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("junk_mult");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new RealVariant(3.0));
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(3));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("junk_mult");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new RealVariant(5.0));
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(4));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("junk_mult");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new RealVariant(6.6));
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(5));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("junk_mult");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new RealVariant(10.0));

            } else if (boon_slowness.Check(token)) {

                yield return token;

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("ob_boons");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("boon_slowness"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpGreater);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.CfMatch);
                yield return new IdentifierToken("ob_boons_tier");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("boon_slowness"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("_speed");
                yield return new Token(TokenType.OpAssignMul);
                yield return new ConstantToken(new RealVariant(0.8));
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(2));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("_speed");
                yield return new Token(TokenType.OpAssignMul);
                yield return new ConstantToken(new RealVariant(0.7));
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(3));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("_speed");
                yield return new Token(TokenType.OpAssignMul);
                yield return new ConstantToken(new RealVariant(0.6));
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(4));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("_speed");
                yield return new Token(TokenType.OpAssignMul);
                yield return new ConstantToken(new RealVariant(0.5));
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(5));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("_speed");
                yield return new Token(TokenType.OpAssignMul);
                yield return new ConstantToken(new RealVariant(0.33));

            } else if (rollsizer.Check(token)) {

                yield return token;
                yield return new Token(TokenType.CfPass);

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("rollsizer");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(1));

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("rollsizing");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(1));

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("ob_buffs_tier");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("buff_small"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpGreater);
                yield return new IdentifierToken("rollsizer");
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("rollsizer");
                yield return new Token(TokenType.OpAssign);
                yield return new IdentifierToken("ob_buffs_tier");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("buff_small"));
                yield return new Token(TokenType.BracketClose);

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("ob_buffs_tier");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("buff_large"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpGreater);
                yield return new IdentifierToken("rollsizer");
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("rollsizer");
                yield return new Token(TokenType.OpAssign);
                yield return new IdentifierToken("ob_buffs_tier");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("buff_large"));
                yield return new Token(TokenType.BracketClose);

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("ob_buffs_tier");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("buff_quality"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpGreater);
                yield return new IdentifierToken("rollsizer");
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("rollsizer");
                yield return new Token(TokenType.OpAssign);
                yield return new IdentifierToken("ob_buffs_tier");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("buff_quality"));
                yield return new Token(TokenType.BracketClose);

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("ob_buffs_tier");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("buff_rarity"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpGreater);
                yield return new IdentifierToken("rollsizer");
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("rollsizer");
                yield return new Token(TokenType.OpAssign);
                yield return new IdentifierToken("ob_buffs_tier");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("buff_rarity"));
                yield return new Token(TokenType.BracketClose);

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfMatch);
                yield return new IdentifierToken("rollsizer");
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 2);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("rollsizing");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(3));
                yield return new Token(TokenType.Newline, 2);
                yield return new ConstantToken(new IntVariant(2));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("rollsizing");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(3));
                yield return new Token(TokenType.Newline, 2);
                yield return new ConstantToken(new IntVariant(3));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("rollsizing");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(4));
                yield return new Token(TokenType.Newline, 2);
                yield return new ConstantToken(new IntVariant(4));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("rollsizing");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(5));
                yield return new Token(TokenType.Newline, 2);
                yield return new ConstantToken(new IntVariant(5));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("rollsizing");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(7));

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfFor);
                yield return new IdentifierToken("i");
                yield return new Token(TokenType.OpIn);
                yield return new IdentifierToken("rollsizing");
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 2);

            } else if (roller.Check(token)) {

                yield return token;

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("check_small");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(false));

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("check_sparkling");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(false));

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("check_large");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(false));

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("check_gold");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(false));

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("ob_buffs");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("buff_small"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpGreater);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.CfMatch);
                yield return new IdentifierToken("ob_buffs_tier");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("buff_small"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.BuiltInFunc, 39);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpLess);
                yield return new ConstantToken(new RealVariant(0.5));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("check_small");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(true));
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(2));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.BuiltInFunc, 39);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpLess);
                yield return new ConstantToken(new RealVariant(0.6));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("check_small");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(true));
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(3));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.BuiltInFunc, 39);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpLess);
                yield return new ConstantToken(new RealVariant(0.75));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("check_small");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(true));
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(4));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.BuiltInFunc, 39);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpLess);
                yield return new ConstantToken(new RealVariant(0.9));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("check_small");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(true));
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(5));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("check_small");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(true));

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("ob_buffs");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("buff_large"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpGreater);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.CfMatch);
                yield return new IdentifierToken("ob_buffs_tier");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("buff_large"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.BuiltInFunc, 39);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpLess);
                yield return new ConstantToken(new RealVariant(0.5));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("check_large");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(true));
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(2));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.BuiltInFunc, 39);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpLess);
                yield return new ConstantToken(new RealVariant(0.6));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("check_large");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(true));
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(3));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.BuiltInFunc, 39);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpLess);
                yield return new ConstantToken(new RealVariant(0.75));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("check_large");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(true));
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(4));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.BuiltInFunc, 39);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpLess);
                yield return new ConstantToken(new RealVariant(0.9));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("check_large");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(true));
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(5));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("check_large");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(true));

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("ob_buffs");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("buff_quality"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpGreater);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.CfMatch);
                yield return new IdentifierToken("ob_buffs_tier");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("buff_quality"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.BuiltInFunc, 39);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpLess);
                yield return new ConstantToken(new RealVariant(0.5));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("check_sparkling");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(true));
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(2));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.BuiltInFunc, 39);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpLess);
                yield return new ConstantToken(new RealVariant(0.6));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("check_sparkling");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(true));
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(3));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.BuiltInFunc, 39);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpLess);
                yield return new ConstantToken(new RealVariant(0.75));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("check_sparkling");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(true));
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(4));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.BuiltInFunc, 39);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpLess);
                yield return new ConstantToken(new RealVariant(0.9));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("check_sparkling");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(true));
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(5));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("check_sparkling");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(true));

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("ob_buffs");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("buff_rarity"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpGreater);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.CfMatch);
                yield return new IdentifierToken("ob_buffs_tier");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("buff_rarity"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.BuiltInFunc, 39);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpLess);
                yield return new ConstantToken(new RealVariant(0.5));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("check_gold");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(true));
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(2));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.BuiltInFunc, 39);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpLess);
                yield return new ConstantToken(new RealVariant(0.6));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("check_gold");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(true));
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(3));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.BuiltInFunc, 39);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpLess);
                yield return new ConstantToken(new RealVariant(0.75));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("check_gold");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(true));
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(4));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.BuiltInFunc, 39);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpLess);
                yield return new ConstantToken(new RealVariant(0.9));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("check_gold");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(true));
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(5));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("check_gold");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(true));

            } else {
                yield return token;
            }
        }
    }
}
