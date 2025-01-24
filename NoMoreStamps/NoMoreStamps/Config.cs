using System.Text.Json.Serialization;

namespace HideFakeCanvases;

public class Config {
    [JsonInclude] public bool LargePastesFilter = false;
    [JsonInclude] public bool CustomCanvasFilter = false;
    [JsonInclude] public bool GIFFilter = false;
    [JsonInclude] public bool HideUsersChalkForLarge = false;
    [JsonInclude] public bool HideUsersChalkForCustom = false;
    [JsonInclude] public bool HideUsersChalkForGIF = true;
    [JsonInclude] public bool HostKickForLarge = false;
    [JsonInclude] public bool HostKickForCustom = false;
    [JsonInclude] public bool HostKickForGIF = false;
    [JsonInclude] public int JoinDelayTime = 7;
}
