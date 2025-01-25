using System.Text.Json.Serialization;

namespace RedChalk;

public class Config {
    [JsonInclude] public bool SettingDoesNothing = true;
}
