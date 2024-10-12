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
    public class HandleEnemyBlockCollision
    {

        private Vector2 enemyPosition;
        private int enemyWidth;
        private int enemyHeight;

        public HandleEnemyBlockCollision(Vector2 enemyPos, int eWidth, int eHeight)
        {
            enemyPosition = enemyPos;
            enemyWidth = eWidth;
            enemyHeight = eHeight;
        }

        public void PlayerBlockCollision(ref Vector2 spritePosition, Vector2 previousPosition, Rectangle blockBoundingBox)
        {
            Rectangle enemyBoundingBox = new Rectangle((int)spritePosition.X, (int)spritePosition.Y, enemyWidth, enemyHeight);

            if (blockBoundingBox.Intersects(enemyBoundingBox))
            {
                Rectangle intersection = Rectangle.Intersect(enemyBoundingBox, blockBoundingBox);

                // First, resolve vertical collisions
                if (intersection.Height < intersection.Width)
                {
                    if (previousPosition.Y < blockBoundingBox.Y) // Coming from the top
                    {
                        spritePosition.Y = blockBoundingBox.Top - enemyHeight;
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
                        spritePosition.X = blockBoundingBox.Left - enemyWidth;
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
