using System.Collections.Generic;
using FishAlmanac.GameData;
using StardewValley;

namespace FishAlmanac.Util
{
    public static class BundleUtils
    {
        //==============================================================================
        public static Dictionary<int, Bundle> GetBundles()
        {
            var bundles = Parser.ParseBundles();
            var cc = Game1.getLocationFromName("CommunityCenter") as StardewValley.Locations.CommunityCenter;
            foreach (var (key, value) in bundles)
            {
                if (key is >= 23 and <= 26 || cc == null)
                {
                    continue;
                }

                var ccBundle = cc.bundles[key];
                for (var i = 0; i < value.RequiredItems.Count; ++i)
                {
                    value.CompleteItems[i] = ccBundle[i];
                }
            }

            return bundles;
        }
    }
}