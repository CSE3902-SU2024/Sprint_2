using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2.Enemy.Projectiles
{
    public class Fireball
    {
        protected Texture2D spriteSheet;
        protected Rectangle[] sourceRectangles;
        protected int currentFrame;
        protected Vector2 position;
        protected Vector2 velocity;
        private float timePerFrame = 0.1f;  // Animation speed for fireball
        private float timeElapsed;
        private float scale = 2.0f;

        public Fireball(Texture2D spriteSheet, Vector2 startPosition, Vector2 velocity, Rectangle[] fireballFrames)
        {
            this.spriteSheet = spriteSheet;
            this.position = startPosition;
            this.velocity = velocity;
            this.sourceRectangles = fireballFrames;
            this.currentFrame = 0;
        }

        public virtual void Update(GameTime gameTime)
        {
            // Move the fireball
            position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Animate the fireball
            timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timeElapsed > timePerFrame)
            {
                currentFrame = (currentFrame + 1) % sourceRectangles.Length;
                timeElapsed = 0f;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                spriteSheet,
                position,
                sourceRectangles[currentFrame],  // Current fireball frame
                Color.White,
                0f,
                Vector2.Zero,
                scale,
                SpriteEffects.None,
                0f
            );
        }

        public bool IsOffScreen()
        {
            // Check if the fireball goes off the screen
            return position.X < 0 || position.Y < 0 || position.X > 800 || position.Y > 480;
        }
    }
}
