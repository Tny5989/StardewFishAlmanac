using System;
using System.Collections.Generic;
using System.Linq;
using FishAlmanac.GameData;
using FishAlmanac.Ui.Components.Images;
using FishAlmanac.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;

namespace FishAlmanac.Ui.Components.Cards
{
    public class FishCard : Card
    {
        //==============================================================================
        private FishImage Portrait { get; set; }

        //==============================================================================
        private Label FishName { get; set; }

        //==============================================================================
        private Label FishTime { get; set; }

        //==============================================================================
        private Label LocationsTitle { get; set; }

        //==============================================================================
        private Separator LocationsTitleSeparator { get; set; }

        //==============================================================================
        private LabelList FishLocations { get; set; }

        //==============================================================================
        private Separator Separator { get; set; }

        //==============================================================================
        private Checkbox ShippedBox { get; set; }

        //==============================================================================
        private Checkbox BundledBox { get; set; }


        //==============================================================================
        public FishCard(IMonitor monitor, Fish fish, IEnumerable<Location> locations, bool shipped, bool inBundle,
            bool bundleComplete) : base(monitor)
        {
            Portrait = new FishImage(Monitor) { FishId = fish.Id };
            FishName = new Label(Monitor) { Text = fish.Name };
            FishTime = new Label(Monitor) { Text = FormatTime(fish.StartTime, fish.StopTime) };
            LocationsTitle = new Label(Monitor)
                { Text = "Sightings", Alignment = Alignment.Center };
            LocationsTitleSeparator = new Separator(Monitor);
            FishLocations = new LabelList(Monitor, locations.Select(temp => temp.Name));
            Separator = new Separator(Monitor);
            ShippedBox = new Checkbox(Monitor, "Shipped") { Checked = shipped };
            BundledBox = new Checkbox(Monitor, "Bundled") { Checked = bundleComplete, Disabled = !inBundle };
        }

        //==============================================================================
        public override void Draw(SpriteBatch b)
        {
            PositionPortrait();
            PositionFishName();
            PositionFishTime();
            PositionShippedBox();
            PositionBundledBox();
            PositionLocationsTitle();
            PositionLocationsTitleSeparator();
            PositionFishLocations();
            PositionSeparator();
            Portrait.Draw(b);
            FishName.Draw(b);
            FishTime.Draw(b);
            ShippedBox.Draw(b);
            BundledBox.Draw(b);
            LocationsTitle.Draw(b);
            LocationsTitleSeparator.Draw(b);
            FishLocations.Draw(b);
            Separator.Draw(b);
        }

        //==============================================================================
        private void PositionPortrait()
        {
            var nameSize = FishName.Font.MeasureString(FishName.Text);
            var timeSize = FishTime.Font.MeasureString(FishTime.Text);
            var combinedHeight = (int)(nameSize.Y + timeSize.Y + 64);
            Portrait.Bounds = new Rectangle()
            {
                X = Bounds.X + Bounds.Width / 4 - 32,
                Y = Bounds.Y + (Bounds.Height - combinedHeight) / 2,
                Width = 64,
                Height = 64
            };
        }

        //==============================================================================
        private void PositionFishName()
        {
            var size = FishName.Font.MeasureString(FishName.Text);
            FishName.Bounds = new Rectangle()
            {
                X = Bounds.X + Bounds.Width / 4 - (int)size.X / 2,
                Y = Portrait.Bounds.Bottom + 1,
                Width = (int)size.X,
                Height = (int)size.Y
            };
        }

        //==============================================================================
        private void PositionFishTime()
        {
            var size = FishTime.Font.MeasureString(FishTime.Text);
            FishTime.Bounds = new Rectangle()
            {
                X = Bounds.X + Bounds.Width / 4 - (int)size.X / 2,
                Y = FishName.Bounds.Bottom + 1,
                Width = (int)size.X,
                Height = (int)size.Y
            };
        }

        //==============================================================================
        private void PositionLocationsTitle()
        {
            var size = LocationsTitle.Font.MeasureString(LocationsTitle.Text);
            LocationsTitle.Bounds = new Rectangle()
            {
                X = Bounds.X + Bounds.Width / 2,
                Y = Bounds.Y + 10,
                Width = Bounds.Width / 2,
                Height = (int)size.Y
            };
        }

        //==============================================================================
        private void PositionLocationsTitleSeparator()
        {
            LocationsTitleSeparator.Bounds = new Rectangle()
            {
                X = Bounds.X + Bounds.Width / 2 + 10,
                Y = LocationsTitle.Bounds.Bottom + 1,
                Width = Bounds.Width / 2 - 20,
                Height = 1
            };
        }

        //==============================================================================
        private void PositionFishLocations()
        {
            FishLocations.Bounds = new Rectangle()
            {
                X = Bounds.X + Bounds.Width / 2,
                Y = LocationsTitleSeparator.Bounds.Bottom + 1,
                Width = Bounds.Width / 2,
                Height = (Bounds.Y + Bounds.Height) - LocationsTitleSeparator.Bounds.Bottom - 1
            };
        }

        //==============================================================================
        private void PositionSeparator()
        {
            Separator.Bounds = new Rectangle()
            {
                X = Bounds.X + Bounds.Width / 2,
                Y = Bounds.Y + 10,
                Width = 1,
                Height = Bounds.Height - 20
            };
        }

        //==============================================================================
        private void PositionShippedBox()
        {
            ShippedBox.Bounds = new Rectangle()
            {
                X = Bounds.X,
                Y = FishTime.Bounds.Bottom + 1,
                Width = Bounds.Width / 4,
                Height = Bounds.Bottom - FishTime.Bounds.Bottom - 1
            };
        }

        //==============================================================================
        private void PositionBundledBox()
        {
            BundledBox.Bounds = new Rectangle()
            {
                X = Bounds.X + Bounds.Width / 4,
                Y = FishTime.Bounds.Bottom + 1,
                Width = Bounds.Width / 4,
                Height = Bounds.Bottom - FishTime.Bounds.Bottom - 1
            };
        }

        //==============================================================================
        private static string FormatTime(int start, int end)
        {
            var startDate = new DateTime(DateTime.Now.Year, 1, 1, start / 100, 0, 0);
            var stopDate = new DateTime(DateTime.Now.Year, 1, 1, end / 100, 0, 0);
            return $"{startDate:hh:mm tt} - {stopDate:hh:mm tt}";
        }
    }
}