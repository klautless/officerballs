using System.Text.Json.Serialization;

namespace OfficerBallsBuffLib;

public class Config {
    [JsonInclude] public bool RandomBuffsFromFishing = false;
    [JsonInclude] public bool RandomBuffsFromBuddies = false;
    [JsonInclude] public bool RandomBuffsFromToilets = false;
}
