using StardewValley;

namespace FishAlmanac.Util
{
    public static class DayUtils
    {
        //==============================================================================
        public static WeatherType Weather()
        {
            var raining = Game1.isRaining;
            var snowing = Game1.isSnowing;
            var lightning = Game1.isLightning;
            var debris = Game1.isDebrisWeather;

            if (raining && !debris && !lightning && !snowing)
            {
                return WeatherType.Rain;
            }

            if (raining && !debris && lightning && !snowing)
            {
                return WeatherType.Storm;
            }

            if (!raining && debris && !lightning && !snowing)
            {
                return WeatherType.Wind;
            }

            if (!raining && !debris && !lightning && snowing)
            {
                return WeatherType.Snow;
            }

            return WeatherType.Sun;
        }

        //==============================================================================
        public static SeasonType Season()
        {
            return Game1.currentSeason switch
            {
                "summer" => SeasonType.Summer,
                "fall" => SeasonType.Fall,
                "winter" => SeasonType.Winter,
                _ => SeasonType.Spring
            };
        }
    }
}