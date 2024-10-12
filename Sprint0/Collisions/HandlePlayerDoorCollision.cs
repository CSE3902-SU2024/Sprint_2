using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.Interfaces;
using Sprint0.Player;

namespace Sprint2.Collisions
{
    public class HandlePlayerDoorCollision
    {

        private Vector2 playerPosition;
        private Vector2 doorPosition;
        private int playerWidth;
        private int playerHeight;
        private int doorWidth;
        private int doorHeight;

        public HandlePlayerDoorCollision(Vector2 playerPos, Vector2 doorPos, int pWidth, int pHeight, int wWidth, int wHeight)
        {
            playerPosition = playerPos;
            doorPosition = doorPos;
            playerWidth = pWidth;
            playerHeight = pHeight;
            doorWidth = wWidth;
            doorHeight = wHeight;
        }
        private static Rectangle GetScaledRectangle(int x, int y, int width, int height)
        {
            return new Rectangle(
                x,
                y,
                (int)(width),
                (int)(height)
            );
        }

        public void PlayerDoorCollision(ref Vector2 playerPosition, float playerVelocity)
        {
            Rectangle playerBoundingBox = GetScaledRectangle((int)playerPosition.X, (int)playerPosition.Y, playerWidth, playerHeight);
            Rectangle doorBoundingBox = GetScaledRectangle((int)doorPosition.X, (int)doorPosition.Y, doorWidth, doorHeight);

            Rectangle intersection = Rectangle.Empty;

            // Check if there is an intersection between the player and the wall
            if (playerBoundingBox.Intersects(doorBoundingBox))
            {
                // Get the intersection area
                intersection = Rectangle.Intersect(playerBoundingBox, doorBoundingBox);

                // Handle the collision based on the direction of the player velocity
                if (intersection.Width < intersection.Height)
                {
                    // Horizontal collision
                    if (playerVelocity > 0) // Moving right
                    {
                        playerPosition.X = doorBoundingBox.Left - playerWidth;
                    }
                    else if (playerVelocity < 0) // Moving left
                    {
                        playerPosition.X = doorBoundingBox.Right;
                    }
                }
                else
                {
                    // Vertical collision
                    if (playerVelocity > 0) // Moving down
                    {
                        playerPosition.Y = doorBoundingBox.Top - playerHeight;
                    }
                    else if (playerVelocity < 0) // Moving up
                    {
                        playerPosition.Y = doorBoundingBox.Bottom;
                    }
                }
            }
        }

    }
}
