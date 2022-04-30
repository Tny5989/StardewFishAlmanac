using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using InputButtons = Microsoft.Xna.Framework.Input.Buttons;

namespace FishAlmanac.Ui.Components.Base
{
    public interface IComponent
    {
        //==============================================================================
        public IMonitor Monitor { get; set; }
        

        //==============================================================================
        public void Draw(SpriteBatch b);
        
        //==============================================================================
        public void Update(Rectangle bounds);

        //==============================================================================
        public void HandleScrollWheel(int direction);

        //==============================================================================
        public void HandleLeftClick(int x, int y);

        //==============================================================================
        public void HandleGamepadInput(InputButtons button);
    }
}