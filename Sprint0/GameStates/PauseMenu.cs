using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Sprint2.Map;
using static System.Formats.Asn1.AsnWriter;

namespace Sprint2.GameStates
{
    public class PauseMenu : IGameState
    {
       // public GameStage currentGameStage;
        public Texture2D _texture;
        public SpriteBatch _spriteBatch;
        public Vector2 _scale;
        static GraphicsDevice _graphics;
        private SpriteFont font;

        private float timer;
        private bool showText;

        public Texture2D pauseScreen;

        private string PauseText = "PAUSE MENU";
        private string Return = "Press SPACE to return to game";

        Vector2 PauseSize;
        Vector2 ReturnSize;

        float PauseScale;
        float ReturnScale;
        public PauseMenu(SpriteBatch spriteBatch, ContentManager content, GraphicsDevice graphics)
        {
            font = content.Load<SpriteFont>("File");
            showText = true;
            timer = 0f;

            _spriteBatch = spriteBatch;
            _graphics = graphics;

            float dpi = 96;

            float targetHeight1 = (2f / 2.45f) * dpi;
            float targetHeight2 = (1f / 2.45f) * dpi;

            PauseSize = font.MeasureString(PauseText);
            ReturnSize = font.MeasureString(Return);

            PauseScale = targetHeight1 / PauseSize.Y;
            ReturnScale = targetHeight2 / ReturnSize.Y;


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
            UpdatePauseMenu(gameTime);
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timer >= 0.5f)
            {
                showText = !showText;
                timer = 0;
            }
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
            if (showText)
            {
                _spriteBatch.DrawString(font, PauseText, GetCenter(PauseSize,20,PauseScale), Color.White, 0f, Vector2.Zero, PauseScale, SpriteEffects.None, 0f);
                _spriteBatch.DrawString(font, Return, GetCenter(ReturnSize,800,ReturnScale), Color.White, 0f, Vector2.Zero, ReturnScale, SpriteEffects.None, 0f);
            }
        }

        public int GetLinkHealth()
        {
            return 1;
        }

        public Vector2 GetCenter(Vector2 size, int y , float scale)
        {
            return new Vector2((_graphics.Viewport.Width - (size.X *scale))/2, y);
        }
    }
}
