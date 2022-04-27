using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using InputButtons = Microsoft.Xna.Framework.Input.Buttons;

namespace FishAlmanac.Ui.Components.Images
{
    public abstract class Image : IComponent
    {
        //==============================================================================
        public Rectangle Bounds { get; set; }

        //==============================================================================
        public Color Color { get; set; }
        
        //==============================================================================
        public IMonitor Monitor { get; set; }


        //==============================================================================
        protected Image(IMonitor monitor)
        {
            Bounds = new Rectangle();
            Color = Color.White;
            Monitor = monitor;
        }

        //==============================================================================
        public void Draw(SpriteBatch b)
        {
            b.Draw(GetTexture(), Bounds, GetSourceRectangle(), Color, 0f, Vector2.Zero, SpriteEffects.None, 0f);
        }

        //==============================================================================
        public virtual void HandleScrollWheel(int direction)
        {
        }

        //==============================================================================
        public virtual void HandleLeftClick(int x, int y)
        {
        }
        
        //==============================================================================
        public virtual void HandleGamepadInput(InputButtons button)
        {
        }

        //==============================================================================
        protected abstract Texture2D GetTexture();

        //==============================================================================
        protected abstract Rectangle GetSourceRectangle();
    }
}