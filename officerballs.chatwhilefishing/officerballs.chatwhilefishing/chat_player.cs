using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;

namespace OfficerballsChatFishing;

public class PlayerChat : IScriptMod {
    public bool ShouldRun(string path) => path == "res://Scenes/Entities/Player/player.gdc";

    // returns a list of tokens for the new script, with the input being the original script's tokens
    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens) {
        // wait for any newline after any reference to "_ready"
        var replaceFishingFinish = new MultiTokenWaiter([
            t => t is IdentifierToken {Name: "PlayerData"},
            t => t.Type is TokenType.Period,
            t => t is IdentifierToken {Name: "_catch_fish"},
            t => t.Type is TokenType.ParenthesisOpen,
            t => t.Type is TokenType.ParenthesisClose,
            t => t.Type is TokenType.Newline,
            t => t.Type is TokenType.Newline,
        ]);

        var declare = new MultiTokenWaiter([

            t => t.Type is TokenType.PrVar,
            t => t is IdentifierToken {Name: "bonus_text"},
            t => t.Type is TokenType.OpAssign,
            t => t.Type is TokenType.BracketOpen,
            t => t.Type is TokenType.BracketClose,

        ]);

        var bonus1 = new MultiTokenWaiter([

            t => t is ConstantToken {Value: StringVariant {Value: "Your Double Hook doubled the fish!"}},
            t => t.Type is TokenType.ParenthesisClose,

        ]);

        var bonus2 = new MultiTokenWaiter([

            t => t is ConstantToken {Value: StringVariant {Value: "How lucky! You found a bonus [color=#d57900]Coin Bag[/color] aswell!"}},
            t => t.Type is TokenType.ParenthesisClose,

        ]);

        // loop through all tokens in the script
        foreach (var token in tokens) {
            if (replaceFishingFinish.Check(token)) {

                yield return token;
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("size_prefix");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.CurlyBracketOpen);
                yield return new ConstantToken(new RealVariant(0.1));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new StringVariant("Microscopic "));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new RealVariant(0.25));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new StringVariant("Tiny "));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new RealVariant(0.5));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new StringVariant("Small "));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new RealVariant(1.0));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new StringVariant(""));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new RealVariant(1.5));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new StringVariant("Large "));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new RealVariant(1.75));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new StringVariant("Huge "));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new RealVariant(2.25));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new StringVariant("Massive "));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new RealVariant(2.75));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new StringVariant("Gigantic "));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new RealVariant(3.25));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new StringVariant("Colossal "));
                yield return new Token(TokenType.CurlyBracketClose);

                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("st_data");
                yield return new Token(TokenType.OpAssign);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_find_item_code");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("ref");
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("st_avg");
                yield return new Token(TokenType.OpAssign);
                yield return new IdentifierToken("Globals");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("item_data");
                yield return new Token(TokenType.BracketOpen);
                yield return new IdentifierToken("st_data");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("id"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("file"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("average_size");

                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("st_calc");
                yield return new Token(TokenType.OpAssign);
                yield return new IdentifierToken("size");
                yield return new Token(TokenType.OpDiv);
                yield return new IdentifierToken("st_avg");

                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("st_quality");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant(""));

                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("st_prefix");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant(""));

                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.CfFor);
                yield return new IdentifierToken("p");
                yield return new Token(TokenType.OpIn);
                yield return new IdentifierToken("size_prefix");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("keys");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 3);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("p");
                yield return new Token(TokenType.OpGreater);
                yield return new IdentifierToken("st_calc");
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CfBreak);

                yield return new Token(TokenType.Newline, 3);
                yield return new IdentifierToken("st_prefix");
                yield return new Token(TokenType.OpAssign);
                yield return new IdentifierToken("size_prefix");
                yield return new Token(TokenType.BracketOpen);
                yield return new IdentifierToken("p");
                yield return new Token(TokenType.BracketClose);

                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("st_name");
                yield return new Token(TokenType.OpAssign);
                yield return new IdentifierToken("Globals");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("item_data");
                yield return new Token(TokenType.BracketOpen);
                yield return new IdentifierToken("st_data");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("id"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("file"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("item_name");

                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("st_blurb");
                yield return new Token(TokenType.OpAssign);
                yield return new IdentifierToken("Globals");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("item_data");
                yield return new Token(TokenType.BracketOpen);
                yield return new IdentifierToken("st_data");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("id"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("file"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("catch_blurb");

                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("QUALITY_DATA");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("has");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("st_data");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("quality"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("st_quality");
                yield return new Token(TokenType.OpAssign);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("QUALITY_DATA");
                yield return new Token(TokenType.BracketOpen);
                yield return new IdentifierToken("st_data");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("quality"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("name");

                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("st_bonus");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant(""));

                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("st_bonus_text");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("size");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpGreater);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CfFor);
                yield return new IdentifierToken("ttt");
                yield return new Token(TokenType.OpIn);
                yield return new IdentifierToken("st_bonus_text");
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("st_bonus");
                yield return new Token(TokenType.OpAssign);
                yield return new IdentifierToken("st_bonus");
                yield return new Token(TokenType.OpAdd);
                yield return new ConstantToken(new StringVariant("\n"));
                yield return new Token(TokenType.OpAdd);
                yield return new Token(TokenType.BuiltInFunc, 62);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("ttt");
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 2);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_send_notification");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("You caught a "));
                yield return new Token(TokenType.OpAdd);
                yield return new Token(TokenType.BuiltInFunc, 62);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("st_quality");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpAdd);
                yield return new Token(TokenType.BuiltInFunc, 62);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("st_prefix");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpAdd);
                yield return new Token(TokenType.BuiltInFunc, 62);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("st_name");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpAdd);
                yield return new ConstantToken(new StringVariant("! (It's "));
                yield return new Token(TokenType.OpAdd);
                yield return new Token(TokenType.BuiltInFunc, 62); //str
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("size");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpAdd);
                yield return new ConstantToken(new StringVariant("cm!)\n\n"));
                yield return new Token(TokenType.OpAdd);
                yield return new Token(TokenType.BuiltInFunc, 62); //str
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("st_blurb");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpAdd);
                yield return new Token(TokenType.BuiltInFunc, 62); //str
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("st_bonus");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.CfIf);
                yield return new ConstantToken(new BoolVariant(false));
                yield return new Token(TokenType.Colon);



            } else if (declare.Check(token)) {

                yield return token;

                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("st_bonus_text");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.BracketOpen);
                yield return new Token(TokenType.BracketClose);

            } else if (bonus1.Check(token)) {

                yield return token;

                yield return new Token(TokenType.Newline, 3);
                yield return new IdentifierToken("st_bonus_text");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("append");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("Your Double Hook doubled the fish!"));
                yield return new Token(TokenType.ParenthesisClose);

            } else if (bonus2.Check(token)) {

                yield return token;

                yield return new Token(TokenType.Newline, 3);
                yield return new IdentifierToken("st_bonus_text");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("append");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("How lucky! You found a bonus Coin Bag as well!"));
                yield return new Token(TokenType.ParenthesisClose);

            }
            else {
                yield return token;
            }
        }
    }
}
