using GDWeave;

namespace BallsWorldPatch;

public class Mod : IMod {
    public Config Config;

    public Mod(IModInterface modInterface) {
        this.Config = modInterface.ReadConfig<Config>();
        modInterface.RegisterScriptMod(new WorldInstanceMod());
        modInterface.RegisterScriptMod(new ActorWipePatch());
        modInterface.RegisterScriptMod(new RipplePatch());
        modInterface.Logger.Information("Hello, world!");
    }

    public void Dispose() {
        // Cleanup anything you do here
    }
}
