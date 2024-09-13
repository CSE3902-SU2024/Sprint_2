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
    internal class StaticSprite : ISprite
    {
        private readonly Texture2D texture;
        private readonly Vector2 position;

        /// <summary>
        /// Initializes a new instance of the <see cref="StaticSprite"/> class.
        /// </summary>
        /// <param name="texture">The texture of the static sprite (e.g., Mario standing).</param>
        public StaticSprite(Texture2D texture)
        {
            this.texture = texture;
            // Set the position to draw the sprite at the center of the screen
            position = new Vector2(400 - texture.Width / 2, 240 - texture.Height / 2); // Assuming screen size 800x480
        }

        /// <summary>
        /// Updates the sprite. Since this is a static sprite, no updates are needed.
        /// </summary>
        /// <param name="gameTime">The game time information.</param>
        public void Update(GameTime gameTime)
        {
            // No update logic needed for a static sprite
        }

        /// <summary>
        /// Draws the static sprite on the screen.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch instance used for drawing.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
