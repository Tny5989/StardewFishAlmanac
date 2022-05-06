using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewValley;

namespace FishAlmanac.Ui.Components.Images
{
    public class LetterImage : Image
    {
        //==============================================================================
        private static Texture2D Texture => Game1.content.Load<Texture2D>("LooseSprites\\letterBG");

        //==============================================================================
        private static Rectangle Rectangle => new Rectangle(0, 0, 320, 180);


        //==============================================================================
        public LetterImage(IMonitor monitor) : base(monitor)
        {
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