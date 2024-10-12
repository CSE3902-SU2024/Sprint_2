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
    public class HandleEnemyWallCollision
    {

        private Vector2 enemyPosition;
        private Vector2 wallPosition;
        private int enemyWidth;
        private int enemyHeight;
        private int wallWidth;
        private int wallHeight;

        public HandleEnemyWallCollision(Vector2 enemyPos, Vector2 wallPos, int eWidth, int eHeight, int wWidth, int wHeight) 
        {
            enemyPosition = enemyPos;
            wallPosition = wallPos;
            enemyWidth = eWidth;
            enemyHeight = eHeight;
            wallWidth = wWidth;
            wallHeight = wHeight;
        }

        public void EnemyWallCollision(ref Vector2 enemyPosition, Vector2 enemyVelocity)
        {
            Rectangle enemyBoundingBox = new Rectangle((int)enemyPosition.X, (int)enemyPosition.Y, enemyWidth, enemyHeight);
            Rectangle wallBoundingBox = new Rectangle((int)wallPosition.X, (int)wallPosition.Y, wallWidth, wallHeight);

            Rectangle intersection = Rectangle.Empty;

            // Check if there is an intersection between the player and the wall
            if (enemyBoundingBox.Intersects(wallBoundingBox))
            {
                // Get the intersection area
                intersection = Rectangle.Intersect(enemyBoundingBox, wallBoundingBox);

                // Handle the collision based on the direction of the player velocity
                if (intersection.Width < intersection.Height)
                {
                    // Horizontal collision
                    if (enemyVelocity.X > 0) // Moving right
                    {
                        enemyPosition.X = wallBoundingBox.Left - enemyWidth;
                    }
                    else if (enemyVelocity.X < 0) // Moving left
                    {
                        enemyPosition.X = wallBoundingBox.Right;
                    }
                }
                else
                {
                    // Vertical collision
                    if (enemyVelocity.Y > 0) // Moving down
                    {
                        enemyPosition.Y = wallBoundingBox.Top - enemyWidth;
                    }
                    else if (enemyVelocity.Y < 0) // Moving up
                    {
                        enemyPosition.Y = wallBoundingBox.Bottom;
                    }
                }
            }

        }
        
    }
}
