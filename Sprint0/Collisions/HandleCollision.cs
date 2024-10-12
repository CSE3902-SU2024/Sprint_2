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
    public class HandleCollision
    {

        private Vector2 playerPosition;
        private Vector2 wallPosition;
        private int playerWidth;
        private int playerHeight;
        private int wallWidth;
        private int wallHeight;

        public HandleCollision(Vector2 playerPos, Vector2 wallPos, int pWidth, int pHeight, int wWidth, int wHeight)
        {
            playerPosition = playerPos;
            wallPosition = wallPos;
            playerWidth = pWidth;
            playerHeight = pHeight;
            wallWidth = wWidth;
            wallHeight = wHeight;
        }

        public void HandleCollisionBlock(ref Vector2 spritePosition, Vector2 previousPosition, Rectangle blockBoundingBox)
        {
            Rectangle playerBoundingBox = new Rectangle((int)spritePosition.X, (int)spritePosition.Y, playerWidth, playerHeight);

            if (blockBoundingBox.Intersects(playerBoundingBox))
            {
                Rectangle intersection = Rectangle.Intersect(playerBoundingBox, blockBoundingBox);

                // First, resolve vertical collisions
                if (intersection.Height < intersection.Width)
                {
                    if (previousPosition.Y < blockBoundingBox.Y) // Coming from the top
                    {
                        spritePosition.Y = blockBoundingBox.Top - playerHeight;
                    }
                    else if (previousPosition.Y > blockBoundingBox.Y) // Coming from below
                    {
                        spritePosition.Y = blockBoundingBox.Bottom;
                    }
                }
                // Then, resolve horizontal collisions
                else
                {
                    if (previousPosition.X < blockBoundingBox.X) // Coming from the left
                    {
                        spritePosition.X = blockBoundingBox.Left - playerWidth;
                    }
                    else if (previousPosition.X > blockBoundingBox.X) // Coming from the right
                    {
                        spritePosition.X = blockBoundingBox.Right;
                    }
                }
            }
        }

        public void HandlePlayerWallCollision(ref Vector2 playerPosition, Vector2 playerVelocity)
        {
            Rectangle playerBoundingBox = new Rectangle((int)playerPosition.X, (int)playerPosition.Y, playerWidth, playerHeight);
            Rectangle wallBoundingBox = new Rectangle((int)wallPosition.X, (int)wallPosition.Y, wallWidth, wallHeight);

            Rectangle intersection = Rectangle.Empty;

            // Check if there is an intersection between the player and the wall
            if (playerBoundingBox.Intersects(wallBoundingBox))
            {
                // Get the intersection area
                intersection = Rectangle.Intersect(playerBoundingBox, wallBoundingBox);

                // Handle the collision based on the direction of the player velocity
                if (intersection.Width < intersection.Height)
                {
                    // Horizontal collision
                    if (playerVelocity.X > 0) // Moving right
                    {
                        playerPosition.X = wallBoundingBox.Left - playerWidth;
                    }
                    else if (playerVelocity.X < 0) // Moving left
                    {
                        playerPosition.X = wallBoundingBox.Right;
                    }
                }
                else
                {
                    // Vertical collision
                    if (playerVelocity.Y > 0) // Moving down
                    {
                        playerPosition.Y = wallBoundingBox.Top - playerHeight;
                    }
                    else if (playerVelocity.Y < 0) // Moving up
                    {
                        playerPosition.Y = wallBoundingBox.Bottom;
                    }
                }
            }

            // This is template for other collisions
            //public void HandleEnemyWallCollision(ref Vector2 playerPosition, Vector2 playerVelocity)
            //{
            //    Rectangle playerBoundingBox = new Rectangle((int)playerPosition.X, (int)playerPosition.Y, playerWidth, playerHeight);
            //    Rectangle wallBoundingBox = new Rectangle((int)wallPosition.X, (int)wallPosition.Y, wallWidth, wallHeight);
            //
        }
    }
}
