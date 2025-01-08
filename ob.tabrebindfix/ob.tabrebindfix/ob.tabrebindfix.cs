using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;

namespace TabRebindFix;

public class TabRebindFixMod : IScriptMod {
    public bool ShouldRun(string path) => path == "res://Scenes/HUD/playerhud.gdc";

    // returns a list of tokens for the new script, with the input being the original script's tokens
    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens) {
        // wait for any newline after any reference to "_ready"

        var prewaiter = new MultiTokenWaiter([

            t => t is ConstantToken {Value: StringVariant {Value:"menu_open"}},
            t => t.Type is TokenType.ParenthesisClose,

        ]);

        // loop through all tokens in the script
        foreach (var token in tokens) {
            if (prewaiter.Check(token)) {

                yield return token;
                yield return new Token(TokenType.OpAnd);
                yield return new Token(TokenType.OpNot);
                yield return new IdentifierToken("using_chat");

            } else {
                // return the original token
                yield return token;
            }
        }
    }
}
