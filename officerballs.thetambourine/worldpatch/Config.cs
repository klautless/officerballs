using System.Text.Json.Serialization;

namespace BallsWorldPatch;

public class Config {
    [JsonInclude] public bool SomeSetting = true;
}
