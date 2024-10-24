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


namespace Sprint2.Map
{
    public enum GameStage
    {
        StartMenu,
        Dungeon
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
        private Link _link;

        // Start Menu
        private Texture2D titleScreen;
        private SpriteFont font;
        private float timer;
        private bool showText;

        public StageManager(Rectangle[] sourceRectangles, Texture2D texture, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, Link link, ContentManager content)
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

            _nextStageDecider = new NextStageDecider(link, _scale, _DoorMap);
            _DrawDungeon = new DrawDungeon(sourceRectangles, texture, spriteBatch, _scale, _link, _DungeonMap, _DoorMap, _EnemyItem, _ItemMap);
            //currentStage = new Stage1(this, _DungeonMap, _DoorMap, _link, drawDungeon);

            titleScreen = content.Load<Texture2D>("TitleScreen");
            font = content.Load<SpriteFont>("File");         
            timer = 0f;
            showText = true;


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

            //_nextStageDecider.Update(StageIndex);
            //_DrawDungeon.Update(StageIndex);
            //_EnemyItem.Update(StageIndex, gameTime);
            //_ItemMap.Update(StageIndex, gameTime);
            //LinkEnemyCollision.HandleCollisions(_link, _EnemyItem, StageIndex, _link._scale);
        }

        private void UpdateStartMenu(GameTime gameTime)
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
        }

        private void UpdateDungeon(GameTime gameTime)
        {
            _nextStageDecider.Update(StageIndex);
            _DrawDungeon.Update(StageIndex);
            _EnemyItem.Update(StageIndex, gameTime);
            _ItemMap.Update(StageIndex, gameTime);
            LinkEnemyCollision.HandleCollisions(_link, _EnemyItem, StageIndex, _link._scale);
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

            //_DrawDungeon.Draw();
            //DebugDraw.DrawHitboxes(_spriteBatch, _link, _EnemyItem, StageIndex, _scale);

        }

        private void DrawStartMenu()
        {
            //_spriteBatch.Begin();

            Rectangle sourceRectangle = new Rectangle(0, 10, 245, 225);

            Vector2 position = new Vector2(0, 0);

            // Draw the title screen
            _spriteBatch.Draw(titleScreen, position, sourceRectangle, Color.White);

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

        private void DrawDungeon()
        {
            _DrawDungeon.Draw();
            DebugDraw.DrawHitboxes(_spriteBatch, _link, _EnemyItem, StageIndex, _scale);
        }

    }
}
