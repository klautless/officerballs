using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;

namespace BallsWorldPatch;

public class ActorWipePatch : IScriptMod
{
    public bool ShouldRun(string path) => path == "res://Scenes/Entities/Player/player.gdc";

    // returns a list of tokens for the new script, with the input being the original script's tokens
    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens)
    {
        // wait for any newline after any reference to "_ready"
        var funcstop = new MultiTokenWaiter([
            t => t.Type is TokenType.PrFunction,
            t => t is IdentifierToken {Name: "_wipe_actor"},
            t => t.Type is TokenType.ParenthesisOpen,
            t => t is IdentifierToken {Name: "id"},
            t => t.Type is TokenType.ParenthesisClose,
            t => t.Type is TokenType.Colon,
            t => t.Type is TokenType.Newline,
        ]);

        var wipeadd = new MultiTokenWaiter([
            t => t is IdentifierToken {Name: "FALLBACK_ITEM"},
            t => t.Type is TokenType.Period,
            t => t is IdentifierToken {Name: "duplicate"},
            t => t.Type is TokenType.ParenthesisOpen,
            t => t.Type is TokenType.ParenthesisClose,
            t => t.Type is TokenType.ParenthesisClose,

        ]);

        var wipesub1 = new MultiTokenWaiter([
            t => t.Type is TokenType.CfIf,
            t => t is IdentifierToken {Name: "prop"},
            t => t.Type is TokenType.Period,
            t => t is IdentifierToken {Name: "ref"},
            t => t.Type is TokenType.OpEqual,
            t => t is IdentifierToken {Name: "ref"},
            t => t.Type is TokenType.Colon,
            t => t.Type is TokenType.Newline,
            t => t is IdentifierToken {Name: "_wipe_actor"},
            t => t.Type is TokenType.ParenthesisOpen,
            t => t is IdentifierToken {Name: "prop"},
            t => t.Type is TokenType.Period,
            t => t is IdentifierToken {Name: "id"},
            t => t.Type is TokenType.ParenthesisClose,

        ]);

        var wipesub2 = new MultiTokenWaiter([
            t => t.Type is TokenType.CfFor,
            t => t is IdentifierToken {Name: "prop"},
            t => t.Type is TokenType.OpIn,
            t => t is IdentifierToken {Name: "prop_ids"},
            t => t.Type is TokenType.Colon,
            t => t.Type is TokenType.Newline,
            t => t is IdentifierToken {Name: "_wipe_actor"},
            t => t.Type is TokenType.ParenthesisOpen,
            t => t is IdentifierToken {Name: "prop"},
            t => t.Type is TokenType.Period,
            t => t is IdentifierToken {Name: "id"},
            t => t.Type is TokenType.ParenthesisClose,


        ]);



        // loop through all tokens in the script
        foreach (var token in tokens)
        {
            if (funcstop.Check(token))
            {

                yield return token;

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfReturn);
                yield return new Token(TokenType.Newline, 1);

            }
            else if (wipeadd.Check(token)){

                yield return token;
                yield return new Token(TokenType.Newline);
                yield return new Token(TokenType.PrFunction);
                yield return new IdentifierToken("_wipe_alt");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("id");
                yield return new Token(TokenType.Comma);
                yield return new IdentifierToken("key");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(false));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.OpNot);
                yield return new IdentifierToken("key");
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CfReturn);
                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("actor");
                yield return new Token(TokenType.OpAssign);
                yield return new IdentifierToken("world");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_get_actor_by_id");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("id");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.BuiltInFunc, 89);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("actor");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 2);
                yield return new IdentifierToken("actor");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_deinstantiate");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new BoolVariant(true));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Newline);
            }
            else if (wipesub1.Check(token))
            {
                yield return token;
                yield return new Token(TokenType.Newline, 3);
                yield return new IdentifierToken("_wipe_alt");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("prop");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("id");
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new BoolVariant(true));
                yield return new Token(TokenType.ParenthesisClose);
            }
            else if (wipesub2.Check(token))
            {
                yield return token;
                yield return new Token(TokenType.Newline, 2);
                yield return new IdentifierToken("_wipe_alt");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("prop");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("id");
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new BoolVariant(true));
                yield return new Token(TokenType.ParenthesisClose);

            }
            else
            {
                // return the original token
                yield return token;
            }
        }
    }
}
