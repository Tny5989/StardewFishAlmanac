﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewValley;

namespace FishAlmanac.Ui.Components
{
    public class LabelList : IComponent
    {
        //==============================================================================
        public Rectangle Bounds { get; set; }

        //==============================================================================
        public Color Color { get; set; }

        //==============================================================================
        public IMonitor Monitor { get; set; }

        //==============================================================================
        private List<Label> Items { get; set; }

        //==============================================================================
        private Vector2 MaxDimensions { get; set; }


        //==============================================================================
        public LabelList(IMonitor monitor, IEnumerable<string> items)
        {
            Bounds = new Rectangle();
            Color = Color.Black;
            Monitor = monitor;
            Items = new List<Label>();
            MaxDimensions = new Vector2();

            CreateLabels(items);
        }

        //==============================================================================
        public void Draw(SpriteBatch b)
        {
            if (!PositionLabels())
            {
                return;
            }

            foreach (var item in Items)
            {
                item.Draw(b);
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
        private void CreateLabels(IEnumerable<string> items)
        {
            var maxDim = new Vector2(0, 0);
            foreach (var str in items)
            {
                var dim = Game1.smallFont.MeasureString(str);
                maxDim.X = Math.Max(maxDim.X, dim.X);
                maxDim.Y = Math.Max(maxDim.Y, dim.Y);
                Items.Add(new Label(Monitor) { Text = str });
            }
            MaxDimensions = maxDim;
        }

        //==============================================================================
        private bool PositionLabels()
        {
            var maxWidth = Math.Min(MaxDimensions.X, Bounds.Width - 20);
            var maxRows = (int)((Bounds.Height - 20) / MaxDimensions.Y);

            for (var i = 0; i < Items.Count; ++i)
            {
                Items[i].Bounds = new Rectangle()
                {
                    X = Bounds.X + (int)(Bounds.Width - maxWidth) / 2,
                    Y= Bounds.Y + 10 + (int)(MaxDimensions.Y * i),
                    Width = (int)maxWidth,
                    Height = (int)MaxDimensions.Y
                };
            }

            if (maxRows >= Items.Count)
            {
                return true;
            }

            Monitor.Log($"Not enough space to show locations.");
            return false;
        }
    }
}