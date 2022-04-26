using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewValley;

namespace FishAlmanac.Ui.Components.Images
{
    public class FishImage : Image
    {
        //==============================================================================
        private static Lazy<Texture2D> Texture => new(Game1.content.Load<Texture2D>("Maps\\springobjects"));

        //==============================================================================
        public int FishId { get; set; }


        //==============================================================================
        public FishImage(IMonitor monitor) : base(monitor)
        {
            FishId = 128;
        }

        //==============================================================================
        protected override Texture2D GetTexture()
        {
            return Texture.Value;
        }

        //==============================================================================
        protected override Rectangle GetSourceRectangle()
        {
            if (FishId < 0)
            {
                return new Rectangle();
            }

            var row = FishId / 24;
            var col = FishId % 24;
            return new Rectangle(16 * col, 16 * row, 16, 16);
        }
    }
}