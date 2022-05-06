using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using xTile.ObjectModel;

namespace FishAlmanac
{
    public class ModEntry : Mod
    {
        //==============================================================================
        private const string ConfigModId = "spacechase0.GenericModConfigMenu";

        //==============================================================================
        private ModConfig Config { get; set; }

        //==============================================================================
        public override void Entry(IModHelper helper)
        {
            Config = Helper.ReadConfig<ModConfig>();
            helper.Events.GameLoop.GameLaunched += OnGameLaunched;
            helper.Events.Input.ButtonsChanged += OnButtonsChanged;
        }

        //==============================================================================
        private void OnGameLaunched(object sender, GameLaunchedEventArgs args)
        {
            var menu = Helper.ModRegistry.GetApi<IGenericModConfigMenuApi>(ConfigModId);
            if (menu == null)
            {
                return;
            }

            ModConfig.Register(menu, ModManifest, Helper, Config);
        }

        //==============================================================================
        private void OnButtonsChanged(object sender, ButtonsChangedEventArgs args)
        {
            if (Config.ShowUi.JustPressed())
            {
                HandleAction("FishAlmanac");
            }
        }

        //==============================================================================
        private void HandleAction(PropertyValue action)
        {
            if (action == null)
            {
                return;
            }

            if (action == "FishAlmanac" && Context.IsPlayerFree && Game1.activeClickableMenu == null)
            {
                Game1.activeClickableMenu = new Ui.FishAlmanac(Monitor);
            }
        }
    }
}