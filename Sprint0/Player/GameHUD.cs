using Microsoft.Xna.Framework;
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

                  new Rectangle(519, 117, 8, 8), // X [5]
                  new Rectangle(528, 117, 8, 8), //0 [6]
                  new Rectangle(537, 117, 8, 8), //1 [7]
                  new Rectangle(546, 117, 8, 8), //2 [8]
                  new Rectangle(555, 117, 8, 8), //3 [9]
                  new Rectangle(564, 117, 8, 8), //4 [10]
                  new Rectangle(573, 117, 8, 8), //5 [11]
                  new Rectangle(582, 117, 8, 8), //6 [12]
                  new Rectangle(591, 117, 8, 8), //7 [13]
                  new Rectangle(600, 117, 8, 8), //8 [14]
                  new Rectangle(609, 117, 8, 8), //9 [15]
                
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
                _spriteBatch.Draw(_hudTexture,
                    new Rectangle((int)_position.X, (int)_position.Y, 8, 8),
                    heartSource, Color.White);
            }
            Rectangle NumberText;
            if (_link.keyCount != 0 && _link.keyCount <=9)
            {
                NumberText = cutOuts[_link.keyCount + 6]; //1key-> cutout 7
            } else
            {
                //work in progress
            }
            

        }
        

       
    }
}