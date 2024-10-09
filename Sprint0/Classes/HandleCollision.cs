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
using Sprint0.Classes;
using Sprint0.Interfaces;
using Sprint0.Player;

namespace Sprint0.Classes
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
                int blockX = blockBoundingBox.X;
                int blockY = blockBoundingBox.Y;
                int blockWidth = blockBoundingBox.Width;
                int blockHeight = blockBoundingBox.Height;

                // Check which side the collision is happening on
                if (previousPosition.Y < blockY) // Coming from the top
                {
                    spritePosition.Y = blockY - playerHeight; // Prevent moving down into the block
                }
                else if (previousPosition.Y > blockY) // Coming from below
                {
                    spritePosition.Y = blockY + blockHeight; // Prevent moving up into the block
                }

                if (previousPosition.X < blockX) // Coming from the left
                {
                    spritePosition.X = blockX - playerWidth; // Prevent moving right into the block
                }
                else if (previousPosition.X > blockX) // Coming from the right
                {
                    spritePosition.X = blockX + blockWidth; // Prevent moving left into the block
                }
            }
        }

        public void HandlePlayerWallCollision(ref Vector2 playerPosition, Vector2 playerVelocity)
        {
            Rectangle playerBoundingBox = new Rectangle((int)playerPosition.X, (int)playerPosition.Y, playerWidth, playerHeight);
            Rectangle wallBoundingBox = new Rectangle((int)wallPosition.X, (int)wallPosition.Y, wallWidth, wallHeight);

            if (playerBoundingBox.Intersects(wallBoundingBox))
            {
                if (playerVelocity.X > 0) // Moving right
                {
                    playerPosition.X = wallBoundingBox.Left - playerBoundingBox.Width;
                }
                else if (playerVelocity.X < 0) // Moving left
                {
                    playerPosition.X = wallBoundingBox.Right;
                }

                if (playerVelocity.Y > 0) // Moving down
                {
                    playerPosition.Y = wallBoundingBox.Top - playerBoundingBox.Height;
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
        //}
    }
}
