using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;

namespace HideFakeCanvases;

public class StampsBegone2 : IScriptMod {
    public bool ShouldRun(string path) => path == "res://Scenes/Singletons/SteamNetwork.gdc";

    // returns a list of tokens for the new script, with the input being the original script's tokens
    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens) {
        // wait for any newline after any reference to "_ready"
        var waiter = new MultiTokenWaiter([ //TYPE_ARRAY, TYPE_INT]): return
            
            t => t is IdentifierToken {Name: "TYPE_ARRAY"},
            t => t.Type is TokenType.Comma,
            t => t is IdentifierToken {Name: "TYPE_INT"},
            t => t.Type is TokenType.BracketClose,
            t => t.Type is TokenType.ParenthesisClose,
            t => t.Type is TokenType.Colon,
            t => t.Type is TokenType.CfReturn,

        ]);

        var addvars = new MultiTokenWaiter([
            
            t => t.Type is TokenType.PrVar,
            t => t is IdentifierToken {Name: "NETWORK_TIMER"},

        ]);

        var addreset = new MultiTokenWaiter([

            t => t is ConstantToken {Value: StringVariant {Value: "Resetting Lobby Status..."}},
            t => t.Type is TokenType.ParenthesisClose,

        ]);

        var addtimeprocesser = new MultiTokenWaiter([

            t => t is IdentifierToken {Name: "run_callbacks"},
            t => t.Type is TokenType.ParenthesisOpen,
            t => t.Type is TokenType.ParenthesisClose,

        ]);

        // loop through all tokens in the script
        foreach (var token in tokens) {
            if (addvars.Check(token)) {

                yield return token;

                yield return new Token(TokenType.Newline);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("chalksynced");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(false));
                yield return new Token(TokenType.Newline);

                yield return new Token(TokenType.Newline);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("chalkcontrol");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(true));
                yield return new Token(TokenType.Newline);

                yield return new Token(TokenType.Newline);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("gifcontrol");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(true));
                yield return new Token(TokenType.Newline);

                yield return new Token(TokenType.Newline);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("largecontrol");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(true));
                yield return new Token(TokenType.Newline);

                yield return new Token(TokenType.Newline);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("customcontrol");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(true));
                yield return new Token(TokenType.Newline);

                yield return new Token(TokenType.Newline);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("blockforlarge");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(false));
                yield return new Token(TokenType.Newline);

                yield return new Token(TokenType.Newline);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("blockforgif");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(true));
                yield return new Token(TokenType.Newline);

                yield return new Token(TokenType.Newline);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("blockforcustom");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(false));
                yield return new Token(TokenType.Newline);

                yield return new Token(TokenType.Newline);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("kickforlarge");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(false));
                yield return new Token(TokenType.Newline);

                yield return new Token(TokenType.Newline);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("kickforgif");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(false));
                yield return new Token(TokenType.Newline);

                yield return new Token(TokenType.Newline);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("kickforcustom");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(false));
                yield return new Token(TokenType.Newline);

                yield return new Token(TokenType.Newline);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("chalkconvicts");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.BracketOpen);
                yield return new Token(TokenType.BracketClose);

                yield return new Token(TokenType.Newline);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("chalkconvicted");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.BracketOpen);
                yield return new Token(TokenType.BracketClose);

                yield return new Token(TokenType.Newline);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("chalktimer");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(0));

                yield return new Token(TokenType.Newline);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("chalkcompared");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(0));

            } else if (addreset.Check(token)) {

                yield return token;

                yield return new Token(TokenType.Newline, 1);
                yield return new IdentifierToken("chalksynced");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(false));
                yield return new Token(TokenType.Newline, 1);

                yield return new Token(TokenType.Newline, 1);
                yield return new IdentifierToken("chalkconvicted");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("clear");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);

            } else if (addtimeprocesser.Check(token)) {

                yield return token;

                yield return new Token(TokenType.Newline, 1);
                yield return new IdentifierToken("chalkcompared");
                yield return new Token(TokenType.OpAssign);
                yield return new IdentifierToken("Time");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("get_unix_time_from_system");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("chalkcompared");
                yield return new Token(TokenType.OpGreaterEqual);
                yield return new IdentifierToken("chalktimer");
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 2);
                yield return new IdentifierToken("chalktimer");
                yield return new Token(TokenType.OpAssign);
                yield return new IdentifierToken("Time");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("get_unix_time_from_system");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpAdd);
                yield return new ConstantToken(new IntVariant(1));

                yield return new Token(TokenType.Newline, 2);
                yield return new IdentifierToken("chalkconvicts");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("clear");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);

            } else if (waiter.Check(token)) {

                yield return token;

                yield return new Token(TokenType.Newline, 4);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("chalkcontrol");
                yield return new Token(TokenType.OpAnd);
                yield return new IdentifierToken("chalkconvicted");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("size");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpGreater);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CfFor);
                yield return new IdentifierToken("i");
                yield return new Token(TokenType.OpIn);
                yield return new IdentifierToken("chalkconvicted");
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 5);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("i");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("member"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpEqual);
                yield return new IdentifierToken("PACKET_SENDER");
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CfReturn);

                yield return new Token(TokenType.Newline, 4);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("datasize");
                yield return new Token(TokenType.OpAssign);
                yield return new IdentifierToken("DATA");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("data"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("size");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 4);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("temp_count");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(0));

                yield return new Token(TokenType.Newline, 4);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("chalkconvicts");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("size");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpGreater);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.OpAnd);
                yield return new IdentifierToken("chalksynced");
                yield return new Token(TokenType.OpAnd);
                yield return new IdentifierToken("chalkcontrol");
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CfFor);
                yield return new IdentifierToken("i");
                yield return new Token(TokenType.OpIn);
                yield return new IdentifierToken("chalkconvicts");
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 5);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("i");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("member"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpEqual);
                yield return new IdentifierToken("PACKET_SENDER");
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 6);
                yield return new IdentifierToken("temp_count");
                yield return new Token(TokenType.OpAssign);
                yield return new IdentifierToken("i");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("quantity"));
                yield return new Token(TokenType.BracketClose);

                yield return new Token(TokenType.Newline, 6);
                yield return new IdentifierToken("chalkconvicts");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("erase");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("i");
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 4);
                yield return new IdentifierToken("temp_count");
                yield return new Token(TokenType.OpAssignAdd);
                yield return new ConstantToken(new IntVariant(1));

                yield return new Token(TokenType.Newline, 4);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("datasize");
                yield return new Token(TokenType.OpGreater);
                yield return new ConstantToken(new IntVariant(3800));
                yield return new Token(TokenType.OpAnd);
                yield return new IdentifierToken("chalksynced");
                yield return new Token(TokenType.OpAnd);
                yield return new IdentifierToken("chalkcontrol");
                yield return new Token(TokenType.OpAnd);
                yield return new IdentifierToken("largecontrol");
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 5);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("GAME_MASTER");
                yield return new Token(TokenType.OpAnd);
                yield return new IdentifierToken("kickforlarge");
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("_kick_player");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("PACKET_SENDER");
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 5);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_send_notification");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("_get_username_from_id");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("PACKET_SENDER");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpAdd);
                yield return new ConstantToken(new StringVariant(" tried sending too much chalk - likely a large stamp."));
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 5);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("blockforlarge");
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("chalkconvicted");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("append");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.CurlyBracketOpen);
                yield return new ConstantToken(new StringVariant("member"));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("PACKET_SENDER");
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("reason"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new StringVariant("large paste"));
                yield return new Token(TokenType.CurlyBracketClose);
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 5);
                yield return new Token(TokenType.CfReturn);

                yield return new Token(TokenType.Newline, 4);
                yield return new Token(TokenType.CfElif);
                yield return new IdentifierToken("chalksynced");
                yield return new Token(TokenType.OpEqual);
                yield return new ConstantToken(new BoolVariant(false));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("chalksynced");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(true));

                yield return new Token(TokenType.Newline, 4);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("temp_count");
                yield return new Token(TokenType.OpGreaterEqual);
                yield return new ConstantToken(new IntVariant(6));
                yield return new Token(TokenType.OpAnd);
                yield return new IdentifierToken("chalkcontrol");
                yield return new Token(TokenType.OpAnd);
                yield return new IdentifierToken("gifcontrol");
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 5);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("blockforgif");
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("chalkconvicted");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("append");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.CurlyBracketOpen);
                yield return new ConstantToken(new StringVariant("member"));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("PACKET_SENDER");
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("reason"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new StringVariant("gif playback"));
                yield return new Token(TokenType.CurlyBracketClose);
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 5);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_send_notification");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("_get_username_from_id");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("PACKET_SENDER");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpAdd);
                yield return new ConstantToken(new StringVariant(" tried playing a .gif."));
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 5);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("GAME_MASTER");
                yield return new Token(TokenType.OpAnd);
                yield return new IdentifierToken("kickforgif");
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("_kick_player");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("PACKET_SENDER");
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 4);
                yield return new Token(TokenType.CfElse);
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 5);
                yield return new IdentifierToken("chalkconvicts");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("append");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.CurlyBracketOpen);
                yield return new ConstantToken(new StringVariant("member"));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("PACKET_SENDER");
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("quantity"));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("temp_count");
                yield return new Token(TokenType.CurlyBracketClose);
                yield return new Token(TokenType.ParenthesisClose);

            } else {
                // return the original token
                yield return token;
            }
        }
    }
}
