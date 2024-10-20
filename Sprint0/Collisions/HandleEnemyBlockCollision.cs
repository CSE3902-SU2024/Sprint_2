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
using Sprint2.Map;
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

        public HandleEnemyBlockCollision(Vector2 blockPos, int eWidth, int eHeight, int bWidth, int bHeight)
        {
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

        public void EnemyBlockCollision(Enemy_Item_Map enemyItemMap, int currentRoomNumber, Vector2 scale) // or List<IEnemy> enemies, Vector2 spritePosition, Vector2 scale
            //public static void HandleCollisions(Link link, Enemy_Item_Map enemyItemMap, int currentRoomNumber, Vector2 scale)
        {
            Rectangle blockBoundingBox = GetScaledRectangle((int)blockPosition.X, (int)blockPosition.Y, blockWidth, blockHeight, scale);
            List<IEnemy> enemiesInRoom = enemyItemMap.GetEnemies(currentRoomNumber);

            // Iterate over each enemy and check for collision
            foreach (IEnemy enemy in enemiesInRoom) //or Enemy enemy in enemies
            {
                Rectangle enemyBoundingBox = GetScaledRectangle((int)enemy.Position.X, (int)enemy.Position.Y, enemy.Width, enemy.Height, scale);

                //int enemyPositionX = (int)enemy.Position.X;
                //int enemyPositionY = (int)enemy.Position.Y;

                Vector2 newEnemyPosition = enemy.Position;

                if (enemy is Keese keese)
                {

                }

                else if (enemy is Dragon dragon)
                {
                    enemyBoundingBox = GetScaledRectangle((int)enemy.Position.X, (int)enemy.Position.Y, 32, 32, scale);

                    if (blockBoundingBox.Intersects(enemyBoundingBox))
                    {
                        Rectangle intersection = Rectangle.Intersect(enemyBoundingBox, blockBoundingBox);

                        // Resolve vertical collision
                        if (intersection.Height < intersection.Width)
                        {
                            if (enemy.Position.Y < blockBoundingBox.Y) // Coming from the top
                            {
                                //enemyPositionY = (int)((int)blockBoundingBox.Top - (enemy.Height * scale.Y));
                                newEnemyPosition.Y = (int)((int)blockBoundingBox.Top - (enemy.Height * scale.Y));
                            }
                            else if (enemy.Position.Y > blockBoundingBox.Y) // Coming from below
                            {
                                //enemyPositionY = blockBoundingBox.Bottom;
                                newEnemyPosition.Y = blockBoundingBox.Bottom;
                            }
                        }
                        // Resolve horizontal collision
                        else
                        {
                            if (enemy.Position.X < blockBoundingBox.X) // Coming from the left
                            {
                                //enemyPositionX -= intersection.Width;
                                newEnemyPosition.X = blockBoundingBox.Left - (enemy.Width * scale.X);
                            }
                            else if (enemy.Position.X > blockBoundingBox.X) // Coming from the right
                            {
                                //enemyPositionX = blockBoundingBox.Right;
                                newEnemyPosition.X = blockBoundingBox.Right;
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
                            if (enemy.Position.Y < blockBoundingBox.Y) // Coming from the top
                            {
                                //enemyPositionY = (int)((int)blockBoundingBox.Top - (enemy.Height * scale.Y));
                                newEnemyPosition.Y = (int)((int)blockBoundingBox.Top - (enemy.Height * scale.Y));
                            }
                            else if (enemy.Position.Y > blockBoundingBox.Y) // Coming from below
                            {
                                //enemyPositionY = blockBoundingBox.Bottom;
                                newEnemyPosition.Y = blockBoundingBox.Bottom;
                            }
                        }
                        // Resolve horizontal collision
                        else
                        {
                            if (enemy.Position.X < blockBoundingBox.X) // Coming from the left
                            {
                                //enemyPositionX -= intersection.Width;
                                newEnemyPosition.X = blockBoundingBox.Left - (enemy.Width * scale.X);
                            }
                            else if (enemyPosition.X > blockBoundingBox.X) // Coming from the right
                            {
                                //enemyPositionX = blockBoundingBox.Right;
                                newEnemyPosition.X = blockBoundingBox.Right;
                            }
                        }
                    }
                }

                enemy.Position = newEnemyPosition;

            }
        }

    }
}