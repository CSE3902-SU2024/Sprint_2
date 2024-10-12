using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;
using Sprint0.Player;


namespace Sprint2.Map
{
    public class StageManager
    {
        public IStage currentStage;
        public Rectangle[] _sourceRectangles;
        public Texture2D _texture;
        public SpriteBatch _spriteBatch;
        public Vector2 _scale;
        public SpriteEffects _spriteEffects;
        static GraphicsDevice _graphicsDevice;
        private DoorDecoder _doorDecoder;
        public NextStageDecider _nextStageDecider;
        Vector2 tilePosition;
        DungeonMap map;
        private Link _link;
        public Vector2 doorPosition;
        public StageManager(Rectangle[] sourceRectangles, Texture2D texture, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, Link link) 
        { 
            _sourceRectangles = sourceRectangles;
            _texture = texture;
            _spriteBatch = spriteBatch;
            _link = link;
//map = new DungeonMap("../../../Map/Dungeon_Map.csv");
            currentStage = new Stage1(this, map, _link);
            _doorDecoder = new DoorDecoder();
            _spriteEffects = SpriteEffects.None;
            _graphicsDevice = graphicsDevice;
            _scale.X = (float)_graphicsDevice.Viewport.Width / 256.0f;
            _scale.Y = (float)_graphicsDevice.Viewport.Height / 176.0f;
            doorPosition = new Vector2(1, 1);
            _nextStageDecider = new NextStageDecider(link, _scale, this);

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
        public void DrawWalls()
        {
            _spriteBatch.Draw(_texture,Vector2.Zero, _sourceRectangles[5], Color.White, 0f, Vector2.Zero, _scale, _spriteEffects, 0f);
            // TODO class to calculate positions? 
            _spriteBatch.Draw(_texture, new Vector2(0, 32 *_scale.Y), _sourceRectangles[6], Color.White, 0f, Vector2.Zero, _scale, _spriteEffects, 0f);
            _spriteBatch.Draw(_texture, new Vector2(0, 143 * _scale.Y), _sourceRectangles[7], Color.White, 0f, Vector2.Zero, _scale, _spriteEffects, 0f);
            _spriteBatch.Draw(_texture, new Vector2(224* _scale.X, 32 * _scale.Y), _sourceRectangles[8], Color.White, 0f, Vector2.Zero, _scale, _spriteEffects, 0f);

        }
        public void DrawTiles(int[,] room)
        {
            tilePosition = new Vector2(32 * _scale.X, 32 * _scale.Y);
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    int tileIdx = room[i, j];
                    _spriteBatch.Draw(_texture, tilePosition, _sourceRectangles[tileIdx], Color.White, 0f, Vector2.Zero, _scale, _spriteEffects, 0f);
                    tilePosition.X += 15 * _scale.X;
                }
                tilePosition.X = 32 * _scale.X;
                tilePosition.Y += 15 * _scale.Y;
            }
        }
        public void DrawDoors(int[] doorCodes)
        {
            for (int i = 0; i < 4; i++)
            {
                int doorIdx = _doorDecoder.DecodeDoor(i, doorCodes[i]);
                switch (i)
                {
                    case 0:
                        doorPosition.X = 112 * _scale.X;
                        doorPosition.Y = 0;
                        break;
                    case 1:
                        doorPosition.X = 0;
                        doorPosition.Y = 72* _scale.Y;
                        break;
                    case 2:
                        doorPosition.X = 224 * _scale.X;
                        doorPosition.Y = 72 * _scale.Y;
                        break;
                    case 3:
                        doorPosition.X = 112 * _scale.X;
                        doorPosition.Y = 143 * _scale.Y;
                        break;
                    default: break;

                }
                _spriteBatch.Draw(_texture, doorPosition, _sourceRectangles[doorIdx], Color.White, 0f, Vector2.Zero, _scale, _spriteEffects, 0f);
                   
            }
        }
    }
}
