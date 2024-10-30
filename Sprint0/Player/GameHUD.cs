using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Sprint0.Player;
using Sprint2.Collisions;
using System.Collections.Generic;
using Sprint2.Enemy;
using Sprint0.Collisions;
using System;
using static System.Formats.Asn1.AsnWriter;
using Sprint2.Classes;
using Sprint2.GameStates;

namespace Sprint2
{
    public class GameHUD
    {
        private SpriteBatch _spriteBatch;
        private Texture2D _hudTexture;
        private Rectangle _hudBackground;
        private Vector2 _scale;
        private Link _link;

        private Rectangle _healthBarPosition;
        private const int HUD_WIDTH = 256;
        private const int HUD_HEIGHT = 48;
      
        private const int HEART_WIDTH = 8;
        private const int HEART_HEIGHT = 8;
        private const int HEART_SPACING = 12;  // Space between hearts
        private const int MAX_HEALTH = 3;

        private readonly GraphicsDevice graphicsDevice;
        private readonly ContentManager content;
        public GameHUD(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, ContentManager content, Link link, Vector2 scale)
        {
            _spriteBatch = spriteBatch;
            _link = link;
            _scale = scale;
            this.graphicsDevice = graphicsDevice;
            this.content = content;
            LoadContent(content);
            InitializeHUDPositions();
        }

        private void LoadContent(ContentManager content)
        {
            try
            {
                _hudTexture = content.Load<Texture2D>("NES - The Legend of Zelda - HUD & Pause Screen");
            }
            catch (ContentLoadException e)
            {
                Console.WriteLine($"Error loading content: {e.Message}");
                throw;
            }
        }
        private void InitializeHUDPositions()
        {
            _hudBackground = new Rectangle(258, 11, 256, 48);//(int)(HUD_WIDTH * _scale.X), (int)(HUD_HEIGHT * _scale.Y));
            _healthBarPosition = new Rectangle(20, 20, (int)(HEART_WIDTH * _scale.X), (int)(HEART_HEIGHT * _scale.Y));

        }
        public void Draw()
        {
            //background
            _spriteBatch.Begin();
            _spriteBatch.Draw(_hudTexture, _hudBackground, new Rectangle(0, 0, HUD_WIDTH, HUD_HEIGHT), Color.White);

            //healthbar
            for (int i = 0; i < MAX_HEALTH; i++)
            {
                Rectangle heartSource = new Rectangle(i < _link.Health ? 0 : HEART_WIDTH, 0, HEART_WIDTH, HEART_HEIGHT);
                _spriteBatch.Draw(_hudTexture, new Rectangle(_healthBarPosition.X + (i * (int)(HEART_WIDTH * 1.5f * _scale.X)), _healthBarPosition.Y, _healthBarPosition.Width, _healthBarPosition.Height), heartSource, Color.White);
            }
            _spriteBatch.End();
        }
        public static void DrawRectangle(SpriteBatch spriteBatch, Rectangle rectangle, Color color, Vector2 scale, int lineWidth = 1)
        {
            // Scale only the size, not the position
            Rectangle scaledRectangle = new Rectangle(
                rectangle.Left, // Keep the original position
                rectangle.Top,  // Keep the original position
                (int)(rectangle.Width * scale.X), // Scale the width
                (int)(rectangle.Height * scale.Y) // Scale the height
            );
        }
    }
}