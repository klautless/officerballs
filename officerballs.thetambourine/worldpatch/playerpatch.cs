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
            t => t is IdentifierToken {Name: "_get_actor_by_id"},
            t => t.Type is TokenType.ParenthesisOpen,
            t => t is IdentifierToken {Name: "id"},
            t => t.Type is TokenType.ParenthesisClose,
        ]);

        var wipeadd = new MultiTokenWaiter([
            t => t is IdentifierToken {Name: "FALLBACK_ITEM"},
            t => t.Type is TokenType.Period,
            t => t is IdentifierToken {Name: "duplicate"},
            t => t.Type is TokenType.ParenthesisOpen,
            t => t.Type is TokenType.ParenthesisClose,
            t => t.Type is TokenType.ParenthesisClose,

        ]);


        var declared = new MultiTokenWaiter([
            t => t.Type is TokenType.PrVar,
            t => t is IdentifierToken {Name: "camera_zoom"},
            t => t.Type is TokenType.OpAssign,
            t => t is ConstantToken {Value: RealVariant{ Value: 5.0}},
        ]);



        // loop through all tokens in the script
        foreach (var token in tokens)
        {
            if (funcstop.Check(token))
            {

                yield return token;

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("actor");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("actor_type");
                yield return new Token(TokenType.OpNotEqual);
                yield return new ConstantToken(new StringVariant("fish_spawn"));
                yield return new Token(TokenType.OpAnd);
                yield return new IdentifierToken("actor");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("actor_type");
                yield return new Token(TokenType.OpNotEqual);
                yield return new ConstantToken(new StringVariant("fish_spawn_alien"));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("_wipe_alt");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("id");
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new BoolVariant(true));
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfElse);
                yield return new Token(TokenType.Colon);
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

            } else if (declared.Check(token)) {

                yield return token;

                yield return new Token(TokenType.Newline);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("tamb_choice");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(0));

                yield return new Token(TokenType.Newline);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("meteortimer");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(-1));

                yield return new Token(TokenType.Newline);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("meteored");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(false));

                yield return new Token(TokenType.Newline);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("meteorPos");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.BuiltInType, 7);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("meteorZone");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant(""));

                yield return new Token(TokenType.Newline);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("meteorZoneOwner");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(-1));

                yield return new Token(TokenType.Newline);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("rippletimer");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(-1));

                yield return new Token(TokenType.Newline);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("hatDisable");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(true));

                yield return new Token(TokenType.Newline);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("rippleHat");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(true));

                yield return new Token(TokenType.Newline);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("lockHatPos");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(false));

                yield return new Token(TokenType.Newline);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("rippleHatPos");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.BuiltInType, 7);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("rippleHeightOffset");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new RealVariant(-0.32));

                yield return new Token(TokenType.Newline);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("rippleHatZone");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant(""));

                yield return new Token(TokenType.Newline);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("rippleHatZoneOwner");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(-1));

                yield return new Token(TokenType.Newline);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("dedilatch");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(false));

                yield return new Token(TokenType.Newline);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("internallatch");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(false));

            } else {
                // return the original token
                yield return token;
            }
        }
    }
}
