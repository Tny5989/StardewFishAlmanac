using System.Collections.Generic;

namespace FishAlmanac.GameData
{
    public class Location
    {
        //==============================================================================
        public string Name { get; }
        
        //==============================================================================
        public Dictionary<int, int> SpringFish { get; }
        
        //==============================================================================
        public Dictionary<int, int> SummerFish { get; }
        
        //==============================================================================
        public Dictionary<int, int> FallFish { get; }
        
        //==============================================================================
        public Dictionary<int, int> WinterFish { get; }

        
        //==============================================================================
        public Location(string name, string data)
        {
            Name = name;

            var parts = data.Split('/');
            SpringFish = ParseFish(parts, 4);
            SummerFish = ParseFish(parts, 5);
            FallFish = ParseFish(parts, 6);
            WinterFish = ParseFish(parts, 7);
        }

        //==============================================================================
        private static Dictionary<int, int> ParseFish(IReadOnlyList<string> data, int idx)
        {
            var rawData = data[idx].Split(' ');
            var ret = new Dictionary<int, int>();
            for (var i = 0; i < rawData.Length; i += 2)
            {
                var id = int.Parse(rawData[i]);
                if (id != -1)
                {
                    ret.Add(id, int.Parse(rawData[i + 1]));
                }
            }

            return ret;
        }
    }
}