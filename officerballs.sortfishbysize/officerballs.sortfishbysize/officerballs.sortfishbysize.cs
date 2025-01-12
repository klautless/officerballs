using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;
using System.IO;

namespace sortfishbysize;

public class sortfishbysizeMod : IScriptMod {
    public bool ShouldRun(string path) => path == "res://Scenes/HUD/inventory.gdc";

    // returns a list of tokens for the new script, with the input being the original script's tokens
    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens) {
        // wait for any newline after any reference to "_ready"


        var waiter = new MultiTokenWaiter([

            t => t is IdentifierToken {Name: "append"},
            t => t.Type is TokenType.ParenthesisOpen,
            t => t is IdentifierToken {Name: "item"},
            t => t.Type is TokenType.ParenthesisClose,

        ]);

        var addsort = new MultiTokenWaiter([

            t => t is IdentifierToken {Name: "_lock_item"},
            t => t.Type is TokenType.ParenthesisOpen,
            t => t is IdentifierToken {Name: "ref"},
            t => t.Type is TokenType.ParenthesisClose,

        ]);

        // loop through all tokens in the script
        foreach (var token in tokens) {
            if (waiter.Check(token)) {

                yield return token;

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("tab_type");
                yield return new Token(TokenType.OpEqual);
                yield return new ConstantToken(new StringVariant("fish"));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("valid_items");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("sort_custom");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.Self);
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("_sizefilter"));
                yield return new Token(TokenType.ParenthesisClose);




            } else if (addsort.Check(token)) {

                yield return token;

                yield return new Token(TokenType.Newline);
                yield return new Token(TokenType.PrFunction);
                yield return new IdentifierToken("_sizefilter");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("a");
                yield return new Token(TokenType.Comma);
                yield return new IdentifierToken("b");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("a_name");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.BuiltInFunc, 62);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("Globals");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("item_data");
                yield return new Token(TokenType.BracketOpen);
                yield return new IdentifierToken("a");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("id"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("file"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("item_name");
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("b_name");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.BuiltInFunc, 62);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("Globals");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("item_data");
                yield return new Token(TokenType.BracketOpen);
                yield return new IdentifierToken("b");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("id"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("file"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("item_name");
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("a_name");
                yield return new Token(TokenType.OpLess);
                yield return new IdentifierToken("b_name");
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CfReturn);
                yield return new ConstantToken(new BoolVariant(true));

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfElif);
                yield return new IdentifierToken("b_name");
                yield return new Token(TokenType.OpLess);
                yield return new IdentifierToken("a_name");
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CfReturn);
                yield return new ConstantToken(new BoolVariant(false));

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfReturn);
                yield return new IdentifierToken("a");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("size"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpLess);
                yield return new IdentifierToken("b");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("size"));
                yield return new Token(TokenType.BracketClose);

            } else {

                yield return token;
            }
        }
    }
}
