using System.Collections.Generic;

namespace FishAlmanac.GameData
{
    public class Item
    {
        //==============================================================================
        public int Id { get; }
        
        //==============================================================================
        public string Name { get; }
        
        //==============================================================================
        public string Description { get; }

        
        //==============================================================================
        public Item(int id, string data)
        {
            Id = id;
            
            var parts = data.Split('/');
            Name = ParseName(parts);
            Description = ParseDescription(parts);
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

            return Id == (obj as Item)?.Id;
        }

        //==============================================================================
        private static string ParseName(IReadOnlyList<string> data)
        {
            return data[0];
        }
        
        //==============================================================================
        private static string ParseDescription(IReadOnlyList<string> data)
        {
            return data[5];
        }
    }
}