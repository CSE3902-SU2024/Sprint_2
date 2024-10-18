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
using Sprint2.Enemy;
using static System.Formats.Asn1.AsnWriter;

namespace Sprint2.Collisions
{
    public class HandleEnemyBlockCollision
    {

        private Vector2 enemyPosition;
        private Vector2 blockPosition;
        private int enemyWidth;
        private int enemyHeight;
        private int blockWidth;
        private int blockHeight;

        public HandleEnemyBlockCollision(Vector2 enemyPos, Vector2 blockPos, int eWidth, int eHeight, int bWidth, int bHeight)
        {
            enemyPosition = enemyPos;
            blockPosition = blockPos;
            enemyWidth = eWidth;
            enemyHeight = eHeight;
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

        public void EnemyBlockCollision(IEnemy enemy, Vector2 spritePosition, Vector2 scale) // or List<IEnemy> enemies, Vector2 spritePosition, Vector2 scale
        {
            Rectangle blockBoundingBox = GetScaledRectangle((int)blockPosition.X, (int)blockPosition.Y, blockWidth, blockHeight, scale);

            // Iterate over each enemy and check for collision
            //foreach (IEnemy enemy in enemies) //or Enemy enemy in enemies
            //{
            Rectangle enemyBoundingBox = GetScaledRectangle((int)enemyPosition.X, (int)enemyPosition.Y, enemyWidth, enemyHeight, scale);

            if (enemy is Keese keese)
            {

            }

            else if (enemy is Dragon dragon)
            {
                enemyBoundingBox = GetScaledRectangle((int)enemyPosition.X, (int)enemyPosition.Y, 32, 32, scale);

                if (blockBoundingBox.Intersects(enemyBoundingBox))
                {
                    Rectangle intersection = Rectangle.Intersect(enemyBoundingBox, blockBoundingBox);

                    // Resolve vertical collision
                    if (intersection.Height < intersection.Width)
                    {
                        if (enemyPosition.Y < blockBoundingBox.Y) // Coming from the top
                        {
                            enemyPosition.Y = blockBoundingBox.Top - (enemyHeight * scale.Y);
                        }
                        else if (enemyPosition.Y > blockBoundingBox.Y) // Coming from below
                        {
                            enemyPosition.Y = blockBoundingBox.Bottom;
                        }
                    }
                    // Resolve horizontal collision
                    else
                    {
                        if (enemyPosition.X < blockBoundingBox.X) // Coming from the left
                        {
                            enemyPosition.X -= intersection.Width;
                        }
                        else if (enemyPosition.X > blockBoundingBox.X) // Coming from the right
                        {
                            enemyPosition.X = blockBoundingBox.Right;
                        }
                    }
                }
            }

            else
            {
                if (blockBoundingBox.Intersects(enemyBoundingBox))
                {
                    Rectangle intersection = Rectangle.Intersect(enemyBoundingBox, blockBoundingBox);

                    // Resolve vertical collision
                    if (intersection.Height < intersection.Width)
                    {
                        if (enemyPosition.Y < blockBoundingBox.Y) // Coming from the top
                        {
                            enemyPosition.Y = blockBoundingBox.Top - (enemyHeight * scale.Y);
                        }
                        else if (enemyPosition.Y > blockBoundingBox.Y) // Coming from below
                        {
                            enemyPosition.Y = blockBoundingBox.Bottom;
                        }
                    }
                    // Resolve horizontal collision
                    else
                    {
                        if (enemyPosition.X < blockBoundingBox.X) // Coming from the left
                        {
                            enemyPosition.X -= intersection.Width;
                        }
                        else if (enemyPosition.X > blockBoundingBox.X) // Coming from the right
                        {
                            enemyPosition.X = blockBoundingBox.Right;
                        }
                    }
                }
            }

            //}
        }

    }
}