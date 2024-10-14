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
using static System.Formats.Asn1.AsnWriter;

namespace Sprint2.Collisions
{
    public class HandlePlayerBlockCollision
    {

        private Vector2 playerPosition;
        private Vector2 blockPosition;
        private int playerWidth;
        private int playerHeight;
        private int blockWidth;
        private int blockHeight;

        public HandlePlayerBlockCollision(Vector2 playerPos, Vector2 blockPos, int pWidth, int pHeight, int bWidth, int bHeight)
        {
            playerPosition = playerPos;
            blockPosition = blockPos;
            playerWidth = pWidth;
            playerHeight = pHeight;
            blockWidth = bWidth;
            blockHeight = bHeight;
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

        public void PlayerBlockCollision(ref Vector2 spritePosition, Vector2 previousPosition, Vector2 scale)
        {
            //Rectangle playerBoundingBox = new Rectangle((int)spritePosition.X, (int)spritePosition.Y, playerWidth, playerHeight);
            Rectangle playerBoundingBox = GetScaledRectangle((int)playerPosition.X, (int)playerPosition.Y, playerWidth, playerHeight, scale);
            Rectangle blockBoundingBox = GetScaledRectangle((int)blockPosition.X, (int)blockPosition.Y, blockWidth, blockHeight, scale);

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
