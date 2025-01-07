using System.Text.Json.Serialization;

namespace AutoAllTags;

public class Config {
    [JsonInclude] public bool SomeSetting = true;
}
