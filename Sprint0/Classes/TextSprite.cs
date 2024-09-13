using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.Interfaces;

namespace Sprint0.Classes
{
    public class TextSprite : ISprite
    {
        private readonly SpriteFont font;
        private readonly string text;
        private readonly Vector2 position;
        private readonly Color color;

        public TextSprite(SpriteFont font, string text, Vector2 position, Color color)
        {
            this.font = font;
            this.text = text;
            this.position = position;
            this.color = color;
        }

        public void Update(GameTime gameTime)
        { }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, text, position, color);
        }
    }
}
