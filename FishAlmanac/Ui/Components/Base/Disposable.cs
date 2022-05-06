using System;
using System.Collections.Generic;
using StardewModdingAPI;

namespace FishAlmanac.Ui.Components.Base
{
    internal class Disposable<T> : IDisposable
    {
        //==============================================================================
        public IMonitor Monitor { get; set; }

        //==============================================================================
        private List<IObserver<T>> Observers { get; }

        //==============================================================================
        private IObserver<T> Observer { get; }


        //==============================================================================
        internal Disposable(IMonitor monitor, List<IObserver<T>> observers, IObserver<T> observer)
        {
            Monitor = monitor;
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