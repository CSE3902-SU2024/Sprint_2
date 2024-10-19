using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;
using Sprint0.Player;
using Sprint2.Collisions;
using static System.Formats.Asn1.AsnWriter;
using Sprint2.Enemy;
using Microsoft.Xna.Framework.Content;


namespace Sprint2.Map
{
    public class StageManager
    {
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
        private Link _link;
        private Dragon dragon;
        //private Gel gel;
        //private Goriya goriya;
        //private Keese keese;
        //private Stalfos stalfos;
        //private Stalfos stalfos;

        public StageManager(Rectangle[] sourceRectangles, Texture2D texture, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, Link link, ContentManager content)
        {
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

            _nextStageDecider = new NextStageDecider(link, _scale, _DoorMap);
            _DrawDungeon = new DrawDungeon(sourceRectangles, texture, spriteBatch, _scale, _link, _DungeonMap, _DoorMap, _EnemyItem);
            //currentStage = new Stage1(this, _DungeonMap, _DoorMap, _link, drawDungeon);


        }

        public void Update(GameTime gameTime)
        {
            _nextStageDecider.Update(StageIndex);
            _DrawDungeon.Update(StageIndex);
            _EnemyItem.Update(StageIndex, gameTime);
        }

            public void NextStage()
        {
            StageIndex = _nextStageDecider.DecideStage();
        }

        public void Draw()
        {
            _DrawDungeon.Draw();
        }
        
    }
}
