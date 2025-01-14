using GDWeave;

namespace OptimizeAid;

public class Mod : IMod {
    public Config Config;

    public Mod(IModInterface modInterface) {
        this.Config = modInterface.ReadConfig<Config>();
        modInterface.RegisterScriptMod(new BushMod());
        modInterface.RegisterScriptMod(new WindMod());
        modInterface.RegisterScriptMod(new MushroomMod());
        modInterface.RegisterScriptMod(new RainMod());
        modInterface.RegisterScriptMod(new RainMod2());
        modInterface.RegisterScriptMod(new Zones());
        modInterface.RegisterScriptMod(new ParticleMod());
        modInterface.Logger.Information("Hello, world!");
    }

    public void Dispose() {
        // Cleanup anything you do here
    }
}
