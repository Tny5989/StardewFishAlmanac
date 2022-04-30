using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewValley;

namespace FishAlmanac.Ui.Components.Buttons
{
    #region Base

    public abstract class ArrowButton : Button
    {
        //==============================================================================
        private static Texture2D Texture => Game1.mouseCursors;

        //==============================================================================
        protected virtual Rectangle Rectangle => new Rectangle(0, 0, 0, 0);


        //==============================================================================
        protected ArrowButton(IMonitor monitor) : base(monitor)
        {
        }


        //==============================================================================
        public override void Draw(SpriteBatch b)
        {
            base.Draw(b);
            b.Draw(Texture, GetModifiedBounds(GetBoundsModifier(Game1.input.GetMouseState().Position)), Rectangle,
                Color, 0f, Vector2.Zero, SpriteEffects.None, 0f);
            Clicked = false;
        }

        //==============================================================================
        private int GetBoundsModifier(Point cursorPosition)
        {
            var modifier = 0;
            if (Bounds.Contains(cursorPosition))
            {
                modifier += 2;
            }

            if (Clicked)
            {
                modifier -= 2;
            }

            return modifier;
        }

        //==============================================================================
        private Rectangle GetModifiedBounds(int modifier)
        {
            return new Rectangle
            {
                X = Bounds.X - modifier,
                Y = Bounds.Y - modifier,
                Width = Bounds.Width + modifier * 2,
                Height = Bounds.Height + modifier * 2
            };
        }
    }

    #endregion

    #region Left

    public class LeftButton : ArrowButton
    {
        //==============================================================================
        protected override Rectangle Rectangle => new Rectangle(8, 268, 43, 39);


        //==============================================================================
        public LeftButton(IMonitor monitor) : base(monitor)
        {
        }
    }

    #endregion

    #region Right

    public class RightButton : ArrowButton
    {
        //==============================================================================
        protected override Rectangle Rectangle => new Rectangle(12, 204, 43, 39);


        //==============================================================================
        public RightButton(IMonitor monitor) : base(monitor)
        {
        }
    }

    #endregion

    #region Up

    public class UpButton : ArrowButton
    {
        //==============================================================================
        protected override Rectangle Rectangle => new Rectangle(76, 72, 39, 43);


        //==============================================================================
        public UpButton(IMonitor monitor) : base(monitor)
        {
        }
    }

    #endregion

    #region Down

    public class DownButton : ArrowButton
    {
        //==============================================================================
        protected override Rectangle Rectangle => new Rectangle(12, 76, 39, 43);


        //==============================================================================
        public DownButton(IMonitor monitor) : base(monitor)
        {
        }
    }

    #endregion
}