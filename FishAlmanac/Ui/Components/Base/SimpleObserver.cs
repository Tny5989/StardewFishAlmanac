using System;
using StardewModdingAPI;

namespace FishAlmanac.Ui.Components.Base
{
    public abstract class SimpleObserver<T> : Component, IObserver<T>
    {
        //==============================================================================
        protected SimpleObserver(IMonitor monitor) : base(monitor)
        {
        }

        //==============================================================================
        public void OnCompleted()
        {
        }

        //==============================================================================
        public void OnError(Exception error)
        {
        }

        //==============================================================================
        public abstract void OnNext(T value);
    }
}