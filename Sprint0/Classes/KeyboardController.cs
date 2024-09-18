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
        private Link _link;
        private readonly SpriteManager spriteManager;
        private KeyboardState _previousKeyboardState;

        public KeyboardController(Link link)
        {
            _link = link;
            _previousKeyboardState = Keyboard.GetState(); // Save the initial keyboard state
        }

        public void Update()
        {
            KeyboardState currentKeyboardState = Keyboard.GetState();

            // Handle movement
            if (currentKeyboardState.IsKeyDown(Keys.W) || currentKeyboardState.IsKeyDown(Keys.Up))
            {
                _link.Move(Direction.Up);
            }
            if (currentKeyboardState.IsKeyDown(Keys.S) || currentKeyboardState.IsKeyDown(Keys.Down))
            {
                _link.Move(Direction.Down);
            }
            if (currentKeyboardState.IsKeyDown(Keys.A) || currentKeyboardState.IsKeyDown(Keys.Left))
            {
                _link.Move(Direction.Left);
            }
            if (currentKeyboardState.IsKeyDown(Keys.D) || currentKeyboardState.IsKeyDown(Keys.Right))
            {
                _link.Move(Direction.Right);
            }

            // Handle attacking 
            if (IsKeyPressed(currentKeyboardState, Keys.Z) || IsKeyPressed(currentKeyboardState, Keys.N))
            {
                _link.Attack();
            }

            if (IsKeyPressed(currentKeyboardState, Keys.E))
            {
                _link.TakeDamage();
            }


            // Update the previous keyboard state for the next frame
            _previousKeyboardState = currentKeyboardState;
        }

        private bool IsKeyPressed(KeyboardState currentKeyboardState, Keys key)
        {
            return currentKeyboardState.IsKeyDown(key) && _previousKeyboardState.IsKeyUp(key);
        }
    }
}