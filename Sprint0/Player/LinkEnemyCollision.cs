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

        private const int ArrowHitboxWidthRL = 16; //RIGHT LEFT
        private const int ArrowHitboxHeightRL = 5; //RIGHT LEFT
        private const int ArrowHitboxWidthUD = 5; //UP DOWN
        private const int ArrowHitboxHeightUD = 16; //UP DOWN


        private const int BoomerangHitbox = 8; //height and width the same
        private const int BombHitbox = 16; //height and width the same






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
                if (link.currentState is ArrowRight || link.currentState is ArrowLeft || link.currentState is ArrowUp || link.currentState is ArrowDown)
                {
                    Rectangle ArrowHitbox = GetArrowHitbox(link, scale);
                    if (ArrowHitbox.Intersects(enemyHitbox))
                    {
                        HandleArrowEnemyCollision(link, enemy);
                    }
                }
                if (link.currentState is BoomerangRight || link.currentState is BoomerangLeft || link.currentState is BoomerangUp || link.currentState is BoomerangDown)
                {
                    Rectangle BoomerangHitbox = GetBoomerangHitbox(link, scale);
                    if (BoomerangHitbox.Intersects(enemyHitbox))
                    {
                        HandleBoomerangEnemyCollision(link, enemy);
                    }
                }
                if (link.currentState is BombRight)
                {
                    Rectangle BombHitbox = GetBombHitbox(link, scale);
                    if (BombHitbox.Intersects(enemyHitbox) &&(((BombRight)link.currentState)._boomTimer == 0))
                    {
                        HandleBombEnemyCollision(link, enemy);
                    }
                } else if (link.currentState is BombLeft)
                {
                    Rectangle BombHitbox = GetBombHitbox(link, scale);
                    if (BombHitbox.Intersects(enemyHitbox) && (((BombLeft)link.currentState)._boomTimer == 0))
                    {
                        HandleBombEnemyCollision(link, enemy);
                    } 
                } else if (link.currentState is BombUp)
                {
                    Rectangle BombHitbox = GetBombHitbox(link, scale);
                    if (BombHitbox.Intersects(enemyHitbox) && (((BombUp)link.currentState)._boomTimer == 0))
                    {
                        HandleBombEnemyCollision(link, enemy);
                    }
                } else if (link.currentState is BombDown)
                {
                    Rectangle BombHitbox = GetBombHitbox(link, scale);
                    if (BombHitbox.Intersects(enemyHitbox) && (((BombDown)link.currentState)._boomTimer == 0))
                    {
                        HandleBombEnemyCollision(link, enemy);
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
        private static void HandleArrowEnemyCollision(Link link, Enemy enemy)  //if we later want death by arrow, death by sword etc
        {
            enemy.TakeDamage();
            if (link.currentState is ArrowRight)
            {
                link.currentState = new LinkRight(link);
            } else if (link.currentState is ArrowLeft)
            {
                link.currentState = new LinkLeft(link);
            }
            else if (link.currentState is ArrowUp)
            {
                link.currentState = new LinkUp(link);
            }
            else if (link.currentState is ArrowDown)
            {
                link.currentState = new LinkDown(link);
            }
        }
        private static void HandleBoomerangEnemyCollision(Link link, Enemy enemy)  //if we later want death by arrow, death by sword etc
        {
            enemy.TakeDamage();
        }
        private static void HandleBombEnemyCollision(Link link, Enemy enemy) //if we later want death by arrow, death by sword etc
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
        public static Rectangle GetArrowHitbox(Link link, Vector2 scale)
        {
            Vector2 ArrowPosition;
            int width, height;


            if (link.currentState is ArrowRight)
            {

                ArrowPosition = ((ArrowRight)link.currentState)._weaponPosition; // Cast to ArrowRight
                width = ArrowHitboxWidthRL;
                height = ArrowHitboxHeightRL;
            }
            else if (link.currentState is ArrowLeft)
            {
                ArrowPosition = ((ArrowLeft)link.currentState)._weaponPosition; // Cast to ArrowRight
                width = ArrowHitboxWidthRL;
                height = ArrowHitboxHeightRL;
            }
            else if (link.currentState is ArrowUp)
            {
                ArrowPosition = ((ArrowUp)link.currentState)._weaponPosition; // Cast to ArrowRight
                width = ArrowHitboxWidthUD;
                height = ArrowHitboxHeightUD;
            }
            else if (link.currentState is ArrowDown)
            {
                ArrowPosition = ((ArrowDown)link.currentState)._weaponPosition; // Cast to ArrowRight
                width = ArrowHitboxWidthUD;
                height = ArrowHitboxHeightUD;
            }
            else
            {
                // Return an empty rectangle if Link is not in a sword state
                return Rectangle.Empty;
            }

            return GetScaledRectangle((int)ArrowPosition.X, (int)ArrowPosition.Y, width, height, scale);
        }
        public static Rectangle GetBoomerangHitbox(Link link, Vector2 scale)
        {
            Vector2 BoomerangPosition= link._position;
            int width, height;
            width = BoomerangHitbox;
            height = BoomerangHitbox;

            if (link.currentState is BoomerangRight) BoomerangPosition = ((BoomerangRight)link.currentState)._weaponPosition;
            if (link.currentState is BoomerangLeft) BoomerangPosition = ((BoomerangLeft)link.currentState)._weaponPosition;
            if (link.currentState is BoomerangUp) BoomerangPosition = ((BoomerangUp)link.currentState)._weaponPosition;
            if (link.currentState is BoomerangDown) BoomerangPosition = ((BoomerangDown)link.currentState)._weaponPosition;

            return GetScaledRectangle((int)BoomerangPosition.X, (int)BoomerangPosition.Y, width, height, scale);
        }
        public static Rectangle GetBombHitbox(Link link, Vector2 scale)
        {
            Vector2 BombPosition = link._position;
            int width, height;
            width = BombHitbox;
            height = BombHitbox;
            if (link.currentState is BombRight) BombPosition = ((BombRight)link.currentState)._weaponPosition;
            if (link.currentState is BombLeft) BombPosition = ((BombLeft)link.currentState)._weaponPosition;
            if (link.currentState is BombUp) BombPosition = ((BombUp)link.currentState)._weaponPosition;
            if (link.currentState is BombDown) BombPosition = ((BombDown)link.currentState)._weaponPosition;

            return GetScaledRectangle((int)BombPosition.X, (int)BombPosition.Y, width, height, scale);

        }

    }
}
