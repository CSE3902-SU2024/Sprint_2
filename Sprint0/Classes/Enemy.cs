using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.Classes
{
    public class Enemy
    {
        private Texture2D spriteSheet;
        private Rectangle[] sourceRectangles; // For animation frames
        private Vector2 position;
        private int currentFrame;
        private float timePerFrame = 0.1f; // 100ms per frame
        private float timeElapsed;
        private Direction enemyDirection = Direction.Right; // Default direction is right

        public enum Direction
        {
            Left,
            Right,
            Up,
            Down
        }

        public Enemy(Vector2 startPosition)
        {
            position = startPosition;
        }

        public void LoadContent(ContentManager content, string texturePath)
        {
            spriteSheet = content.Load<Texture2D>("boxGhostSpriteSheet");
            sourceRectangles = SpriteSheetHelper.CreateEnemyFrames(); // Get frames from helper
        }

        public void Update(GameTime gameTime)
        {
            timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Simple frame advancement logic
            if (timeElapsed > timePerFrame)
            {
                currentFrame = (currentFrame + 1) % sourceRectangles.Length;
                timeElapsed = 0f;
            }

            // Logic to handle movement/direction updates can be added here
            // E.g., position += new Vector2(xSpeed, ySpeed); based on direction
        }

        public void ChangeDirection(Direction newDirection)
        {
            enemyDirection = newDirection;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            SpriteEffects spriteEffect = SpriteEffects.None;

            switch (enemyDirection)
            {
                case Direction.Left:
                    spriteEffect = SpriteEffects.FlipHorizontally;
                    break;
                case Direction.Right:
                    spriteEffect = SpriteEffects.None;
                    break;
                case Direction.Up:
                    spriteEffect = SpriteEffects.FlipVertically;
                    break;
                case Direction.Down:
                    spriteEffect = SpriteEffects.None;
                    break;
            }

            spriteBatch.Draw(
                spriteSheet,
                position,
                sourceRectangles[currentFrame], // The current animation frame
                Color.White,
                0f,
                Vector2.Zero,
                1f,
                spriteEffect,
                0f
            );
        }
    }
}
