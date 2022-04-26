using System.Collections.Generic;
using System.Linq;
using StardewValley;

namespace FishAlmanac.GameData
{
    public static class Parser
    {
        //==============================================================================
        public static Dictionary<int, Fish> ParseFish()
        {
            var gameData = Game1.content.Load<Dictionary<int, string>>("Data\\Fish");
            return gameData.Select(temp => new Fish(temp.Key, temp.Value)).ToDictionary(temp => temp.Id);
        }

        //==============================================================================
        public static List<Location> ParseLocations()
        {
            var gameData = Game1.content.Load<Dictionary<string, string>>("Data\\Locations");
            gameData.Remove("Temp");
            gameData.Remove("fishingGame");
            return gameData.Select(temp => new Location(temp.Key, temp.Value)).ToList();
        }

        //==============================================================================
        public static Dictionary<int, Item> ParseItems()
        {
            var gameData = Game1.content.Load<Dictionary<int, string>>("Data\\ObjectInformation");
            return gameData.Select(temp => new Item(temp.Key, temp.Value)).ToDictionary(temp => temp.Id);
        }

        //==============================================================================
        public static Dictionary<int, Bundle> ParseBundles()
        {
            var gameData = Game1.netWorldState.Value.BundleData;
            return gameData.Select(temp => new Bundle(temp.Key, temp.Value)).ToDictionary(temp => temp.Id);
        }
    }
}