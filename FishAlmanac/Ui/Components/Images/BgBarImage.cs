using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewValley;

namespace FishAlmanac.Ui.Components.Images
{
    public class BgBarImage : Image
    {
        //==============================================================================
        private static Texture2D Texture => Game1.mouseCursors;

        //==============================================================================
        private static Rectangle Rectangle => new Rectangle(403, 383, 6, 6);


        //==============================================================================
        public BgBarImage(IMonitor monitor) : base(monitor)
        {
        }

        //==============================================================================
        public override void Draw(SpriteBatch b)
        {
            base.Draw(b);
            b.Draw(Texture, Bounds, GetInnerSourceRectangle(), Color, 0f, Vector2.Zero, SpriteEffects.None, 0f);

            var rect = GetTopRectangle();
            b.Draw(Texture, new Rectangle(Bounds.X, Bounds.Y, Bounds.Width, rect.Height), rect, Color, 0f, Vector2.Zero,
                SpriteEffects.None, 0f);

            rect = GetBotRectangle();
            b.Draw(Texture, new Rectangle(Bounds.X, Bounds.Y + Bounds.Height - rect.Height, Bounds.Width, rect.Height),
                rect, Color, 0f, Vector2.Zero, SpriteEffects.None, 0f);
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

        //==============================================================================
        private Rectangle GetInnerSourceRectangle()
        {
            var src = GetSourceRectangle();
            return new Rectangle()
            {
                X = src.X,
                Y = src.Y + 2,
                Width = src.Width,
                Height = 2
            };
        }

        //==============================================================================
        private Rectangle GetTopRectangle()
        {
            var src = GetSourceRectangle();
            return new Rectangle()
            {
                X = src.X,
                Y = src.Y,
                Width = src.Width,
                Height = 2
            };
        }

        //==============================================================================
        private Rectangle GetBotRectangle()
        {
            var src = GetSourceRectangle();
            return new Rectangle()
            {
                X = src.X,
                Y = src.Y + src.Height - 1,
                Width = src.Width,
                Height = 1
            };
        }
    }
}