using GDWeave;

namespace OfficerBallsBuffLib;

public class Mod : IMod {
    public Config Config;

    public Mod(IModInterface modInterface) {
        this.Config = modInterface.ReadConfig<Config>();
        modInterface.RegisterScriptMod(new BufflibPlayer());
        modInterface.RegisterScriptMod(new BufflibFXBox());
        modInterface.RegisterScriptMod(new BufflibPlayerHUD());
        modInterface.RegisterScriptMod(new BufflibPlayerData());
        modInterface.RegisterScriptMod(new BufflibMinigame());
        modInterface.RegisterScriptMod(new BuffBuddies());
        modInterface.Logger.Information("[officer balls] buff library engaged");
    }

    public void Dispose() {
        // Cleanup anything you do here
    }
}
