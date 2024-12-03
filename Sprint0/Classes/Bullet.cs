using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.Player;

namespace Sprint0.Classes
{
    public class Bullet
    {
        private Texture2D _sprite;
        private Vector2 _position;
        private Vector2 _direction;
        private Vector2 _scale;
        private ILinkState.Direction _bulletDirection;

        public float Speed = 10f;
        public float Lifetime = 1f; // 1 second before despawning
        private float _currentLifetime = 0f;

        public Bullet(Texture2D sprite, Vector2 startPosition, Vector2 direction, Vector2 scale, ILinkState.Direction bulletDirection)
        {
            _sprite = sprite;
            _position = startPosition;
            _direction = Vector2.Normalize(direction);
            _scale = scale;
            _bulletDirection = bulletDirection;
        }

        public void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Move bullet based on its direction
            _position += _direction * Speed;

            // Track lifetime
            _currentLifetime += deltaTime;
        }

        public bool IsExpired()
        {
            return _currentLifetime >= Lifetime;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                _sprite,
                _position,
                null,
                Color.White,
                0f,
                Vector2.Zero,
                _scale,
                GetSpriteEffects(),
                0f
            );
        }

        private SpriteEffects GetSpriteEffects()
        {
            // Adjust sprite orientation based on bullet direction
            switch (_bulletDirection)
            {
                case ILinkState.Direction.left: return SpriteEffects.FlipHorizontally;
                case ILinkState.Direction.up: return SpriteEffects.FlipVertically;
                default: return SpriteEffects.None;
            }
        }

        public Rectangle GetBoundingBox()
        {
            return new Rectangle(
                (int)_position.X,
                (int)_position.Y,
                (int)(16 * _scale.X),  // Adjust size based on your bullet sprite
                (int)(16 * _scale.Y)
            );
        }
    }
}