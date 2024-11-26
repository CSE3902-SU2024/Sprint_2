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
        public ChatBox chatBox;
        private bool isPlayerNearby = false;
        private const float INTERACTION_DISTANCE = 32f;  
        public Link _link;

        public Vector2 Position { get => position; set => position = value; }
        public int Width { get; } = 16;
        public int Height { get; } = 16;

        private readonly string[] conversationLines = new string[]
        {
        "Hello traveler",
        "Welcome to Dungeon",
        "Pick up your weapon"
        };
        private readonly string finalMessage = "Go, traveler";
        private bool canInteract = false;

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
            if (alive && chatBox != null)
            {
               
                timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

                  
                if (timeElapsed > 0.2f)
                {
                    currentFrame = (currentFrame + 1) % sourceRectangles.Length;
                    timeElapsed = 0f;
                }
                float distance = Vector2.Distance(_link._position, position);
                bool wasNearby = canInteract;
                canInteract = distance < 32 * _scale.X; // Interaction distance

                if (canInteract && !wasNearby)
                {
                    chatBox.Show("Press F to talk");
                }
                else if (!canInteract && wasNearby)
                {
                    chatBox.Hide();
                }

                chatBox?.Update();



            }
            }
        public bool CanInteract => canInteract;

        public void StartConversation()
        {
            if (chatBox != null)   
            {
                chatBox.StartConversation(conversationLines, finalMessage);
            }
        }

        public void AdvanceConversation()
        {
            if (chatBox != null)   
            {
                chatBox.AdvanceConversation();
            }
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            if (alive)
            {
                spriteBatch.Draw(spriteSheet, position, sourceRectangles[currentFrame], Color.White, 0f, Vector2.Zero, _scale, SpriteEffects.None, 0f);
                chatBox?.Draw(spriteBatch);
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