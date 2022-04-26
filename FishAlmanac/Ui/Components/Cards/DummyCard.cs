using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;

namespace FishAlmanac.Ui.Components.Cards
{
    public class DummyCard : Card
    {
        //==============================================================================
        public DummyCard(IMonitor monitor) : base(monitor)
        {
            
        }
        
        //==============================================================================
        public override void Draw(SpriteBatch b)
        {
        }
    }
}