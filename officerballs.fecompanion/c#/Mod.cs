using GDWeave;

namespace FeCompanion;

public class Mod : IMod {
    public Config Config;

    public Mod(IModInterface modInterface) {
        this.Config = modInterface.ReadConfig<Config>();
        modInterface.RegisterScriptMod(new CompanionMod());
        modInterface.RegisterScriptMod(new DataMod());
        modInterface.Logger.Information("[officer balls] fish exchange companion deployed");
    }

    public void Dispose() {
        // Cleanup anything you do here
    }
}
