using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.Player;
using Sprint2.Collisions;

namespace Sprint2.Map
{
    public class DrawDungeon
    {
        private Link _link;
        public Vector2 doorPosition;
        public Rectangle[] _sourceRectangles;
        public Texture2D _texture;
        public SpriteBatch _spriteBatch;
        public Vector2 _scale;
        public int stage;
        private DoorDecoder _doorDecoder;
        Vector2 tilePosition;
        public SpriteEffects _spriteEffects;

        public DrawDungeon(Rectangle[] sourceRectangles, Texture2D texture, SpriteBatch spriteBatch, Vector2 scale, Link link)
        {
            _sourceRectangles = sourceRectangles;
            _texture = texture;
            _spriteBatch = spriteBatch;
            _scale = scale;
            doorPosition = new Vector2(1, 1);
            stage = 0;
            _doorDecoder = new DoorDecoder();
            _spriteEffects = SpriteEffects.None;
            _link = link;
        }

        public void Update(int currentStage)
        {
            stage = currentStage;
        }
        public void Draw(int[,] room, int[] doorCodes)
        {
            DrawWalls();
            DrawTiles(room);
            DrawDoors(doorCodes);
        }
        public void DrawWalls()
        {
            _spriteBatch.Draw(_texture, Vector2.Zero, _sourceRectangles[5], Color.White, 0f, Vector2.Zero, _scale, _spriteEffects, 0f);
            // TODO class to calculate positions? 
            _spriteBatch.Draw(_texture, new Vector2(0, 32 * _scale.Y), _sourceRectangles[6], Color.White, 0f, Vector2.Zero, _scale, _spriteEffects, 0f);
            _spriteBatch.Draw(_texture, new Vector2(0, 143 * _scale.Y), _sourceRectangles[7], Color.White, 0f, Vector2.Zero, _scale, _spriteEffects, 0f);
            _spriteBatch.Draw(_texture, new Vector2(224 * _scale.X, 32 * _scale.Y), _sourceRectangles[8], Color.White, 0f, Vector2.Zero, _scale, _spriteEffects, 0f);

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
                        doorPosition.Y = 72 * _scale.Y;
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
        public void DrawTiles(int[,] room)
        {
            tilePosition = new Vector2(32 * _scale.X, 32 * _scale.Y);
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    int tileIdx = room[i, j];

                    _spriteBatch.Draw(_texture, tilePosition, _sourceRectangles[tileIdx], Color.White, 0f, Vector2.Zero, _scale, _spriteEffects, 0f);

                    // Collision for all the tiles for 1
                    if (tileIdx == 1)
                    {
                        HandlePlayerBlockCollision playerBlockCollision = new HandlePlayerBlockCollision(_link._position, tilePosition, 16, 16, 16, 16);
                        playerBlockCollision.PlayerBlockCollision(ref _link._position, _link._previousPosition, _scale);
                    }

                    tilePosition.X += 16 * _scale.X;
                }
                tilePosition.X = 32 * _scale.X;
                tilePosition.Y += 16 * _scale.Y;
            }
        }

    }
}
