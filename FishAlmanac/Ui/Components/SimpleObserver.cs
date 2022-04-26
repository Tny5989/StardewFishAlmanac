using System;

namespace FishAlmanac.Ui.Components
{
    public abstract class SimpleObserver<T> : IObserver<T>
    {
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