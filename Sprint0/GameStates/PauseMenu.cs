using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Sprint2.Map;

namespace Sprint2.GameStates
{
    public class PauseMenu : IGameState
    {
        public GameStage currentGameStage;
        public Texture2D _texture;
        public SpriteBatch _spriteBatch;
        public Vector2 _scale;
        static GraphicsDevice _graphicsDevice;


        public Texture2D pauseScreen;
      //  private bool isPaused;
        public PauseMenu(Texture2D texture, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, ContentManager content)
        {
            currentGameStage = GameStage.PauseMenu;
            _spriteBatch = spriteBatch;
            _graphicsDevice = graphicsDevice;
            pauseScreen = content.Load<Texture2D>("Pause");
        //    isPaused = false;
        }

       // public bool IsPaused => isPaused;

        public void TogglePause()
        {
          
                MediaPlayer.Pause();
            
        }
        public void LoadContent(ContentManager Content)
        {

        }

        public void Update(GameTime gameTime)
        {
            //if (isPaused)
            //{
                UpdatePauseMenu(gameTime);
           // }
        }

        public void UpdatePauseMenu(GameTime gameTime)
        {
            if (MediaPlayer.State == MediaState.Playing)
            {
                MediaPlayer.Stop();
            }
        }

        public void Draw()
        {
          //  if (isPaused)
       //     {
                DrawPauseMenu();
         //   }
        }

        public void DrawPauseMenu()
        {
            Rectangle sourceRectangle = new Rectangle(1, 1, 210, 58);

            Vector2 position = new Vector2(0, 240);

            Vector2 scale = new Vector2(1f, 1f);

            // Draw the title screen
            _spriteBatch.Draw(pauseScreen, position, sourceRectangle, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        }

        public int GetLinkHealth()
        {
            return 1;
        }
    }
}
