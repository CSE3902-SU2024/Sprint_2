using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprint0.Interfaces;

namespace Sprint0.Classes
{
    internal class MovingAnimatedSprite : ISprite
    {
        private readonly Texture2D[] frames;
        private Vector2 position;
        private float speed = 150f; // Speed in pixels per second
        private float screenWidth;
        private int currentFrame;
        private double timeElapsed;
        private double timeToUpdate = 0.1; // Time between frames (in seconds)
        private bool movingRight = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="MovingAnimatedSprite"/> class.
        /// </summary>
        /// <param name="frames">The texture array for animation frames (e.g., Mario running).</param>
        /// <param name="screenWidth">The width of the game screen.</param>
        public MovingAnimatedSprite(Texture2D[] frames, int screenWidth)
        {
            this.frames = frames;
            this.screenWidth = screenWidth;
            currentFrame = 0;
            timeElapsed = 0;
            // Start at the left center of the screen
            position = new Vector2(0, 240 - frames[0].Height / 2);
        }

        /// <summary>
        /// Updates the sprite's position and animation frame, moving left to right.
        /// </summary>
        /// <param name="gameTime">Game time information.</param>
        public void Update(GameTime gameTime)
        {
            // Update animation frame
            timeElapsed += gameTime.ElapsedGameTime.TotalSeconds;
            if (timeElapsed > timeToUpdate)
            {
                currentFrame++;
                if (currentFrame >= frames.Length)
                {
                    currentFrame = 0; // Loop back to the first frame
                }
                timeElapsed -= timeToUpdate;
            }

            // Update position
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (movingRight)
            {
                position.X += speed * delta;
                if (position.X + frames[0].Width >= screenWidth)
                {
                    position.X = screenWidth - frames[0].Width;
                    movingRight = false; // Reverse direction
                }
            }
            else
            {
                position.X -= speed * delta;
                if (position.X <= 0)
                {
                    position.X = 0;
                    movingRight = true; // Reverse direction
                }
            }
        }

        /// <summary>
        /// Draws the moving and animated sprite on the screen.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch instance used for drawing.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(frames[currentFrame], position, Color.White);
        }
    }
}
