using GDWeave;

namespace RedChalk;

public class Mod : IMod {
    public Config Config;

    public Mod(IModInterface modInterface) {
        this.Config = modInterface.ReadConfig<Config>();
        modInterface.RegisterScriptMod(new RedChalkMod());
        modInterface.Logger.Information("[officer balls] red chalk engaged");
    }

    public void Dispose() {
        // Cleanup anything you do here
    }
}
