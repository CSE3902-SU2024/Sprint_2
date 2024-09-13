using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Sprint0.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.Classes
{
    internal class AnimatedSprite : ISprite
    {
        private readonly Texture2D[] frames;
        private readonly Vector2 position;
        private int currentFrame;
        private double timeElapsed;
        private double timeToUpdate = 0.1; // Time between frames (in seconds)

        /// <summary>
        /// Initializes a new instance of the <see cref="AnimatedSprite"/> class.
        /// </summary>
        /// <param name="frames">The texture array for animation frames (e.g., Mario running).</param>
        public AnimatedSprite(Texture2D[] frames)
        {
            this.frames = frames;
            currentFrame = 0;
            timeElapsed = 0;
            // Set the position to draw the sprite at the center of the screen
            position = new Vector2(400 - frames[0].Width / 2, 240 - frames[0].Height / 2); // Assuming screen size 800x480
        }

        /// <summary>
        /// Updates the animation frames based on the elapsed time.
        /// </summary>
        /// <param name="gameTime">The game time information.</param>
        public void Update(GameTime gameTime)
        {
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
        }

        /// <summary>
        /// Draws the animated sprite on the screen.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch instance used for drawing.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(frames[currentFrame], position, Color.White);
        }
    }
}
