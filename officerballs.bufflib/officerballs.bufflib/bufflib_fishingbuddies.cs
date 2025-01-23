using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;

namespace OfficerBallsBuffLib;

public class BuffBuddies : IScriptMod {
    public bool ShouldRun(string path) => path == "res://Scenes/Entities/Props/fish_trap.gdc";

    // returns a list of tokens for the new script, with the input being the original script's tokens
    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens) {

        var addavar = new MultiTokenWaiter([

            t => t.Type is TokenType.PrVar,
            t => t is IdentifierToken {Name: "progress"},
            t => t.Type is TokenType.OpAssign,
            t => t is ConstantToken {Value: IntVariant {Value: 0}},

        ]);

        var buff_speedbuddies = new MultiTokenWaiter([

            t => t.Type is TokenType.OpAssign,
            t => t is IdentifierToken {Name: "get_parent"},
            t => t.Type is TokenType.ParenthesisOpen,
            t => t.Type is TokenType.ParenthesisClose,
            t => t.Type is TokenType.Period,
            t => t is IdentifierToken {Name: "controlled"},

        ]);

        var buff_doublebuddies1 = new MultiTokenWaiter([

            t => t.Type is TokenType.Dollar,
            t => t is IdentifierToken {Name: "caught"},
            t => t.Type is TokenType.Period,
            t => t is IdentifierToken {Name: "visible"},
            t => t.Type is TokenType.OpAssign,
            t => t is ConstantToken {Value: BoolVariant {Value: true}},
            t => t.Type is TokenType.Newline,

        ]);

        var buff_doublebuddies2 = new MultiTokenWaiter([

            t => t is IdentifierToken {Name: "data"},
            t => t.Type is TokenType.OpAssign,
            t => t is IdentifierToken {Name: "Globals"},
            t => t.Type is TokenType.Period,
            t => t is IdentifierToken {Name: "item_data"},
            t => t.Type is TokenType.BracketOpen,
            t => t is IdentifierToken {Name: "fish_roll"},
            t => t.Type is TokenType.BracketClose,
            t => t.Type is TokenType.BracketOpen,
            t => t is ConstantToken {Value: StringVariant {Value: "file"}},
            t => t.Type is TokenType.BracketClose,

        ]);

        // loop through all tokens in the script
        foreach (var token in tokens) {
            if (buff_speedbuddies.Check(token)) {

                yield return token;

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("get_parent");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("ob_buffs");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("buff_speedbuddies"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpGreater);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.CfMatch);
                yield return new IdentifierToken("get_parent");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("ob_buffs_tier");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("buff_speedbuddies"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Dollar);
                yield return new IdentifierToken("interactable");
                yield return new Token(TokenType.OpDiv);
                yield return new IdentifierToken("Timer");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("wait_time");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new RealVariant(0.9));
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(2));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Dollar);
                yield return new IdentifierToken("interactable");
                yield return new Token(TokenType.OpDiv);
                yield return new IdentifierToken("Timer");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("wait_time");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new RealVariant(0.8));
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(3));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Dollar);
                yield return new IdentifierToken("interactable");
                yield return new Token(TokenType.OpDiv);
                yield return new IdentifierToken("Timer");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("wait_time");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new RealVariant(0.7));
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(4));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Dollar);
                yield return new IdentifierToken("interactable");
                yield return new Token(TokenType.OpDiv);
                yield return new IdentifierToken("Timer");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("wait_time");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new RealVariant(0.6));
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(5));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Dollar);
                yield return new IdentifierToken("interactable");
                yield return new Token(TokenType.OpDiv);
                yield return new IdentifierToken("Timer");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("wait_time");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new RealVariant(0.5));
                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfElif);
                yield return new Token(TokenType.Dollar);
                yield return new IdentifierToken("interactable");
                yield return new Token(TokenType.OpDiv);
                yield return new IdentifierToken("Timer");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("wait_time");
                yield return new Token(TokenType.OpNotEqual);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Dollar);
                yield return new IdentifierToken("interactable");
                yield return new Token(TokenType.OpDiv);
                yield return new IdentifierToken("Timer");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("wait_time");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(1));

            } else if (addavar.Check(token)) {

                yield return token;

                yield return new Token(TokenType.Newline);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("dublin");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(false));

            } else if (buff_doublebuddies1.Check(token)) {
                
                yield return token;

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("get_parent");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("ob_buffs");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("buff_doublebuddies"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpGreater);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.CfMatch);
                yield return new IdentifierToken("get_parent");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("ob_buffs_tier");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("buff_doublebuddies"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.BuiltInFunc, 39);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpLess);
                yield return new ConstantToken(new RealVariant(0.05));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 4);
                yield return new IdentifierToken("dublin");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(true));
                yield return new Token(TokenType.Newline, 4);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_send_notification");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("one of your fishing buddies caught doubles!"));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(2));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.BuiltInFunc, 39);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpLess);
                yield return new ConstantToken(new RealVariant(0.1));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 4);
                yield return new IdentifierToken("dublin");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(true));
                yield return new Token(TokenType.Newline, 4);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_send_notification");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("one of your fishing buddies caught doubles!"));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(3));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.BuiltInFunc, 39);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpLess);
                yield return new ConstantToken(new RealVariant(0.15));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 4);
                yield return new IdentifierToken("dublin");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(true));
                yield return new Token(TokenType.Newline, 4);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_send_notification");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("one of your fishing buddies caught doubles!"));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(4));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.BuiltInFunc, 39);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpLess);
                yield return new ConstantToken(new RealVariant(0.2));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 4);
                yield return new IdentifierToken("dublin");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(true));
                yield return new Token(TokenType.Newline, 4);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_send_notification");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("one of your fishing buddies caught doubles!"));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Newline, 3);
                yield return new ConstantToken(new IntVariant(5));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.BuiltInFunc, 39);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpLess);
                yield return new ConstantToken(new RealVariant(0.25));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 4);
                yield return new IdentifierToken("dublin");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(true));
                yield return new Token(TokenType.Newline, 4);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_send_notification");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("one of your fishing buddies caught doubles!"));
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 1);

            } else if (buff_doublebuddies2.Check(token)) {

                yield return token;

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("dublin");
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 2);
                yield return new IdentifierToken("dublin");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(false));

                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("doublefish");
                yield return new Token(TokenType.OpAssign);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_add_item");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("fish_roll");
                yield return new Token(TokenType.Comma);
                yield return new Token(TokenType.OpSub);
                yield return new ConstantToken(new IntVariant(1));
                yield return new Token(TokenType.Comma);
                yield return new IdentifierToken("size");
                yield return new Token(TokenType.Comma);
                yield return new IdentifierToken("quality");
                yield return new Token(TokenType.Comma);
                yield return new Token(TokenType.BracketOpen);
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.ParenthesisClose);


            } else {

                yield return token;
            }
        }
    }
}
