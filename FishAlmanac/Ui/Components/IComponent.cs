using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using InputButtons = Microsoft.Xna.Framework.Input.Buttons;

namespace FishAlmanac.Ui.Components
{
    public interface IComponent
    {
        //==============================================================================
        public Rectangle Bounds { get; set; }

        //==============================================================================
        public Color Color { get; set; }

        //==============================================================================
        public IMonitor Monitor { get; set; }


        //==============================================================================
        public void Draw(SpriteBatch b);

        //==============================================================================
        public void HandleScrollWheel(int direction);

        //==============================================================================
        public void HandleLeftClick(int x, int y);

        //==============================================================================
        public void HandleGamepadInput(InputButtons button);
    }
}