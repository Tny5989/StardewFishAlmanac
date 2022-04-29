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
        private Label FishWeather { get; set; }

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
            FishWeather = new Label(Monitor) { Text = FormatWeather(fish.Weathers) };
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
            Portrait.Draw(b);
            FishName.Draw(b);
            FishTime.Draw(b);
            FishWeather.Draw(b);
            ShippedBox.Draw(b);
            BundledBox.Draw(b);
            LocationsTitle.Draw(b);
            LocationsTitleSeparator.Draw(b);
            FishLocations.Draw(b);
            Separator.Draw(b);
        }

        //==============================================================================
        public override void Update(Rectangle bounds)
        {
            base.Update(bounds);

            PositionPortrait();
            PositionFishName();
            PositionFishTime();
            PositionFishWeather();
            PositionShippedBox();
            PositionBundledBox();
            PositionLocationsTitle();
            PositionLocationsTitleSeparator();
            PositionFishLocations();
            PositionSeparator();
        }

        //==============================================================================
        private void PositionPortrait()
        {
            var nameSize = FishName.Font.MeasureString(FishName.Text);
            var timeSize = FishTime.Font.MeasureString(FishTime.Text);
            var weatherSize = FishWeather.Font.MeasureString(FishWeather.Text);
            var combinedHeight = (int)(nameSize.Y + timeSize.Y + weatherSize.Y + 64);
            Portrait.Update(new Rectangle()
            {
                X = Bounds.X + Bounds.Width / 4 - 32,
                Y = Bounds.Y + (Bounds.Height - combinedHeight) / 2,
                Width = 64,
                Height = 64
            });
        }

        //==============================================================================
        private void PositionFishName()
        {
            var size = FishName.Font.MeasureString(FishName.Text);
            FishName.Update(new Rectangle()
            {
                X = Bounds.X + Bounds.Width / 4 - (int)size.X / 2,
                Y = Portrait.Bounds.Bottom + 1,
                Width = (int)size.X,
                Height = (int)size.Y
            });
        }

        //==============================================================================
        private void PositionFishTime()
        {
            var size = FishTime.Font.MeasureString(FishTime.Text);
            FishTime.Update(new Rectangle()
            {
                X = Bounds.X + Bounds.Width / 4 - (int)size.X / 2,
                Y = FishName.Bounds.Bottom + 1,
                Width = (int)size.X,
                Height = (int)size.Y
            });
        }

        //==============================================================================
        private void PositionFishWeather()
        {
            var size = FishWeather.Font.MeasureString(FishWeather.Text);
            FishWeather.Update(new Rectangle()
            {
                X = Bounds.X + Bounds.Width / 4 - (int)size.X / 2,
                Y = FishTime.Bounds.Bottom + 1,
                Width = (int)size.X,
                Height = (int)size.Y
            });
        }

        //==============================================================================
        private void PositionLocationsTitle()
        {
            var size = LocationsTitle.Font.MeasureString(LocationsTitle.Text);
            LocationsTitle.Update(new Rectangle()
            {
                X = Bounds.X + Bounds.Width / 2,
                Y = Bounds.Y + 10,
                Width = Bounds.Width / 2,
                Height = (int)size.Y
            });
        }

        //==============================================================================
        private void PositionLocationsTitleSeparator()
        {
            LocationsTitleSeparator.Update(new Rectangle()
            {
                X = Bounds.X + Bounds.Width / 2 + 10,
                Y = LocationsTitle.Bounds.Bottom + 1,
                Width = Bounds.Width / 2 - 20,
                Height = 1
            });
        }

        //==============================================================================
        private void PositionFishLocations()
        {
            FishLocations.Update(new Rectangle()
            {
                X = Bounds.X + Bounds.Width / 2,
                Y = LocationsTitleSeparator.Bounds.Bottom + 1,
                Width = Bounds.Width / 2,
                Height = (Bounds.Y + Bounds.Height) - LocationsTitleSeparator.Bounds.Bottom - 1
            });
        }

        //==============================================================================
        private void PositionSeparator()
        {
            Separator.Update(new Rectangle()
            {
                X = Bounds.X + Bounds.Width / 2,
                Y = Bounds.Y + 10,
                Width = 1,
                Height = Bounds.Height - 20
            });
        }

        //==============================================================================
        private void PositionShippedBox()
        {
            ShippedBox.Update(new Rectangle()
            {
                X = Bounds.X,
                Y = FishWeather.Bounds.Bottom + 1,
                Width = Bounds.Width / 4,
                Height = Bounds.Bottom - FishWeather.Bounds.Bottom - 1
            });
        }

        //==============================================================================
        private void PositionBundledBox()
        {
            BundledBox.Update(new Rectangle()
            {
                X = Bounds.X + Bounds.Width / 4,
                Y = FishWeather.Bounds.Bottom + 1,
                Width = Bounds.Width / 4,
                Height = Bounds.Bottom - FishWeather.Bounds.Bottom - 1
            });
        }

        //==============================================================================
        private static string FormatTime(int start, int end)
        {
            var startDate = new DateTime(DateTime.Now.Year, 1, 1, start / 100, 0, 0);
            var stopDate = new DateTime(DateTime.Now.Year, 1, 1, end / 100, 0, 0);
            return $"{startDate:hh:mm tt} - {stopDate:hh:mm tt}";
        }

        //==============================================================================
        private static string FormatWeather(List<WeatherType> weathers)
        {
            var uniqueWeathers = new HashSet<WeatherType>();
            foreach (var weather in weathers)
            {
                switch (weather)
                {
                    case WeatherType.Rain:
                    case WeatherType.Snow:
                    case WeatherType.Storm:
                        uniqueWeathers.Add(WeatherType.Rain);
                        break;
                    default:
                        uniqueWeathers.Add(WeatherType.Sun);
                        break;
                }
            }

            if (uniqueWeathers.Count > 1)
            {
                return "Sun or Rain";
            }

            return uniqueWeathers.Contains(WeatherType.Rain) ? "Rain Only" : "Sun Only";
        }
    }
}