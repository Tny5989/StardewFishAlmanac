using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewValley;

namespace FishAlmanac.Ui.Components.Images
{
    public class BarImage : Image
    {
        //==============================================================================
        private static Texture2D Texture => Game1.mouseCursors;
        
        //==============================================================================
        private static Rectangle Rectangle => new Rectangle(435, 463, 6, 10);


        //==============================================================================
        public BarImage(IMonitor monitor) : base(monitor)
        {
        }

        //==============================================================================
        public override Point GetContentSize()
        {
            return Rectangle.Size;
        }

        //==============================================================================
        protected override Texture2D GetTexture()
        {
            return Texture;
        }

        //==============================================================================
        protected override Rectangle GetSourceRectangle()
        {
            return Rectangle;
        }
    }
}