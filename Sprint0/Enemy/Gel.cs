﻿using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Sprint0.Classes;

namespace Sprint2.Enemy
{
    public class Gel : IEnemy
    {
        private Texture2D spriteSheet;
        private Rectangle[] sourceRectangles;
        public Vector2 position;
        private Vector2 initialPosition;
        private int currentFrame;
        private bool movingRight = true;
        private float movementRange = 100f;
        private float timePerFrame = 0.1f;
        private float timeElapsed;
        private Color currentColor = Color.White;  
        private float damageColorTimer = 0f;
        private const float DAMAGE_COLOR_DURATION = 0.5f;
        private Vector2 _scale;

       
        public Vector2 Position { get => position; set => position = value; }
        public int Width { get; } = 8;
        public int Height { get; } = 16;

        public Gel(Vector2 startPosition)
        {
            position = startPosition;
            initialPosition = startPosition;
        }

        public void LoadContent(ContentManager content, string texturePath, GraphicsDevice graphicsdevice)
        {
            spriteSheet = content.Load<Texture2D>(texturePath);
            sourceRectangles = SpriteSheetHelper.CreateGelFrames();
            _scale.X = (float)graphicsdevice.Viewport.Width / 256.0f;
            _scale.Y = (float)graphicsdevice.Viewport.Height / 176.0f;
        }

        public void Update(GameTime gameTime)
        {
            timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
            damageColorTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            MoveGel();

            if (timeElapsed > timePerFrame)
            {
                currentFrame = (currentFrame + 1) % sourceRectangles.Length;
                timeElapsed = 0f;
            }
            
            if (damageColorTimer <= 0)
            {
                currentColor = Color.White;
            }
        }

        private void MoveGel()
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
            spriteBatch.Draw(spriteSheet, position, sourceRectangles[currentFrame], currentColor, 0f, Vector2.Zero,_scale, SpriteEffects.None, 0f);
        }

      
        public void TakeDamage()
        {
            currentColor = Color.Red;
            damageColorTimer = DAMAGE_COLOR_DURATION;
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
    }
}
