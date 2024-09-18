using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;  
using Sprint0.Interfaces;


namespace Sprint0.Classes
{
    public class KeyboardController : IController
    {
        private readonly SpriteManager spriteManager;
        private KeyboardState previousState;

        public KeyboardController(SpriteManager spriteManager)
        {
            this.spriteManager = spriteManager;
            previousState = Keyboard.GetState(); // Save the initial keyboard state
        }

        public void Update()
        {
            KeyboardState currentState = Keyboard.GetState();

            // Handle key presses on key down (to prevent multiple triggers)
            if (currentState.IsKeyDown(Keys.D1) && previousState.IsKeyUp(Keys.D1))
            {
                spriteManager.SetSprite(SpriteType.Static);
            }
            else if (currentState.IsKeyDown(Keys.D2) && previousState.IsKeyUp(Keys.D2))
            {
                spriteManager.SetSprite(SpriteType.Animated);
            }
            else if (currentState.IsKeyDown(Keys.D3) && previousState.IsKeyUp(Keys.D3))
            {
                spriteManager.SetSprite(SpriteType.Moving);
            }
            else if (currentState.IsKeyDown(Keys.D4) && previousState.IsKeyUp(Keys.D4))
            {
                spriteManager.SetSprite(SpriteType.MovingAnimated);
            }
            else if (currentState.IsKeyDown(Keys.D0) && previousState.IsKeyUp(Keys.D0))
            {
                System.Environment.Exit(0); // Quit the game
            }
            previousState = currentState; // Update the previous state
        }
    }
}