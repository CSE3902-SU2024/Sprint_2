using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.Player;
using Sprint0.Classes;
using System.Collections.Generic;

namespace Sprint0.Collisions
{
    public static class DebugDraw
    {
        private static Texture2D _pixel;

        public static void Initialize(GraphicsDevice graphicsDevice)
        {
            _pixel = new Texture2D(graphicsDevice, 1, 1);
            _pixel.SetData(new[] { Color.White });
        }

        public static void DrawRectangle(SpriteBatch spriteBatch, Rectangle rectangle, Color color, Vector2 scale, int lineWidth = 1)
        {
            // Scale only the size, not the position
            Rectangle scaledRectangle = new Rectangle(
                rectangle.Left, // Keep the original position
                rectangle.Top,  // Keep the original position
                (int)(rectangle.Width * scale.X), // Scale the width
                (int)(rectangle.Height * scale.Y) // Scale the height
            );

            // Draw the left line
            spriteBatch.Draw(_pixel, new Rectangle(scaledRectangle.Left, scaledRectangle.Top, lineWidth, scaledRectangle.Height + lineWidth), color);

            // Draw the right line
            spriteBatch.Draw(_pixel, new Rectangle(scaledRectangle.Right, scaledRectangle.Top, lineWidth, scaledRectangle.Height + lineWidth), color);

            // Draw the top line
            spriteBatch.Draw(_pixel, new Rectangle(scaledRectangle.Left, scaledRectangle.Top, scaledRectangle.Width + lineWidth, lineWidth), color);

            // Draw the bottom line
            spriteBatch.Draw(_pixel, new Rectangle(scaledRectangle.Left, scaledRectangle.Bottom, scaledRectangle.Width + lineWidth, lineWidth), color);
        }




        public static void DrawHitboxes(SpriteBatch spriteBatch, Link link, List<Enemy> enemies, Vector2 scale)
        {
            // Draw Link's hitbox
            Rectangle linkHitbox = new Rectangle((int)link._position.X, (int)link._position.Y, LinkEnemyCollision.LinkHitboxWidth, LinkEnemyCollision.LinkHitboxHeight);
            DrawRectangle(spriteBatch, linkHitbox, Color.Blue, scale);

            // Draw sword hitbox if Link is attacking
            if (link.currentState is SwordRight || link.currentState is SwordLeft || link.currentState is SwordUp || link.currentState is SwordDown)
            {
                Rectangle swordHitbox = LinkEnemyCollision.GetSwordHitbox(link, scale);
                DrawRectangle(spriteBatch, swordHitbox, Color.Red, new Vector2(1.0f, 1.0f)); //I already scale it in GetSwordHitbox
            }
            //Draw arrow hitbox if Link is arrow attack
            if (link.currentState is ArrowRight || link.currentState is ArrowLeft || link.currentState is ArrowUp || link.currentState is ArrowDown)
            {
                Rectangle ArrowHitbox = LinkEnemyCollision.GetArrowHitbox(link, scale);
                DrawRectangle(spriteBatch, ArrowHitbox, Color.Azure, new Vector2(1.0f, 1.0f)); //I already scale it in GetArrowHitbox
            }

            // Draw enemy hitboxes
            foreach (var enemy in enemies)
            {
                Rectangle enemyHitbox = new Rectangle((int)enemy.Position.X, (int)enemy.Position.Y, enemy.Width, enemy.Height);
                DrawRectangle(spriteBatch, enemyHitbox, Color.Green, scale);
            }
        }

    }
}
