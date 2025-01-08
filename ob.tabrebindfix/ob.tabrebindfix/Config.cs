using System.Text.Json.Serialization;

namespace TabRebindFix;

public class Config {
    [JsonInclude] public bool SomeSetting = true;
}
