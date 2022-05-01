using System;
using System.Collections.Generic;
using FishAlmanac.Ui.Components.Base;
using FishAlmanac.Ui.Components.Buttons;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewValley;
using InputButtons = Microsoft.Xna.Framework.Input.Buttons;

namespace FishAlmanac.Ui.Components
{
    public class NavBar : SimpleObserver<Button>, IObservable<string>
    {
        //==============================================================================
        private enum Indices
        {
            PreviousButton,
            NextButton
        }

        //==============================================================================
        private Orientation Orientation { get; }

        //==============================================================================
        private List<IObserver<string>> Observers { get; }


        //==============================================================================
        public NavBar(IMonitor monitor) : base(monitor)
        {
            Orientation = Orientation.Horizontal;
            Observers = new List<IObserver<string>>();
            CreateNavButtons();
            ShowButtons(false, false);
        }

        //==============================================================================
        public override void Update(Rectangle bounds)
        {
            base.Update(bounds);
            PositionNavButtons();
        }

        //==============================================================================
        public override bool HandleScrollWheel(int direction)
        {
            base.HandleScrollWheel(direction);
            var component = direction switch
            {
                > 0 => Components[(int)Indices.PreviousButton],
                _ => Components[(int)Indices.NextButton]
            };
            component.HandleLeftClick(component.Bounds.X, component.Bounds.Y);
            return true;
        }

        //==============================================================================
        public override void OnNext(Button value)
        {
            if (value == Components[(int)Indices.PreviousButton])
            {
                foreach (var observer in Observers)
                {
                    observer.OnNext("previous");
                }
            }
            else if (value == Components[(int)Indices.NextButton])
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

            return new Disposable<string>(Monitor, Observers, observer);
        }

        //==============================================================================
        private void CreateNavButtons()
        {
            switch (Orientation)
            {
                case Orientation.Vertical:
                    Components.Add(new UpButton(Monitor));
                    Components.Add(new DownButton(Monitor));
                    break;
                case Orientation.Horizontal:
                default:
                    Components.Add(new LeftButton(Monitor));
                    Components.Add(new RightButton(Monitor));
                    break;
            }

            GetComponent<ArrowButton>((int)Indices.PreviousButton).Subscribe(this);
            GetComponent<ArrowButton>((int)Indices.NextButton).Subscribe(this);
        }

        //==============================================================================
        public void ShowButtons(bool previous, bool next)
        {
            Components[(int)Indices.PreviousButton].Visible = previous;
            Components[(int)Indices.NextButton].Visible = next;
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
                    Components[(int)Indices.PreviousButton]
                        .Update(new Rectangle(x, y, Bounds.Width - 20, Bounds.Width - 20));
                    y = Bounds.Y + Bounds.Height - 10 - (Bounds.Width - 20);
                    Components[(int)Indices.NextButton]
                        .Update(new Rectangle(x, y, Bounds.Width - 20, Bounds.Width - 20));
                    break;
                }
                case Orientation.Horizontal:
                default:
                {
                    var x = Bounds.X + 10;
                    var y = Bounds.Y + 10;
                    Components[(int)Indices.PreviousButton]
                        .Update(new Rectangle(x, y, Bounds.Height - 20, Bounds.Height - 20));
                    x = Bounds.X + Bounds.Width - 10 - (Bounds.Height - 20);
                    Components[(int)Indices.NextButton]
                        .Update(new Rectangle(x, y, Bounds.Height - 20, Bounds.Height - 20));
                    break;
                }
            }
        }
    }
}