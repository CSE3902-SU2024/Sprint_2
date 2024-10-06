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



        public static void HandleCollisions(Link link, List<Enemy> enemies)
        {
            Rectangle linkHitbox = new Rectangle((int)link._position.X, (int)link._position.Y, LinkHitboxWidth, LinkHitboxHeight);

            foreach (var enemy in enemies)
            {
                Rectangle enemyHitbox = new Rectangle((int)enemy.Position.X, (int)enemy.Position.Y, enemy.Width, enemy.Height);

                if (linkHitbox.Intersects(enemyHitbox))
                {
                    HandleLinkEnemyCollision(link, enemy);
                }

                if (link.currentState is SwordRight || link.currentState is SwordLeft || link.currentState is SwordUp || link.currentState is SwordDown)
                {
                    Rectangle swordHitbox = GetSwordHitbox(link);
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

        public static Rectangle GetSwordHitbox(Link link)
        {

            int offsetX = 0;
            int offsetY = 0;

            if (link.currentState is SwordRight)
            {
                offsetX = SwordHitboxWidthRL;
                offsetY = SwordHitboxHeightRL;
                return new Rectangle(
               (int)link._position.X + offsetX,
               (int)link._position.Y + offsetY,
               SwordHitboxWidthRL,
               SwordHitboxHeightRL
           );
            }
            else if (link.currentState is SwordLeft)
            {
                offsetX = -SwordHitboxWidthRL;
                offsetY = SwordHitboxHeightRL;
                return new Rectangle(
               (int)link._position.X + offsetX,
               (int)link._position.Y + offsetY,
               SwordHitboxWidthRL,
               SwordHitboxHeightRL
           );
            }
            else if (link.currentState is SwordUp)
            {
                offsetX = SwordHitboxWidthUD;
                offsetY = -SwordHitboxHeightUD;
                return new Rectangle(
               (int)link._position.X + offsetX,
               (int)link._position.Y + offsetY,
               SwordHitboxWidthUD,
               SwordHitboxHeightUD
           );
            }
            else if (link.currentState is SwordDown)
            {
                offsetX = SwordHitboxWidthUD;
                offsetY = -SwordHitboxWidthUD;
                return new Rectangle(
               (int)link._position.X + offsetX,
               (int)link._position.Y + offsetY,
               SwordHitboxWidthUD,
               SwordHitboxHeightUD
           );
            }
            return new Rectangle(0,0,0,0);

           
        }
    }
}