using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0.Player
{
    public class Arrow
    {
        private Vector2 _position;
        private Vector2 _velocity;
        private int _currentFrame;
        private bool _isActive;
        private float _rotation;

        // Constants
        private const int FRAME_WIDTH = 16; // Adjust based on your sprite sheet
        private const int FRAME_HEIGHT = 16; // Adjust based on your sprite sheet
        private const int TOTAL_FRAMES = 4; // Number of animation frames
        private const int ANIMATION_SPEED = 5; // Frames before changing animation
        private int _frameCounter;

        // Screen boundaries
        private int _screenWidth;
        private int _screenHeight;

        public Arrow(Vector2 position, Vector2 direction, float speed, int screenWidth, int screenHeight)
        {
            _position = position;
            _velocity = Vector2.Normalize(direction) * speed;
            _currentFrame = 0;
            _isActive = true;
            _rotation = (float)System.Math.Atan2(direction.Y, direction.X);
            _frameCounter = 0;

            _screenWidth = screenWidth;
            _screenHeight = screenHeight;
        }

        public void Update(GameTime gameTime)
        {
            if (_isActive)
            {
                // Move the arrow
                _position += _velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

                // Check if arrow is off-screen
                if (IsOffScreen())
                {
                    _isActive = false;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            if (_isActive)
            {
                Rectangle sourceRectangle = new Rectangle(_currentFrame * FRAME_WIDTH, 0, FRAME_WIDTH, FRAME_HEIGHT);
                Vector2 origin = new Vector2(FRAME_WIDTH / 2f, FRAME_HEIGHT / 2f);

                spriteBatch.Draw(
                    texture,
                    _position,
                    sourceRectangle,
                    Color.White,
                    _rotation,
                    origin,
                    Vector2.One,
                    SpriteEffects.None,
                    0f
                );
            }
        }

        private bool IsOffScreen()
        {
            return _position.X < -FRAME_WIDTH || _position.X > _screenWidth ||
                   _position.Y < -FRAME_HEIGHT || _position.Y > _screenHeight;
        }

        public Rectangle GetBounds()
        {
            return new Rectangle(
                (int)(_position.X - FRAME_WIDTH / 2),
                (int)(_position.Y - FRAME_HEIGHT / 2),
                FRAME_WIDTH,
                FRAME_HEIGHT
            );
        }
    }
}