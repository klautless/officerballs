using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;
using System.Xml.Linq;

namespace OptimizeAid;

public class PlayerList : IScriptMod
{
    public bool ShouldRun(string path) => path == "res://Scenes/HUD/Playerlist/playerlist.gdc";

    // returns a list of tokens for the new script, with the input being the original script's tokens
    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens)
    {

        var hook0a = new MultiTokenWaiter([
            t => t is IdentifierToken {Name: "_refresh_list"},
            t => t.Type is TokenType.ParenthesisOpen,
            t => t.Type is TokenType.ParenthesisClose,
            t => t.Type is TokenType.Colon,
            t => t.Type is TokenType.Newline,
        ]);

        var hook0b = new MultiTokenWaiter([

            t => t is IdentifierToken {Name: "playerlist"},
            t => t.Type is TokenType.Period,
            t => t is IdentifierToken {Name: "get_children"},
            t => t.Type is TokenType.ParenthesisOpen,
            t => t.Type is TokenType.ParenthesisClose,
            t => t.Type is TokenType.Colon,
            t => t is IdentifierToken {Name: "child"},
            t => t.Type is TokenType.Period,
            t => t is IdentifierToken {Name: "queue_free"},
            t => t.Type is TokenType.ParenthesisOpen,
            t => t.Type is TokenType.ParenthesisClose,
            t => t.Type is TokenType.Newline,

        ]);

        var hook0c = new MultiTokenWaiter([

            t => t is IdentifierToken {Name: "player_data"},
            t => t.Type is TokenType.Comma,
            t => t is ConstantToken {Value: IntVariant {Value: 0}},
            t => t.Type is TokenType.ParenthesisClose,


        ]);

        var hook1a = new MultiTokenWaiter([
            t => t is IdentifierToken {Name: "WEB_LOBBY_MEMBERS"},
            t => t.Type is TokenType.Period,
            t => t is IdentifierToken {Name: "size"},
            t => t.Type is TokenType.ParenthesisOpen,
            t => t.Type is TokenType.ParenthesisClose,
            t => t.Type is TokenType.OpLessEqual,
            t => t is ConstantToken {Value: IntVariant {Value: 0}},
            t => t.Type is TokenType.Newline,
            t => t.Type is TokenType.Newline,
        ]);

        var hook1b = new MultiTokenWaiter([

            t => t is IdentifierToken {Name: "requestlist"},
            t => t.Type is TokenType.Period,
            t => t is IdentifierToken {Name: "get_children"},
            t => t.Type is TokenType.ParenthesisOpen,
            t => t.Type is TokenType.ParenthesisClose,
            t => t.Type is TokenType.Colon,
            t => t is IdentifierToken {Name: "child"},
            t => t.Type is TokenType.Period,
            t => t is IdentifierToken {Name: "queue_free"},
            t => t.Type is TokenType.ParenthesisOpen,
            t => t.Type is TokenType.ParenthesisClose,
            t => t.Type is TokenType.Newline,

        ]);

        var hook1c = new MultiTokenWaiter([

            t => t is IdentifierToken {Name: "player_data"},
            t => t.Type is TokenType.Comma,
            t => t is ConstantToken {Value: IntVariant {Value: 1}},
            t => t.Type is TokenType.ParenthesisClose,


        ]);

        var hook2a = new MultiTokenWaiter([
            t => t is IdentifierToken {Name: "WEB_LOBBY_JOIN_QUEUE"},
            t => t.Type is TokenType.Period,
            t => t is IdentifierToken {Name: "size"},
            t => t.Type is TokenType.ParenthesisOpen,
            t => t.Type is TokenType.ParenthesisClose,
            t => t.Type is TokenType.OpLessEqual,
            t => t is ConstantToken {Value: IntVariant {Value: 0}},
            t => t.Type is TokenType.Newline,
            t => t.Type is TokenType.Newline,
        ]);

        var hook2b = new MultiTokenWaiter([

            t => t is IdentifierToken {Name: "banlist"},
            t => t.Type is TokenType.Period,
            t => t is IdentifierToken {Name: "get_children"},
            t => t.Type is TokenType.ParenthesisOpen,
            t => t.Type is TokenType.ParenthesisClose,
            t => t.Type is TokenType.Colon,
            t => t is IdentifierToken {Name: "child"},
            t => t.Type is TokenType.Period,
            t => t is IdentifierToken {Name: "queue_free"},
            t => t.Type is TokenType.ParenthesisOpen,
            t => t.Type is TokenType.ParenthesisClose,
            t => t.Type is TokenType.Newline,

        ]);

        var hook2c = new MultiTokenWaiter([

            t => t is IdentifierToken {Name: "player_data"},
            t => t.Type is TokenType.Comma,
            t => t is ConstantToken {Value: IntVariant {Value: 2}},
            t => t.Type is TokenType.ParenthesisClose,


        ]);

        var hook3a = new MultiTokenWaiter([
            t => t is IdentifierToken {Name: "WEB_LOBBY_REJECTS"},
            t => t.Type is TokenType.Period,
            t => t is IdentifierToken {Name: "size"},
            t => t.Type is TokenType.ParenthesisOpen,
            t => t.Type is TokenType.ParenthesisClose,
            t => t.Type is TokenType.OpLessEqual,
            t => t is ConstantToken {Value: IntVariant {Value: 0}},
            t => t.Type is TokenType.Newline,
            t => t.Type is TokenType.Newline,
        ]);

        var hook3b = new MultiTokenWaiter([

            t => t is IdentifierToken {Name: "blocklist"},
            t => t.Type is TokenType.Period,
            t => t is IdentifierToken {Name: "get_children"},
            t => t.Type is TokenType.ParenthesisOpen,
            t => t.Type is TokenType.ParenthesisClose,
            t => t.Type is TokenType.Colon,
            t => t is IdentifierToken {Name: "child"},
            t => t.Type is TokenType.Period,
            t => t is IdentifierToken {Name: "queue_free"},
            t => t.Type is TokenType.ParenthesisOpen,
            t => t.Type is TokenType.ParenthesisClose,
            t => t.Type is TokenType.Newline,

        ]);

        var hook3c = new MultiTokenWaiter([

            t => t is IdentifierToken {Name: "player_data"},
            t => t.Type is TokenType.Comma,
            t => t is ConstantToken {Value: IntVariant {Value: 3}},
            t => t.Type is TokenType.ParenthesisClose,


        ]);

        var firstlatch = new MultiTokenWaiter([

            t => t is IdentifierToken {Name: "disabled"},
            t => t.Type is TokenType.OpAssign,
            t => t is ConstantToken {Value: BoolVariant {Value: true}},


        ]);

        var variablation = new MultiTokenWaiter([

            t => t is IdentifierToken {Name: "ban_count_label"},

        ]);

        // loop through all tokens in the script
        foreach (var token in tokens)
        {
            if (firstlatch.Check(token))
            {

                yield return token;

                yield return new Token(TokenType.Newline, 1);
                yield return new IdentifierToken("first_pass");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(true));

            } else if(hook0a.Check(token)) {

                yield return token;
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("in_playerlist");
                yield return new Token(TokenType.OpNotEqual);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("WEB_LOBBY_MEMBERS");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("size");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpOr);
                yield return new Token(TokenType.OpNot);
                yield return new IdentifierToken("first_pass");
                yield return new Token(TokenType.Colon);

            } else if (hook0b.Check(token)) {

                yield return token;
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("in_playerlist");
                yield return new Token(TokenType.OpNotEqual);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("WEB_LOBBY_MEMBERS");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("size");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpOr);
                yield return new Token(TokenType.OpNot);
                yield return new IdentifierToken("first_pass");
                yield return new Token(TokenType.Colon);

            } else if (hook0c.Check(token)) {

                yield return token;

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("in_playerlist");
                yield return new Token(TokenType.OpNotEqual);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("WEB_LOBBY_MEMBERS");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("size");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("in_playerlist");
                yield return new Token(TokenType.OpAssign);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("WEB_LOBBY_MEMBERS");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("size");

            } else if (hook1a.Check(token)) {

                yield return token;
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("in_requestlist");
                yield return new Token(TokenType.OpNotEqual);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("WEB_LOBBY_JOIN_QUEUE");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("size");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpOr);
                yield return new Token(TokenType.OpNot);
                yield return new IdentifierToken("first_pass");
                yield return new Token(TokenType.Colon);

            } else if (hook1b.Check(token)) {

                yield return token;
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("in_requestlist");
                yield return new Token(TokenType.OpNotEqual);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("WEB_LOBBY_JOIN_QUEUE");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("size");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpOr);
                yield return new Token(TokenType.OpNot);
                yield return new IdentifierToken("first_pass");
                yield return new Token(TokenType.Colon);

            } else if (hook1c.Check(token)) {

                yield return token;

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("in_requestlist");
                yield return new Token(TokenType.OpNotEqual);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("WEB_LOBBY_JOIN_QUEUE");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("size");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("in_requestlist");
                yield return new Token(TokenType.OpAssign);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("WEB_LOBBY_JOIN_QUEUE");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("size");

            } else if (hook2a.Check(token)) {

                yield return token;
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("in_banlist");
                yield return new Token(TokenType.OpNotEqual);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("WEB_LOBBY_REJECTS");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("size");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpOr);
                yield return new Token(TokenType.OpNot);
                yield return new IdentifierToken("first_pass");
                yield return new Token(TokenType.Colon);

            } else if (hook2b.Check(token)) {

                yield return token;
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("in_banlist");
                yield return new Token(TokenType.OpNotEqual);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("WEB_LOBBY_REJECTS");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("size");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpOr);
                yield return new Token(TokenType.OpNot);
                yield return new IdentifierToken("first_pass");
                yield return new Token(TokenType.Colon);

            } else if (hook2c.Check(token)) {

                yield return token;

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("in_banlist");
                yield return new Token(TokenType.OpNotEqual);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("WEB_LOBBY_REJECTS");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("size");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("in_banlist");
                yield return new Token(TokenType.OpAssign);
                yield return new IdentifierToken("Network");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("WEB_LOBBY_REJECTS");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("size");

            } else if (hook3a.Check(token)) {

                yield return token;
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("in_blocklist");
                yield return new Token(TokenType.OpNotEqual);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("players_hidden");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("size");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpOr);
                yield return new Token(TokenType.OpNot);
                yield return new IdentifierToken("first_pass");
                yield return new Token(TokenType.Colon);

            } else if (hook3b.Check(token)) {

                yield return token;
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("in_blocklist");
                yield return new Token(TokenType.OpNotEqual);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("players_hidden");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("size");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpOr);
                yield return new Token(TokenType.OpNot);
                yield return new IdentifierToken("first_pass");
                yield return new Token(TokenType.Colon);


            } else if (hook3c.Check(token)) {

                yield return token;

                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("in_blocklist");
                yield return new Token(TokenType.OpNotEqual);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("players_hidden");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("size");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("in_blocklist");
                yield return new Token(TokenType.OpAssign);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("players_hidden");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("size");

            } else if (variablation.Check(token)) {

                yield return token;

                yield return new Token(TokenType.Newline);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("in_playerlist");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(0));

                yield return new Token(TokenType.Newline);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("in_requestlist");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(0));

                yield return new Token(TokenType.Newline);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("in_banlist");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(0));

                yield return new Token(TokenType.Newline);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("in_blocklist");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(0));

                yield return new Token(TokenType.Newline);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("first_pass");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(false));

            }
            else
            {
                // return the original token
                yield return token;
            }
        }
    }
}
