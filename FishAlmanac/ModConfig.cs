using StardewModdingAPI;
using StardewModdingAPI.Utilities;

namespace FishAlmanac
{
    public class ModConfig
    {
        //==============================================================================
        public KeybindList ShowUi { get; set; } = KeybindList.Parse("F2");

        //==============================================================================
        public static void Register(IGenericModConfigMenuApi menu, IManifest manifest, IModHelper helper,
            ModConfig config)
        {
            menu.Register(mod: manifest, reset: () => config = new ModConfig(), save: () => helper.WriteConfig(config));
            menu.AddKeybindList(mod: manifest, name: () => "Show UI", getValue: () => config.ShowUi,
                setValue: value => config.ShowUi = value);
        }
    }
}