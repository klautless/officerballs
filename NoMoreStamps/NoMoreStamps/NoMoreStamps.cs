using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;

namespace HideFakeCanvases;

public class StampsBegone : IScriptMod {
    public bool ShouldRun(string path) => path == "res://Scenes/World/world.gdc";

    // returns a list of tokens for the new script, with the input being the original script's tokens
    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens) {
        // wait for any newline after any reference to "_ready"
        var cumBlock = new MultiTokenWaiter([

            t => t.Type is TokenType.CfElse,
            t => t is IdentifierToken {Name: "network_sender"},

        ]);

        var onReady = new MultiTokenWaiter([

            t => t.Type is TokenType.CfIf,
            t => t.Type is TokenType.OpNot,
            t => t is IdentifierToken {Name: "backdrop"},
            t => t.Type is TokenType.Colon,

        ]);

        // loop through all tokens in the script
        foreach (var token in tokens) {
            if (cumBlock.Check(token)) {

                yield return token;

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("actor_type");
                yield return new Token(TokenType.OpEqual);
                yield return new ConstantToken(new StringVariant("canvas"));
                yield return new Token(TokenType.OpAnd);
                yield return new IdentifierToken("get_node");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("/root/NoMoreStamps"));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("config");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("CustomCanvasFilter");
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 2);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_send_notification");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_get_username_from_id");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("owner_id");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpAdd);
                yield return new ConstantToken(new StringVariant(" tried spawning in a custom canvas."));
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("GAME_MASTER");
                yield return new Token(TokenType.OpAnd);
                yield return new IdentifierToken("get_node");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("/root/NoMoreStamps"));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("config");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("HostKickForCustom");
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_kick_player");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("owner_id");
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 2);
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
                yield return new Token(TokenType.CfFor);
                yield return new IdentifierToken("i");
                yield return new Token(TokenType.OpIn);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("chalkconvicted");
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 3);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("i");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("member"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpEqual);
                yield return new IdentifierToken("owner_id");
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CfReturn);

                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("get_node");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("/root/NoMoreStamps"));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("config");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("HideUsersChalkForCustom");
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("chalkconvicted");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("append");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.CurlyBracketOpen);
                yield return new ConstantToken(new StringVariant("member"));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("owner_id");
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("reason"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new StringVariant("custom canvas"));
                yield return new Token(TokenType.CurlyBracketClose);
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.CfReturn);

                yield return new Token(TokenType.Newline, 1);

            } else if (onReady.Check(token)) {

                yield return token;

                yield return new Token(TokenType.Newline, 2);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("joindelay");
                yield return new Token(TokenType.OpAssign);
                yield return new IdentifierToken("Time");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("get_unix_time_from_system");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpAdd);
                yield return new IdentifierToken("get_node");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("/root/NoMoreStamps"));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("config");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("JoinDelayTime");


            } else {
                // return the original token
                yield return token;
            }
        }
    }
}
