using System;
using System.Collections.Generic;
using FishAlmanac.Ui.Components.Base;
using StardewModdingAPI;
using InputButtons = Microsoft.Xna.Framework.Input.Buttons;

namespace FishAlmanac.Ui.Components.Buttons
{
    public abstract class Button : Component, IObservable<Button>
    {
        //==============================================================================
        protected bool Clicked { get; set; }

        //==============================================================================
        private List<IObserver<Button>> Observers { get; }


        //==============================================================================
        protected Button(IMonitor monitor) : base(monitor)
        {
            Clicked = false;
            Observers = new List<IObserver<Button>>();
        }

        //==============================================================================
        public override void HandleLeftClick(int x, int y)
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
        public IDisposable Subscribe(IObserver<Button> observer)
        {
            if (!Observers.Contains(observer))
            {
                Observers.Add(observer);
            }

            return new Disposable<Button>(Monitor, Observers, observer);
        }
    }
}