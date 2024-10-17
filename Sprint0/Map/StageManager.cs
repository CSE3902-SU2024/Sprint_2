using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;
using Sprint0.Player;
using Sprint2.Collisions;
using static System.Formats.Asn1.AsnWriter;
using Sprint2.Enemy;


namespace Sprint2.Map
{
    public class StageManager
    {
        public DrawDungeon drawDungeon;
        public IStage currentStage;
        public Rectangle[] _sourceRectangles;
        public Texture2D _texture;
        public SpriteBatch _spriteBatch;
        public Vector2 _scale;
        public SpriteEffects _spriteEffects;
        static GraphicsDevice _graphicsDevice;
        private DoorDecoder _doorDecoder;
        public NextStageDecider _nextStageDecider;
        DungeonMap map;
        private Link _link;
        private Dragon dragon;
        //private Gel gel;
        //private Goriya goriya;
        //private Keese keese;
        //private Stalfos stalfos;

        public StageManager(Rectangle[] sourceRectangles, Texture2D texture, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, Link link) 
        { 
            _sourceRectangles = sourceRectangles;
            _texture = texture;
            _spriteBatch = spriteBatch;
            _link = link;
            map = new DungeonMap("../../../Map/DungeonMap2.csv");
            _graphicsDevice = graphicsDevice;
            _scale.X = (float)_graphicsDevice.Viewport.Width / 256.0f;
            _scale.Y = (float)_graphicsDevice.Viewport.Height / 176.0f;
            _nextStageDecider = new NextStageDecider(link, _scale, this);
            drawDungeon = new DrawDungeon(sourceRectangles, texture, spriteBatch, _scale, _link);
            currentStage = new Stage1(this, map, _link, drawDungeon);

            //Debug.WriteLine(_graphicsDevice.Viewport.);

        }
        public void NextStage()
        {
            Debug.WriteLine("Next Stage");
            _nextStageDecider.DecideStage();
        }
        public void StageUp()
        {
            currentStage.UpStage();
        }

        public void StageDown()
        {
            currentStage.DownStage();
        }
        public void StageRight()
        {
            currentStage.RightStage();
        }

        public void StageLeft()
        {
            currentStage.LeftStage();
        }
        public void Draw()
        {
            currentStage.Draw();
        }

    }
}
