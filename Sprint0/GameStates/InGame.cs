using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;
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
    public class InGame : IGameState
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
        private Link _link;

        // Start Menu
        public Texture2D titleScreen;
        public Texture2D endScreen;
        public SpriteFont font;
        public float timer;
        public bool showText;
        Song backgroundMusic;
        Song titleSequence;
        Song endSequence;

        public InGame(Rectangle[] sourceRectangles, Texture2D texture, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, Link link, ContentManager content)
        {

            currentGameStage = GameStage.StartMenu;

            StageIndex = 0;
            _texture = texture;
            _spriteBatch = spriteBatch;
            _link = link;
            _graphicsDevice = graphicsDevice;
            _scale.X = (float)_graphicsDevice.Viewport.Width / 256.0f;
            _scale.Y = (float)_graphicsDevice.Viewport.Height / 176.0f;
            _DungeonMap = new DungeonMap("../../../Map/DungeonMap2.csv");
            _DoorMap = new DoorMap("../../../Map/Dungeon_Doors.csv");
            _EnemyItem = new Enemy_Item_Map("../../../Map/EnemyItem_Map.csv", _scale, graphicsDevice, content);
            _ItemMap = new ItemMap("../../../Map/ItemMap.csv", _scale, graphicsDevice, content, _link);

        //    _nextStageDecider = new NextStageDecider(link, _scale, _DoorMap, );
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

        }

        public void LoadContent(ContentManager Content)
        {

        }

        public void Update(GameTime gameTime)
        {
            UpdateDungeon(gameTime);
        }

        public void UpdateDungeon(GameTime gameTime)
        {
            _nextStageDecider.Update(StageIndex);
            _DrawDungeon.Update(StageIndex);
            _EnemyItem.Update(StageIndex, gameTime);
            _ItemMap.Update(StageIndex, gameTime);
            LinkEnemyCollision.HandleCollisions(_link, _EnemyItem, StageIndex, _link._scale);

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

        public void Draw()
        {

            DrawDungeon();

        }
  

        public void DrawDungeon()
        {
            _DrawDungeon.Draw();
            DebugDraw.DrawHitboxes(_spriteBatch, _link, _EnemyItem, StageIndex, _scale);
        }
    }
}
