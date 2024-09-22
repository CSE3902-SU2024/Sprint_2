using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0.Classes
{
    public class Arrow
    {
        public Vector2 Position { get; private set; }
        public Vector2 Velocity { get; private set; }
        private Texture2D _texture;
        private Rectangle _sourceRectangle;
        public bool IsActive { get; private set; }

        public Arrow(Texture2D texture, Rectangle sourceRectangle)
        {
            _texture = texture;
            _sourceRectangle = sourceRectangle;
            IsActive = false;
        }

        public void Fire(Vector2 position, Vector2 direction)
        {
            Position = position;
            Velocity = direction * 5f; // Adjust speed as needed
            IsActive = true;
        }

        public void Update(GameTime gameTime)
        {
            if (IsActive)
            {
                Position += Velocity;
                // Deactivate arrow if it goes off-screen or hits something
                // You'll need to implement this logic based on your game's requirements
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (IsActive)
            {
                spriteBatch.Draw(_texture, Position, _sourceRectangle, Color.White);
            }
        }
    }
}