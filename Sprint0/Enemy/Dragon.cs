using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Sprint0.Classes;
using System;
using Sprint2.Enemy.Projectiles;
using System.Collections.Generic;

namespace Sprint2.Enemy
{
    public class Dragon : IEnemy
    {
        private Texture2D spriteSheet;
        private Rectangle[] sourceRectangles;
        public Vector2 position;
        public Vector2 initialPosition;
        private int currentFrame;
        private bool movingRight = true;
        private float movementRange = 100f;
        private float timePerFrame = 0.1f;
        private float timeElapsed;
        private Color currentColor = Color.White; 
        private float damageColorTimer = 0f;
        private const float DAMAGE_COLOR_DURATION = 0.5f;
        private float fireballCooldown = 1f;
        private float timeSinceLastShot;
        private Vector2 _scale;
        private int health;


        private Rectangle[] fireballRectangles; 

       
        public Vector2 Position { get => position; set => position = value; }
        public int Width { get; private set; } = 24;
        public int Height { get; private set; } = 32;


        public List<Fireball> fireballs { get; private set; }
        public Dragon(Vector2 startPosition)
        {
            health = 100;
            position = startPosition;
            initialPosition = startPosition;
            fireballs = new List<Fireball>();
        }

      
        public void LoadContent(ContentManager content, string texturePath, GraphicsDevice graphicsdevice)
        {
            spriteSheet = content.Load<Texture2D>(texturePath);
            sourceRectangles = SpriteSheetHelper.CreateDragonFrames();
            fireballRectangles = SpriteSheetHelper.CreateFireballFrames(); 
            _scale.X = (float)graphicsdevice.Viewport.Width / 256.0f;
            _scale.Y = (float)graphicsdevice.Viewport.Height / 176.0f;
        }

       
        public void Update(GameTime gameTime)
        {
            timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
            damageColorTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            MoveDragon();

            if (timeElapsed > timePerFrame)
            {
                currentFrame = (currentFrame + 1) % sourceRectangles.Length;
                timeElapsed = 0f;
            }

          
            if (damageColorTimer <= 0)
            {
                currentColor = Color.White;
            }

           
            timeSinceLastShot += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timeSinceLastShot > fireballCooldown)
            {
                ShootFireball();  
                timeSinceLastShot = 0f;
            }

         
            for (int i = 0; i < fireballs.Count; i++)
            {
                fireballs[i].Update(gameTime);

             
                if (fireballs[i].IsOffScreen())
                {
                    fireballs.RemoveAt(i);
                    i--;
                }
            }
        }

   
        private void MoveDragon()
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

      
        private void ShootFireball()
        {
            Vector2 fireballPosition = new Vector2(position.X, position.Y);

            // Add fireballs going in different directions
            fireballs.Add(new Fireball(spriteSheet, fireballPosition, new Vector2(-200, 0), fireballRectangles)); 
            fireballs.Add(new Fireball(spriteSheet, fireballPosition, new Vector2(-200, -100), fireballRectangles)); 
            fireballs.Add(new Fireball(spriteSheet, fireballPosition, new Vector2(-200, 100), fireballRectangles)); 
        }

        
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                spriteSheet,
                position,
                sourceRectangles[currentFrame],
                currentColor,
                0f,
                Vector2.Zero,
                _scale,
                SpriteEffects.None,
                0f
            );

           
            foreach (var fireball in fireballs)
            {
                fireball.Draw(spriteBatch);
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
            fireballs.Clear(); 
        }

       
        public void TakeDamage()
        {
            health -= 1;
            currentColor = Color.Red;
            damageColorTimer = DAMAGE_COLOR_DURATION;
            if (health <= 0)
            {
                position.X = 20000;
                position.Y = 20000;
            }   
        }
    }
}

