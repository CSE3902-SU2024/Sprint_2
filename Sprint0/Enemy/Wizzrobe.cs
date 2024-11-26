using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Sprint0.Classes;
using System;
using Sprint0.Player;
using Sprint2.UI;

namespace Sprint2.Enemy
{
    public class Wizzrobe : IEnemy
    {
        private Texture2D spriteSheet;
        private Rectangle[] sourceRectangles;
        private Vector2 position;
        private Vector2 initialPosition;
        private int currentFrame;
        private float timeElapsed;
        private Vector2 _scale;
        private Boolean alive;
        private ChatBox chatBox;
        private bool isPlayerNearby;
        private const float INTERACTION_DISTANCE = 32f;  
        public Link _link;

        public Vector2 Position { get => position; set => position = value; }
        public int Width { get; } = 16;
        public int Height { get; } = 16;

         

        public Wizzrobe(Vector2 startPosition, Link link)
        {
            position = startPosition;
            initialPosition = startPosition;
            alive = true;
            _link = link;
            isPlayerNearby = false;

        }

        public void LoadContent(ContentManager content, string texturePath, GraphicsDevice graphicsdevice, Vector2 scale)
        {
            spriteSheet = content.Load<Texture2D>(texturePath);
         
            sourceRectangles = SpriteSheetHelper.CreateWizzrobeFrames();
            _scale = scale;
            chatBox = new ChatBox(graphicsdevice, content, scale);
        }
        

        public void Update(GameTime gameTime)
        {
            if (alive)
            {
               
                timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

                  
                if (timeElapsed > 0.2f)
                {
                    currentFrame = (currentFrame + 1) % sourceRectangles.Length;
                    timeElapsed = 0f;
                }
                float distance = Vector2.Distance(_link._position, position);
                bool wasNearby = isPlayerNearby;
                isPlayerNearby = distance < INTERACTION_DISTANCE * _scale.X;

                // Show chat box when player close to chat box
                if (isPlayerNearby && !wasNearby)
                {
                    chatBox.Show("Hello traveler!");
                }
                else if (!isPlayerNearby && wasNearby)
                {
                    chatBox.Hide();
                }

                chatBox.Update();



            }
            }
        

        public void Draw(SpriteBatch spriteBatch)
        {
            if (alive)
            {
                spriteBatch.Draw(spriteSheet, position, sourceRectangles[currentFrame], Color.White, 0f, Vector2.Zero, _scale, SpriteEffects.None, 0f);
                if (isPlayerNearby)
                {
                    chatBox.Draw(spriteBatch);
                }
            }
        }

        
        public void TakeDamage()
        {
            // NPCs don't take dmg
        }

        public Boolean GetState()
        {
            return alive;
        }

        public void Reset()
        {
            position = initialPosition;
            currentFrame = 0;
            timeElapsed = 0f;
            
        }
    }
}