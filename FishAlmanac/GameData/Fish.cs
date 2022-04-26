using System.Collections.Generic;
using FishAlmanac.Util;

namespace FishAlmanac.GameData
{
    public class Fish
    {
        //==============================================================================
        public int Id { get; }
        
        //==============================================================================
        public string Name { get; }
        
        //==============================================================================
        public int StartTime { get; }
        
        //==============================================================================
        public int StopTime { get; }
        
        //==============================================================================
        public List<WeatherType> Weathers { get; }

        
        //==============================================================================
        public Fish(int id, string data)
        {
            Id = id;
            
            var parts = data.Split('/');
            Name = ParseName(parts);
            (StartTime, StopTime) = ParseTime(parts);
            Weathers = ParseWeather(parts);
        }

        //==============================================================================
        public override int GetHashCode()
        {
            return Id;
        }

        //==============================================================================
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (GetType() != obj.GetType())
            {
                return false;
            }

            return Id == (obj as Fish)?.Id;
        }

        //==============================================================================
        private static string ParseName(IReadOnlyList<string> data)
        {
            return data[0];
        }

        //==============================================================================
        private static (int, int) ParseTime(IReadOnlyList<string> data)
        {
            if (data[1] == "trap")
            {
                return (600, 200);
            }

            var rawTimes = data[5].Split(' ');
            return (int.Parse(rawTimes[0]) % 2400, int.Parse(rawTimes[1]) % 2400);
        }

        //==============================================================================
        private static List<WeatherType> ParseWeather(IReadOnlyList<string> data)
        {
            if (data[1] == "trap")
            {
                return new List<WeatherType>
                    { WeatherType.Sun, WeatherType.Rain, WeatherType.Wind, WeatherType.Storm, WeatherType.Snow };
            }

            var weathers = new List<WeatherType>();
            switch (data[7])
            {
                case "sunny":
                    weathers.Add(WeatherType.Sun);
                    weathers.Add(WeatherType.Wind);
                    break;
                case "rainy":
                    weathers.Add(WeatherType.Rain);
                    weathers.Add(WeatherType.Storm);
                    weathers.Add(WeatherType.Snow);
                    break;
                default:
                    weathers.Add(WeatherType.Sun);
                    weathers.Add(WeatherType.Wind);
                    weathers.Add(WeatherType.Rain);
                    weathers.Add(WeatherType.Storm);
                    weathers.Add(WeatherType.Snow);
                    break;
            }

            return weathers;
        }
    }
}