using System;
using System.Collections.Generic;
using System.Linq;
using FishAlmanac.GameData;
using FishAlmanac.Ui.Components.Images;
using FishAlmanac.Util;
using Microsoft.Xna.Framework;
using StardewModdingAPI;

namespace FishAlmanac.Ui.Components.Cards
{
    public class FishCard : Card
    {
        //==============================================================================
        private enum Indices
        {
            Portrait,
            Name,
            Time,
            Weather,
            LocationsTitle,
            TitleSeparator,
            LocationsList,
            MiddleSeparator,
            Shipped,
            Bundled
        }

        //==============================================================================
        public FishCard(IMonitor monitor, Fish fish, IEnumerable<Location> locations, bool shipped, bool inBundle,
            bool bundleComplete) : base(monitor)
        {
            Components.Add(new FishImage(Monitor) { FishId = fish.Id });
            Components.Add(new Label(Monitor) { Text = fish.Name });
            Components.Add(new Label(Monitor) { Text = FormatTime(fish.StartTime, fish.StopTime) });
            Components.Add(new Label(Monitor) { Text = FormatWeather(fish.Weathers) });
            Components.Add(new Label(Monitor) { Text = "Sightings", Alignment = Alignment.Center });
            Components.Add(new Separator(Monitor));
            Components.Add(new LabelList(Monitor, locations.Select(temp => temp.Name)));
            Components.Add(new Separator(Monitor));
            Components.Add(new Checkbox(Monitor, "Shipped") { Checked = shipped });
            Components.Add(new Checkbox(Monitor, "Bundled") { Checked = bundleComplete, Disabled = !inBundle });
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
            var nameLabel = GetComponent<Label>((int)Indices.Name);
            var nameSize = nameLabel.Font.MeasureString(nameLabel.Text);

            var timeLabel = GetComponent<Label>((int)Indices.Time);
            var timeSize = timeLabel.Font.MeasureString(timeLabel.Text);

            var weatherLabel = GetComponent<Label>((int)Indices.Weather);
            var weatherSize = weatherLabel.Font.MeasureString(weatherLabel.Text);

            var combinedHeight = (int)(nameSize.Y + timeSize.Y + weatherSize.Y + 64);

            Components[(int)Indices.Portrait].Update(new Rectangle()
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
            var label = GetComponent<Label>((int)Indices.Name);
            var size = label.Font.MeasureString(label.Text);
            label.Update(new Rectangle()
            {
                X = Bounds.X + Bounds.Width / 4 - (int)size.X / 2,
                Y = Components[(int)Indices.Portrait].Bounds.Bottom + 1,
                Width = (int)size.X,
                Height = (int)size.Y
            });
        }

        //==============================================================================
        private void PositionFishTime()
        {
            var label = GetComponent<Label>((int)Indices.Time);
            var size = label.Font.MeasureString(label.Text);
            label.Update(new Rectangle()
            {
                X = Bounds.X + Bounds.Width / 4 - (int)size.X / 2,
                Y = Components[(int)Indices.Name].Bounds.Bottom + 1,
                Width = (int)size.X,
                Height = (int)size.Y
            });
        }

        //==============================================================================
        private void PositionFishWeather()
        {
            var label = GetComponent<Label>((int)Indices.Weather);
            var size = label.Font.MeasureString(label.Text);
            label.Update(new Rectangle()
            {
                X = Bounds.X + Bounds.Width / 4 - (int)size.X / 2,
                Y = Components[(int)Indices.Time].Bounds.Bottom + 1,
                Width = (int)size.X,
                Height = (int)size.Y
            });
        }

        //==============================================================================
        private void PositionLocationsTitle()
        {
            var label = GetComponent<Label>((int)Indices.LocationsTitle);
            var size = label.Font.MeasureString(label.Text);
            label.Update(new Rectangle()
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
            Components[(int)Indices.TitleSeparator].Update(new Rectangle()
            {
                X = Bounds.X + Bounds.Width / 2 + 10,
                Y = Components[(int)Indices.LocationsTitle].Bounds.Bottom + 1,
                Width = Bounds.Width / 2 - 20,
                Height = 1
            });
        }

        //==============================================================================
        private void PositionFishLocations()
        {
            Components[(int)Indices.LocationsList].Update(new Rectangle()
            {
                X = Bounds.X + Bounds.Width / 2,
                Y = Components[(int)Indices.TitleSeparator].Bounds.Bottom + 1,
                Width = Bounds.Width / 2,
                Height = (Bounds.Y + Bounds.Height) - Components[(int)Indices.TitleSeparator].Bounds.Bottom - 1
            });
        }

        //==============================================================================
        private void PositionSeparator()
        {
            Components[(int)Indices.MiddleSeparator].Update(new Rectangle()
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
            Components[(int)Indices.Shipped].Update(new Rectangle()
            {
                X = Bounds.X,
                Y = Components[(int)Indices.Weather].Bounds.Bottom + 1,
                Width = Bounds.Width / 4,
                Height = Bounds.Bottom - Components[(int)Indices.Weather].Bounds.Bottom - 1
            });
        }

        //==============================================================================
        private void PositionBundledBox()
        {
            Components[(int)Indices.Bundled].Update(new Rectangle()
            {
                X = Bounds.X + Bounds.Width / 4,
                Y = Components[(int)Indices.Weather].Bounds.Bottom + 1,
                Width = Bounds.Width / 4,
                Height = Bounds.Bottom - Components[(int)Indices.Weather].Bounds.Bottom - 1
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