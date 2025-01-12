using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;

namespace HideFakeCanvases;

public class StampsBegone : IScriptMod {
    public bool ShouldRun(string path) => path == "res://Scenes/World/world.gdc";

    // returns a list of tokens for the new script, with the input being the original script's tokens
    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens) {
        // wait for any newline after any reference to "_ready"
        var waiter = new MultiTokenWaiter([

            t => t.Type is TokenType.CfElse,
            t => t is IdentifierToken {Name: "network_sender"},

        ]);

        // loop through all tokens in the script
        foreach (var token in tokens) {
            if (waiter.Check(token)) {
                // found our match, return the original newline
                yield return token;

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("actor_type");
                yield return new Token(TokenType.OpEqual);
                yield return new ConstantToken(new StringVariant("canvas"));
                yield return new Token(TokenType.OpAnd);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("chalkcontrol");
                yield return new Token(TokenType.OpAnd);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("customcontrol");
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
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("kickforcustom");
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
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("blockforcustom");
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

                // don't forget another newline!
                yield return token;
            } else {
                // return the original token
                yield return token;
            }
        }
    }
}
