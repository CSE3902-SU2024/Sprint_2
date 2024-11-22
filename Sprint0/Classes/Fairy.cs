﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.Player;
using Sprint2.Classes;
using static Sprint0.Player.ILinkState;
using static Sprint2.Classes.Iitem;

namespace Sprint0.Classes
{
    internal class Fairy : Iitem
    {
        public ILinkState currentState;
        public Link _link;
        public Texture2D Sprite;
        public Rectangle[] SourceRectangles;
        public Vector2 Position;
        public Vector2 OriginalPosition { get; set; }
        private int itemFrame;
        public Vector2 _scale;
        private float timePerFrame = 0.5f; // 100ms per frame
        private float timeElapsed;
        private int currentFrame;
        public bool follow = false;

        public ItemType currentItemType { get; set; }

        public Fairy(Vector2 position, Link link)
        {

            Position = position;
            OriginalPosition = position;
            _link = link;

        }
        private static Rectangle GetScaledRectangle(int x, int y, int width, int height, Vector2 scale)
        {
            return new Rectangle(
                x,
                y,
                (int)(width * scale.X),
                (int)(height * scale.Y)
            );
        }

        public void LoadContent(ContentManager content, string texturePath, GraphicsDevice graphicsdevice, ItemType itemType, Vector2 scale)
        {
            Sprite = content.Load<Texture2D>(texturePath);

            SourceRectangles = SpriteSheetHelper.CreateFairyItemFrames();
            currentItemType = ItemType.fairy;

            _scale = scale;
        }

        public void Update(GameTime gameTime)
        {

            timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timeElapsed > timePerFrame)
            {
                currentFrame = (currentFrame + 1) % SourceRectangles.Length;
                timeElapsed = 0f;
            }

            Rectangle playerBoundingBox = GetScaledRectangle((int)_link._position.X, (int)_link._position.Y, 16, 16, _link._scale);
            Rectangle itemBoundingBox = GetScaledRectangle((int)Position.X, (int)Position.Y, 16, 16, _link._scale);
            if (playerBoundingBox.Intersects(itemBoundingBox))
            {
                follow = true;
            }
            //if (_link.currentDirection == Direction.down && follow)
            //{
            //    Position.Y = _link._position.Y - 13 * _scale.Y;
            //    Position.X = _link._position.X;
            //}
            //else if (_link.currentDirection == Direction.up && follow)
            //{
            //    Position.Y = _link._position.Y + 12 * _scale.Y;
            //    Position.X = _link._position.X;
            //}
            //else if (_link.currentDirection == Direction.left && follow)
            //{
            //    Position.Y = _link._position.Y;
            //    Position.X = _link._position.X + 14 * _scale.X;
            //}
            //else if (_link.currentDirection == Direction.right && follow )
            //{
            //    Position.Y = _link._position.Y;
            //    Position.X = _link._position.X - 12 * _scale.X;
            //}

        }

        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(Sprite, Position, SourceRectangles[currentFrame], Color.White, 0f, Vector2.Zero, _scale, SpriteEffects.None, 0f);

        }
    }
}
