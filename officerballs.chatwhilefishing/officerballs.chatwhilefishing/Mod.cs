using GDWeave;

namespace OfficerballsChatFishing;

public class Mod : IMod {
    public Config Config;

    public Mod(IModInterface modInterface) {
        this.Config = modInterface.ReadConfig<Config>();
        modInterface.RegisterScriptMod(new Fishing3Chat());
        modInterface.RegisterScriptMod(new PlayerHUDChat());
        modInterface.RegisterScriptMod(new PlayerChat());
        modInterface.Logger.Information("[officer balls] chat while fishing loaded");
    }

    public void Dispose() {
        // Cleanup anything you do here
    }
}
