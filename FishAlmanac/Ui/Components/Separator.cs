using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewValley;
using InputButtons = Microsoft.Xna.Framework.Input.Buttons;

namespace FishAlmanac.Ui.Components
{
    public class Separator : IComponent
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
        public Rectangle Bounds { get; set; }

        //==============================================================================
        public Color Color { get; set; }

        //==============================================================================
        public IMonitor Monitor { get; set; }

        //==============================================================================
        public Separator(IMonitor monitor)
        {
            Bounds = new Rectangle();
            Color = Color.Black;
            Monitor = monitor;
        }

        //==============================================================================
        public void Draw(SpriteBatch b)
        {
            b.Draw(Texture, Bounds, Color);
        }
        
        //==============================================================================
        public void Update(Rectangle bounds)
        {
            Bounds = bounds;
        }

        //==============================================================================
        public void HandleScrollWheel(int direction)
        {
        }

        //==============================================================================
        public void HandleLeftClick(int x, int y)
        {
        }

        //==============================================================================
        public void HandleGamepadInput(InputButtons button)
        {
        }
    }
}