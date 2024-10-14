using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Sprint0.Classes;

namespace Sprint2.Enemy
{
    public class Keese : IEnemy
    {
        private Texture2D spriteSheet;
        private Rectangle[] sourceRectangles;
        private Vector2 position;
        private Vector2 initialPosition;
        private int currentFrame;
        private bool movingRight = true;
        private float movementRange = 100f;
        private float timePerFrame = 0.1f;
        private float timeElapsed;

        // Implement IEnemy properties
        public Vector2 Position { get => position; set => position = value; }
        public int Width { get; } = 16;
        public int Height { get; } = 16;

        public Keese(Vector2 startPosition)
        {
            position = startPosition;
            initialPosition = startPosition;
        }

        public void LoadContent(ContentManager content, string texturePath)
        {
            spriteSheet = content.Load<Texture2D>(texturePath);
            sourceRectangles = SpriteSheetHelper.CreateKeeseFrames();
        }

        public void Update(GameTime gameTime)
        {
            timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
            MoveKeese();

            if (timeElapsed > timePerFrame)
            {
                currentFrame = (currentFrame + 1) % sourceRectangles.Length;
                timeElapsed = 0f;
            }
        }

        private void MoveKeese()
        {
            if (movingRight)
            {
                position.X += 1f;
                if (position.X >= initialPosition.X + movementRange)
                    movingRight = false;
            }
            else
            {
                position.X -= 1f;
                if (position.X <= initialPosition.X - movementRange)
                    movingRight = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(spriteSheet, position, sourceRectangles[currentFrame], Color.White, 0f, Vector2.Zero, 4.0f, SpriteEffects.None, 0f);
        }

        // Implement IEnemy methods
        public void TakeDamage()
        {
            // Handle damage logic
        }

        public void Reset()
        {
            position = initialPosition;
            movingRight = true;
            currentFrame = 0;
            timeElapsed = 0f;
        }
    }
}
