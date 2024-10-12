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
    public class HandlePlayerBlockCollision
    {

        private Vector2 playerPosition;
        private int playerWidth;
        private int playerHeight;

        public HandlePlayerBlockCollision(Vector2 playerPos, int pWidth, int pHeight)
        {
            playerPosition = playerPos;
            playerWidth = pWidth;
            playerHeight = pHeight;
        }

        public void PlayerBlockCollision(ref Vector2 spritePosition, Vector2 previousPosition, Rectangle blockBoundingBox)
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
    }
}
