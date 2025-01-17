using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;

namespace BobberControl;

public class BobberControlMod : IScriptMod {
    public bool ShouldRun(string path) => path == "res://Scenes/Entities/Player/player.gdc";

    // returns a list of tokens for the new script, with the input being the original script's tokens
    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens) {
        // wait for any newline after any reference to "_ready"
        
        var waiter2 = new MultiTokenWaiter([

            t => t.Type is TokenType.Newline,
            t => t.Type is TokenType.CfIf,
            t => t is IdentifierToken {Name: "freecamming"},
        ]);

        var waiter3 = new MultiTokenWaiter([

            t => t is ConstantToken {Value: StringVariant {Value: "mouse_look"}},
            t => t.Type is TokenType.ParenthesisClose,

        ]);

        var waiter4 = new MultiTokenWaiter([

            t => t is IdentifierToken {Name: "STATES"},
            t => t.Type is TokenType.Period,
            t => t is IdentifierToken {Name: "FISHING"},
            t => t.Type is TokenType.Colon,

            t => t.Type is TokenType.Newline,
            t => t is IdentifierToken {Name: "cam_push"},
            t => t.Type is TokenType.OpAssign,
            t => t.Type is TokenType.OpSub,
            t => t is ConstantToken {Value: RealVariant {Value: 0.3}},

            t => t.Type is TokenType.Newline,
            t => t is IdentifierToken {Name: "locked"},
            t => t.Type is TokenType.OpAssign,

        ]);

        var waiter5 = new MultiTokenWaiter([

            t => t.Type is TokenType.CfElse,
            t => t is ConstantToken {Value: RealVariant {Value: 0.01}},

        ]);

        var waiter6 = new MultiTokenWaiter([

            t => t is IdentifierToken {Name: "is_valid_fishing_spot"},
            t => t.Type is TokenType.OpAssign,
            t => t is ConstantToken {Value: BoolVariant {Value: false}},

        ]);


        var waiter8 = new MultiTokenWaiter([

            t => t.Type is TokenType.OpAnd,
            t => t is IdentifierToken {Name: "state"},
            t => t.Type is TokenType.OpEqual,
            t => t is IdentifierToken {Name: "STATES"},
            t => t.Type is TokenType.Period,
            t => t is IdentifierToken {Name: "FISHING"},

        ]);

        var waiter9 = new MultiTokenWaiter([

            t => t.Type is TokenType.CfIf,
            t => t is IdentifierToken {Name: "emote_locked"},

        ]);

        var waiter10 = new MultiTokenWaiter([

            t => t is IdentifierToken {Name: "_toggle_sit"},
            t => t.Type is TokenType.ParenthesisOpen,
            t => t is ConstantToken {Value: BoolVariant {Value: true}},
            t => t.Type is TokenType.ParenthesisClose,

        ]);

        var binds = new MultiTokenWaiter([

            t => t is IdentifierToken {Name: "MOUSE_MODE_VISIBLE"},
            t => t.Type is TokenType.ParenthesisClose,
            t => t.Type is TokenType.Newline,
            t => t.Type is TokenType.CfReturn,

        ]);

        var tambaddon = new MultiTokenWaiter([

            t => t.Type is TokenType.ParenthesisClose,
            t => t.Type is TokenType.Newline,
            t => t is IdentifierToken {Name: "bobber_control"},
            t => t.Type is TokenType.OpAssign,
            t => t is ConstantToken {Value: BoolVariant {Value: true}},

        ]);

        var declared = new MultiTokenWaiter([
            t => t.Type is TokenType.PrVar,
            t => t is IdentifierToken {Name: "camera_zoom"},
            t => t.Type is TokenType.OpAssign,
            t => t is ConstantToken {Value: RealVariant{ Value: 5.0}},
        ]);

        var tambaddon2 = new MultiTokenWaiter([

            t => t is IdentifierToken {Name: "is_valid_fishing_spot"},
            t => t.Type is TokenType.OpAssign,
            t => t is ConstantToken {Value: BoolVariant {Value: true}},

        ]);


        // loop through all tokens in the script
        foreach (var token in tokens) {
            
            if (waiter2.Check(token))
            {

                yield return token;
                yield return new Token(TokenType.OpAnd);
                yield return new ConstantToken(new BoolVariant(false));


            }
            else if (waiter3.Check(token))
            {

                yield return token;
                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("freecamming");
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CfReturn);

            } else if (waiter4.Check(token)) {

                yield return token;
                yield return new ConstantToken(new BoolVariant(false));
                yield return new Token(TokenType.OpAnd);

            } else if (waiter5.Check(token)) {

                yield return token;

                yield return new Token(TokenType.Newline, 3);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("Input");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("is_action_pressed");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("move_walk"));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("rod_cast_dist");
                yield return new Token(TokenType.OpAssignAdd);
                yield return new ConstantToken(new RealVariant(0.05));

            } else if (waiter6.Check(token)) { 
            
                yield return token;
                yield return new Token(TokenType.OpOr);
                yield return new ConstantToken(new BoolVariant(true));


            } else if (waiter8.Check(token)) {

                yield return token;
                yield return new Token(TokenType.OpAnd);
                yield return new ConstantToken(new BoolVariant(false));

            } else if (waiter9.Check(token)) {
                
                yield return token;
                yield return new Token(TokenType.OpAnd);
                yield return new ConstantToken(new BoolVariant(false));

            } else if (binds.Check(token)) {

                yield return token;

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("state");
                yield return new Token(TokenType.OpEqual);
                yield return new IdentifierToken("STATES");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("FISHING");
                yield return new Token(TokenType.OpAnd);
                yield return new IdentifierToken("Input");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("is_action_pressed");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("move_walk"));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpAnd);
                yield return new IdentifierToken("Input");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("is_action_just_released");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("zoom_in"));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 2);
                yield return new IdentifierToken("bobber_vpos");
                yield return new Token(TokenType.OpAssignSub);
                yield return new ConstantToken(new RealVariant(0.05));
                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.CfReturn);

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("state");
                yield return new Token(TokenType.OpEqual);
                yield return new IdentifierToken("STATES");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("FISHING");
                yield return new Token(TokenType.OpAnd);
                yield return new IdentifierToken("Input");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("is_action_pressed");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("move_walk"));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpAnd);
                yield return new IdentifierToken("Input");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("is_action_just_released");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("zoom_out"));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 2);
                yield return new IdentifierToken("bobber_vpos");
                yield return new Token(TokenType.OpAssignAdd);
                yield return new ConstantToken(new RealVariant(0.05));
                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.CfReturn);

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("state");
                yield return new Token(TokenType.OpEqual);
                yield return new IdentifierToken("STATES");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("FISHING");
                yield return new Token(TokenType.OpAnd);
                yield return new IdentifierToken("Input");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("is_action_pressed");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("move_walk"));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpAnd);
                yield return new IdentifierToken("Input");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("is_action_just_pressed");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("mouse_look"));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 2);
                yield return new IdentifierToken("_enter_state");  // $bobber.global_transform.origin
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("STATES");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("FISHING_CANCEL");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.CfReturn);

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("state");
                yield return new Token(TokenType.OpEqual);
                yield return new IdentifierToken("STATES");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("FISHING");
                yield return new Token(TokenType.OpAnd);
                yield return new IdentifierToken("Input");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("is_action_pressed");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("move_walk"));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpAnd);
                yield return new IdentifierToken("Input");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("is_action_just_pressed");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("move_down"));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.CfFor);
                yield return new IdentifierToken("actor");
                yield return new Token(TokenType.OpIn);
                yield return new IdentifierToken("get_tree");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("get_nodes_in_group");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("player"));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 3);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.OpNot);
                yield return new Token(TokenType.BuiltInFunc, 89);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("actor");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CfContinue);
                yield return new Token(TokenType.Newline, 3);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("dist");
                yield return new Token(TokenType.OpAssign);
                yield return new IdentifierToken("actor");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("global_transform");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("origin");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("distance_to");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.Dollar);
                yield return new IdentifierToken("bobber");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("global_transform");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("origin");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Newline, 3);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("dist");
                yield return new Token(TokenType.OpLessEqual);
                yield return new ConstantToken(new RealVariant(2.5));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_send_P2P_Packet");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.CurlyBracketOpen);
                yield return new ConstantToken(new StringVariant("type"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new StringVariant("player_punch"));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("from_pos"));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Dollar);
                yield return new IdentifierToken("bobber");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("global_transform");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("origin");
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("punch_type"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.CurlyBracketClose);
                yield return new Token(TokenType.Comma);
                yield return new Token(TokenType.BuiltInFunc, 62);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("actor");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("owner_id");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new IntVariant(2));
                yield return new Token(TokenType.Comma);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("CHANNELS");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("ACTOR_ACTION");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.CfReturn);


            } else if (tambaddon.Check(token)) {

                yield return token;

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("tambaddon");
                yield return new Token(TokenType.OpAssign);
                yield return new IdentifierToken("get_node_or_null");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("/root/officerballstambourine"));
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.OpNot);
                yield return new IdentifierToken("tambaddon");
                yield return new Token(TokenType.OpAnd);
                yield return new IdentifierToken("tamb_attempt");
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("tamb_attempt");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(false));

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfElif);
                yield return new IdentifierToken("tambaddon");
                yield return new Token(TokenType.OpAnd);
                yield return new IdentifierToken("tamb_attempt");
                yield return new Token(TokenType.OpAnd);
                yield return new IdentifierToken("tamb_choice");
                yield return new Token(TokenType.OpEqual);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 2);
                yield return new IdentifierToken("tamb_attempt");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(false));

                yield return new Token(TokenType.Newline, 2);
                yield return new IdentifierToken("lockHatPos");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(true));

                yield return new Token(TokenType.Newline, 2);
                yield return new IdentifierToken("rippleHatPos");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.Dollar);
                yield return new IdentifierToken("bobber");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("global_transform");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("origin");
                yield return new Token(TokenType.OpAdd);
                yield return new Token(TokenType.BuiltInType, 7);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new RealVariant(0.0));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new RealVariant(0.6));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new RealVariant(0.0));
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 2);
                yield return new IdentifierToken("rippleHatZone");
                yield return new Token(TokenType.OpAssign);
                yield return new IdentifierToken("current_zone");

                yield return new Token(TokenType.Newline, 2);
                yield return new IdentifierToken("rippleHatZoneOwner");
                yield return new Token(TokenType.OpAssign);
                yield return new IdentifierToken("current_zone_owner");

                yield return new Token(TokenType.Newline, 2);
                yield return new IdentifierToken("rippletimer");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(0));

                yield return new Token(TokenType.Newline, 2);
                yield return new IdentifierToken("internallatch");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(true));

                yield return new Token(TokenType.Newline, 2);
                yield return new IdentifierToken("dedilatch");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(true));

                yield return new Token(TokenType.Newline, 2);
                yield return new IdentifierToken("hatDisable");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(false));

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfElif);
                yield return new IdentifierToken("tambaddon");
                yield return new Token(TokenType.OpAnd);
                yield return new IdentifierToken("tamb_attempt");
                yield return new Token(TokenType.OpAnd);
                yield return new IdentifierToken("tamb_choice");
                yield return new Token(TokenType.OpEqual);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 2);
                yield return new IdentifierToken("tamb_attempt");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(false));

                yield return new Token(TokenType.Newline, 2);
                yield return new IdentifierToken("meteorPos");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.Dollar);
                yield return new IdentifierToken("bobber");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("global_transform");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("origin");
                yield return new Token(TokenType.OpSub);
                yield return new Token(TokenType.BuiltInType, 7);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new RealVariant(0.0));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new RealVariant(0.4));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new RealVariant(0.0));
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 2);
                yield return new IdentifierToken("meteorZone");
                yield return new Token(TokenType.OpAssign);
                yield return new IdentifierToken("current_zone");

                yield return new Token(TokenType.Newline, 2);
                yield return new IdentifierToken("meteorZoneOwner");
                yield return new Token(TokenType.OpAssign);
                yield return new IdentifierToken("current_zone_owner");

                yield return new Token(TokenType.Newline, 2);
                yield return new IdentifierToken("meteortimer");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(0));

                yield return new Token(TokenType.Newline, 2);
                yield return new IdentifierToken("meteored");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(true));

            } else if (declared.Check(token)) {

                yield return token;

                yield return new Token(TokenType.Newline);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("tamb_attempt");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(false));

            } else if (tambaddon2.Check(token)){

                yield return token;

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("Input");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("is_action_pressed");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("move_walk"));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("tamb_attempt");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(true));


            } else {
                // return the original token
                yield return token;
            }
        }
    }
}
