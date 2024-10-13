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
    public class HandlePlayerWallCollision
    {

        private Vector2 playerPosition;
        private Vector2 wallPosition;
        private int playerWidth;
        private int playerHeight;
        private int wallWidth;
        private int wallHeight;

        public HandlePlayerWallCollision(Vector2 playerPos, Vector2 wallPos, int pWidth, int pHeight, int wWidth, int wHeight)
        {
            playerPosition = playerPos;
            wallPosition = wallPos;
            playerWidth = pWidth;
            playerHeight = pHeight;
            wallWidth = wWidth;
            wallHeight = wHeight;
        }

        private static Rectangle GetScaledRectangle(int x, int y, int width, int height, Vector2 scale)
        {
            return new Rectangle(
                x,
                y,
                (int)(width * scale.X),
                (int)(height * scale.Y)
            );
        }

        public void PlayerWallCollision(ref Vector2 playerPosition, Vector2 previousPosition, Vector2 scale)
        {
            Rectangle playerBoundingBox = GetScaledRectangle((int)playerPosition.X, (int)playerPosition.Y, playerWidth, playerHeight, scale);
            Rectangle wallBoundingBox = GetScaledRectangle((int)wallPosition.X, (int)wallPosition.Y, wallWidth, wallHeight, scale);

            Rectangle intersection = Rectangle.Empty;

            // Check if there is an intersection between the player and the wall
            if (playerBoundingBox.Intersects(wallBoundingBox))
            {
                // Get the intersection area
                intersection = Rectangle.Intersect(playerBoundingBox, wallBoundingBox);

                Vector2 movementDirection = playerPosition - previousPosition;

                // Handle the collision based on the direction of the player velocity
                if (intersection.Width < intersection.Height)
                {
                    // Horizontal collision
                    if (movementDirection.X > 0) // Moving right
                    {
                        playerPosition.X = wallBoundingBox.Left - (playerWidth * scale.X);
                    }
                    else if (movementDirection.X < 0) // Moving left
                    {
                        playerPosition.X = wallBoundingBox.Right;
                    }
                }
                else
                {
                    // Vertical collision
                    if (movementDirection.Y > 0) // Moving down
                    {
                        playerPosition.Y = wallBoundingBox.Top - (playerHeight * scale.Y);
                    }
                    else if (movementDirection.Y < 0) // Moving up
                    {
                        playerPosition.Y = wallBoundingBox.Bottom;
                    }
                }
            }
        }

    }
}
