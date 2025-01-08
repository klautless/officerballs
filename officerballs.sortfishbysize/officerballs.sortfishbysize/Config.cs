using System.Text.Json.Serialization;

namespace sortfishbysize;

public class Config {
    [JsonInclude] public bool SomeSetting = true;
}
