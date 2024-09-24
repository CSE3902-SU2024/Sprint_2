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
        private Rectangle[] sourceRectangles; // Dragon frames
        private Rectangle[] projectileRectangles; // Projectile frames
        private Vector2 position;
        private Vector2 initialPosition;
        private float movementRange = 100f; // The range within which the dragon will move left-right
        private bool movingRight = true;
        private int currentFrame;
        private float timePerFrame = 0.1f; // 100ms per frame for dragon
        private float timeElapsed;

        private float projectileCooldown = 1f; // 1 second cooldown between shots
        private float timeSinceLastShot;
        private List<Projectile> projectiles; // To store all projectiles
        private Direction enemyDirection;
        
        private List<Enemy> enemies;
        private int currentEnemyIndex = 0;
        public EnemyType currentEnemyType { get; set; }  // Add { get; set; } to make it accessible


        public enum Direction
        {
            Left,
            Right,
            Up,
            Down
        }
        public enum EnemyType
        {
            Dragon,
            Goriya
        }

        public Enemy(Vector2 startPosition)
        {
            position = startPosition;
            initialPosition = startPosition;
            projectiles = new List<Projectile>();
            enemies = new List<Enemy>();
            currentEnemyType = EnemyType.Dragon;
        }

        public void LoadContent(ContentManager content, string texturePath)
        {
            spriteSheet = content.Load<Texture2D>(texturePath);
            if (currentEnemyType == EnemyType.Dragon)
            {
                sourceRectangles = SpriteSheetHelper.CreateDragonFrames(); // Dragon frames
                projectileRectangles = SpriteSheetHelper.CreateProjectileFrames(); // Dragon's projectiles
            }
            else if (currentEnemyType == EnemyType.Goriya)
            {
                sourceRectangles = SpriteSheetHelper.CreateGoriyaFrames(); // Goriya frames
                projectileRectangles = SpriteSheetHelper.CreateBoomerangFrames(); // Goriya's boomerang
            }
        }

        public void Update(GameTime gameTime)
        {
            
            
            timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Animate the current enemy
            if (timeElapsed > timePerFrame)
            {
                currentFrame = (currentFrame + 1) % sourceRectangles.Length;
                timeElapsed = 0f;
            }

            // Update movement (left-right for Dragon, up-down-left-right for Goriya)
            if (currentEnemyType == EnemyType.Dragon)
            {
                if (movingRight)
                {
                    position.X += 1f; // Move right
                    if (position.X >= initialPosition.X + movementRange)
                        movingRight = false; // Switch direction when hitting the right edge of range
                }
                else
                {
                    position.X -= 1f; // Move left
                    if (position.X <= initialPosition.X - movementRange)
                        movingRight = true; // Switch direction when hitting the left edge of range
                }
            }
            else if (currentEnemyType == EnemyType.Goriya)
            {
                MoveGoriya(); // Custom Goriya movement (up-down-left-right)
            }

            // Shoot projectiles/boomerang every 1 second
            timeSinceLastShot += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timeSinceLastShot > projectileCooldown)
            {
                ShootProjectiles();
                timeSinceLastShot = 0f;
            }

            // Update all projectiles
            for (int i = 0; i < projectiles.Count; i++)
            {
                projectiles[i].Update(gameTime);

                // Optionally, remove the projectile if it goes off-screen
                if (projectiles[i].IsOffScreen())
                {
                    projectiles.RemoveAt(i);
                    i--;
                }
            }
        }

        private void MoveGoriya()
        {
            // Basic Goriya movement logic
            switch (enemyDirection)
            {
                case Direction.Up:
                    position.Y -= 1f; // Move up
                    currentFrame = 1; // Facing up frame
                    break;
                case Direction.Down:
                    position.Y += 1f; // Move down
                    currentFrame = 0; // Facing down frame
                    break;
                case Direction.Right:
                    position.X += 1f; // Move right
                    currentFrame = 2; // Facing right frame
                    break;
                case Direction.Left:
                    position.X -= 1f; // Move left
                    currentFrame = 2; // Use the same frame but flip horizontally
                    break;
            }
        }

        public void ChangeDirection(Direction newDirection)
        {
            enemyDirection = newDirection;
        }

        private void ShootProjectiles()
        {
            Vector2 projectilePosition = new Vector2(position.X, position.Y);

            if (currentEnemyType == EnemyType.Dragon)
            {
                // Dragon's projectiles
                projectiles.Add(new Projectile(spriteSheet, projectilePosition, new Vector2(-200, 0), projectileRectangles)); // Left
                projectiles.Add(new Projectile(spriteSheet, projectilePosition, new Vector2(-200, -100), projectileRectangles)); // Left-top
                projectiles.Add(new Projectile(spriteSheet, projectilePosition, new Vector2(-200, 100), projectileRectangles)); // Left-bottom
            }
            else if (currentEnemyType == EnemyType.Goriya)
            {
                // Goriya's boomerang (for simplicity, it only shoots one boomerang)
                projectiles.Add(new Boomerang(spriteSheet, projectilePosition, new Vector2(-200, 0), projectileRectangles));
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            SpriteEffects spriteEffect = SpriteEffects.None;


            float scale = 4.0f;

            spriteBatch.Draw(
                spriteSheet,
                position,
                sourceRectangles[currentFrame], // The current animation frame
                Color.White,
                0f,
                Vector2.Zero,
                scale,
                spriteEffect,
                0f
            );
            foreach (var projectile in projectiles)
            {
                projectile.Draw(spriteBatch);
            }

        }
        public Vector2 GetPosition()
        {
            return position;
        }
    }
    // The Projectile Class
    public class Projectile
    {
        protected Texture2D spriteSheet;  // Protected to allow access in derived classes
        protected Rectangle[] sourceRectangles;
        protected int currentFrame;
        protected Vector2 position;
        protected Vector2 velocity;  // Made protected to allow access in Boomerang
        private float timePerFrame = 0.1f; // Animation speed for projectile
        private float timeElapsed;
        private float scale = 2.0f;

        public Projectile(Texture2D spriteSheet, Vector2 startPosition, Vector2 velocity, Rectangle[] projectileFrames)
        {
            this.spriteSheet = spriteSheet;
            this.position = startPosition;
            this.velocity = velocity;
            this.sourceRectangles = projectileFrames;
            this.currentFrame = 0;
        }

        public virtual void Update(GameTime gameTime)
        {
            // Move the projectile
            position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Animate the projectile
            timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timeElapsed > timePerFrame)
            {
                currentFrame = (currentFrame + 1) % sourceRectangles.Length;
                timeElapsed = 0f;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                spriteSheet,
                position,
                sourceRectangles[currentFrame], // Current projectile frame
                Color.White,
                0f,
                Vector2.Zero,
                scale,
                SpriteEffects.None,
                0f
            );
        }

        public bool IsOffScreen()
        {
            // Check if the projectile goes off the screen
            return position.X < 0 || position.Y < 0 || position.X > 800 || position.Y > 480; // Adjust based on screen size
        }
    }
    public class Boomerang : Projectile
    {
        private Vector2 startPosition;

        public Boomerang(Texture2D spriteSheet, Vector2 startPosition, Vector2 velocity, Rectangle[] boomerangFrames)
            : base(spriteSheet, startPosition, velocity, boomerangFrames)
        {
            this.startPosition = startPosition;
        }

        // Override Update method to implement boomerang behavior
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            // Reverse boomerang when it reaches a certain distance from the start
            if (Vector2.Distance(startPosition, position) > 150)
            {
                velocity *= -1; // Start returning to Goriya
            }
        }
    }

}
