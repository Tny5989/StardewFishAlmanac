using FishAlmanac.Ui.Components.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewValley;
using InputButtons = Microsoft.Xna.Framework.Input.Buttons;

namespace FishAlmanac.Ui.Components
{
    public class Label : Component
    {
        //==============================================================================
        public string Text { get; init; }

        //==============================================================================
        public Alignment Alignment { get; set; }

        //==============================================================================
        public SpriteFont Font { get; }

        //==============================================================================
        private string AdjustedText { get; set; }

        //==============================================================================
        private Vector2 TextPosition { get; set; }


        //==============================================================================
        public Label(IMonitor monitor) : base(monitor)
        {
            Text = "";
            Alignment = Alignment.Left;
            Font = Game1.smallFont;
            TextPosition = new Vector2();
            Color = Color.Black;
        }

        //==============================================================================
        public override void Draw(SpriteBatch b)
        {
            base.Draw(b);
            b.DrawString(Font, AdjustedText, TextPosition, Color, 0f, Vector2.Zero, 1f,
                SpriteEffects.None, 1f);
        }

        //==============================================================================
        public override void Update(Rectangle bounds)
        {
            base.Update(bounds);
            AdjustedText = GetText();
            TextPosition = GetTextPosition(Font.MeasureString(AdjustedText));
        }

        //==============================================================================
        private string GetText()
        {
            var adjustedText = Text;
            var size = Font.MeasureString(adjustedText);
            var index = 1;
            while (size.X > Bounds.Width)
            {
                adjustedText = Text[..^index] + "...";
                size = Font.MeasureString(adjustedText);
                index += 1;
            }

            return adjustedText;
        }

        //==============================================================================
        private Vector2 GetTextPosition(Vector2 textSize)
        {
            var position = new Vector2(Bounds.X, Bounds.Y + (Bounds.Height - textSize.Y) / 2);
            switch (Alignment)
            {
                case Alignment.Center:
                    position.X += (Bounds.Width - textSize.X) / 2;
                    break;
                case Alignment.Right:
                    position.X += Bounds.Width - textSize.X;
                    break;
                case Alignment.Left:
                default:
                    break;
            }

            return position;
        }
    }
}