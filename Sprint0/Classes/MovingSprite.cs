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
    internal class MovingSprite : ISprite
    {
        private readonly Texture2D texture;
        private Vector2 position;
        private float speed = 100f; // Speed in pixels per second
        private float screenHeight;
        private bool movingDown = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="MovingSprite"/> class.
        /// </summary>
        /// <param name="texture">The texture of the sprite (e.g., Mario standing).</param>
        /// <param name="screenHeight">The height of the game screen.</param>
        public MovingSprite(Texture2D texture, int screenHeight)
        {
            this.texture = texture;
            this.screenHeight = screenHeight;
            // Start at the top center of the screen
            position = new Vector2(400 - texture.Width / 2, 0);
        }

        /// <summary>
        /// Updates the sprite's position, moving it up and down the screen.
        /// </summary>
        /// <param name="gameTime">Game time information.</param>
        public void Update(GameTime gameTime)
        {
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (movingDown)
            {
                position.Y += speed * delta;
                if (position.Y + texture.Height >= screenHeight)
                {
                    position.Y = screenHeight - texture.Height;
                    movingDown = false; // Reverse direction
                }
            }
            else
            {
                position.Y -= speed * delta;
                if (position.Y <= 0)
                {
                    position.Y = 0;
                    movingDown = true; // Reverse direction
                }
            }
        }

        /// <summary>
        /// Draws the moving sprite on the screen.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch instance used for drawing.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
