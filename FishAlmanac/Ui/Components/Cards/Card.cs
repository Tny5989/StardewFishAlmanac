using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;

namespace FishAlmanac.Ui.Components.Cards
{
    public abstract class Card : IComponent, IObservable<string>
    {
        //==============================================================================
        public Rectangle Bounds { get; set; }
        
        //==============================================================================
        public Color Color { get; set; }
        
        //==============================================================================
        public IMonitor Monitor { get; set; }

        //==============================================================================
        private List<IObserver<string>> Observers { get; }


        //==============================================================================
        protected Card(IMonitor monitor)
        {
            Bounds = new Rectangle();
            Color = Color.White;
            Monitor = monitor;
            Observers = new List<IObserver<string>>();
        }
        
        //==============================================================================
        public abstract void Draw(SpriteBatch b);

        //==============================================================================
        public virtual void HandleScrollWheel(int direction)
        {
        }

        //==============================================================================
        public virtual void HandleLeftClick(int x, int y)
        {
        }

        //==============================================================================
        public IDisposable Subscribe(IObserver<string> observer)
        {
            if (!Observers.Contains(observer))
            {
                Observers.Add(observer);
            }

            return new Disposable<string>(Observers, observer);
        }
    }
}