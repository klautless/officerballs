using System.Text.Json.Serialization;

namespace HideFakeCanvases;

public class Config {
    [JsonInclude] public bool SomeSetting = true;
}
