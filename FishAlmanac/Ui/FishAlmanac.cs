using System;
using System.Collections.Generic;
using System.Linq;
using FishAlmanac.GameData;
using FishAlmanac.Ui.Components;
using FishAlmanac.Ui.Components.Cards;
using FishAlmanac.Ui.Components.Images;
using FishAlmanac.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Menus;
using Bundle = FishAlmanac.GameData.Bundle;
using Fish = FishAlmanac.GameData.Fish;

namespace FishAlmanac.Ui
{
    public class FishAlmanac : IClickableMenu
    {
        //==============================================================================
        public IMonitor Monitor { get; set; }

        //==============================================================================
        private LetterImage Background { get; }

        //==============================================================================
        private CardDisplay Display { get; }


        //==============================================================================
        public FishAlmanac(IMonitor monitor)
        {
            Monitor = monitor;
            Background = new LetterImage(monitor);
            Display = new CardDisplay(monitor);
            CreateCards(FishUtils.GetLocationsForFish(DayUtils.Season(), DayUtils.Weather()), BundleUtils.GetBundles());
            CalculateDimensions();
        }

        //==============================================================================
        public override void receiveKeyPress(Keys key)
        {
            if (key == Keys.Escape)
            {
                exitThisMenu();
            }
        }

        //==============================================================================
        public override void receiveScrollWheelAction(int direction)
        {
            var temp = Math.Min(1, Math.Max(-1, direction));
            Display.HandleScrollWheel(temp * 10);
        }

        //==============================================================================
        public override void receiveLeftClick(int x, int y, bool playSound = true)
        {
            if (Display.Bounds.Contains(x, y))
            {
                Display.HandleLeftClick(x, y);
            }
            else
            {
                base.receiveLeftClick(x, y, playSound);
            }
        }

        //==============================================================================
        public override void draw(SpriteBatch b)
        {
            using (var backgroundBatch = new SpriteBatch(Game1.graphics.GraphicsDevice))
            {
                backgroundBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp,
                    null, null);
                Background.Draw(backgroundBatch);
                backgroundBatch.End();
            }

            using (var contentBatch = new SpriteBatch(Game1.graphics.GraphicsDevice))
            {
                contentBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp,
                    null, null);
                Display.Draw(contentBatch);
                contentBatch.End();
            }

            drawMouse(Game1.spriteBatch);
        }

        //==============================================================================
        public override void gameWindowSizeChanged(Rectangle oldBounds, Rectangle newBounds)
        {
            CalculateDimensions();
        }

        //==============================================================================
        private void CreateCards(Dictionary<Fish, List<Location>> data, Dictionary<int, Bundle> bundles)
        {
            foreach (var (fish, locations) in data.OrderBy(i => i.Key.Name))
            {
                if (locations.Count <= 0)
                {
                    continue;
                }

                var (inBundle, bundleComplete) = BundleUtils.ItemBundleStatus(fish.Id, bundles);
                Display.AddCard(new FishCard(Monitor, fish, locations, ShipUtils.HasShippedItem(fish.Id),
                    inBundle, inBundle && bundleComplete));
            }
        }

        //==============================================================================
        private void CalculateDimensions()
        {
            width = 600 + borderWidth * 2;
            height = 300 + borderWidth * 2;

            xPositionOnScreen = (Game1.uiViewport.Width - width) / 2;
            yPositionOnScreen = (Game1.uiViewport.Height - height) / 2;

            var bounds = new Rectangle(xPositionOnScreen, yPositionOnScreen, width, height);
            Background.Update(bounds);
            Display.Update(bounds);
        }
    }
}