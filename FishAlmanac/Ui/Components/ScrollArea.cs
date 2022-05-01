using System;
using FishAlmanac.Ui.Components.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewValley;
using InputButtons = Microsoft.Xna.Framework.Input.Buttons;

namespace FishAlmanac.Ui.Components
{
    public class ScrollArea : Component
    {
        //==============================================================================
        private enum Indices
        {
            Component,
            Scrollbar
        }

        //==============================================================================
        private int ContentHeight { get; set; }

        //==============================================================================
        private int Offset { get; set; }

        //==============================================================================
        private int MaxOffset { get; set; }

        //==============================================================================
        public ScrollArea(IMonitor monitor, Component component) : base(monitor)
        {
            ContentHeight = 1;
            Offset = 0;
            MaxOffset = 0;

            Components.Add(component);
            Components.Add(new Scrollbar(Monitor));
        }

        //==============================================================================
        public override void Draw(SpriteBatch b)
        {
            using var contentBatch = new SpriteBatch(Game1.graphics.GraphicsDevice);
            contentBatch.GraphicsDevice.ScissorRectangle =
                new Rectangle(Bounds.X, Bounds.Y, Bounds.Width, Bounds.Height);
            contentBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp, null,
                new RasterizerState() { ScissorTestEnable = true });

            var component = Components[(int)Indices.Component];
            Components[(int)Indices.Component].Update(new Rectangle()
            {
                X = Bounds.X,
                Y = Bounds.Y - Offset,
                Width = Bounds.Width - ((MaxOffset > 0) ? 20 : 0),
                Height = component.GetContentSize().Y
            });

            base.Draw(contentBatch);

            contentBatch.End();
        }

        //==============================================================================
        public override void Update(Rectangle bounds)
        {
            base.Update(bounds);

            var component = Components[(int)Indices.Component];
            Offset = (int)(Offset / (float)ContentHeight * component.GetContentSize().Y);
            ContentHeight = component.GetContentSize().Y;
            MaxOffset = Math.Max(ContentHeight - Bounds.Height, 0);

            var scrollbar = GetComponent<Scrollbar>((int)Indices.Scrollbar);
            scrollbar.Update(new Rectangle()
            {
                X = Bounds.X + Bounds.Width - 20,
                Y = Bounds.Y + 10,
                Width = 10,
                Height = Bounds.Height - 20
            });
            scrollbar.UpdateScroll(Offset / (float)MaxOffset);
            scrollbar.Visible = MaxOffset > 0;
        }

        //==============================================================================
        public override bool HandleScrollWheel(int direction)
        {
            if (base.HandleScrollWheel(direction))
            {
                return false;
            }

            if (MaxOffset == 0)
            {
                return false;
            }

            var mousePos = Game1.input.GetMouseState().Position;
            if (!Bounds.Contains(mousePos))
            {
                return false;
            }

            Offset = Math.Clamp(Offset - direction, 0, MaxOffset);
            GetComponent<Scrollbar>((int)Indices.Scrollbar).UpdateScroll(Offset / (float)MaxOffset);
            return true;
        }
    }
}