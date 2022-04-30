using System;
using System.Collections.Generic;
using StardewModdingAPI;

namespace FishAlmanac.Ui.Components.Base
{
    internal class Disposable<T> : Component, IDisposable
    {
        //==============================================================================
        private List<IObserver<T>> Observers { get; }

        //==============================================================================
        private IObserver<T> Observer { get; }


        //==============================================================================
        internal Disposable(IMonitor monitor, List<IObserver<T>> observers, IObserver<T> observer) : base(monitor)
        {
            Observers = observers;
            Observer = observer;
        }

        //==============================================================================
        public void Dispose()
        {
            if (Observers.Contains(Observer))
            {
                Observers.Remove(Observer);
            }
        }
    }
}