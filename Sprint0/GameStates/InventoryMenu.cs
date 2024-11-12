using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
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
        public InventoryMenu(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, ContentManager content, GameHUD gameHUD)
        {
            _spriteBatch = spriteBatch;
            _graphicsDevice = graphicsDevice;
            _gameHUD = gameHUD;
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
            // Draw inventory portion (top part of the screen)
            Rectangle inventorySource = new Rectangle(1, 11, 256, 88);
            _spriteBatch.Draw(inventoryScreen, _position, inventorySource, Color.White,
                             0f, Vector2.Zero, _scale, SpriteEffects.None, 0f);

            // Draw HUD at bottom of inventory
            Vector2 hudPosition = new Vector2(_position.X, _position.Y + (88 * _scale.Y));
            _gameHUD.SetPosition(hudPosition);
            _gameHUD.Draw();
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