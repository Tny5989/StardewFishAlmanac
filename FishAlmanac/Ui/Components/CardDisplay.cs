using System;
using FishAlmanac.Ui.Components.Base;
using FishAlmanac.Ui.Components.Cards;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewValley;
using InputButtons = Microsoft.Xna.Framework.Input.Buttons;

namespace FishAlmanac.Ui.Components
{
    public class CardDisplay : SimpleObserver<string>
    {
        //==============================================================================
        private enum Indices
        {
            NavBar,
            Separator,
            Cards
        }

        //==============================================================================
        private int Index { get; set; }

        //==============================================================================
        private int Count { get; set; }


        //==============================================================================
        public CardDisplay(IMonitor monitor) : base(monitor)
        {
            Index = 0;
            Count = 0;

            Components.Add(new NavBar(monitor));
            Components.Add(new Separator(monitor));

            GetComponent<NavBar>((int)Indices.NavBar).Subscribe(this);
        }

        //==============================================================================
        public override void Draw(SpriteBatch b)
        {
            UpdateNavBar();
            base.Draw(b);
        }

        //==============================================================================
        public override void Update(Rectangle bounds)
        {
            base.Update(bounds);

            PositionNavBar();
            PositionSeparator();
            PositionCards();
        }

        //==============================================================================
        public override void OnNext(string value)
        {
            var mod = value switch
            {
                "previous" => -1,
                "next" => 1,
                _ => 0
            };

            GetComponent<Card>((int)Indices.Cards + Index).Visible = false;
            Index = Math.Clamp(Index + mod, 0, Count - 1);
            GetComponent<Card>((int)Indices.Cards + Index).Visible = true;
        }

        //==============================================================================
        public void AddCard(Card card)
        {
            card.Visible = false;
            Components.Add(card);
            ++Count;
            OnNext("");
        }

        //==============================================================================
        public override bool HandleScrollWheel(int direction)
        {
            for (var i = Components.Count - 1; i >= 0; --i)
            {
                if (Components[i].HandleScrollWheel(direction))
                {
                    return true;
                }
            }

            return false;
        }

        //==============================================================================
        private void PositionNavBar()
        {
            GetComponent<NavBar>((int)Indices.NavBar).Update(new Rectangle(Bounds.X,
                Bounds.Y + Bounds.Height - Game1.tileSize, Bounds.Width, Game1.tileSize));
        }

        //==============================================================================
        private void PositionSeparator()
        {
            GetComponent<Separator>((int)Indices.Separator).Update(new Rectangle(Bounds.X + 10,
                Bounds.Y + Bounds.Height - Game1.tileSize - 1, Bounds.Width - 20, 1));
        }

        //==============================================================================
        private void PositionCards()
        {
            for (var i = 0; i < Count; ++i)
            {
                GetComponent<Card>((int)Indices.Cards + i).Update(new Rectangle(Bounds.X, Bounds.Y, Bounds.Width,
                    Bounds.Height - Game1.tileSize));
            }
        }

        //==============================================================================
        private void UpdateNavBar()
        {
            GetComponent<NavBar>(0).ShowButtons(Index > 0, Index < Count - 1);
        }
    }
}