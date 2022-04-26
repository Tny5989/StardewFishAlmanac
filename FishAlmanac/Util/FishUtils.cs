using System.Collections.Generic;
using FishAlmanac.GameData;

namespace FishAlmanac.Util
{
    public static class FishUtils
    {
        //==============================================================================
        public static Dictionary<Fish, List<Location>> GetLocationsForFish(SeasonType season, WeatherType weather)
        {
            var fish = Parser.ParseFish();
            var locations = Parser.ParseLocations();
            var map = new Dictionary<Fish, List<Location>>();
            foreach (var (_, value) in fish)
            {
                map.Add(value, GetLocationsForFish(value, locations, season, weather));
            }

            return map;
        }

        //==============================================================================
        private static List<Location> GetLocationsForFish(Fish fish, List<Location> locations, SeasonType season,
            WeatherType weather)
        {
            var canCatchHere = new List<Location>();

            if (!CanCatchFishInWeather(fish, weather))
            {
                return canCatchHere;
            }

            foreach (var location in locations)
            {
                var catchable = GetFishForSeasonFromLocation(location, season);
                foreach (var (key, _) in catchable)
                {
                    if (key == fish.Id)
                    {
                        canCatchHere.Add(location);
                    }
                }
            }

            return canCatchHere;
        }

        //==============================================================================
        private static Dictionary<int, int> GetFishForSeasonFromLocation(Location location, SeasonType season)
        {
            return season switch
            {
                SeasonType.Spring => location.SpringFish,
                SeasonType.Summer => location.SummerFish,
                SeasonType.Fall => location.FallFish,
                SeasonType.Winter => location.WinterFish,
                _ => new Dictionary<int, int>()
            };
        }

        //==============================================================================
        private static bool CanCatchFishInWeather(Fish fish, WeatherType weather)
        {
            return fish.Weathers.Contains(weather);
        }
    }
}