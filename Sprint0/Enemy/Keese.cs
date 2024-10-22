using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Sprint0.Classes;
using System;

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
        private Color currentColor = Color.White;
        private float damageColorTimer = 0f;
        private const float DAMAGE_COLOR_DURATION = 0.5f;
        private int hurtTime;
        private Vector2 speed;
        private Vector2 _scale;
        private Random random;
        private Boolean alive;
        private Direction currentDirection;
        private int randCount;
        
        public enum Direction
        {
            Left,
            Right,
            Up,
            Down
        }
   
        public Vector2 Position { get => position; set => position = value; }
        public int Width { get; } = 16;
        public int Height { get; } = 16;

        public Keese(Vector2 startPosition)
        {
            hurtTime = 2;
            position = startPosition;
            initialPosition = startPosition;
            alive = true;
            random = new Random();
            SetRandomDirection(); 

        }

        public void LoadContent(ContentManager content, string texturePath, GraphicsDevice graphicsdevice)
        {
            spriteSheet = content.Load<Texture2D>(texturePath);
            sourceRectangles = SpriteSheetHelper.CreateKeeseFrames();
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
                if (randCount > 60* random.Next(1, 3))
                {
                    SetRandomDirection();
                    randCount = 0;
                }
                MoveKeese();

                if (timeElapsed > timePerFrame)
                {
                    currentFrame = (currentFrame + 1) % sourceRectangles.Length;
                    timeElapsed = 0f;
                }
                // Reset color after damage timer expires
                if (damageColorTimer <= 0)
                {
                    currentColor = Color.White;
                }
            }
        }
        private void SetRandomDirection()
        {
            currentDirection = (Direction)random.Next(0,4);
            
        }

        private void MoveKeese()
        {
            if (alive)
            {
                switch (currentDirection)
                {

                    case (Direction.Left):
                        //if(!blocked){
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
                        if (position.X + speed.X < 214 * _scale.X)
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
                        if (position.Y + speed.Y < 134 * _scale.Y)
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
            if (alive)
            {
                spriteBatch.Draw(spriteSheet, position, sourceRectangles[currentFrame], currentColor, 0f, Vector2.Zero, _scale, SpriteEffects.None, 0f);
            }
        }

     
        public void TakeDamage()
        {
            currentColor = Color.Red;
            damageColorTimer = DAMAGE_COLOR_DURATION;
            hurtTime -= 1;
            if (hurtTime <= 0)
            {
                alive = false;
                position.X = 20000;
                position.Y = 20000;
            }
        }

        

        public void Reset()
        {
            position = initialPosition;
            currentFrame = 0;
            timeElapsed = 0f;
            damageColorTimer = 0f;
            currentColor = Color.White;
        }
    }
}
