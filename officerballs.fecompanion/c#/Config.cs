using System.Text.Json.Serialization;

namespace FeCompanion;

public class Config {
    [JsonInclude] public bool HideLetterNotifications = false;
}
