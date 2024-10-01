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
        private Rectangle[] sourceRectangles;  
        private Rectangle[] projectileRectangles;  
        private Vector2 position;
        private Vector2 initialPosition;
        private float movementRange = 100f; // The range within which the enemies move
        private bool movingRight = true;
        private bool movingUp = false;
        private bool movingLeft = false;
        private bool movingDown = false;
        private int currentFrame;
        private float timePerFrame = 0.1f; // 100ms per frame 
        private float timeElapsed;

        private float projectileCooldown = 1f; // 1 second cooldown between shots
        private float timeSinceLastShot;
        private List<Projectile> projectiles; // To store all projectiles
        
        private bool isFlipped = false;

        private List<Enemy> enemies;
        private int currentEnemyIndex = 0;
        private Direction currentDirection = Direction.Right;


        private bool hasThrownBoomerang = false;
        private bool waitingForBoomerang = false;
        private float boomerangWaitTime = 1.3f;  // Wait for 1.3 second after the boomerang is thrown
        private float boomerangTimer = 0f;
        public EnemyType currentEnemyType { get; set; }  



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
            Goriya,
            Stalfos,
            Keese,
            Gel
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
            switch (currentEnemyType)
            {
                case EnemyType.Dragon:
                    sourceRectangles = SpriteSheetHelper.CreateDragonFrames(); // Dragon frames
                    projectileRectangles = SpriteSheetHelper.CreateProjectileFrames(); // Dragon's projectiles
                    break;
                case EnemyType.Goriya:
                    sourceRectangles = SpriteSheetHelper.CreateGoriyaFrames(); // Goriya frames
                    projectileRectangles = SpriteSheetHelper.CreateBoomerangFrames(); // Goriya's boomerang
                    break;
                case EnemyType.Stalfos:
                    sourceRectangles = SpriteSheetHelper.CreateStalfosFrames();
                    break;
                case EnemyType.Keese:
                    sourceRectangles = SpriteSheetHelper.CreateKeeseFrames();
                    break;
                case EnemyType.Gel:
                    sourceRectangles = SpriteSheetHelper.CreateGelFrames();
                    break;
            }
        }

        public void Update(GameTime gameTime)
        {
            timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

            switch (currentEnemyType)
            {
                case EnemyType.Dragon:
                    MoveDragon();
                    if (timeElapsed > timePerFrame)
                    {
                        currentFrame = (currentFrame + 1) % sourceRectangles.Length;
                        timeElapsed = 0f;
                    }
                    break;
                case EnemyType.Goriya:
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
                    }
                    break;
                case EnemyType.Stalfos:
                    MoveRandom();
                    if (timeElapsed > 0.1f)
                    {
                        isFlipped = !isFlipped;
                        timeElapsed = 0f;
                    }
                    break;
                case EnemyType.Keese:
                    MoveRandom();
                    if (timeElapsed > timePerFrame)
                    {
                        currentFrame = (currentFrame + 1) % sourceRectangles.Length;
                        timeElapsed = 0f;
                    }
                    break;
                case EnemyType.Gel:
                    MoveRandom();
                    if (timeElapsed > timePerFrame)
                    {
                        currentFrame = (currentFrame + 1) % sourceRectangles.Length;
                        timeElapsed = 0f;
                    }
                    break;
            }

            timeSinceLastShot += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timeSinceLastShot > projectileCooldown)
            {
                ShootProjectiles();
                timeSinceLastShot = 0f;
            }

            for (int i = 0; i < projectiles.Count; i++)
            {
                projectiles[i].Update(gameTime);

                if (projectiles[i] is Boomerang boomerang && boomerang.IsReturned())
                {
                    projectiles.RemoveAt(i);
                    i--;
                    waitingForBoomerang = false;
                    hasThrownBoomerang = false;
                }
            }
        }
        private void MoveDragon()
        {
            if (movingRight)
            {
                position.X += 1f;  
                if (position.X >= initialPosition.X + movementRange)
                    movingRight = false; // Switch direction when hitting the right edge of range
            }
            else
            {
                position.X -= 1f;  
                if (position.X <= initialPosition.X - movementRange)
                    movingRight = true; // Switch direction  
            }
        }
        
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

        private Random random = new Random();

        //randomly choose a direction moving pattern
        private void MoveRandom()
        {
            switch (currentDirection)
            {
                case Direction.Right:
                    position.X += 1f;
                    if (position.X >= initialPosition.X + movementRange)
                    {
                        ChangeDirection();  
                    }
                    break;

                case Direction.Left:
                    position.X -= 1f;
                    if (position.X <= initialPosition.X - movementRange)
                    {
                        ChangeDirection();  
                    }
                    break;

                case Direction.Up:
                    position.Y -= 1f;
                    if (position.Y <= initialPosition.Y - movementRange)
                    {
                        ChangeDirection();  
                    }
                    break;

                case Direction.Down:
                    position.Y += 1f;
                    if (position.Y >= initialPosition.Y + movementRange)
                    {
                        ChangeDirection();  
                    }
                    break;
            }
        }

        private void ChangeDirection()
        {
            int newDirection = random.Next(0, 4);  
            currentDirection = (Direction)newDirection;  
        }


        //Shoot boomerang
        private void ShootBoomerang()
        {
            if (!hasThrownBoomerang)
            {
                Vector2 projectilePosition = new Vector2(position.X, position.Y);
                //Goriya's boomerang
                projectiles.Add(new Boomerang(spriteSheet, projectilePosition, new Vector2(200, 0), projectileRectangles)); 
                hasThrownBoomerang = true;
                waitingForBoomerang = true;   
            }
        }

        // Shoot projectiles
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
           
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            SpriteEffects spriteEffect = isFlipped ? SpriteEffects.FlipHorizontally : SpriteEffects.None;



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

        public void Reset()
        {
            position = initialPosition;
            movingRight = true;
            movingUp = false;
            movingLeft = false;
            movingDown = false;
            currentFrame = 0;
            timeElapsed = 0f;
            timeSinceLastShot = 0f;
            projectiles.Clear();
            hasThrownBoomerang = false;
            waitingForBoomerang = false;
            boomerangTimer = 0f;
        }
    }


    // The Projectile Class
    public class Projectile
    {
        protected Texture2D spriteSheet;   
        protected Rectangle[] sourceRectangles;
        protected int currentFrame;
        protected Vector2 position;
        protected Vector2 velocity;   
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
            return position.X < 0 || position.Y < 0 || position.X > 800 || position.Y > 480;  
        }
    }
    public class Boomerang : Projectile
    {
        private Vector2 startPosition;
        private bool returning = false;

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
            if (!returning && Vector2.Distance(startPosition, position) > 150)
            {
                velocity *= -1;  // Reverse the velocity to return
                returning = true;
            }
        }
        public bool IsReturned()
        {
            // Check if boomerang has returned to Goriya
            return returning && Vector2.Distance(startPosition, position) < 1f;
        }
    }

}
