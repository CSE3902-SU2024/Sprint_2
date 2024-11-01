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
        private Rectangle[] cutOuts;
        private Vector2 _scale;
        private Link _link;

        private Rectangle _healthBarPosition;
        private const int HUD_WIDTH = 256;
        private const int HUD_HEIGHT = 48;
      
        private const int HEART_WIDTH = 8;
        private const int HEART_HEIGHT = 8;
        private const int HEART_SPACING = 2;  // Space between hearts
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
            _hudBackground = new Rectangle(0, 0, graphicsDevice.Viewport.Width, (int)(HUD_HEIGHT * _scale.Y * 1.2));
            _healthBarPosition = new Rectangle((int)(100 * _scale.X), (int)(10 * _scale.Y), (int)(HEART_WIDTH * _scale.X), (int) (HEART_HEIGHT * _scale.Y));

            cutOuts = new Rectangle[]
             {
                  new Rectangle(258, 11,255,55) ,  //the background
                  new Rectangle(645, 117, 8, 8)     // 1 full heart
             };

        }
        public void Draw()
        {
            //background
            _spriteBatch.Begin();
            _spriteBatch.Draw(_hudTexture, _hudBackground, cutOuts[0], Color.White);


            //healthbar
            for (int i = 0; i < MAX_HEALTH; i++)
            {
                Rectangle heartSource = i < _link.Health ? cutOuts[1] : new Rectangle(cutOuts[0].X + HEART_WIDTH, cutOuts[0].Y, HEART_WIDTH, HEART_HEIGHT);
                _spriteBatch.Draw(_hudTexture, new Rectangle(_healthBarPosition.X + (i * (int)(HEART_WIDTH * _scale.X)), _healthBarPosition.Y, _healthBarPosition.Width, _healthBarPosition.Height), heartSource, Color.White);
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