using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using xTile.ObjectModel;
using Location = xTile.Dimensions.Location;

namespace FishAlmanac
{
    public class ModEntry : Mod
    {
        //==============================================================================
        public override void Entry(IModHelper helper)
        {
            helper.Events.Input.ButtonPressed += OnButtonPressed;
        }

        //==============================================================================
        private void OnButtonPressed(object sender, ButtonPressedEventArgs args)
        {
            if (!Context.IsWorldReady || args.IsSuppressed())
            {
                return;
            }

            if (args.Button != SButton.ControllerA && args.Button != SButton.MouseRight)
            {
                return;
            }

            if (!args.IsDown(args.Button))
            {
                return;
            }

            HandleAction(GetTileAction(args.Cursor.GrabTile));
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

        //==============================================================================
        private static PropertyValue GetTileAction(Vector2 pos)
        {
            var loc = new Location((int)pos.X * Game1.tileSize, (int)pos.Y * Game1.tileSize);
            var tile = Game1.currentLocation.map.GetLayer("Buildings").PickTile(loc, Game1.viewport.Size);
            if (tile == null)
            {
                return null;
            }

            tile.Properties.TryGetValue("Action", out var action);
            return action;
        }
    }
}