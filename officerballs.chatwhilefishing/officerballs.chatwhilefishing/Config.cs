using System.Text.Json.Serialization;

namespace OfficerballsChatFishing;

public class Config {
    [JsonInclude] public bool SomeSetting = true;
}
