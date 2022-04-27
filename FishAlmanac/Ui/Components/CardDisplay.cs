using System;
using System.Collections.Generic;
using FishAlmanac.Ui.Components.Cards;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewValley;
using InputButtons = Microsoft.Xna.Framework.Input.Buttons;

namespace FishAlmanac.Ui.Components
{
    public class CardDisplay : SimpleObserver<string>, IComponent
    {
        //==============================================================================
        public Rectangle Bounds { get; set; }

        //==============================================================================
        public Color Color { get; set; }

        //==============================================================================
        public IMonitor Monitor { get; set; }

        //==============================================================================
        public List<Card> Cards { get; set; }

        //==============================================================================
        private NavBar NavBar { get; set; }

        //==============================================================================
        private Separator Separator { get; set; }

        //==============================================================================
        private int Index { get; set; }


        //==============================================================================
        public CardDisplay(IMonitor monitor)
        {
            Bounds = new Rectangle();
            Color = Color.White;
            Monitor = monitor;
            Cards = new List<Card>();
            NavBar = new NavBar(monitor);
            Separator = new Separator(monitor);
            Index = 0;

            NavBar.Subscribe(this);
        }

        //==============================================================================
        public void Draw(SpriteBatch b)
        {
            PositionNavBar();
            PositionSeparator();
            PositionCards();

            UpdateNavBar();

            if (Cards.Count > 0)
            {
                Cards[Index].Draw(b);
            }

            Separator.Draw(b);
            NavBar.Draw(b);
        }

        //==============================================================================
        public void HandleScrollWheel(int direction)
        {
            NavBar.HandleScrollWheel(direction);
            if (Cards.Count > 0)
            {
                Cards[Index].HandleScrollWheel(direction);
            }
        }

        //==============================================================================
        public void HandleLeftClick(int x, int y)
        {
            NavBar.HandleLeftClick(x, y);
            if (Cards.Count > 0)
            {
                Cards[Index].HandleLeftClick(x, y);
            }
        }

        //==============================================================================
        public void HandleGamepadInput(InputButtons button)
        {
            NavBar.HandleGamepadInput(button);
        }

        //==============================================================================
        public override void OnNext(string value)
        {
            switch (value)
            {
                case "previous":
                    Index -= 1;
                    break;
                case "next":
                    Index += 1;
                    break;
            }

            Index = Math.Clamp(Index, 0, Cards.Count - 1);
        }

        //==============================================================================
        private void PositionNavBar()
        {
            NavBar.Bounds = new Rectangle(Bounds.X, Bounds.Y + Bounds.Height - Game1.tileSize, Bounds.Width,
                Game1.tileSize);
        }

        //==============================================================================
        private void PositionSeparator()
        {
            Separator.Bounds = new Rectangle(Bounds.X + 10, Bounds.Y + Bounds.Height - Game1.tileSize - 1,
                Bounds.Width - 20, 1);
        }

        //==============================================================================
        private void PositionCards()
        {
            foreach (var card in Cards)
            {
                card.Bounds = new Rectangle(Bounds.X, Bounds.Y, Bounds.Width, Bounds.Height - Game1.tileSize);
            }
        }

        //==============================================================================
        private void UpdateNavBar()
        {
            NavBar.ShowPrevious = Index > 0;
            NavBar.ShowNext = Index < Cards.Count - 1;
        }
    }
}