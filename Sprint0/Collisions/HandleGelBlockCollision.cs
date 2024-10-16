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
    public class HandleGelBlockCollision
    {

        private Vector2 gelPosition;
        private Vector2 blockPosition;
        private int gelWidth;
        private int gelHeight;
        private int blockWidth;
        private int blockHeight;

        public HandleGelBlockCollision(Vector2 gelPos, Vector2 blockPos, int gWidth, int gHeight, int bWidth, int bHeight)
        {
            gelPosition = gelPos;
            blockPosition = blockPos;
            gelWidth = gWidth;
            gelHeight = gHeight;
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

        public void GelBlockCollision(ref Vector2 spritePosition, Vector2 scale)
        {
            Rectangle gelBoundingBox = GetScaledRectangle((int)gelPosition.X, (int)gelPosition.Y, gelWidth, gelHeight, scale);
            Rectangle blockBoundingBox = GetScaledRectangle((int)blockPosition.X, (int)blockPosition.Y, blockWidth, blockHeight, scale);

            if (blockBoundingBox.Intersects(gelBoundingBox))
            {
                Rectangle intersection = Rectangle.Intersect(gelBoundingBox, blockBoundingBox);

                // First, resolve vertical collisions
                if (intersection.Height < intersection.Width)
                {
                    if (spritePosition.Y < blockBoundingBox.Y) // Coming from the top
                    {
                        spritePosition.Y = blockBoundingBox.Top - (gelHeight * scale.Y);
                    }
                    else if (spritePosition.Y > blockBoundingBox.Y) // Coming from below
                    {
                        spritePosition.Y = blockBoundingBox.Bottom;
                    }
                }
                // Then, resolve horizontal collisions
                else
                {
                    if (spritePosition.X < blockBoundingBox.X) // Coming from the left
                    {
                        spritePosition.X -= intersection.Width;
                    }
                    else if (spritePosition.X > blockBoundingBox.X) // Coming from the right
                    {
                        spritePosition.X = blockBoundingBox.Right;
                    }
                }
            }
        }
    }
}