using System.Text.Json.Serialization;

namespace Nyan;

public class Config {
    [JsonInclude] public bool SomeSetting = true;
}
