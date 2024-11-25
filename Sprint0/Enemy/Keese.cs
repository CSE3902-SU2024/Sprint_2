using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Sprint0.Classes;
using System;
using Microsoft.Xna.Framework.Audio;
using Sprint0.Player;

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
        private int health;
        private Vector2 speed;
        private Vector2 _scale;
        private Random random;
        private Boolean alive;
        private Direction currentDirection;
        private int randCount;

        public SpriteBatch spriteBatch;
        public Texture2D enemyDeath;
        private bool isDying;
        private float deathAnimationTimer = 0f;
        private const float DEATH_ANIMATION_DURATION = 0.5f;
        public SoundEffect deathSound;
        private int immunityDuration = 10;
        private int remainingImmunityFrames = 0;
        private bool isImmune;
        public Link _link;





        private int currentDeathFrame = 0;
        private float deathFrameTime = 0.1f; // Time each death frame is displayed
        private float deathFrameElapsed = 0f;
        private Rectangle[] deathSourceRectangles = { new Rectangle(0, 0, 15, 15), new Rectangle(16, 0, 15, 15), new Rectangle(32, 0, 15, 15), new Rectangle(48, 0, 15, 15)
        };

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


        public Keese(Vector2 startPosition, Link link)
        {
            health = 2;
            position = startPosition;
            initialPosition = startPosition;
            alive = true;
            random = new Random();
            SetRandomDirection();
            _link = link;



        }

        public void LoadContent(ContentManager content, string texturePath, GraphicsDevice graphicsdevice, Vector2 scale)
        {
            spriteSheet = content.Load<Texture2D>(texturePath);
            sourceRectangles = SpriteSheetHelper.CreateKeeseFrames();
            _scale = scale;
            speed = Vector2.One;

            enemyDeath = content.Load<Texture2D>("EnemyDeath");
            deathSound = content.Load<SoundEffect>("OOT_Enemy_Poof1");
        }

        public void Update(GameTime gameTime)
        {
            if (isDying)
            {

                deathFrameElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (deathFrameElapsed >= deathFrameTime)
                {
                    currentDeathFrame++;

                    deathFrameElapsed = 0f;

                    if (currentDeathFrame >= deathSourceRectangles.Length)
                    {
                        isDying = false;
                        position = new Vector2(20000, 20000); // Move off screen
                    }
                }
            }
            else if (alive)
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
                if (isImmune)
                {
                    remainingImmunityFrames--;

                    if (remainingImmunityFrames <= 0)
                    {
                        isImmune = false;
                    }
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

                            if (position.X <= (32))
                            {
                                position.X += 1f;
                            }
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

                            if (position.X <= (224))
                            {
                                position.X -= 1f;
                            }
                        }
                        else
                        {
                            currentDirection = Direction.Left;
                        }
                        break;
                    case (Direction.Up):
                        if (position.Y - speed.Y > 87 * _scale.Y)
                        {
                            position.Y -= speed.Y;

                            if (position.X <= (224))
                            {
                                position.Y -= 1f;
                            }
                        }
                        else
                        {
                            currentDirection = Direction.Down;
                        }
                        break;
                    case (Direction.Down):
                        if (position.Y + speed.Y < 143 * _scale.Y)
                        {
                            position.Y += speed.Y;

                            if (position.X <= (224))
                            {
                                position.Y -= 1f;
                            }
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
            if (isDying)
            {
                spriteBatch.Draw(enemyDeath, position, deathSourceRectangles[currentDeathFrame], Color.White, 0f, Vector2.Zero, _scale, SpriteEffects.None, 0f);
            }
            else if (alive)
            {
                spriteBatch.Draw(spriteSheet, position, sourceRectangles[currentFrame], currentColor, 0f, Vector2.Zero, _scale, SpriteEffects.None, 0f);
            }
        }

     
        public void TakeDamage()
        {
            currentColor = Color.Red;
            damageColorTimer = DAMAGE_COLOR_DURATION;
            if (!isImmune)
            {
                if (_link.hasPotion)
                {
                    health = Math.Max(0, 0);
                    _link.hasPotion = false;
                }
                else
                {
                    health = Math.Max(0, health - 1);
                }
                remainingImmunityFrames = immunityDuration;
                isImmune = true;
            }

            if (health <= 0 && alive)
            {
                alive = false;
                isDying = true;
                deathAnimationTimer = DEATH_ANIMATION_DURATION;
                deathSound.Play();
            }

            //if (hurtTime <= 0)
            //{
            //    alive = false;
            //    position.X = 20000;
            //    position.Y = 20000;
            //}
        }

        

        public void Reset()
        {
            position = initialPosition;
            currentFrame = 0;
            timeElapsed = 0f;
            damageColorTimer = 0f;
            currentColor = Color.White;
        }
        public Boolean GetState()
        {
            return alive;
        }

    }
}
