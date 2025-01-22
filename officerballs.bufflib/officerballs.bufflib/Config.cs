using System.Text.Json.Serialization;

namespace OfficerBallsBuffLib;

public class Config {
    [JsonInclude] public bool SomeSetting = true;
}
