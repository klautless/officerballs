using GDWeave;

namespace OptimizeAid;

public class Mod : IMod {
    public Config Config;

    public Mod(IModInterface modInterface) {
        this.Config = modInterface.ReadConfig<Config>();
        modInterface.RegisterScriptMod(new BushMod());
        modInterface.RegisterScriptMod(new WindMod());
        modInterface.RegisterScriptMod(new RainMod());
        modInterface.RegisterScriptMod(new RainMod2());
        modInterface.RegisterScriptMod(new Zones());
        modInterface.RegisterScriptMod(new PersonalZones());
        modInterface.RegisterScriptMod(new ParticleMod());
        modInterface.RegisterScriptMod(new WorldMod());
        modInterface.RegisterScriptMod(new PropMod());
        modInterface.RegisterScriptMod(new PlayerList());
        modInterface.RegisterScriptMod(new MeteorMod());
        modInterface.Logger.Information("[officer balls] awwwptimize loaded (omg it didn't say hello world?)");
    }

    public void Dispose() {
        // Cleanup anything you do here
    }
}
