using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Sprint0.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprint2.Enemy.Projectiles;

namespace Sprint2.Enemy
{
    public class Goriya : IEnemy
    {
        private Texture2D spriteSheet;
        private Rectangle[] sourceRectangles;
        private Vector2 position;
        private Vector2 initialPosition;
        private int currentFrame;
        private bool movingRight = true;
        private bool movingUp = false;
        private bool movingLeft = false;
        private bool movingDown = false;
        private float movementRange = 100f;
      
        private bool hasThrownBoomerang = false;
        private bool waitingForBoomerang = false;
        private float boomerangWaitTime = 1.3f;
        private float boomerangTimer = 0f;

        private float timeElapsed;
        private bool isFlipped = false; 
        private Color currentColor = Color.White;
        private float damageColorTimer = 0f;
        private const float DAMAGE_COLOR_DURATION = 0.5f;
        private Vector2 _scale;


        public List<Boomerang> projectiles { get; private set; }


        // Implement IEnemy properties
        public Vector2 Position { get => position; set => position = value; }
        public int Width { get; } = 16;
        public int Height { get; } = 16;

        // Constructor
        public Goriya(Vector2 startPosition)
        {
            position = startPosition;
            initialPosition = startPosition;
            projectiles = new List<Boomerang>();
        }

        // Load content and sprites
        public void LoadContent(ContentManager content, string texturePath, GraphicsDevice graphicsdevice)
        {
            spriteSheet = content.Load<Texture2D>(texturePath);
            sourceRectangles = SpriteSheetHelper.CreateGoriyaFrames();
            _scale.X = (float)graphicsdevice.Viewport.Width / 256.0f;
            _scale.Y = (float)graphicsdevice.Viewport.Height / 176.0f;
        }

        // Update the Goriya state (movement, animation, etc.)
        public void Update(GameTime gameTime)
        {
            timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
            damageColorTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            MoveGoriya(gameTime);
            if (timeElapsed > 0.1f)
            {
                if (movingRight)
                {
                    currentFrame = (currentFrame == 2) ? 3 : 2;
                    isFlipped = false;
                    timeElapsed = 0f;
                }
                else if (movingUp)
                {
                    isFlipped = !isFlipped;
                    currentFrame = 1;
                    timeElapsed = 0f;
                }
                else if (movingLeft)
                {
                    currentFrame = (currentFrame == 2) ? 3 : 2;
                    isFlipped = true;
                    timeElapsed = 0f;
                }
                else if (movingDown)
                {
                    isFlipped = !isFlipped;
                    currentFrame = 0;
                    timeElapsed = 0f;
                }
                if (damageColorTimer <= 0)
                {
                    currentColor = Color.White; //current damage logic
                }
            }

            // Update boomerang projectiles
            for (int i = 0; i < projectiles.Count; i++)
            {
                projectiles[i].Update(gameTime);

                // Remove boomerang if it returns
                if (projectiles[i].IsReturned())
                {
                    projectiles.RemoveAt(i);
                    waitingForBoomerang = false;
                    hasThrownBoomerang = false;
                }
            }
        }

        // Move Goriya, shoot boomerang at the edge of range
        private void MoveGoriya(GameTime gameTime)
        {
            if (waitingForBoomerang)
            {
                boomerangTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (boomerangTimer >= boomerangWaitTime)
                {
                    waitingForBoomerang = false;
                    boomerangTimer = 0f;
                }
                return;
            }

            if (movingRight)
            {
                position.X += 1f; // Move right

                if (position.X >= initialPosition.X + movementRange)
                {
                    ShootBoomerang();
                    if (waitingForBoomerang == false)
                    {
                        movingRight = false; // Switch direction when hitting the right edge of range
                        movingUp = true;
                    }
                }

            }
            else if (movingUp)
            {
                position.Y -= 1f;

                if (position.Y <= initialPosition.Y - movementRange)
                    movingUp = false;
                movingLeft = true;
            }
            else if (movingLeft)
            {
                position.X -= 1f;
                if (position.X <= initialPosition.X - movementRange)
                    movingLeft = false;
                movingDown = true;
            }
            else if (movingDown)
            {
                position.Y += 1f;

                if (position.Y >= initialPosition.Y + movementRange)
                    movingRight = true;
            }


        }


        // Shoot boomerang
        private void ShootBoomerang()
        {
            if (!hasThrownBoomerang)
            {
                Vector2 projectilePosition = new Vector2(position.X, position.Y);
                projectiles.Add(new Boomerang(spriteSheet, projectilePosition, new Vector2(200, 0), SpriteSheetHelper.CreateBoomerangFrames(), 5.0f));
                hasThrownBoomerang = true;
                waitingForBoomerang = true;
            }
        }

        // Draw Goriya and projectiles (boomerang)
        public void Draw(SpriteBatch spriteBatch)
        {
            SpriteEffects spriteEffect = isFlipped ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            spriteBatch.Draw(spriteSheet, position, sourceRectangles[currentFrame], currentColor, 0f, Vector2.Zero, _scale, spriteEffect, 0f);
            foreach (var projectile in projectiles)
            {
                projectile.Draw(spriteBatch);
            }
        }

        // Handle damage (from IEnemy interface)
        public void TakeDamage()
        {
            currentColor = Color.Red;
            damageColorTimer = DAMAGE_COLOR_DURATION;
        }

        // Reset Goriya state (from IEnemy interface)
        public void Reset()
        {
            position = initialPosition;
            movingRight = true;
            currentFrame = 0;
            hasThrownBoomerang = false;
            waitingForBoomerang = false;
            boomerangTimer = 0f;
            damageColorTimer = 0f;
            currentColor = Color.White;
            projectiles.Clear(); // Clear boomerangs
        }
    }
}

