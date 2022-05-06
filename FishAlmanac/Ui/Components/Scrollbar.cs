using FishAlmanac.Ui.Components.Base;
using FishAlmanac.Ui.Components.Images;
using Microsoft.Xna.Framework;
using StardewModdingAPI;

namespace FishAlmanac.Ui.Components
{
    public class Scrollbar : Component
    {
        //==============================================================================
        private enum Indices
        {
            BgBar,
            Bar
        }

        //==============================================================================
        private float CurrentScroll { get; set; }

        //==============================================================================
        public Scrollbar(IMonitor monitor) : base(monitor)
        {
            CurrentScroll = 0;

            Components.Add(new BgBarImage(Monitor));
            Components.Add(new BarImage(Monitor));
        }

        //==============================================================================
        public override void Update(Rectangle bounds)
        {
            base.Update(bounds);

            PositionBgBar();
            PositionBar();
        }

        //==============================================================================
        public void UpdateScroll(float current)
        {
            CurrentScroll = current;
            PositionBar();
        }

        //==============================================================================
        private void PositionBgBar()
        {
            Components[(int)Indices.BgBar].Update(Bounds);
        }

        //==============================================================================
        private void PositionBar()
        {
            Components[(int)Indices.Bar].Update(new Rectangle()
            {
                X = Bounds.X,
                Y = (int)((Bounds.Height - 20) * CurrentScroll + Bounds.Y),
                Width = Bounds.Width,
                Height = 20
            });
        }
    }
}