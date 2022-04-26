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

        //==============================================================================
        public static (bool, bool) ItemBundleStatus(int itemId, Dictionary<int, Bundle> bundles)
        {
            var inBundle = false;
            var bundleComplete = true;
            foreach (var (_, value) in bundles)
            {
                for (var i = 0; i < value.RequiredItems.Count; ++i)
                {
                    if (value.RequiredItems[i] != itemId)
                    {
                        continue;
                    }

                    inBundle = true;
                    bundleComplete = bundleComplete && value.CompleteItems[i];
                }
            }

            return (inBundle, bundleComplete);
        }
    }
}