using System;
using System.Collections.Generic;
using FishAlmanac.Ui.Components.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewValley;
using InputButtons = Microsoft.Xna.Framework.Input.Buttons;

namespace FishAlmanac.Ui.Components
{
    public class LabelList : Component
    {
        //==============================================================================
        private Vector2 MaxDimensions { get; set; }

        //==============================================================================
        private bool Valid { get; set; }


        //==============================================================================
        public LabelList(IMonitor monitor, IEnumerable<string> items) : base(monitor)
        {
            MaxDimensions = new Vector2();
            Color = Color.Black;

            CreateLabels(items);
        }

        //==============================================================================
        public override void Draw(SpriteBatch b)
        {
            if (!Valid)
            {
                return;
            }

            base.Draw(b);
        }

        //==============================================================================
        public override void Update(Rectangle bounds)
        {
            base.Update(bounds);
            Valid = PositionLabels();
        }
        
        //==============================================================================
        public override Point GetContentSize()
        {
            return new Point()
            {
                X = Bounds.Width,
                Y = (int)(MaxDimensions.Y * Components.Count + 20)
            };
        }

        //==============================================================================
        private void CreateLabels(IEnumerable<string> items)
        {
            var maxDim = new Vector2(0, 0);
            foreach (var str in items)
            {
                var dim = Game1.smallFont.MeasureString(str);
                maxDim.X = Math.Max(maxDim.X, dim.X);
                maxDim.Y = Math.Max(maxDim.Y, dim.Y);

                var item = new Label(Monitor) { Text = str };
                Components.Add(item);
            }

            MaxDimensions = maxDim;
        }

        //==============================================================================
        private bool PositionLabels()
        {
            var maxWidth = Math.Min(MaxDimensions.X, Bounds.Width - 20);
            var maxRows = (int)((Bounds.Height - 20) / MaxDimensions.Y);

            for (var i = 0; i < Components.Count; ++i)
            {
                Components[i].Update(new Rectangle()
                {
                    X = Bounds.X + (int)(Bounds.Width - maxWidth) / 2,
                    Y = Bounds.Y + 10 + (int)(MaxDimensions.Y * i),
                    Width = (int)maxWidth,
                    Height = (int)MaxDimensions.Y
                });
            }

            if (maxRows >= Components.Count)
            {
                return true;
            }

            Monitor.Log($"Not enough space to show locations.");
            return false;
        }
    }
}