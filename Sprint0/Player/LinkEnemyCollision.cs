using Microsoft.Xna.Framework;
using Sprint0.Player;
using Sprint0.Interfaces;
using System.Collections.Generic;
using Sprint0.Classes;

namespace Sprint0.Collisions
{
    public class LinkEnemyCollision
    {
        public const int LinkHitboxWidth = 16;
        public const int LinkHitboxHeight = 16;
        private const int SwordHitboxWidthRL = 16; //RIGHT LEFT
        private const int SwordHitboxHeightRL = 7; //RIGHT LEFT

        private const int SwordHitboxWidthUD = 7; //UP DOWN
        private const int SwordHitboxHeightUD = 16; //UP DOWN




        private static Rectangle GetScaledRectangle(int x, int y, int width, int height, Vector2 scale)
        {
            return new Rectangle(
                x,
                y,
                (int)(width * scale.X),
                (int)(height * scale.Y)
            );
        }
        public static void HandleCollisions(Link link, List<Enemy> enemies, Vector2 scale)
        {
            Rectangle linkHitbox = GetScaledRectangle((int)link._position.X, (int)link._position.Y, LinkHitboxWidth, LinkHitboxHeight, scale);

            foreach (var enemy in enemies)
            {
                Rectangle enemyHitbox = GetScaledRectangle((int)enemy.Position.X, (int)enemy.Position.Y, enemy.Width, enemy.Height, scale);

                if (linkHitbox.Intersects(enemyHitbox))
                {
                    HandleLinkEnemyCollision(link, enemy);
                }

                if (link.currentState is SwordRight || link.currentState is SwordLeft || link.currentState is SwordUp || link.currentState is SwordDown)
                {
                    Rectangle swordHitbox = GetSwordHitbox(link, scale);
                    if (swordHitbox.Intersects(enemyHitbox))
                    {
                        HandleSwordEnemyCollision(link, enemy);
                    }
                }
            }
        }

        private static void HandleLinkEnemyCollision(Link link, Enemy enemy)
        {
            link.TakeDamage();
            
        }

        private static void HandleSwordEnemyCollision(Link link, Enemy enemy)
        {
            enemy.TakeDamage();
        }

        public static Rectangle GetSwordHitbox(Link link, Vector2 scale)
        {
            Vector2 swordPosition = link._position;
            int width, height;

            if (link.currentState is SwordRight)
            {
                swordPosition.X += 13 * scale.X;
                swordPosition.Y += 6 * scale.Y;
                width = SwordHitboxWidthRL;
                height = SwordHitboxHeightRL;
            }
            else if (link.currentState is SwordLeft)
            {
                swordPosition.X -= 13 * scale.X;
                swordPosition.Y += 6 * scale.Y;
                width = SwordHitboxWidthRL;
                height = SwordHitboxHeightRL;
            }
            else if (link.currentState is SwordUp)
            {
                swordPosition.X += 3 * scale.X;
                swordPosition.Y -= 13 * scale.Y;
                width = SwordHitboxWidthUD;
                height = SwordHitboxHeightUD;
            }
            else if (link.currentState is SwordDown)
            {
                swordPosition.X += 5 * scale.X;
                swordPosition.Y += 14 * scale.Y;
                width = SwordHitboxWidthUD;
                height = SwordHitboxHeightUD;
            }
            else
            {
                // Return an empty rectangle if Link is not in a sword state
                return Rectangle.Empty;
            }

            return GetScaledRectangle((int)swordPosition.X, (int)swordPosition.Y, width, height, scale);
        }


    }
    
}