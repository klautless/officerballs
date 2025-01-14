using System.Text.Json.Serialization;

namespace OptimizeAid;

public class Config {
    [JsonInclude] public bool SomeSetting = true;
}
