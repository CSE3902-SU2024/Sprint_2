using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Sprint0.Classes;
using System;
using Sprint2.Enemy.Projectiles;
using System.Collections.Generic;

namespace Sprint2.Enemy
{
    public class Dragon : IEnemy
    {
        private Texture2D spriteSheet;
        private Rectangle[] sourceRectangles;
        private Vector2 position;
        private Vector2 initialPosition;
        private int currentFrame;
        private bool movingRight = true;
        private float movementRange = 100f;
        private float timePerFrame = 0.1f;
        private float timeElapsed;
        private Color currentColor = Color.White;  // For damage effect
        private float damageColorTimer = 0f;
        private const float DAMAGE_COLOR_DURATION = 0.5f;
        private float fireballCooldown = 1f; // 1 second cooldown between shots
        private float timeSinceLastShot;
        private List<Fireball> fireballs; // To store all fireballs
        private Rectangle[] fireballRectangles; // Fireball frames

        // Implement IEnemy properties
        public Vector2 Position { get => position; set => position = value; }
        public int Width { get; private set; } = 24;
        public int Height { get; private set; } = 32;

        public Dragon(Vector2 startPosition)
        {
            position = startPosition;
            initialPosition = startPosition;
            fireballs = new List<Fireball>();
        }

        // Load content and sprites
        public void LoadContent(ContentManager content, string texturePath)
        {
            spriteSheet = content.Load<Texture2D>(texturePath);
            sourceRectangles = SpriteSheetHelper.CreateDragonFrames();
            fireballRectangles = SpriteSheetHelper.CreateFireballFrames(); // Load fireball frames
        }

        // Update the dragon state (movement, animation, etc.)
        public void Update(GameTime gameTime)
        {
            timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
            damageColorTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            MoveDragon();

            if (timeElapsed > timePerFrame)
            {
                currentFrame = (currentFrame + 1) % sourceRectangles.Length;
                timeElapsed = 0f;
            }

            // Reset color after damage timer expires
            if (damageColorTimer <= 0)
            {
                currentColor = Color.White;
            }

            // Handle fireball shooting cooldown
            timeSinceLastShot += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timeSinceLastShot > fireballCooldown)
            {
                ShootFireball();  // Shoot fireballs in different directions
                timeSinceLastShot = 0f;
            }

            // Update fireballs
            for (int i = 0; i < fireballs.Count; i++)
            {
                fireballs[i].Update(gameTime);

                // Remove the fireball if it goes off-screen
                if (fireballs[i].IsOffScreen())
                {
                    fireballs.RemoveAt(i);
                    i--;
                }
            }
        }

        // Move the dragon within a range
        private void MoveDragon()
        {
            if (movingRight)
            {
                position.X += 1f;
                if (position.X >= initialPosition.X + movementRange)
                    movingRight = false;
            }
            else
            {
                position.X -= 1f;
                if (position.X <= initialPosition.X - movementRange)
                    movingRight = true;
            }
        }

        // Shoot fireballs in different directions
        private void ShootFireball()
        {
            Vector2 fireballPosition = new Vector2(position.X, position.Y);

            // Add fireballs going in different directions
            fireballs.Add(new Fireball(spriteSheet, fireballPosition, new Vector2(-200, 0), fireballRectangles)); // Left
            fireballs.Add(new Fireball(spriteSheet, fireballPosition, new Vector2(-200, -100), fireballRectangles)); // Left-top
            fireballs.Add(new Fireball(spriteSheet, fireballPosition, new Vector2(-200, 100), fireballRectangles)); // Left-bottom
        }

        // Draw the dragon and its fireballs on the screen
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                spriteSheet,
                position,
                sourceRectangles[currentFrame],
                currentColor,
                0f,
                Vector2.Zero,
                4.0f,
                SpriteEffects.None,
                0f
            );

            // Draw fireballs
            foreach (var fireball in fireballs)
            {
                fireball.Draw(spriteBatch);
            }
        }

        // Reset the dragon state
        public void Reset()
        {
            position = initialPosition;
            movingRight = true;
            currentFrame = 0;
            timeElapsed = 0f;
            damageColorTimer = 0f;
            currentColor = Color.White;
            fireballs.Clear(); // Clear fireballs on reset
        }

        // Handle the dragon taking damage
        public void TakeDamage()
        {
            currentColor = Color.Red;
            damageColorTimer = DAMAGE_COLOR_DURATION;
        }
    }
}

