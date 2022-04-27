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

            if (!args.IsDown(args.Button))
            {
                return;
            }

            if (args.Button is SButton.ControllerA)
            {
                HandleAction(GetTileAction(Game1.player.GetGrabTile()));
            }
            else if (args.Button is SButton.MouseRight)
            {
                HandleAction(GetTileAction(GetCursorGrabTile()));
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

        //==============================================================================
        private PropertyValue GetTileAction(Vector2 pos)
        {
            if (!Utility.tileWithinRadiusOfPlayer((int)pos.X, (int)pos.Y, 1, Game1.player))
            {
                return null;
            }
            
            var loc = new Location((int)pos.X * Game1.tileSize, (int)pos.Y * Game1.tileSize);
            var tile = Game1.currentLocation.map.GetLayer("Buildings").PickTile(loc, Game1.viewport.Size);
            if (tile == null)
            {
                return null;
            }

            tile.Properties.TryGetValue("Action", out var action);
            return action;
        }

        //==============================================================================
        private static Vector2 GetCursorGrabTile()
        {
            return new Vector2((float)(Game1.getOldMouseX() + Game1.viewport.X),
                (float)(Game1.getOldMouseY() + Game1.viewport.Y)) / Game1.tileSize;
        }
    }
}