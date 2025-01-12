using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;

namespace HideFakeCanvases;

public class StampsBegone3 : IScriptMod {
    public bool ShouldRun(string path) => path == "res://Scenes/Entities/Player/player.gdc";

    // returns a list of tokens for the new script, with the input being the original script's tokens
    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens) {
        // wait for any newline after any reference to "_ready"
        var featuremenu = new MultiTokenWaiter([ //TYPE_ARRAY, TYPE_INT]): return
            
            t => t is IdentifierToken {Name: "MOUSE_MODE_VISIBLE"},
            t => t.Type is TokenType.ParenthesisClose,
            t => t.Type is TokenType.Newline,
            t => t.Type is TokenType.CfReturn,

        ]);

        var variableadds = new MultiTokenWaiter([

            t => t is ConstantToken {Value: StringVariant {Value: "npc title here"}}

        ]);

        // loop through all tokens in the script
        foreach (var token in tokens) {
            if (featuremenu.Check(token)) {

                yield return token;

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("held_item");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("id");
                yield return new Token(TokenType.OpEqual);
                yield return new ConstantToken(new StringVariant("chalk_eraser"));
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.CfIf);
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

                yield return new Token(TokenType.Newline, 3);
                yield return new IdentifierToken("chalkcfg_mode");
                yield return new Token(TokenType.OpAssignAdd);
                yield return new ConstantToken(new IntVariant(1));

                yield return new Token(TokenType.Newline, 3);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("chalkcfg_mode");
                yield return new Token(TokenType.OpGreater);
                yield return new ConstantToken(new IntVariant(3));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("chalkcfg_mode");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(0));

                yield return new Token(TokenType.Newline, 3);
                yield return new Token(TokenType.CfElif);
                yield return new IdentifierToken("chalkcfg_mode");
                yield return new Token(TokenType.OpEqual);
                yield return new ConstantToken(new IntVariant(2));
                yield return new Token(TokenType.OpAnd);
                yield return new Token(TokenType.OpNot);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("GAME_MASTER");
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("chalkcfg_mode");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(3));

                yield return new Token(TokenType.Newline, 3);
                yield return new Token(TokenType.CfMatch);
                yield return new IdentifierToken("chalkcfg_mode");
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 4);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 5);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_send_notification");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("NoMoreStamps main settings menu selected"));
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 5);
                yield return new IdentifierToken("chalkcfg_index");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(-2));

                yield return new Token(TokenType.Newline, 4);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 5);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_send_notification");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("NoMoreStamps auto-hide settings menu selected"));
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 5);
                yield return new IdentifierToken("chalkcfg_index");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(-2));

                yield return new Token(TokenType.Newline, 4);
                yield return new ConstantToken(new IntVariant(2));
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 5);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_send_notification");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("NoMoreStamps auto-kick settings menu selected"));
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 5);
                yield return new IdentifierToken("chalkcfg_index");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(-2));

                yield return new Token(TokenType.Newline, 4);
                yield return new ConstantToken(new IntVariant(3));
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 5);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_send_notification");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("NoMoreStamps blocked player view selected"));
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 5);
                yield return new IdentifierToken("chalkcfg_index");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(-2));



                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.CfIf);
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
                yield return new Token(TokenType.OpAnd);
                yield return new IdentifierToken("chalkcfg_mode");
                yield return new Token(TokenType.OpNotEqual);
                yield return new ConstantToken(new IntVariant(-1));
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 3);
                yield return new Token(TokenType.CfMatch);
                yield return new IdentifierToken("chalkcfg_mode");
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 4);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 5);
                yield return new IdentifierToken("chalkcfg_index");
                yield return new Token(TokenType.OpAssignAdd);
                yield return new ConstantToken(new IntVariant(1));

                yield return new Token(TokenType.Newline, 5);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("chalkcfg_index");
                yield return new Token(TokenType.OpGreater);
                yield return new ConstantToken(new IntVariant(3));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("chalkcfg_index");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(0));

                yield return new Token(TokenType.Newline, 5);
                yield return new Token(TokenType.CfElif);
                yield return new IdentifierToken("chalkcfg_index");
                yield return new Token(TokenType.OpLess);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("chalkcfg_index");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(3));

                yield return new Token(TokenType.Newline, 5);
                yield return new Token(TokenType.CfMatch);
                yield return new IdentifierToken("chalkcfg_index");
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 6);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_send_notification");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("NoMoreStamps: Global toggle selected - currently "));
                yield return new Token(TokenType.OpAdd);
                yield return new Token(TokenType.BuiltInFunc, 62);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("chalkcontrol");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 6);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_send_notification");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("NoMoreStamps: Large chalk blocker selected - currently "));
                yield return new Token(TokenType.OpAdd);
                yield return new Token(TokenType.BuiltInFunc, 62);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("largecontrol");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 6);
                yield return new ConstantToken(new IntVariant(2));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_send_notification");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("NoMoreStamps: gif chalk blocker selected - currently "));
                yield return new Token(TokenType.OpAdd);
                yield return new Token(TokenType.BuiltInFunc, 62);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("gifcontrol");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 6);
                yield return new ConstantToken(new IntVariant(3));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_send_notification");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("NoMoreStamps: custom canvas chalk blocker selected - currently "));
                yield return new Token(TokenType.OpAdd);
                yield return new Token(TokenType.BuiltInFunc, 62);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("customcontrol");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 5);
                yield return new Token(TokenType.CfReturn);

                yield return new Token(TokenType.Newline, 4);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 5);
                yield return new IdentifierToken("chalkcfg_index");
                yield return new Token(TokenType.OpAssignAdd);
                yield return new ConstantToken(new IntVariant(1));

                yield return new Token(TokenType.Newline, 5);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("chalkcfg_index");
                yield return new Token(TokenType.OpGreater);
                yield return new ConstantToken(new IntVariant(2));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("chalkcfg_index");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(0));

                yield return new Token(TokenType.Newline, 5);
                yield return new Token(TokenType.CfElif);
                yield return new IdentifierToken("chalkcfg_index");
                yield return new Token(TokenType.OpLess);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("chalkcfg_index");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(2));

                yield return new Token(TokenType.Newline, 5);
                yield return new Token(TokenType.CfMatch);
                yield return new IdentifierToken("chalkcfg_index");
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 6);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_send_notification");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("NoMoreStamps: auto-hide for large selected - currently "));
                yield return new Token(TokenType.OpAdd);
                yield return new Token(TokenType.BuiltInFunc, 62);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("blockforlarge");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 6);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_send_notification");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("NoMoreStamps: auto-hide for gif selected - currently "));
                yield return new Token(TokenType.OpAdd);
                yield return new Token(TokenType.BuiltInFunc, 62);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("blockforgif");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 6);
                yield return new ConstantToken(new IntVariant(2));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_send_notification");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("NoMoreStamps: auto-hide for custom selected - currently "));
                yield return new Token(TokenType.OpAdd);
                yield return new Token(TokenType.BuiltInFunc, 62);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("blockforcustom");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 5);
                yield return new Token(TokenType.CfReturn);



                yield return new Token(TokenType.Newline, 4);
                yield return new ConstantToken(new IntVariant(2));
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 5);
                yield return new IdentifierToken("chalkcfg_index");
                yield return new Token(TokenType.OpAssignAdd);
                yield return new ConstantToken(new IntVariant(1));

                yield return new Token(TokenType.Newline, 5);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("chalkcfg_index");
                yield return new Token(TokenType.OpGreater);
                yield return new ConstantToken(new IntVariant(2));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("chalkcfg_index");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(0));

                yield return new Token(TokenType.Newline, 5);
                yield return new Token(TokenType.CfElif);
                yield return new IdentifierToken("chalkcfg_index");
                yield return new Token(TokenType.OpLess);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("chalkcfg_index");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(2));

                yield return new Token(TokenType.Newline, 5);
                yield return new Token(TokenType.CfMatch);
                yield return new IdentifierToken("chalkcfg_index");
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 6);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_send_notification");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("NoMoreStamps: kick for large selected - currently "));
                yield return new Token(TokenType.OpAdd);
                yield return new Token(TokenType.BuiltInFunc, 62);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("kickforlarge");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 6);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_send_notification");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("NoMoreStamps: kick for gif selected - currently "));
                yield return new Token(TokenType.OpAdd);
                yield return new Token(TokenType.BuiltInFunc, 62);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("kickforgif");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 6);
                yield return new ConstantToken(new IntVariant(2));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_send_notification");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("NoMoreStamps: kick for custom selected - currently "));
                yield return new Token(TokenType.OpAdd);
                yield return new Token(TokenType.BuiltInFunc, 62);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("kickforcustom");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 5);
                yield return new Token(TokenType.CfReturn);

                yield return new Token(TokenType.Newline, 4);
                yield return new ConstantToken(new IntVariant(3));
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 5);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("chalkconvicted");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("size");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpGreater);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 6);
                yield return new IdentifierToken("chalkcfg_index");
                yield return new Token(TokenType.OpAssignAdd);
                yield return new ConstantToken(new IntVariant(1));

                yield return new Token(TokenType.Newline, 6);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("chalkcfg_index");
                yield return new Token(TokenType.OpGreater);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("chalkconvicted");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("size");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("chalkcfg_index");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(0));

                yield return new Token(TokenType.Newline, 6);
                yield return new Token(TokenType.CfElif);
                yield return new IdentifierToken("chalkcfg_index");
                yield return new Token(TokenType.OpLess);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("chalkcfg_index");
                yield return new Token(TokenType.OpAssign);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("chalkconvicted");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("size");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 6);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("thestring");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant("NoMoreStamps blocklist: "));
                yield return new Token(TokenType.OpAdd);
                yield return new Token(TokenType.BuiltInFunc, 62);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_get_username_from_id");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("chalkconvicted");
                yield return new Token(TokenType.BracketOpen);
                yield return new IdentifierToken("chalkcfg_index");
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("member");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpAdd);
                yield return new ConstantToken(new StringVariant(" - reason: "));
                yield return new Token(TokenType.OpAdd);
                yield return new Token(TokenType.BuiltInFunc, 62);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("chalkconvicted");
                yield return new Token(TokenType.BracketOpen);
                yield return new IdentifierToken("chalkcfg_index");
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("reason");
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 6);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_send_notification");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("thestring");
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 6);
                yield return new Token(TokenType.CfReturn);

                yield return new Token(TokenType.Newline, 5);
                yield return new Token(TokenType.CfElse);
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_send_notification");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("NoMoreStamps: no players currently blocked."));
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 5);
                yield return new Token(TokenType.CfReturn);





                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.CfIf);
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
                yield return new Token(TokenType.OpAnd);
                yield return new IdentifierToken("chalkcfg_mode");
                yield return new Token(TokenType.OpNotEqual);
                yield return new ConstantToken(new IntVariant(-2));
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 3);
                yield return new Token(TokenType.CfMatch);
                yield return new IdentifierToken("chalkcfg_mode");
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 4);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 5);
                yield return new IdentifierToken("chalkcfg_index");
                yield return new Token(TokenType.OpAssignSub);
                yield return new ConstantToken(new IntVariant(1));

                yield return new Token(TokenType.Newline, 5);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("chalkcfg_index");
                yield return new Token(TokenType.OpGreater);
                yield return new ConstantToken(new IntVariant(3));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("chalkcfg_index");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(0));

                yield return new Token(TokenType.Newline, 5);
                yield return new Token(TokenType.CfElif);
                yield return new IdentifierToken("chalkcfg_index");
                yield return new Token(TokenType.OpLess);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("chalkcfg_index");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(3));

                yield return new Token(TokenType.Newline, 5);
                yield return new Token(TokenType.CfMatch);
                yield return new IdentifierToken("chalkcfg_index");
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 6);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_send_notification");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("NoMoreStamps: Global toggle selected - currently "));
                yield return new Token(TokenType.OpAdd);
                yield return new Token(TokenType.BuiltInFunc, 62);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("chalkcontrol");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 6);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_send_notification");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("NoMoreStamps: Large chalk blocker selected - currently "));
                yield return new Token(TokenType.OpAdd);
                yield return new Token(TokenType.BuiltInFunc, 62);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("largecontrol");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 6);
                yield return new ConstantToken(new IntVariant(2));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_send_notification");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("NoMoreStamps: gif chalk blocker selected - currently "));
                yield return new Token(TokenType.OpAdd);
                yield return new Token(TokenType.BuiltInFunc, 62);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("gifcontrol");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 6);
                yield return new ConstantToken(new IntVariant(3));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_send_notification");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("NoMoreStamps: custom canvas chalk blocker selected - currently "));
                yield return new Token(TokenType.OpAdd);
                yield return new Token(TokenType.BuiltInFunc, 62);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("customcontrol");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 5);
                yield return new Token(TokenType.CfReturn);

                yield return new Token(TokenType.Newline, 4);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 5);
                yield return new IdentifierToken("chalkcfg_index");
                yield return new Token(TokenType.OpAssignSub);
                yield return new ConstantToken(new IntVariant(1));

                yield return new Token(TokenType.Newline, 5);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("chalkcfg_index");
                yield return new Token(TokenType.OpGreater);
                yield return new ConstantToken(new IntVariant(2));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("chalkcfg_index");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(0));

                yield return new Token(TokenType.Newline, 5);
                yield return new Token(TokenType.CfElif);
                yield return new IdentifierToken("chalkcfg_index");
                yield return new Token(TokenType.OpLess);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("chalkcfg_index");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(2));

                yield return new Token(TokenType.Newline, 5);
                yield return new Token(TokenType.CfMatch);
                yield return new IdentifierToken("chalkcfg_index");
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 6);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_send_notification");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("NoMoreStamps: auto-hide for large selected - currently "));
                yield return new Token(TokenType.OpAdd);
                yield return new Token(TokenType.BuiltInFunc, 62);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("blockforlarge");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 6);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_send_notification");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("NoMoreStamps: auto-hide for gif selected - currently "));
                yield return new Token(TokenType.OpAdd);
                yield return new Token(TokenType.BuiltInFunc, 62);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("blockforgif");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 6);
                yield return new ConstantToken(new IntVariant(2));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_send_notification");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("NoMoreStamps: auto-hide for custom selected - currently "));
                yield return new Token(TokenType.OpAdd);
                yield return new Token(TokenType.BuiltInFunc, 62);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("blockforcustom");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 5);
                yield return new Token(TokenType.CfReturn);




                yield return new Token(TokenType.Newline, 4);
                yield return new ConstantToken(new IntVariant(2));
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 5);
                yield return new IdentifierToken("chalkcfg_index");
                yield return new Token(TokenType.OpAssignSub);
                yield return new ConstantToken(new IntVariant(1));

                yield return new Token(TokenType.Newline, 5);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("chalkcfg_index");
                yield return new Token(TokenType.OpGreater);
                yield return new ConstantToken(new IntVariant(2));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("chalkcfg_index");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(0));

                yield return new Token(TokenType.Newline, 5);
                yield return new Token(TokenType.CfElif);
                yield return new IdentifierToken("chalkcfg_index");
                yield return new Token(TokenType.OpLess);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("chalkcfg_index");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(2));

                yield return new Token(TokenType.Newline, 5);
                yield return new Token(TokenType.CfMatch);
                yield return new IdentifierToken("chalkcfg_index");
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 6);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_send_notification");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("NoMoreStamps: kick for large selected - currently "));
                yield return new Token(TokenType.OpAdd);
                yield return new Token(TokenType.BuiltInFunc, 62);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("kickforlarge");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 6);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_send_notification");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("NoMoreStamps: kick for gif selected - currently "));
                yield return new Token(TokenType.OpAdd);
                yield return new Token(TokenType.BuiltInFunc, 62);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("kickforgif");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 6);
                yield return new ConstantToken(new IntVariant(2));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_send_notification");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("NoMoreStamps: kick for custom selected - currently "));
                yield return new Token(TokenType.OpAdd);
                yield return new Token(TokenType.BuiltInFunc, 62);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("kickforcustom");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 5);
                yield return new Token(TokenType.CfReturn);

                yield return new Token(TokenType.Newline, 4);
                yield return new ConstantToken(new IntVariant(3));
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 5);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("chalkconvicted");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("size");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpGreater);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 6);
                yield return new IdentifierToken("chalkcfg_index");
                yield return new Token(TokenType.OpAssignSub);
                yield return new ConstantToken(new IntVariant(1));

                yield return new Token(TokenType.Newline, 6);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("chalkcfg_index");
                yield return new Token(TokenType.OpGreater);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("chalkconvicted");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("size");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("chalkcfg_index");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(0));

                yield return new Token(TokenType.Newline, 6);
                yield return new Token(TokenType.CfElif);
                yield return new IdentifierToken("chalkcfg_index");
                yield return new Token(TokenType.OpLess);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("chalkcfg_index");
                yield return new Token(TokenType.OpAssign);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("chalkconvicted");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("size");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 6);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("thestring");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant("NoMoreStamps blocklist: "));
                yield return new Token(TokenType.OpAdd);
                yield return new Token(TokenType.BuiltInFunc, 62);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_get_username_from_id");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("chalkconvicted");
                yield return new Token(TokenType.BracketOpen);
                yield return new IdentifierToken("chalkcfg_index");
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("member");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpAdd);
                yield return new ConstantToken(new StringVariant(" - reason: "));
                yield return new Token(TokenType.OpAdd);
                yield return new Token(TokenType.BuiltInFunc, 62);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("chalkconvicted");
                yield return new Token(TokenType.BracketOpen);
                yield return new IdentifierToken("chalkcfg_index");
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("reason");
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 6);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_send_notification");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("thestring");
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 6);
                yield return new Token(TokenType.CfReturn);

                yield return new Token(TokenType.Newline, 5);
                yield return new Token(TokenType.CfElse);
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_send_notification");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("NoMoreStamps: no players currently blocked."));
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 5);
                yield return new Token(TokenType.CfReturn);



                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.CfIf);
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
                yield return new ConstantToken(new StringVariant("interact"));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpAnd);
                yield return new IdentifierToken("chalkcfg_mode");
                yield return new Token(TokenType.OpNotEqual);
                yield return new ConstantToken(new IntVariant(-1));
                yield return new Token(TokenType.OpAnd);
                yield return new IdentifierToken("chalkcfg_index");
                yield return new Token(TokenType.OpNotEqual);
                yield return new ConstantToken(new IntVariant(-2));
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 3);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("setsavetimer");
                yield return new Token(TokenType.OpAssign);
                yield return new IdentifierToken("Time");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("get_unix_time_from_system");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpAdd);
                yield return new ConstantToken(new IntVariant(15));

                yield return new Token(TokenType.Newline, 3);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("queuesave");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(true));

                yield return new Token(TokenType.Newline, 3);
                yield return new Token(TokenType.CfMatch);
                yield return new IdentifierToken("chalkcfg_mode");
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 4);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 5);
                yield return new Token(TokenType.CfMatch);
                yield return new IdentifierToken("chalkcfg_index");
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 6);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 7);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("chalkcontrol");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.OpNot);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("chalkcontrol");

                yield return new Token(TokenType.Newline, 7);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("chalkcontrol");
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_send_notification");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("NoMoreStamps enabled."));
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 7);
                yield return new Token(TokenType.CfElif);
                yield return new Token(TokenType.OpNot);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("chalkcontrol");
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_send_notification");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("NoMoreStamps disabled."));
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 7);
                yield return new Token(TokenType.CfReturn);

                yield return new Token(TokenType.Newline, 6);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 7);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("largecontrol");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.OpNot);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("largecontrol");

                yield return new Token(TokenType.Newline, 7);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("largecontrol");
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_send_notification");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("NoMoreStamps: large chalk blocking enabled."));
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 7);
                yield return new Token(TokenType.CfElif);
                yield return new Token(TokenType.OpNot);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("largecontrol");
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_send_notification");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("NoMoreStamps: large chalk blocking disabled."));
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 7);
                yield return new Token(TokenType.CfReturn);

                yield return new Token(TokenType.Newline, 6);
                yield return new ConstantToken(new IntVariant(2));
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 7);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("gifcontrol");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.OpNot);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("gifcontrol");

                yield return new Token(TokenType.Newline, 7);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("gifcontrol");
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_send_notification");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("NoMoreStamps: gif blocking enabled."));
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 7);
                yield return new Token(TokenType.CfElif);
                yield return new Token(TokenType.OpNot);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("gifcontrol");
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_send_notification");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("NoMoreStamps: gif blocking disabled."));
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 7);
                yield return new Token(TokenType.CfReturn);

                yield return new Token(TokenType.Newline, 6);
                yield return new ConstantToken(new IntVariant(3));
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 7);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("customcontrol");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.OpNot);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("customcontrol");

                yield return new Token(TokenType.Newline, 7);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("customcontrol");
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_send_notification");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("NoMoreStamps: custom canvas blocking enabled."));
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 7);
                yield return new Token(TokenType.CfElif);
                yield return new Token(TokenType.OpNot);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("customcontrol");
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_send_notification");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("NoMoreStamps: custom canvas blocking disabled."));
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 7);
                yield return new Token(TokenType.CfReturn);

                yield return new Token(TokenType.Newline, 4);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 5);
                yield return new Token(TokenType.CfMatch);
                yield return new IdentifierToken("chalkcfg_index");
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 6);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 7);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("blockforlarge");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.OpNot);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("blockforlarge");

                yield return new Token(TokenType.Newline, 7);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("blockforlarge");
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_send_notification");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("NoMoreStamps: auto-hide for large enabled."));
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 7);
                yield return new Token(TokenType.CfElif);
                yield return new Token(TokenType.OpNot);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("blockforlarge");
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_send_notification");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("NoMoreStamps: auto-hide for large disabled."));
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 7);
                yield return new Token(TokenType.CfReturn);

                yield return new Token(TokenType.Newline, 6);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 7);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("blockforgif");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.OpNot);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("blockforgif");

                yield return new Token(TokenType.Newline, 7);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("blockforgif");
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_send_notification");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("NoMoreStamps: auto-hide for gifs enabled."));
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 7);
                yield return new Token(TokenType.CfElif);
                yield return new Token(TokenType.OpNot);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("blockforgif");
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_send_notification");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("NoMoreStamps: auto-hide for gifs disabled."));
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 7);
                yield return new Token(TokenType.CfReturn);

                yield return new Token(TokenType.Newline, 6);
                yield return new ConstantToken(new IntVariant(2));
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 7);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("blockforcustom");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.OpNot);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("blockforcustom");

                yield return new Token(TokenType.Newline, 7);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("blockforcustom");
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_send_notification");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("NoMoreStamps: auto-hide for custom canvases enabled."));
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 7);
                yield return new Token(TokenType.CfElif);
                yield return new Token(TokenType.OpNot);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("blockforcustom");
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_send_notification");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("NoMoreStamps: auto-hide for custom canvases disabled."));
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 7);
                yield return new Token(TokenType.CfReturn);

                yield return new Token(TokenType.Newline, 4);
                yield return new ConstantToken(new IntVariant(2));
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 5);
                yield return new Token(TokenType.CfMatch);
                yield return new IdentifierToken("chalkcfg_index");
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 6);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 7);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("kickforlarge");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.OpNot);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("kickforlarge");

                yield return new Token(TokenType.Newline, 7);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("kickforlarge");
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_send_notification");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("NoMoreStamps: kicking for large enabled."));
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 7);
                yield return new Token(TokenType.CfElif);
                yield return new Token(TokenType.OpNot);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("kickforlarge");
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_send_notification");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("NoMoreStamps: kicking for large disabled."));
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 7);
                yield return new Token(TokenType.CfReturn);

                yield return new Token(TokenType.Newline, 6);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 7);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("kickforgif");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.OpNot);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("kickforgif");

                yield return new Token(TokenType.Newline, 7);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("kickforgif");
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_send_notification");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("NoMoreStamps: kicking for gifs enabled."));
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 7);
                yield return new Token(TokenType.CfElif);
                yield return new Token(TokenType.OpNot);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("kickforgif");
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_send_notification");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("NoMoreStamps: kicking for gifs disabled."));
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 7);
                yield return new Token(TokenType.CfReturn);

                yield return new Token(TokenType.Newline, 6);
                yield return new ConstantToken(new IntVariant(2));
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 7);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("kickforcustom");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.OpNot);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("kickforcustom");

                yield return new Token(TokenType.Newline, 7);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("kickforcustom");
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_send_notification");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("NoMoreStamps: kicking for custom canvases enabled."));
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 7);
                yield return new Token(TokenType.CfElif);
                yield return new Token(TokenType.OpNot);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("kickforcustom");
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_send_notification");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("NoMoreStamps: kicking for custom canvases disabled."));
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 7);
                yield return new Token(TokenType.CfReturn);

                yield return new Token(TokenType.Newline, 4);
                yield return new ConstantToken(new IntVariant(3));
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 5);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("astring");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant("NoMoreStamps: "));
                yield return new Token(TokenType.OpAdd);
                yield return new Token(TokenType.BuiltInFunc, 62);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_get_username_from_id");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("chalkconvicted");
                yield return new Token(TokenType.BracketOpen);
                yield return new IdentifierToken("chalkcfg_index");
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("member");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpAdd);
                yield return new ConstantToken(new StringVariant(" removed from blocklist."));

                yield return new Token(TokenType.Newline, 5);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("chalkconvicted");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("erase");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("chalkconvicted");
                yield return new Token(TokenType.BracketOpen);
                yield return new IdentifierToken("chalkcfg_index");
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 5);
                yield return new Token(TokenType.CfReturn);

            } else if (variableadds.Check(token)) {

                yield return token;

                yield return new Token(TokenType.Newline);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("chalkcfg_index");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(-2));

                yield return new Token(TokenType.Newline);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("chalkcfg_mode");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(-1));

            } else {

                // return the original token
                yield return token;
            }
        }
    }
}
