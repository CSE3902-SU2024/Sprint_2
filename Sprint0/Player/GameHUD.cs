﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.Player;
using System;

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
        private Vector2 _position;

        private Rectangle _inventoryRegion;
        private Rectangle _bSlotRegion;
        private bool isInventoryVisible;

        private Rectangle _healthBarPosition;
        private const int HUD_WIDTH = 256;
        private const int HUD_HEIGHT = 48;

        private const int HEART_WIDTH = 9;
        private const int HEART_HEIGHT = 9;
        private const int HEART_SPACING = 0;  // Space between hearts
        const int heartsPerRow = 8;  // Set max hearts per row
        private int health;

        private int numKeys;
        private int keyPos = 0;
        int digitSpacing = 8;



        private readonly GraphicsDevice graphicsDevice;
        private readonly ContentManager content;
        public GameHUD(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, ContentManager content, Link link, Vector2 scale)
        {
            _spriteBatch = spriteBatch;
            _link = link;
            _scale = scale;
            _position = Vector2.Zero;
            this.graphicsDevice = graphicsDevice;
            health = _link.Health;
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
            _healthBarPosition = new Rectangle(703, 133, (int)(HEART_WIDTH * _scale.X * 1.15), (int)(HEART_HEIGHT * _scale.Y * 1.15));
            //hard coded heart position and scaling -> reason being HUD size is based on a scale and screenwidth. 

            cutOuts = new Rectangle[]
             {
                  new Rectangle(258, 11,255,55) ,  //the background
                  new Rectangle(645, 117, 8, 8),     // 1 full heart
                  new Rectangle(636, 117, 8, 8),     // 1 half heart
                  new Rectangle(627, 117, 8, 8),     // 1 emtpy heart

                  new Rectangle(519, 117, 8, 8), // X [4]
                  new Rectangle(528, 117, 8, 8), //0 [5]
                  new Rectangle(537, 117, 8, 8), //1 [6]
                  new Rectangle(546, 117, 8, 8), //2 [7]
                  new Rectangle(555, 117, 8, 8), //3 [8]
                  new Rectangle(564, 117, 8, 8), //4 [9]
                  new Rectangle(573, 117, 8, 8), //5 [10]
                  new Rectangle(582, 117, 8, 8), //6 [11]
                  new Rectangle(591, 117, 8, 8), //7 [12]
                  new Rectangle(600, 117, 8, 8), //8 [13]
                  new Rectangle(609, 117, 8, 8), //9 [14]
                
             };

        }

        public void SetPosition(Vector2 position)
        {
            _position = position;
        }

        public void Draw()
        {

            Rectangle adjustedBackground = new Rectangle(
                (int)_position.X,
                (int)_position.Y,
                graphicsDevice.Viewport.Width,
                (int)(HUD_HEIGHT * _scale.Y * 1.2));

            // adjustedBackground 
            _spriteBatch.Draw(_hudTexture, adjustedBackground, cutOuts[0], Color.White);

            // Adjust heart positions to include offset
            for (int i = 0; i < health; i++)
            {
                int row = i / heartsPerRow;
                int column = i % heartsPerRow;
                int heartValue = _link.Health - (i * 2);

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

                // Add position offset to heart positions
                int xPosition = (int)_position.X + _healthBarPosition.X + column * (int)((HEART_WIDTH + HEART_SPACING) * _scale.X);
                int yPosition = (int)_position.Y + _healthBarPosition.Y + row * (int)(HEART_HEIGHT * _scale.Y);

                _spriteBatch.Draw(_hudTexture,
                    new Rectangle(xPosition, yPosition, (int)(HEART_WIDTH * _scale.X), (int)(HEART_HEIGHT * _scale.Y)),
                    heartSource, Color.White);

            }

            int keyCount = _link.keyCount;
            
            Vector2 baseKeyPosition = new Vector2(385, 135); //hardcoded
            Rectangle xSource = cutOuts[4]; // Index 4 is the 'x'
            _spriteBatch.Draw(_hudTexture, new Rectangle((int)baseKeyPosition.X, (int)baseKeyPosition.Y, (int)(8 * _scale.X), (int)(8 * _scale.Y)), xSource, Color.White);
            
            if (keyCount > 0)
            {
                string keyString = keyCount.ToString();
                int digitSpacing = 8; // Width of each digit in pixels

                for (int i = 0; i < keyString.Length; i++)
                {
                    int digit = keyString[i] - '0'; // Convert char to int
                    Rectangle digitSource = cutOuts[digit + 5]; // +7 because digits start at index 7 in cutOuts

                    // Calculate position for each digit
                    float xDigitPos = baseKeyPosition.X + ((i+1) * digitSpacing * _scale.X);
                    float yDigitPos = baseKeyPosition.Y;

                    _spriteBatch.Draw(
                        _hudTexture,
                        new Rectangle(
                            (int)xDigitPos,
                            (int)yDigitPos,
                            (int)(8 * _scale.X),
                            (int)(8 * _scale.Y)
                        ),
                        digitSource,
                        Color.White
                    );
                }
            }
            else
            {
                //0 when no keys
                // Calculate position for each digit
                float xDigitPos = baseKeyPosition.X + 1 * digitSpacing * _scale.X;
                float yDigitPos = baseKeyPosition.Y;

                Rectangle digitSource = cutOuts[5];
                _spriteBatch.Draw(
                    _hudTexture,
                    new Rectangle(
                        (int)xDigitPos,
                        (int)yDigitPos,
                        (int)(8 * _scale.X),
                        (int)(8 * _scale.Y)
                    ),
                    digitSource,
                    Color.White
                );


            }
        }
    }
}
