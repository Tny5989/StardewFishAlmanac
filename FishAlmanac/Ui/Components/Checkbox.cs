using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewValley;

namespace FishAlmanac.Ui.Components
{
    public class Checkbox : IComponent
    {
        //==============================================================================
        private static Texture2D Texture => Game1.mouseCursors;

        //==============================================================================
        private static Rectangle UnCheckedRectangle => new Rectangle(227, 425, 9, 9);

        //==============================================================================
        private static Rectangle CheckedRectangle => new Rectangle(236, 425, 9, 9);

        //==============================================================================
        public Rectangle Bounds { get; set; }

        //==============================================================================
        public Color Color { get; set; }

        //==============================================================================
        public IMonitor Monitor { get; set; }

        //==============================================================================
        public bool Checked { get; set; }
        
        //==============================================================================
        public bool Disabled { get; set; }

        //==============================================================================
        private Label Text { get; set; }


        //==============================================================================
        public Checkbox(IMonitor monitor, string text)
        {
            Bounds = new Rectangle();
            Color = Color.White;
            Monitor = monitor;
            Checked = false;
            Text = new Label(Monitor) { Text = text };
        }

        //==============================================================================
        public void Draw(SpriteBatch b)
        {
            Color = Disabled ? new Color(255, 255, 255, 100) : Color.White;
            Text.Color = Disabled ? new Color(0, 0, 0, 100) : Color.Black;

            var textSize = Text.Font.MeasureString(Text.Text);
            b.Draw(Texture, GetDestinationRectangle(textSize), GetSourceRectangle(),
                Color, 0f, Vector2.Zero, SpriteEffects.None, 0f);

            PositionText(textSize);
            Text.Draw(b);
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
        private Rectangle GetDestinationRectangle(Vector2 textSize)
        {
            return new Rectangle()
            {
                X = Bounds.X + (int)(Bounds.Width - textSize.X - 24) / 2,
                Y = Bounds.Y + (Bounds.Height - 24) / 2,
                Width = 24,
                Height = 24
            };
        }

        //==============================================================================
        private void PositionText(Vector2 textSize)
        {
            var boxBounds = GetDestinationRectangle(textSize);
            Text.Bounds = new Rectangle()
            {
                X = boxBounds.Right + 10,
                Y = boxBounds.Top + (boxBounds.Height - (int)textSize.Y) / 2,
                Width = (int)textSize.X,
                Height = (int)textSize.Y
            };
        }

        //==============================================================================
        private Rectangle GetSourceRectangle()
        {
            return Checked ? CheckedRectangle : UnCheckedRectangle;
        }
    }
}