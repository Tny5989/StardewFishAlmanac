using System;
using FishAlmanac.Ui.Components.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewValley;
using InputButtons = Microsoft.Xna.Framework.Input.Buttons;

namespace FishAlmanac.Ui.Components
{
    public class Separator : Component
    {
        //==============================================================================
        private static Lazy<Texture2D> LazyTexture => new(() =>
        {
            var pixel = new Texture2D(Game1.graphics.GraphicsDevice, 1, 1);
            pixel.SetData(new[] { Color.White });
            return pixel;
        });

        //==============================================================================
        private static Texture2D Texture => LazyTexture.Value;

        //==============================================================================
        public Separator(IMonitor monitor) : base(monitor)
        {
            Color = Color.Black;
        }

        //==============================================================================
        public override void Draw(SpriteBatch b)
        {
            base.Draw(b);
            b.Draw(Texture, Bounds, Color);
        }
    }
}