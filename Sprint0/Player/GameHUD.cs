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
        private const int HEART_SPACING = 0;  // Space between hearts
        private const int MAX_HEALTH = 3;

        private const int MAX_HEALTH_POINTS = MAX_HEALTH * 2; // 3 hearts = 6 health points



        private readonly GraphicsDevice graphicsDevice;
        private readonly ContentManager content;
        public GameHUD(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, ContentManager content, Link link, Vector2 scale)
        {
            _spriteBatch = spriteBatch;
            _link = link;
            _scale = scale;
            this.graphicsDevice = graphicsDevice;
            _link.Health = MAX_HEALTH_POINTS;
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
            _healthBarPosition = new Rectangle(703,133, (int)(HEART_WIDTH * _scale.X*1.15), (int) (HEART_HEIGHT * _scale.Y*1.15));
            //hard coded heart position and scaling -> reason being HUD size is based on a scale and screenwidth. 

            cutOuts = new Rectangle[]
             {
                  new Rectangle(258, 11,255,55) ,  //the background
                  new Rectangle(645, 117, 8, 8),     // 1 full heart
                  new Rectangle(636, 117, 8, 8),     // 1 half heart
                  new Rectangle(627, 117, 8, 8)     // 1 emtpy heart
             };

        }
        public void Draw()
        {
            //background
            _spriteBatch.Begin();
            _spriteBatch.Draw(_hudTexture, _hudBackground, cutOuts[0], Color.White);


            for (int i = 0; i < MAX_HEALTH; i++)
            {
                // Each heart position corresponds to 2 health points
                int heartValue = _link.Health - (i * 2);

                // Determine the source rectangle based on heartValue
                Rectangle heartSource;
                if (heartValue >= 2)
                {
                    heartSource = cutOuts[1];  // Full heart
                }
                else if (heartValue == 1)
                {
                    heartSource = cutOuts[2];  // Half heart
                }
                else
                {
                    heartSource = cutOuts[3];  // Empty heart
                }

                // Draw each heart in the appropriate position with scaling
                _spriteBatch.Draw(
                    _hudTexture,
                    new Rectangle(
                        _healthBarPosition.X + (i * (int)((HEART_WIDTH + HEART_SPACING) * _scale.X)),
                        _healthBarPosition.Y,
                        _healthBarPosition.Width,
                        _healthBarPosition.Height
                    ),
                    heartSource,
                    Color.White
                );
            }

            // End the sprite batch
            _spriteBatch.End();
        }

    }
}