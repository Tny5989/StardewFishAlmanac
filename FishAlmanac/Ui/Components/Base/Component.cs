using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using InputButtons = Microsoft.Xna.Framework.Input.Buttons;

namespace FishAlmanac.Ui.Components.Base
{
    public abstract class Component : IComponent
    {
        //==============================================================================
        public IMonitor Monitor { get; set; }
        
        //==============================================================================
        public Rectangle Bounds { get; private set; }
        
        //==============================================================================
        public Color Color { get; set; }
        
        //==============================================================================
        public bool Visible { get; set; }
        
        //==============================================================================
        protected List<Component> Components { get; }
        
        
        //==============================================================================
        protected Component(IMonitor monitor)
        {
            Monitor = monitor;
            Bounds = new Rectangle();
            Color = Color.White;
            Visible = true;
            Components = new List<Component>();
        }

        //==============================================================================
        protected T GetComponent<T>(int i) where T : Component
        {
            if (i < 0 || i >= Components.Count)
            {
                return null;
            }

            return (Components[i] as T);
        }

        //==============================================================================
        public virtual void Draw(SpriteBatch b)
        {
            if (!Visible)
            {
                return;
            }
            
            foreach (var component in Components.Where(component => component.Visible))
            {
                component.Draw(b);
            }
        }

        //==============================================================================
        public virtual void Update(Rectangle bounds)
        {
            Bounds = bounds;
        }

        //==============================================================================
        public virtual void HandleScrollWheel(int direction)
        {
            if (!Visible)
            {
                return;
            }

            foreach (var component in Components.Where(component => component.Visible))
            {
                component.HandleScrollWheel(direction);
            }
        }

        //==============================================================================
        public virtual void HandleLeftClick(int x, int y)
        {
            if (!Visible)
            {
                return;
            }

            foreach (var component in Components.Where(component => component.Visible))
            {
                component.HandleLeftClick(x, y);
            }
        }

        //==============================================================================
        public virtual void HandleGamepadInput(InputButtons button)
        {
            if (!Visible)
            {
                return;
            }

            foreach (var component in Components.Where(component => component.Visible))
            {
                component.HandleGamepadInput(button);
            }
        }
    }
}