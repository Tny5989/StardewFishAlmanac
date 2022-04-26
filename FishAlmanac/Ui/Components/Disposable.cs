using System;
using System.Collections.Generic;

namespace FishAlmanac.Ui.Components
{
    internal class Disposable<T> : IDisposable
    {
        //==============================================================================
        private List<IObserver<T>> Observers { get; }
        
        //==============================================================================
        private IObserver<T> Observer { get; }

        
        //==============================================================================
        internal Disposable(List<IObserver<T>> observers, IObserver<T> observer)
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