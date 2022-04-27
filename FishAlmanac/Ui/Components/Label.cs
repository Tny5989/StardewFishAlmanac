using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewValley;
using InputButtons = Microsoft.Xna.Framework.Input.Buttons;

namespace FishAlmanac.Ui.Components
{
    public class Label : IComponent
    {
        //==============================================================================
        public Rectangle Bounds { get; set; }

        //==============================================================================
        public Color Color { get; set; }

        //==============================================================================
        public IMonitor Monitor { get; set; }

        //==============================================================================
        public string Text { get; init; }

        //==============================================================================
        public bool Bold { get; set; }

        //==============================================================================
        public Alignment Alignment { get; set; }

        //==============================================================================
        public SpriteFont Font { get; set; }

        //==============================================================================
        private string AdjustedText { get; set; }


        //==============================================================================
        public Label(IMonitor monitor)
        {
            Bounds = new Rectangle();
            Color = Color.Black;
            Monitor = monitor;
            Text = "";
            Bold = false;
            Alignment = Alignment.Left;
            Font = Game1.smallFont;
        }

        //==============================================================================
        public void Draw(SpriteBatch b)
        {
            AdjustedText ??= GetText();
            var textPosition = GetTextPosition(Font.MeasureString(AdjustedText));
            if (Bold)
            {
                Utility.drawBoldText(b, AdjustedText, Font, textPosition, Color);
            }
            else
            {
                b.DrawString(Font, AdjustedText, textPosition, Color, 0f, Vector2.Zero, 1f,
                    SpriteEffects.None, 1f);
            }
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