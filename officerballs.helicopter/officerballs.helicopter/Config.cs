using System.Text.Json.Serialization;

namespace Helicopter;

public class Config {
    [JsonInclude] public bool SomeSetting = true;
}
