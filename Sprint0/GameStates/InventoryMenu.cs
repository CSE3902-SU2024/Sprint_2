using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Sprint0.Player;
using System;
namespace Sprint2.GameStates
{
    public class InventoryMenu : IGameState
    {
        private SpriteBatch _spriteBatch;
        private GraphicsDevice _graphicsDevice;
        private Texture2D inventoryScreen;
        private Vector2 _scale;
        private Vector2 _position;
        private bool _isTransitioning;
        private float _transitionSpeed;
        private float _targetY;
        private GameHUD _gameHUD;
        private bool _isVisible;
        private Link _link; 

        // Constants for inventory grid
        private readonly Vector2 INVENTORY_GRID_START = new Vector2(400, 300); // Adjust these (later)
        private const int GRID_CELL_SIZE = 32; // Adjust this(later)
        private const int GRID_COLUMNS = 4;
        private const int GRID_ROWS = 2;
        public InventoryMenu(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, ContentManager content, GameHUD gameHUD, Link link)
        {
            _spriteBatch = spriteBatch;
            _graphicsDevice = graphicsDevice;
            _gameHUD = gameHUD;
            _link = link;
            inventoryScreen = content.Load<Texture2D>("NES - The Legend of Zelda - HUD & Pause Screen");
            _scale = new Vector2(4.2f, 5f);

            _position = new Vector2(0, -500);
            _targetY = 0;
            _transitionSpeed = 4f;
            _isTransitioning = true;
            _isVisible = false;
        }




        public void StartTransitionIn()
        {
            if (!_isVisible)   
            {
                _position.Y = -500;  
                _targetY = 0;
                _isTransitioning = true;
                _isVisible = true;
            }
        }

        public void StartTransitionOut()
        {
            if (_isVisible)   
            {
                _targetY = -500;
                _isTransitioning = true;
                _isVisible = false;
            }
        }

        public void Update(GameTime gameTime)
        {
            if (MediaPlayer.State == MediaState.Playing)
            {
                MediaPlayer.Pause();
            }

            if (_isTransitioning)
            {
                
                _position.Y = MathHelper.Lerp(_position.Y, _targetY, _transitionSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);

                
                _gameHUD.SetPosition(_position);

                
                if (Math.Abs(_position.Y - _targetY) < 0.5f)
                {
                    _position.Y = _targetY;
                    _isTransitioning = false;
                }
            }
        }

        public void Draw()
        {
            // inventory background
            Rectangle inventorySource = new Rectangle(1, 11, 256, 88);
            _spriteBatch.Draw(inventoryScreen, _position, inventorySource, Color.White,
                             0f, Vector2.Zero, _scale, SpriteEffects.None, 0f);

           
            

            //   inventory items
            if (_link?.inventory != null)
            {
                _link.inventory.Draw(_spriteBatch);
            }

            // Draw HUD at bottom of inventory
            Vector2 hudPosition = new Vector2(_position.X, _position.Y + (88 * _scale.Y));
            _gameHUD.SetPosition(hudPosition);
            _gameHUD.Draw();
        }
        private void DrawInventoryItems()
        {
            //   grid start position  
            Vector2 gridStart = new Vector2(
                _position.X + (INVENTORY_GRID_START.X * _scale.X),
                _position.Y + (INVENTORY_GRID_START.Y * _scale.Y)
            );

            var items = _link.inventory.GetItems();  
            int itemIndex = 0;

            for (int row = 0; row < GRID_ROWS && itemIndex < items.Count; row++)
            {
                for (int col = 0; col < GRID_COLUMNS && itemIndex < items.Count; col++)
                {
                    var item = items[itemIndex];
                    Vector2 itemPosition = new Vector2(
                        gridStart.X + (col * GRID_CELL_SIZE * _scale.X),
                        gridStart.Y + (row * GRID_CELL_SIZE * _scale.Y)
                    );

                    // Draw item
                    _spriteBatch.Draw(
                        item.Sprite,
                        itemPosition,
                        item.SourceRectangles[0],
                        Color.White,
                        0f,
                        Vector2.Zero,
                        _scale,
                        SpriteEffects.None,
                        0f
                    );

                    itemIndex++;
                }
            }
        }

        public int GetLinkHealth()
        {
            return 1;
        }



        public void LoadContent(ContentManager Content)
        {
        }
    }
}