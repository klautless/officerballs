using System.Text.Json.Serialization;

namespace Swerve;

public class Config {
    [JsonInclude] public bool SomeSetting = true;
}
