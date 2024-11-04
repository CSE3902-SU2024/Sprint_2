using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;
using Sprint0.Player;
using Sprint2.Collisions;
using static System.Formats.Asn1.AsnWriter;
using Sprint2.Enemy;
using Microsoft.Xna.Framework.Content;
using Sprint0.Collisions;
using Sprint0.Classes;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Reflection.Metadata;


namespace Sprint2.Map
{
    public enum GameStage
    {
        StartMenu,
        Dungeon,
        PauseMenu,
        End
    }

    public class StageManager
    {
        public GameStage currentGameStage; // To track the current game stage
        public int StageIndex;
        public DrawDungeon _DrawDungeon;
        public Texture2D _texture;
        public SpriteBatch _spriteBatch;
        public Vector2 _scale;
        static GraphicsDevice _graphicsDevice;
        private DoorDecoder _doorDecoder;
        public NextStageDecider _nextStageDecider;
        DungeonMap _DungeonMap;
        DoorMap _DoorMap;
        Enemy_Item_Map _EnemyItem;
        ItemMap _ItemMap;
        Boolean StageAnimating;
        int AnimatingCount;
        private Link _link;
        private StageAnimator _StageAnimator;
  
        // Start Menu
        public Texture2D titleScreen;
        public Texture2D endScreen;
        public SpriteFont font;
        public float timer;
        public bool showText;
        Song backgroundMusic;
        Song titleSequence;
        Song endSequence;

        public StageManager(Rectangle[] sourceRectangles, Texture2D texture, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, Link link, ContentManager content, Vector2 scale)
        {

            currentGameStage = GameStage.StartMenu;
            StageAnimating = false;
            AnimatingCount = 0;
            StageIndex = 0;
            _texture = texture;
            _spriteBatch = spriteBatch;
            _link = link;
            _scale = scale;
            _graphicsDevice = graphicsDevice;
            _DungeonMap = new DungeonMap("../../../Map/DungeonMap2.csv");
            _DoorMap = new DoorMap("../../../Map/Dungeon_Doors.csv");
            _EnemyItem = new Enemy_Item_Map("../../../Map/EnemyItem_Map.csv", _scale, graphicsDevice, content);
            _ItemMap = new ItemMap("../../../Map/ItemMap.csv", _scale, graphicsDevice, content, _link);

            _nextStageDecider = new NextStageDecider(link, _scale, _DoorMap, this);
            _DrawDungeon = new DrawDungeon(sourceRectangles, texture, spriteBatch, _scale, _link, _DungeonMap, _DoorMap, _EnemyItem, _ItemMap);
            //currentStage = new Stage1(this, _DungeonMap, _DoorMap, _link, drawDungeon);

            titleScreen = content.Load<Texture2D>("TitleScreen");
            endScreen = content.Load<Texture2D>("EndingofZelda");
            font = content.Load<SpriteFont>("File");         
            timer = 0f;
            showText = true;

            //Music
            titleSequence = content.Load<Song>("TitleTheme");
            backgroundMusic = content.Load<Song>("DungeonTheme");
            endSequence = content.Load<Song>("EndingTheme");

            _StageAnimator = new StageAnimator(_DungeonMap, _DoorMap, _scale, sourceRectangles, _texture, spriteBatch, _DrawDungeon);
       

        }

        public void Update(GameTime gameTime)
        {
            
                if (currentGameStage == GameStage.StartMenu)
                {
                    UpdateStartMenu(gameTime);
                }
                else if (currentGameStage == GameStage.Dungeon)
                {
                    UpdateDungeon(gameTime);
                }
                else if (currentGameStage == GameStage.PauseMenu)
                {
                    UpdatePauseMenu(gameTime);
                }
                else if (currentGameStage == GameStage.End)
                {
                    UpdateEnd(gameTime);
                }
          

            //_nextStageDecider.Update(StageIndex);
            //_DrawDungeon.Update(StageIndex);
            //_EnemyItem.Update(StageIndex, gameTime);
            //_ItemMap.Update(StageIndex, gameTime);
            //LinkEnemyCollision.HandleCollisions(_link, _EnemyItem, StageIndex, _link._scale);
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


        public void UpdateDungeon(GameTime gameTime)
        {
            if (StageAnimating)
            {
                AnimatingCount-=2;
                _StageAnimator.Update();
            }
            if (AnimatingCount <= 0)
            {
                StageAnimating = false;
            }

            if (!StageAnimating)
            {
                _nextStageDecider.Update(StageIndex);
                _DrawDungeon.Update(StageIndex);
                _EnemyItem.Update(StageIndex, gameTime);
                _ItemMap.Update(StageIndex, gameTime);
                LinkEnemyCollision.HandleCollisions(_link, _EnemyItem, StageIndex, _link._scale);
            }
            if (StageIndex == 0)
            {
                Boolean enemiesPresent = _EnemyItem.AreThereEnemies(StageIndex);
                _DoorMap.AllEnemiesDead(StageIndex, enemiesPresent);
            }  
            if (MediaPlayer.State == MediaState.Playing && MediaPlayer.Queue.ActiveSong == titleSequence)
            {
                MediaPlayer.Stop();
            }

            if (MediaPlayer.State != MediaState.Playing)
            {
                MediaPlayer.Play(backgroundMusic);
                MediaPlayer.IsRepeating = true; // loop the music
            }

            if (Keyboard.GetState().IsKeyDown(Keys.G))
            {
                currentGameStage = GameStage.End;
            }
        }

        public void UpdatePauseMenu(GameTime gameTime)
        {
            if (MediaPlayer.State == MediaState.Playing)
            {
                MediaPlayer.Stop();
            }
        }

        public void UpdateEnd(GameTime gameTime)
        {
            if (MediaPlayer.State == MediaState.Playing && MediaPlayer.Queue.ActiveSong == backgroundMusic)
            {
                MediaPlayer.Stop();
            }
            if (MediaPlayer.State != MediaState.Playing)
            {
                MediaPlayer.Play(endSequence);
                MediaPlayer.IsRepeating = true; // loop the music
            }
        }

        public void NextStage()
        {

            StageIndex = _nextStageDecider.DecideStage();
        }

        public void Draw()
        {

            if (currentGameStage == GameStage.StartMenu)
            {
                DrawStartMenu();
            }
            else if (currentGameStage == GameStage.Dungeon)
            {
                DrawDungeon();
            }
            else if (currentGameStage == GameStage.PauseMenu)
            {
               DrawPauseMenu();
            }
            else if (currentGameStage == GameStage.End)
            {
                DrawEnd();
            }

            //_DrawDungeon.Draw();
            //DebugDraw.DrawHitboxes(_spriteBatch, _link, _EnemyItem, StageIndex, _scale);

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

        public void DrawDungeon()
        {
            if (!StageAnimating)
            {
                _DrawDungeon.Draw(Vector2.Zero, false, StageIndex);
                DebugDraw.DrawHitboxes(_spriteBatch, _link, _EnemyItem, StageIndex, _scale);
            } else
            {
                _StageAnimator.Draw();
            }  
        }

        public void DrawPauseMenu()
        {

        }

        public void DrawEnd()
        {
            Vector2 position = new Vector2(0, 0);
            Vector2 scale = new Vector2(0.8f, 0.5f);
            _spriteBatch.Draw(endScreen, position, null, Color.White, 0f, Vector2.Zero, scale, 0 ,0f);
        }
        public void Animate(int currentStage, int nextStage, int direction)
        {
            StageAnimating = true;
            AnimatingCount = 255;
            _StageAnimator.Animate(currentStage, nextStage, direction);
            Debug.WriteLine("FREEZE!\n");
        }

        public Boolean GetAnimationState()
        {
            return StageAnimating;
        }
    }
}