using FishAlmanac.Ui.Components.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewValley;
using InputButtons = Microsoft.Xna.Framework.Input.Buttons;

namespace FishAlmanac.Ui.Components
{
    public class Checkbox : Component
    {
        //==============================================================================
        private enum Indices
        {
            Text
        }

        //==============================================================================
        private static Texture2D Texture => Game1.mouseCursors;

        //==============================================================================
        private static Rectangle UnCheckedRectangle => new Rectangle(227, 425, 9, 9);

        //==============================================================================
        private static Rectangle CheckedRectangle => new Rectangle(236, 425, 9, 9);

        //==============================================================================
        public bool Checked { get; set; }

        //==============================================================================
        public bool Disabled { get; set; }

        //==============================================================================
        private Rectangle DstRectangle { get; set; }

        //==============================================================================
        private Rectangle SrcRectangle { get; set; }


        //==============================================================================
        public Checkbox(IMonitor monitor, string text) : base(monitor)
        {
            Checked = false;
            DstRectangle = new Rectangle();
            SrcRectangle = new Rectangle();

            Components.Add(new Label(monitor) { Text = text });
        }

        //==============================================================================
        public override void Draw(SpriteBatch b)
        {
            base.Draw(b);
            b.Draw(Texture, DstRectangle, SrcRectangle, Color, 0f, Vector2.Zero, SpriteEffects.None, 0f);
        }

        //==============================================================================
        public override void Update(Rectangle bounds)
        {
            base.Update(bounds);

            var label = GetComponent<Label>(0);

            Color = Disabled ? new Color(255, 255, 255, 100) : Color.White;
            Components[(int)Indices.Text].Color = Disabled ? new Color(0, 0, 0, 100) : Color.Black;

            var textSize = label.Font.MeasureString(label.Text);
            DstRectangle = GetDestinationRectangle(textSize);
            SrcRectangle = GetSourceRectangle();

            PositionText(textSize);
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
            GetComponent<Label>(0).Update(new Rectangle()
            {
                X = DstRectangle.Right + 10,
                Y = DstRectangle.Top + (DstRectangle.Height - (int)textSize.Y) / 2,
                Width = (int)textSize.X,
                Height = (int)textSize.Y
            });
        }

        //==============================================================================
        private Rectangle GetSourceRectangle()
        {
            return Checked ? CheckedRectangle : UnCheckedRectangle;
        }
    }
}