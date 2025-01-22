using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;

namespace OfficerBallsBuffLib;

public class BufflibPlayerData : IScriptMod {
    public bool ShouldRun(string path) => path == "res://Scenes/Singletons/playerdata.gdc";

    // returns a list of tokens for the new script, with the input being the original script's tokens
    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens) {
       
        var buff_valuelift = new MultiTokenWaiter([
            t => t is IdentifierToken {Name: "worth"},
            t => t.Type is TokenType.OpAssign,
            t => t is ConstantToken {Value: IntVariant {Value: 0}},
        ]);

        // loop through all tokens in the script
        foreach (var token in tokens) {
            if (buff_valuelift.Check(token)) {

                yield return token;

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("plactor");

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfFor);
                yield return new IdentifierToken("flactor");
                yield return new Token(TokenType.OpIn);
                yield return new IdentifierToken("get_tree");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("get_nodes_in_group");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("controlled_player"));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("plactor");
                yield return new Token(TokenType.OpAssign);
                yield return new IdentifierToken("flactor");

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.BuiltInFunc, 89); //is_instance_valid
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("plactor");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("plactor");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("ob_buffs");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("buff_valuelift"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.OpGreater);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("worth");
                yield return new Token(TokenType.OpAssignMul);
                yield return new ConstantToken(new IntVariant(2));

            } else {

                yield return token;
            }
        }
    }
}
