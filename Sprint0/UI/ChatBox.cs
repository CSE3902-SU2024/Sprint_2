using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;

namespace Sprint2.UI
{
    public class ChatBox
    {
        private Texture2D chatBoxTexture;
        private SpriteFont font;
        private Rectangle bounds;
        private string currentMessage;
        private bool isVisible;
        private Vector2 textPosition;
        private Vector2 _scale;
        private const int TEXT_PADDING_X = 3;
        private const int TEXT_PADDING_Y = 10;

        public ChatBox(GraphicsDevice graphicsDevice, ContentManager content, Vector2 scale)
        {
            _scale = scale;
            
            chatBoxTexture = content.Load<Texture2D>("ChatBox");

            
            font = content.Load<SpriteFont>("Font");

            // chatbox position and size (later will change according to the text length) so text can fit in chat box
            bounds = new Rectangle(
                (int)(160 * scale.X),  // X position
                (int)(130 * scale.Y), // Y position  
                (int)(33 * scale.X), // Width
                (int)(25 * scale.Y)   // Height
            );

            textPosition = new Vector2(
            bounds.X + TEXT_PADDING_X * scale.X,
            bounds.Y + TEXT_PADDING_Y * scale.Y
            );

            isVisible = false;
        }

        public void Show(string message)
        {
            currentMessage = message;
            isVisible = true;
        }

        public void Hide()
        {
            isVisible = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!isVisible) return;

            // chat background
            spriteBatch.Draw(
                chatBoxTexture,
                bounds,
                null,
                Color.White,
                0f,
                Vector2.Zero,
                SpriteEffects.None,
                0f
            );

          

            // Draw text
            if (currentMessage != null)
            {
                spriteBatch.DrawString(
                    font,
                    currentMessage,
                    textPosition,
                    Color.Red  
                );
            }
        }

        public void Update()
        {
            // later will update chatbox (more conversation)
        }
    }
}