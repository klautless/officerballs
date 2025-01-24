using GDWeave;

namespace HideFakeCanvases;

public class Mod : IMod {
    public Config Config;

    public Mod(IModInterface modInterface) {
        this.Config = modInterface.ReadConfig<Config>();
        modInterface.RegisterScriptMod(new StampsBegone());
        modInterface.RegisterScriptMod(new StampsBegone2());
        modInterface.RegisterScriptMod(new StampsBegone3());
        modInterface.Logger.Information("[officer balls] NoMoreStamps initialized");
    }

    public void Dispose() {
        // Cleanup anything you do here
    }
}
