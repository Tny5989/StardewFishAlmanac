using System.Collections.Generic;

namespace FishAlmanac.GameData
{
    public class Bundle
    {
        //==============================================================================
        public int Id { get; }
        
        //==============================================================================
        public string RoomId { get; }

        //==============================================================================
        public string Name { get; }
        
        //==============================================================================
        public List<int> RequiredItems { get; }
        
        public List<bool> CompleteItems { get; }


        //==============================================================================
        public Bundle(string id, string data)
        {
            var idParts = id.Split('/');
            var dataParts = data.Split('/');

            Id = ParseId(idParts);
            RoomId = ParseRoomId(idParts);

            if (RoomId != "Vault")
            {
                Name = ParseName(dataParts);
                RequiredItems = ParseRequiredItems(dataParts);
            }
            else
            {
                Name = "";
                RequiredItems = new List<int>();
            }

            CompleteItems = PopulateCompleteItems();
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

            return Id == (obj as Bundle)?.Id;
        }

        //==============================================================================
        private static int ParseId(IReadOnlyList<string> data)
        {
            return int.Parse(data[1]);
        }
        
        //==============================================================================
        private static string ParseRoomId(IReadOnlyList<string> data)
        {
            return data[0];
        }

        //==============================================================================
        private static string ParseName(IReadOnlyList<string> data)
        {
            return data[0];
        }

        //==============================================================================
        private static List<int> ParseRequiredItems(IReadOnlyList<string> data)
        {
            var items = new List<int>();
            var parts = data[2].Split(' ');
            for (var i = 0; i < parts.Length; i += 3)
            {
                items.Add(int.Parse(parts[i]));
            }

            return items;
        }

        //==============================================================================
        private List<bool> PopulateCompleteItems()
        {
            var complete = new List<bool>();
            for (var i = 0; i < RequiredItems.Count; ++i)
            {
                complete.Add(false);
            }

            return complete;
        }
    }
}