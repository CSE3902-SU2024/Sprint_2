using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprint0.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.Classes
{
    public class MouseController  : IController
    {
        private readonly SpriteManager spriteManager;
        private MouseState previousState;
        private readonly GraphicsDevice graphicsDevice;

        public MouseController(SpriteManager spriteManager, GraphicsDevice graphicsDevice)
        {
            this.spriteManager = spriteManager;
            this.graphicsDevice = graphicsDevice;
            previousState = Mouse.GetState(); // Get the initial mouse state
        }

        public void Update()
        {
            MouseState currentState = Mouse.GetState();

            // Left mouse click
            if (currentState.LeftButton == ButtonState.Pressed && previousState.LeftButton == ButtonState.Released)
            {
                HandleLeftClick(currentState);
            }

            // Right mouse click (quit the game)
            if (currentState.RightButton == ButtonState.Pressed && previousState.RightButton == ButtonState.Released)
            {
                System.Environment.Exit(0); // Close the game
            }

            previousState = currentState; // Update previous state
        }

        private void HandleLeftClick(MouseState currentState)
        {
            int mouseX = currentState.X;
            int mouseY = currentState.Y;
            int screenWidth = graphicsDevice.Viewport.Width;
            int screenHeight = graphicsDevice.Viewport.Height;

            // Determine which quadrant the click is in
            if (mouseX < screenWidth / 2 && mouseY < screenHeight / 2)
            {
                // Top-left (Quad1)
                spriteManager.SetSprite(SpriteType.Static);
            }
            else if (mouseX >= screenWidth / 2 && mouseY < screenHeight / 2)
            {
                // Top-right (Quad2)
                spriteManager.SetSprite(SpriteType.Animated);
            }
            else if (mouseX < screenWidth / 2 && mouseY >= screenHeight / 2)
            {
                // Bottom-left (Quad3)
                spriteManager.SetSprite(SpriteType.Moving);
            }
            else if (mouseX >= screenWidth / 2 && mouseY >= screenHeight / 2)
            {
                // Bottom-right (Quad4)
                spriteManager.SetSprite(SpriteType.MovingAnimated);
            }
        }
    }
}
