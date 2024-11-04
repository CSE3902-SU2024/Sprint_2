using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.Player;
using Sprint2.Collisions;
using Sprint2.Enemy;
using System.Collections.Generic;

namespace Sprint2.Map
{
 

    public class StageAnimators : IStageChangeAnimator
    {
        private DungeonMap _dungeonMap;
        private DoorMap _doorMap;
        private DoorDecoder _doorDecoder;
        private Vector2 _scale;
        private Rectangle[] _sourceRectangles;
        private Texture2D _texture;
        private SpriteBatch _spriteBatch;
        private SpriteEffects _spriteEffects;
        private int[,] currentTiles;
        private int[,] nextTiles;
        private int[] currentDoors;
        private int [] nextDoors;
        float OffSet;
        
        public StageAnimators(DungeonMap dungeonMap, DoorMap doorMap, Vector2 scale, Rectangle[] sourceRectangles, Texture2D texture, SpriteBatch spriteBatch) 
        {
            _dungeonMap = dungeonMap;
            _doorMap = doorMap;
            _scale = scale;
            _sourceRectangles = sourceRectangles;
            _texture = texture;
            _spriteBatch = spriteBatch;
            _spriteEffects = SpriteEffects.None;
            OffSet = 0.0f;
            _doorDecoder = new DoorDecoder();
        }

        public void AnimateRight(int currentStage, int nextStage)
        {
            currentTiles = _dungeonMap.GetRoom(currentStage);
            nextTiles = _dungeonMap.GetRoom(nextStage);
            currentDoors = _doorMap.GetDoors(currentStage);
            nextDoors = _doorMap.GetDoors(nextStage);
            OffSet = 0.0f;
            
            // TO DO
            // refactor draw dungeon so that it takes an offset value? and a boolean value so that you know if its animating
        }

        public void Update()
        {
            OffSet += _scale.X;
        }

        public void Draw()
        {
           
            DrawTiles();
            DrawWalls();
            DrawDoors();
        }

        public void DrawWalls()
        {

            // Draw O.G. Room
            _spriteBatch.Draw(_texture, new Vector2(0 - OffSet, 55.0f * _scale.Y), _sourceRectangles[5], Color.White, 0f, Vector2.Zero, _scale, _spriteEffects, 0f);
            _spriteBatch.Draw(_texture, new Vector2(0 - OffSet, 87.0f * _scale.Y), _sourceRectangles[6], Color.White, 0f, Vector2.Zero, _scale, _spriteEffects, 0f);
            _spriteBatch.Draw(_texture, new Vector2(0 - OffSet, 198.0f * _scale.Y), _sourceRectangles[7], Color.White, 0f, Vector2.Zero, _scale, _spriteEffects, 0f);
            _spriteBatch.Draw(_texture, new Vector2((224.0f * _scale.X) - OffSet, 87.0f * _scale.Y), _sourceRectangles[8], Color.White, 0f, Vector2.Zero, _scale, _spriteEffects, 0f);

            // Draw NewRoom
            _spriteBatch.Draw(_texture, new Vector2((255.0f *_scale.X) - OffSet, 55.0f * _scale.Y), _sourceRectangles[5], Color.White, 0f, Vector2.Zero, _scale, _spriteEffects, 0f);
            _spriteBatch.Draw(_texture, new Vector2((255.0f * _scale.X) - OffSet, 87.0f * _scale.Y), _sourceRectangles[6], Color.White, 0f, Vector2.Zero, _scale, _spriteEffects, 0f);
            _spriteBatch.Draw(_texture, new Vector2((255.0f * _scale.X) - OffSet, 198.0f * _scale.Y), _sourceRectangles[7], Color.White, 0f, Vector2.Zero, _scale, _spriteEffects, 0f);
            _spriteBatch.Draw(_texture, new Vector2((479.0f * _scale.X) - OffSet, 87.0f * _scale.Y), _sourceRectangles[8], Color.White, 0f, Vector2.Zero, _scale, _spriteEffects, 0f);
        }

        public void DrawTiles()
        {
            Vector2 tilePosition = new Vector2(32 * _scale.X  -OffSet, 87 * _scale.Y );
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    int tileIdx = currentTiles[i, j];
                    _spriteBatch.Draw(_texture, tilePosition, _sourceRectangles[tileIdx], Color.White, 0f, Vector2.Zero, _scale, _spriteEffects, 0f);
                    tilePosition.X += (float)16 * _scale.X;
                }
                tilePosition.X = (float)32 * _scale.X - OffSet;
                tilePosition.Y += (float)16 * _scale.Y;
            }

            Vector2 tilePosition2 = new Vector2(287 * _scale.X - OffSet, 87 * _scale.Y);
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    int tileIdx = nextTiles[i, j];
                    _spriteBatch.Draw(_texture, tilePosition2, _sourceRectangles[tileIdx], Color.White, 0f, Vector2.Zero, _scale, _spriteEffects, 0f);
                    tilePosition2.X += (float)16 * _scale.X;
                }
                tilePosition2.X = (float)287 * _scale.X - OffSet;
                tilePosition2.Y += (float)16 * _scale.Y;
            }

        }
        public void DrawDoors()
        {
            Vector2 doorPosition = new Vector2(0, 0);
            for (int i = 0; i < 4; i++)
            {
                int doorIdx = _doorDecoder.DecodeDoor(i, currentDoors[i]);
                switch (i)
                {
                    case 0:
                        doorPosition.X = 112 * _scale.X - OffSet;
                        doorPosition.Y = 55 * _scale.Y;
                        break;
                    case 1:
                        doorPosition.X = 0 - OffSet;
                        doorPosition.Y = 127 * _scale.Y;
                        break;
                    case 2:
                        doorPosition.X = 224 * _scale.X - OffSet;
                        doorPosition.Y = 127 * _scale.Y;
                        break;
                    case 3:
                        doorPosition.X = 112 * _scale.X - OffSet;
                        doorPosition.Y = 198 * _scale.Y;
                        break;
                    default: break;

                }
                _spriteBatch.Draw(_texture, doorPosition, _sourceRectangles[doorIdx], Color.White, 0f, Vector2.Zero, _scale, _spriteEffects, 0f);
            }
            Vector2 doorPosition2 = new Vector2(0, 0);
            for (int j= 0; j < 4;   j++)
            {
                int doorIdx = _doorDecoder.DecodeDoor(j, nextDoors[j]);
                switch (j)
                {
                    case 0:
                        doorPosition2.X = 367 * _scale.X - OffSet;
                        doorPosition2.Y = 55 * _scale.Y;
                        break;
                    case 1:
                        doorPosition2.X = 255 *_scale.X - OffSet;
                        doorPosition2.Y = 127 * _scale.Y;
                        break;
                    case 2:
                        doorPosition2.X = 479 * _scale.X - OffSet;
                        doorPosition2.Y = 127 * _scale.Y;
                        break;
                    case 3:
                        doorPosition2.X = 367 * _scale.X - OffSet;
                        doorPosition2.Y = 198 * _scale.Y;
                        break;
                    default: break;

                }
                _spriteBatch.Draw(_texture, doorPosition2, _sourceRectangles[doorIdx], Color.White, 0f, Vector2.Zero, _scale, _spriteEffects, 0f);
            }
        }
    }
}
