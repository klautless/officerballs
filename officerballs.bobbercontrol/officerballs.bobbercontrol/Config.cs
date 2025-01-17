using System.Text.Json.Serialization;

namespace BobberControl;

public class Config {
    [JsonInclude] public bool SomeSetting = true;
}
