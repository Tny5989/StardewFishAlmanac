using System;
using System.Collections.Generic;
using FishAlmanac.Ui.Components.Base;
using StardewModdingAPI;
using InputButtons = Microsoft.Xna.Framework.Input.Buttons;

namespace FishAlmanac.Ui.Components.Cards
{
    public abstract class Card : Component, IObservable<string>
    {
        //==============================================================================
        private List<IObserver<string>> Observers { get; }


        //==============================================================================
        protected Card(IMonitor monitor) : base(monitor)
        {
            Observers = new List<IObserver<string>>();
        }

        //==============================================================================
        public IDisposable Subscribe(IObserver<string> observer)
        {
            if (!Observers.Contains(observer))
            {
                Observers.Add(observer);
            }

            return new Disposable<string>(Monitor, Observers, observer);
        }
    }
}