using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using InputButtons = Microsoft.Xna.Framework.Input.Buttons;

namespace FishAlmanac.Ui.Components.Buttons
{
    public abstract class Button : IComponent, IObservable<Button>
    {
        //==============================================================================
        public Rectangle Bounds { get; set; }
        
        //==============================================================================
        public Color Color { get; set; }
        
        //==============================================================================
        public IMonitor Monitor { get; set; }
        
        //==============================================================================
        protected bool Clicked { get; set; }
        
        //==============================================================================
        private List<IObserver<Button>> Observers { get; }


        //==============================================================================
        protected Button(IMonitor monitor)
        {
            Bounds = new Rectangle();
            Color = Color.White;
            Monitor = monitor;
            Clicked = false;
            Observers = new List<IObserver<Button>>();
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
            if (!Bounds.Contains(x, y))
            {
                return;
            }

            Clicked = true;

            foreach (var observer in Observers)
            {
                observer.OnNext(this);
            }
        }
        
        //==============================================================================
        public virtual void HandleGamepadInput(InputButtons button)
        {
            
        }

        //==============================================================================
        public IDisposable Subscribe(IObserver<Button> observer)
        {
            if (!Observers.Contains(observer))
            {
                Observers.Add(observer);
            }

            return new Disposable<Button>(Observers, observer);
        }
    }
}