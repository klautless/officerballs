using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;

namespace AutoAllTags;

public class AutoTagMod : IScriptMod {
    public bool ShouldRun(string path) => path == "res://Scenes/Menus/Main Menu/main_menu.gdc";

    // returns a list of tokens for the new script, with the input being the original script's tokens
    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens) {
        // wait for any newline after any reference to "_ready"
        var waiter = new MultiTokenWaiter([
            t => t is IdentifierToken {Name: "lobby_cap"},
            t => t.Type is TokenType.Colon,
            t => t.Type is TokenType.CfContinue,
        ]);

        var waiter2 = new MultiTokenWaiter([
            t => t is IdentifierToken {Name: "tags_to_filter"},
            t => t.Type is TokenType.Period,
            t => t is IdentifierToken {Name: "append"},
            t => t.Type is TokenType.ParenthesisOpen,
            t => t is IdentifierToken {Name: "child"},
            t => t.Type is TokenType.Period,
            t => t is IdentifierToken {Name: "lobby_tag"},
            t => t.Type is TokenType.ParenthesisClose,


        ]);

        var waiter3 = new MultiTokenWaiter([

            t => t is IdentifierToken {Name: "_close_create_screen"},
            t => t.Type is TokenType.ParenthesisOpen,
            t => t.Type is TokenType.ParenthesisClose,
            t => t.Type is TokenType.Newline,
            t => t is IdentifierToken {Name: "_close_tag_menu"},
            t => t.Type is TokenType.ParenthesisOpen,
            t => t.Type is TokenType.ParenthesisClose,

        ]);

        // loop through all tokens in the script
        foreach (var token in tokens) {
            if (waiter.Check(token)) {

                yield return token;
                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("lobby_real_members");
                yield return new Token(TokenType.OpGreaterEqual);
                yield return new IdentifierToken("lobby_cap");
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CfContinue);


            }
            else if (waiter2.Check(token))
            {

                yield return token;
                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.CfElse);
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("tags_to_filter");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("append");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("child");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("lobby_tag");
                yield return new Token(TokenType.ParenthesisClose);

            }
            else if(waiter3.Check(token))
            {
                yield return token;
                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.Dollar);
                yield return new ConstantToken(new StringVariant("%hide_full"));
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("pressed");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(true));
                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfFor);
                yield return new IdentifierToken("child");
                yield return new Token(TokenType.OpIn);
                yield return new Token(TokenType.Dollar);
                yield return new ConstantToken(new StringVariant("%tag_filter_search"));
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("get_children");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 2);
                yield return new IdentifierToken("child");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("pressed");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(true));

            }
            else
            {
                // return the original token
                yield return token;
            }
        }
    }
}
