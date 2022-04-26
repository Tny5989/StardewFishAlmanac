using StardewValley;

namespace FishAlmanac.Util
{
    public static class ShipUtils
    {
        //==============================================================================
        public static bool HasShippedItem(int id)
        {
            if (Game1.player.basicShipped.ContainsKey(id))
            {
                return Game1.player.basicShipped[id] > 0;
            }

            return false;
        }
    }
}