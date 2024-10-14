using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2.Enemy.Projectiles
{
    public class Boomerang
    {
        private Texture2D spriteSheet;
        private Rectangle[] sourceRectangles; // Array of frames for boomerang animation
        private Vector2 position;
        private Vector2 velocity;
        private Vector2 startPosition;
        private bool returning = false;
        private float scale; // New: Scaling factor for boomerang size
        private float distanceToTravel; // New: Customizable flight distance
        private int currentFrame;
        private float timePerFrame = 0.1f; // Time per frame for boomerang animation
        private float elapsedTime = 0f;

        public Vector2 Position
        {
            get { return position; }
        }
        // Constructor with scaling and flight distance customization
        public Boomerang(Texture2D spriteSheet, Vector2 startPosition, Vector2 velocity, Rectangle[] frames, float scale = 1.0f, float distanceToTravel = 150f)
        {
            this.spriteSheet = spriteSheet;
            this.startPosition = startPosition;
            this.position = startPosition;
            this.velocity = velocity;
            this.sourceRectangles = frames;
            this.scale = scale; // Set scaling factor
            this.distanceToTravel = distanceToTravel; // Set custom flight distance
            this.currentFrame = 0;
        }

        // Update the boomerang's position, handle returning, and animate it
        public void Update(GameTime gameTime)
        {
            // Update position based on velocity
            position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Handle returning when boomerang reaches max distance
            if (!returning && Vector2.Distance(startPosition, position) > distanceToTravel)
            {
                velocity *= -1; // Reverse the direction of the boomerang
                returning = true;
            }

            // Animate the boomerang (cycling through frames)
            elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (elapsedTime > timePerFrame)
            {
                currentFrame = (currentFrame + 1) % sourceRectangles.Length;
                elapsedTime = 0f;
            }
        }

        // Check if the boomerang has returned to the original position
        public bool IsReturned()
        {
            return returning && Vector2.Distance(startPosition, position) < 5f;
        }

        // Draw the boomerang with scaling and animation
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                spriteSheet,
                position,
                sourceRectangles[currentFrame],  // Current boomerang frame
                Color.White,
                0f,
                Vector2.Zero,
                scale,  // Apply scaling to make the boomerang bigger
                SpriteEffects.None,
                0f
            );
        }
    }
}
