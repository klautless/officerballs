using System.Text.Json.Serialization;

namespace BotherMeLess;

public class Config {
    [JsonInclude] public bool ThisDoesNothing = true;
}
