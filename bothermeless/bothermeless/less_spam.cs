using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;

namespace BotherMeLess;

public class PlayerMod : IScriptMod {
    public bool ShouldRun(string path) => path == "res://Scenes/Entities/Player/player.gdc";

    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens) {

        var baitwarn = new MultiTokenWaiter([
            t => t is ConstantToken {Value: StringVariant {Value: "[color=#ac0029]Seems nothing is going to bite... perhaps your bait isn't for this water...[/color]"}},
            t => t.Type is TokenType.Newline,
            t => t.Type is TokenType.Newline,
        ]);

        var chalkwarn = new MultiTokenWaiter([
            t => t is IdentifierToken {Name: "in_zone"},
            t => t.Type is TokenType.OpAssign,
            t => t is ConstantToken {Value: BoolVariant {Value: true}},
            t => t.Type is TokenType.Newline,
            t => t.Type is TokenType.CfIf,
            t => t.Type is TokenType.OpNot,
            t => t is IdentifierToken {Name: "in_zone"},
        ]);

        foreach (var token in tokens) {
            if (baitwarn.Check(token)) {

                yield return token;
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_send_notification");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("no bait attached!"));
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.CfIf);
                yield return new ConstantToken(new BoolVariant(false));
                yield return new Token(TokenType.Colon);

            } else if (chalkwarn.Check(token)) {

                yield return token;
                yield return new Token(TokenType.OpAnd);
                yield return new ConstantToken(new BoolVariant(false));

            } else {
                yield return token;
            }
        }
    }
}
public class PlayerDataMod : IScriptMod
{
    public bool ShouldRun(string path) => path == "res://Scenes/Singletons/playerdata.gdc";

    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens)
    {

        var badgepop = new MultiTokenWaiter([
           
            t => t.Type is TokenType.ParenthesisOpen,
            t => t is ConstantToken {Value: RealVariant {Value: 0.5}},
            t => t.Type is TokenType.ParenthesisClose,
            t => t.Type is TokenType.Comma,
            t => t is ConstantToken {Value: StringVariant {Value: "timeout"}},
            t => t.Type is TokenType.ParenthesisClose,
            t => t.Type is TokenType.Newline,
        ]);
        foreach (var token in tokens)
        {
            if (badgepop.Check(token))
            {

                yield return token;
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("badge_level");
                yield return new Token(TokenType.OpLess);
                yield return new IdentifierToken("BADGE_LEVEL_DATA");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("size");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Colon);


            } else {
                yield return token;
            }
        }
    }
}