using System.Text.Json.Serialization;

namespace ItemShorts;

public class Config {
    [JsonInclude] public bool SomeSetting = true;
}
