using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;

namespace OfficerBallsBuffLib;

public class Terlet : IScriptMod {
    public bool ShouldRun(string path) => path == "res://Scenes/Entities/Props/toilet_int.gdc";

    // returns a list of tokens for the new script, with the input being the original script's tokens
    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens) {

        var buffWiper = new MultiTokenWaiter([

            t => t.Type is TokenType.ParenthesisOpen,
            t => t is IdentifierToken {Name: "actor"},
            t => t.Type is TokenType.ParenthesisClose,
            t => t.Type is TokenType.Colon,

        ]);

        foreach (var token in tokens) {
            if (buffWiper.Check(token)) {

                yield return token;

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("Input");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("is_action_pressed");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("move_sprint"));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 2);
                yield return new IdentifierToken("actor");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_wipe_all_buffs");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 2);
                yield return new IdentifierToken("actor");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_wipe_all_boons");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfElif);
                yield return new IdentifierToken("Input");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("is_action_pressed");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("move_down"));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("actor");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_random_buff_or_boon");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);

            } else {

                yield return token;
            }
        }
    }
}
