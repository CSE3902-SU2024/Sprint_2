using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace Sprint2.GameStates
{
    public class InventoryMenu : IGameState
    {
        private SpriteBatch _spriteBatch;
        private GraphicsDevice _graphicsDevice;
        private Texture2D inventoryScreen;
        private Vector2 _scale;

        public InventoryMenu(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, ContentManager content)
        {
            _spriteBatch = spriteBatch;
            _graphicsDevice = graphicsDevice;
            
            inventoryScreen = content.Load<Texture2D>("NES - The Legend of Zelda - HUD & Pause Screen");  
        }

        public void LoadContent(ContentManager Content)
        {
          
        }

        public void Update(GameTime gameTime)
        {
           
            if (MediaPlayer.State == MediaState.Playing)
            {
                MediaPlayer.Pause();
            }
        }

        public void Draw()
        {
            Rectangle sourceRectangle = new Rectangle(1,11, 256,88);
            Vector2 position = new Vector2(0, 0);
            Vector2 scale = new Vector2(4.2f, 5f);
            _spriteBatch.Draw(inventoryScreen, position, sourceRectangle, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        }

        public int GetLinkHealth()
        {
            return 1;
        }
    }
}