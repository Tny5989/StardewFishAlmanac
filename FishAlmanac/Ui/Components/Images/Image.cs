using FishAlmanac.Ui.Components.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using InputButtons = Microsoft.Xna.Framework.Input.Buttons;

namespace FishAlmanac.Ui.Components.Images
{
    public abstract class Image : Component
    {
        //==============================================================================
        private Rectangle SrcRectangle { get; set; }

        //==============================================================================
        private Texture2D Texture { get; set; }


        //==============================================================================
        protected Image(IMonitor monitor) : base(monitor)
        {
            SrcRectangle = new Rectangle();
        }

        //==============================================================================
        public override void Draw(SpriteBatch b)
        {
            base.Draw(b);
            b.Draw(Texture, Bounds, SrcRectangle, Color, 0f, Vector2.Zero, SpriteEffects.None, 0f);
        }

        //==============================================================================
        public override void Update(Rectangle bounds)
        {
            base.Update(bounds);
            SrcRectangle = GetSourceRectangle();
            Texture = GetTexture();
        }

        //==============================================================================
        protected abstract Texture2D GetTexture();

        //==============================================================================
        protected abstract Rectangle GetSourceRectangle();
    }
}