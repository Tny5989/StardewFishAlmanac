using System;
using System.Collections.Generic;
using FishAlmanac.Ui.Components.Buttons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using InputButtons = Microsoft.Xna.Framework.Input.Buttons;

namespace FishAlmanac.Ui.Components
{
    public class NavBar : SimpleObserver<Button>, IComponent, IObservable<string>
    {
        //==============================================================================
        public Rectangle Bounds { get; set; }

        //==============================================================================
        public Color Color { get; set; }

        //==============================================================================
        public IMonitor Monitor { get; set; }

        //==============================================================================
        public Orientation Orientation { get; set; }

        //==============================================================================
        public bool ShowPrevious { get; set; }

        //==============================================================================
        public bool ShowNext { get; set; }

        //==============================================================================
        private List<IObserver<string>> Observers { get; }

        //==============================================================================
        private ArrowButton PreviousButton { get; set; }

        //==============================================================================
        private ArrowButton NextButton { get; set; }


        //==============================================================================
        public NavBar(IMonitor monitor)
        {
            Bounds = new Rectangle();
            Color = Color.White;
            Monitor = monitor;
            Orientation = Orientation.Horizontal;
            ShowPrevious = false;
            ShowNext = false;
            Observers = new List<IObserver<string>>();
            CreateNavButtons();
        }

        //==============================================================================
        public void Draw(SpriteBatch b)
        {
            PositionNavButtons();
            if (ShowPrevious)
            {
                PreviousButton.Draw(b);
            }

            if (ShowNext)
            {
                NextButton.Draw(b);
            }
        }

        //==============================================================================
        public void HandleScrollWheel(int direction)
        {
            switch (direction)
            {
                case > 0:
                    PreviousButton.HandleLeftClick(PreviousButton.Bounds.X, PreviousButton.Bounds.Y);
                    break;
                default:
                    NextButton.HandleLeftClick(NextButton.Bounds.X, NextButton.Bounds.Y);
                    break;
            }
        }

        //==============================================================================
        public void HandleLeftClick(int x, int y)
        {
            PreviousButton.HandleLeftClick(x, y);
            NextButton.HandleLeftClick(x, y);
        }

        //==============================================================================
        public void HandleGamepadInput(InputButtons button)
        {
            var left = (button & InputButtons.LeftTrigger) == InputButtons.LeftTrigger;
            var right = (button & InputButtons.RightTrigger) == InputButtons.RightTrigger;
            if (left)
            {
                PreviousButton.HandleLeftClick(PreviousButton.Bounds.X, PreviousButton.Bounds.Y);
            }
            if (right)
            {
                NextButton.HandleLeftClick(NextButton.Bounds.X, NextButton.Bounds.Y);
            }
        }

        //==============================================================================
        public override void OnNext(Button value)
        {
            if (value == PreviousButton)
            {
                foreach (var observer in Observers)
                {
                    observer.OnNext("previous");
                }
            }
            else if (value == NextButton)
            {
                foreach (var observer in Observers)
                {
                    observer.OnNext("next");
                }
            }
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

        //==============================================================================
        private void CreateNavButtons()
        {
            switch (Orientation)
            {
                case Orientation.Vertical:
                    PreviousButton = new UpButton(Monitor);
                    NextButton = new DownButton(Monitor);
                    break;
                case Orientation.Horizontal:
                default:
                    PreviousButton = new LeftButton(Monitor);
                    NextButton = new RightButton(Monitor);
                    break;
            }

            PreviousButton.Subscribe(this);
            NextButton.Subscribe(this);
        }

        //==============================================================================
        private void PositionNavButtons()
        {
            switch (Orientation)
            {
                case Orientation.Vertical:
                {
                    var x = Bounds.X + 10;
                    var y = Bounds.Y + 10;
                    PreviousButton.Bounds = new Rectangle(x, y, Bounds.Width - 20, Bounds.Width - 20);
                    y = Bounds.Y + Bounds.Height - 10 - (Bounds.Width - 20);
                    NextButton.Bounds = new Rectangle(x, y, Bounds.Width - 20, Bounds.Width - 20);
                    break;
                }
                case Orientation.Horizontal:
                default:
                {
                    var x = Bounds.X + 10;
                    var y = Bounds.Y + 10;
                    PreviousButton.Bounds = new Rectangle(x, y, Bounds.Height - 20, Bounds.Height - 20);
                    x = Bounds.X + Bounds.Width - 10 - (Bounds.Height - 20);
                    NextButton.Bounds = new Rectangle(x, y, Bounds.Height - 20, Bounds.Height - 20);
                    break;
                }
            }
        }
    }
}