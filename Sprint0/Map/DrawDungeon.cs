using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.Player;
using Sprint2.Classes;
using Sprint2.Collisions;
using Sprint2.Enemy;
using System;
using System.Collections.Generic;

namespace Sprint2.Map
{
    public class DrawDungeon
    {
        private Link _link;
        private Rectangle[] _sourceRectangles;
        private Texture2D _texture;
        private SpriteBatch _spriteBatch;
        private Vector2 _scale;
        private int stage;
        private DoorDecoder _doorDecoder;
        private DoorMap _doorMap;
        private DungeonMap _dungeonMap;
        private SpriteEffects _spriteEffects;
        private Enemy_Item_Map _EnemyItem;
        private ItemMap _itemMap;

        public DrawDungeon(Rectangle[] sourceRectangles, Texture2D texture, SpriteBatch spriteBatch, Vector2 scale, Link link, DungeonMap dungeon, DoorMap doorMap, Enemy_Item_Map enemy_Item_Map, ItemMap itemMap)
        {
            _sourceRectangles = sourceRectangles;
            _texture = texture;
            _spriteBatch = spriteBatch;
            _scale = scale;
            stage = 0;
            _doorDecoder = new DoorDecoder();
            _spriteEffects = SpriteEffects.None;
            _link = link;
            _doorMap = doorMap;
            _dungeonMap = dungeon;
            _EnemyItem = enemy_Item_Map;
            _itemMap = itemMap;
            //_EnemyItem = new Enemy_Item_Map("../../../Map/EnemyItem_Map.csv", _scale, graphicsDevice, content);

        }

        public void Update(int currentStage)
        {
                stage = currentStage;
        }

        private int[] GetDoor(int currentStage) { 
            return _doorMap.GetDoors(currentStage);
        }

        private int[,] GetStage(int currentStage)
        {
            return _dungeonMap.GetRoom(currentStage);
        }
        private List<IEnemy> GetEnemies(int currentStage)
        {
            return _EnemyItem.GetEnemies(currentStage);
        }

        private List<Iitem> Getitems(int currentStage)
        {
            return _itemMap.GetItems(currentStage);
        }

        public void Draw(Vector2 Offset, Boolean Transitioning, int currentStage)
        {
            DrawWalls(Offset);
            DrawTiles(GetStage(currentStage), Offset, Transitioning);
            DrawDoors(GetDoor(currentStage), Offset);

            if (!Transitioning)
            {
                _link.transitioning = false;
                DrawEnemies(GetEnemies(currentStage));
                DrawItems(Getitems(currentStage));

            }   
        }
        public void DrawWalls(Vector2 Offset)
        {
            _spriteBatch.Draw(_texture, new Vector2(0 + Offset.X, 55.0f * _scale.Y + Offset.Y), _sourceRectangles[6], Color.White, 0f, Vector2.Zero, _scale, _spriteEffects, 0f);
            _spriteBatch.Draw(_texture, new Vector2(0 + Offset.X, 87.0f * _scale.Y + Offset.Y), _sourceRectangles[7], Color.White, 0f, Vector2.Zero, _scale, _spriteEffects, 0f);
            _spriteBatch.Draw(_texture, new Vector2(0 + Offset.X, 198.0f * _scale.Y + Offset.Y), _sourceRectangles[8], Color.White, 0f, Vector2.Zero, _scale, _spriteEffects, 0f);
            _spriteBatch.Draw(_texture, new Vector2(224.0f * _scale.X + Offset.X, 87.0f * _scale.Y +Offset.Y), _sourceRectangles[9], Color.White, 0f, Vector2.Zero, _scale, _spriteEffects, 0f);

        }

        public void DrawDoors(int[] doorCodes, Vector2 Offset)
        {
            Vector2 doorPosition = new Vector2(0, 0);
            for (int i = 0; i < 4; i++)
            {
                int doorIdx = _doorDecoder.DecodeDoor(i, doorCodes[i]);
                switch (i)
                {
                    case 0:
                        doorPosition.X = 112 * _scale.X + Offset.X;
                        doorPosition.Y = 55 * _scale.Y + Offset.Y;
                        break;
                    case 1: 
                        doorPosition.X = 0 + Offset.X;
                        doorPosition.Y = 127 * _scale.Y + Offset.Y;
                        break;
                    case 2:
                        doorPosition.X = 224 * _scale.X + Offset.X;
                        doorPosition.Y = 127 * _scale.Y + Offset.Y;
                        break;
                    case 3:
                        doorPosition.X = 112 * _scale.X + Offset.X;
                        doorPosition.Y = 198 * _scale.Y + Offset.Y;
                        break;
                    default: break;

                }
                _spriteBatch.Draw(_texture, doorPosition, _sourceRectangles[doorIdx], Color.White, 0f, Vector2.Zero, _scale, _spriteEffects, 0f);

            }
        }
        public void DrawTiles(int[,] room, Vector2 Offset, Boolean Transitioning)
        {
            Vector2 tilePosition = new Vector2(32 * _scale.X + Offset.X, 87 * _scale.Y + Offset.Y);
            List<IEnemy> enemiesInRoom = _EnemyItem.GetEnemies(stage);
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    int tileIdx = room[i, j];

                    _spriteBatch.Draw(_texture, tilePosition, _sourceRectangles[tileIdx], Color.White, 0f, Vector2.Zero, _scale, _spriteEffects, 0f);
                    if (!Transitioning)
                    {
                        if (tileIdx == 1 || tileIdx == 2 ||tileIdx == 3)
                        {
                            Vector2 EasierAccessTilePosition = tilePosition + new Vector2(3, 3);
                            HandlePlayerBlockCollision playerBlockCollision = new HandlePlayerBlockCollision(_link._position, EasierAccessTilePosition, 16, 16, 13, 13);
                            playerBlockCollision.PlayerBlockCollision(ref _link._position, _link._previousPosition, _scale);

                            foreach (IEnemy enemy in enemiesInRoom)
                            {
                                HandleEnemyBlockCollision enemyBlockCollision = new HandleEnemyBlockCollision(EasierAccessTilePosition, 16, 16, 13, 13);
                                enemyBlockCollision.EnemyBlockCollision(_EnemyItem, stage, _scale);
                            }
                        }
                    }
                    tilePosition.X += (float)16 * _scale.X;
                }
                tilePosition.X = (float)32 * _scale.X + Offset.X;
                tilePosition.Y += (float)16 * _scale.Y;
            }
        }

        public void DrawEnemies(List<IEnemy> enemies)
        {
            foreach (IEnemy enemy in enemies)
            {
                enemy.Draw(_spriteBatch);
            }
        }
        public void DrawItems(List<Iitem> items)
        {
            foreach (Iitem item in items)
            {
                item.Draw(_spriteBatch);
            }
        }

    }
}
