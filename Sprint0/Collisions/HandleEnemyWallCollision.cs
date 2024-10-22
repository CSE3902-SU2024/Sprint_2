//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection.Metadata;
//using System.Security.Cryptography.X509Certificates;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.Graphics;
//using Sprint0.Interfaces;
//using Sprint0.Player;
//using Sprint2.Enemy;
//using Sprint2.Map;
//using static System.Formats.Asn1.AsnWriter;

//namespace Sprint2.Collisions
//{
//    public class HandleEnemyWallCollision
//    {

//        private Vector2 enemyPosition;
//        private Vector2 blockPosition;
//        private int enemyWidth;
//        private int enemyHeight;
//        private int blockWidth;
//        private int blockHeight;
//        private Rectangle wallLeftBoundingBox;
//        private Rectangle wallRightBoundingBox;
//        private Rectangle wallUpBoundingBox;
//        private Rectangle wallDownBoundingBox;
//        private Rectangle dragonBoundingBox;
//        private Link _link;

//        public HandleEnemyWallCollision(Vector2 blockPos, int eWidth, int eHeight, int bWidth, int bHeight)
//        {
//            blockPosition = blockPos;
//            enemyWidth = eWidth;
//            enemyHeight = eHeight;
//            blockWidth = bWidth;
//            blockHeight = bHeight;
//        }

//        private static Rectangle GetScaledRectangle(int x, int y, int width, int height, Vector2 scale)
//        {
//            return new Rectangle(
//                x,
//                y,
//                (int)(width * scale.X),
//                (int)(height * scale.Y)
//            );
//        }

//        public void EnemyWallCollision(Enemy_Item_Map enemyItemMap, int currentRoomNumber, Vector2 scale) // or List<IEnemy> enemies, Vector2 spritePosition, Vector2 scale
//                                                                                                           //public static void HandleCollisions(Link link, Enemy_Item_Map enemyItemMap, int currentRoomNumber, Vector2 scale)
//        {
//            wallLeftBoundingBox = new Rectangle(0, (int)(32 * _link._scale.Y), (int)(32 * _link._scale.X), (int)(112 * _link._scale.Y));
//            wallRightBoundingBox = new Rectangle((int)(224 * _link._scale.X), (int)(32 * _link._scale.Y), (int)(32 * _link._scale.X), (int)(112 * _link._scale.Y));
//            wallUpBoundingBox = new Rectangle(0, 0, (int)(256 * _link._scale.X), (int)(32 * _link._scale.Y));
//            wallDownBoundingBox = new Rectangle(0, (int)(144 * _link._scale.Y), (int)(256 * _link._scale.X), (int)(32 * _link._scale.Y));

//            List<IEnemy> enemiesInRoom = enemyItemMap.GetEnemies(currentRoomNumber);

//            // Iterate over each enemy and check for collision
//            foreach (IEnemy enemy in enemiesInRoom) //or Enemy enemy in enemies
//            {
//                Rectangle enemyBoundingBox = GetScaledRectangle((int)enemy.Position.X, (int)enemy.Position.Y, enemy.Width, enemy.Height, scale);

//                //int enemyPositionX = (int)enemy.Position.X;
//                //int enemyPositionY = (int)enemy.Position.Y;

//                Vector2 newEnemyPosition = enemy.Position;

//                if (enemy is Dragon dragon)
//                {
//                    enemyBoundingBox = GetScaledRectangle((int)enemy.Position.X, (int)enemy.Position.Y, 32, 32, scale);

//                    if (blockBoundingBox.Intersects(enemyBoundingBox))
//                    {
//                        Rectangle intersection = Rectangle.Intersect(enemyBoundingBox, blockBoundingBox);

//                        // Resolve vertical collision
//                        if (intersection.Height < intersection.Width)
//                        {
//                            if (enemy.Position.Y < blockBoundingBox.Y) // Coming from the top
//                            {
//                                //enemyPositionY = (int)((int)blockBoundingBox.Top - (enemy.Height * scale.Y));
//                                newEnemyPosition.Y = (int)((int)blockBoundingBox.Top - (enemy.Height * scale.Y));
//                            }
//                            else if (enemy.Position.Y > blockBoundingBox.Y) // Coming from below
//                            {
//                                //enemyPositionY = blockBoundingBox.Bottom;
//                                //newEnemyPosition.Y = blockBoundingBox.Bottom;
//                            }
//                        }
//                        // Resolve horizontal collision
//                        else
//                        {
//                            if (enemy.Position.X < blockBoundingBox.X) // Coming from the left
//                            {
//                                //enemyPositionX -= intersection.Width;
//                                //newEnemyPosition.X = blockBoundingBox.Left - (enemy.Width * scale.X);
//                                newEnemyPosition.X -= intersection.Width;
//                            }
//                            else if (enemy.Position.X > blockBoundingBox.X) // Coming from the right
//                            {
//                                //enemyPositionX = blockBoundingBox.Right;
//                                newEnemyPosition.X = blockBoundingBox.Right;
//                            }
//                        }
//                    }
//                }

//                else
//                {
//                    if (blockBoundingBox.Intersects(enemyBoundingBox))
//                    {
//                        Rectangle intersection = Rectangle.Intersect(enemyBoundingBox, blockBoundingBox);

//                        // Resolve vertical collision
//                        if (intersection.Height < intersection.Width)
//                        {
//                            if (enemy.Position.Y < blockBoundingBox.Y) // Coming from the top
//                            {
//                                //enemyPositionY = (int)((int)blockBoundingBox.Top - (enemy.Height * scale.Y));
//                                newEnemyPosition.Y = (int)((int)blockBoundingBox.Top - (enemy.Height * scale.Y));
//                            }
//                            else if (enemy.Position.Y > blockBoundingBox.Y) // Coming from below
//                            {
//                                //enemyPositionY = blockBoundingBox.Bottom;
//                                newEnemyPosition.Y = blockBoundingBox.Bottom;
//                            }
//                        }
//                        // Resolve horizontal collision
//                        else
//                        {
//                            if (enemy.Position.X < blockBoundingBox.X) // Coming from the left
//                            {
//                                //enemyPositionX -= intersection.Width;
//                                //newEnemyPosition.X = blockBoundingBox.Left - (enemy.Width * scale.X);
//                                newEnemyPosition.X -= intersection.Width;
//                            }
//                            else if (enemyPosition.X > blockBoundingBox.X) // Coming from the right
//                            {
//                                //enemyPositionX = blockBoundingBox.Right;
//                                newEnemyPosition.X = blockBoundingBox.Right;
//                            }
//                        }
//                    }
//                }

//                enemy.Position = newEnemyPosition;

//            }
//        }

//    }
//}