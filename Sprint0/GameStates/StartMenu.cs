using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Sprint0.Collisions;
using Sprint0.Player;
using Sprint2.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2.GameStates
{
    public class StartMenu : IGameState
    {
        public GameStage currentGameStage; // To track the current game stage
        public Texture2D _texture;
        public SpriteBatch _spriteBatch;
        public Vector2 _scale;
        static GraphicsDevice _graphicsDevice;

        // Start Menu
        public Texture2D titleScreen;
        public SpriteFont font;
        public float timer;
        public bool showText;
        Song titleSequence;

        public StartMenu(Rectangle[] sourceRectangles, Texture2D texture, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, Link link, ContentManager content)
        {

            currentGameStage = GameStage.StartMenu;

            titleScreen = content.Load<Texture2D>("TitleScreen");
            font = content.Load<SpriteFont>("File");
            timer = 0f;
            showText = true;

            //Music
            titleSequence = content.Load<Song>("TitleTheme");

        }

        public void LoadContent(ContentManager Content)
        {

        }

        public void Update(GameTime gameTime)
        {

            UpdateStartMenu(gameTime);

        }
        public int GetLinkHealth()
        {
            return 1;
        }

        public void UpdateStartMenu(GameTime gameTime)
        {
            // Blinking text effect
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timer >= 0.5f)
            {
                showText = !showText;
                timer = 0;
            }

            if (Keyboard.GetState().GetPressedKeys().Length > 0)
            {
                currentGameStage = GameStage.Dungeon;
            }

            if (MediaPlayer.State != MediaState.Playing)
            {
                MediaPlayer.Play(titleSequence);
                MediaPlayer.IsRepeating = true;
            }
        }

        public void Draw()
        {

            DrawStartMenu();

        }

        public void DrawStartMenu()
        {
            //_spriteBatch.Begin();

            Rectangle sourceRectangle = new Rectangle(0, 10, 245, 225);

            Vector2 position = new Vector2(0, 0);

            Vector2 scale = new Vector2(3.26f, 2.15f);

            // Draw the title screen
            _spriteBatch.Draw(titleScreen, position, sourceRectangle, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);

            // Draw text
            if (showText)
            {
                string startText = "PUSH START BUTTON";
                Vector2 textSize = font.MeasureString(startText);
                Vector2 textPosition = new Vector2(
                    (_spriteBatch.GraphicsDevice.Viewport.Width - textSize.X) / 2,
                    400
                );
                _spriteBatch.DrawString(font, startText, textPosition, Color.White);
            }

            //_spriteBatch.End();
        }

    }


}
