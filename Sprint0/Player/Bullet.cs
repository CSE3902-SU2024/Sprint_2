using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace Sprint0.Classes
{
    public class Bullet
    {
        public Vector2 Position { get; private set; }
        public Vector2 Velocity { get; private set; }
        public Texture2D Texture { get; private set; }
        public Rectangle SourceRectangle { get; private set; }
        public Vector2 Scale { get; private set; }
        public float Speed { get; private set; }
        public float Lifetime { get; private set; }
        private float currentLifetime;

        public Bullet(Vector2 startPosition, Vector2 direction, Texture2D texture, Rectangle sourceRectangle, Vector2 scale, float speed, float lifetime)
        {
            Position = startPosition;
            Velocity = Vector2.Normalize(direction) * speed;
            Texture = texture;
            SourceRectangle = sourceRectangle;
            Scale = scale;
            Speed = speed;
            Lifetime = lifetime;
            currentLifetime = 0;
        }

        public void Update(GameTime gameTime)
        {
            Position += Velocity;
            currentLifetime += (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public bool IsExpired()
        {
            return currentLifetime >= Lifetime;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, SourceRectangle, Color.White, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
        }
    }
}
