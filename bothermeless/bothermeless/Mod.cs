using GDWeave;

namespace BotherMeLess;

public class Mod : IMod {
    public Config Config;

    public Mod(IModInterface modInterface) {
        this.Config = modInterface.ReadConfig<Config>();
        modInterface.RegisterScriptMod(new PlayerMod());
        modInterface.RegisterScriptMod(new PlayerDataMod());
        modInterface.Logger.Information("[officer balls] less bothering incoming");
    }

    public void Dispose() {
        // Cleanup anything you do here
    }
}
