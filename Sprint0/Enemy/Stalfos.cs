using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Sprint0.Classes;
using System;

namespace Sprint2.Enemy
{
    public class Stalfos : IEnemy
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
        private Color currentColor = Color.White;
        private float damageColorTimer = 0f;
        private const float DAMAGE_COLOR_DURATION = 0.5f;
        private int healthCount;
        private bool isFliped = false;

        private Vector2 speed;
        private Vector2 _scale;
        private Random random;
        private Boolean alive;
        private Direction currentDirection;
        private int randCount;

        public Vector2 Position { get => position; set => position = value; }
        public int Width { get; } = 16;
        public int Height { get; } = 16;


        public Stalfos(Vector2 startPosition)
        {
            position = startPosition;
            initialPosition = startPosition;
            alive = true;
            random = new Random();
            SetRandomDirection();
        }
        public enum Direction
        {
            Left,
            Right,
            Up,
            Down
        }

        public void LoadContent(ContentManager content, string texturePath, GraphicsDevice graphicsdevice)
        {
            healthCount = 20;
            spriteSheet = content.Load<Texture2D>(texturePath);
            sourceRectangles = SpriteSheetHelper.CreateStalfosFrames();
            _scale.X = (float)graphicsdevice.Viewport.Width / 256.0f;
            _scale.Y = (float)graphicsdevice.Viewport.Height / 176.0f;
            speed = Vector2.One;
        }

        public void Update(GameTime gameTime)
        {
            if (alive)
            {
                randCount++;
                timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
                damageColorTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if(randCount > 60 * random.Next(1, 3))
                {
                    SetRandomDirection();
                    randCount = 0;
                }
                MoveStalfos();

                if (timeElapsed > 0.1f)
                {
                    isFliped = !isFliped;
                    timeElapsed = 0f;
                }
                // Reset color after damage timer expires
                if (damageColorTimer <= 0)
                {
                    currentColor = Color.White;
                }
            }
        }

        private void MoveStalfos()
        {
            if (alive)
            {
                switch (currentDirection)
                {
                    case (Direction.Left):
                        if (position.X - speed.X > 32 * _scale.X)
                        {
                            position.X -= speed.X;
                        }
                        else
                        {
                            currentDirection = Direction.Right;
                        }
                        break;
                    case (Direction.Right):
                        if (position.X + speed.X < 208 * _scale.X)
                        {
                            position.X += speed.X;
                        }
                        else
                        {
                            currentDirection = Direction.Left;
                        }
                        break;
                    case (Direction.Up):
                        if (position.Y - speed.Y > 32 * _scale.Y)
                        {
                            position.Y -= speed.Y;
                        }
                        else
                        {
                            currentDirection = Direction.Down;
                        }
                        break;
                    case (Direction.Down):
                        if (position.Y + speed.Y < 128 * _scale.Y)
                        {
                            position.Y += speed.Y;
                        }
                        else
                        {
                            currentDirection = Direction.Up;
                        }

                        break;
                }

            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            SpriteEffects spriteEffect = isFliped ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            spriteBatch.Draw(spriteSheet, position, sourceRectangles[currentFrame], currentColor, 0f, Vector2.Zero, _scale, spriteEffect, 0f);
        }

       
        public void TakeDamage()
        {
            currentColor = Color.Red;
            damageColorTimer = DAMAGE_COLOR_DURATION;
            healthCount -= 1;
            if (healthCount <= 0)
            {
                alive = false;
                position.X = 20000;
                position.Y = 20000;
            }
        }

        public void Reset()
        {
            position = initialPosition;
            movingRight = true;
            currentFrame = 0;
            timeElapsed = 0f;
            damageColorTimer = 0f;
            currentColor = Color.White;
        }

        private void SetRandomDirection()
        {
            currentDirection = (Direction)random.Next(0, 4);

        }
    }
}

