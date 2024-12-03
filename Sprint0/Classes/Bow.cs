﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.Player;
using Sprint2.Classes;
using static Sprint2.Classes.Iitem;

namespace Sprint0.Classes
{
    internal class Bow : Iitem
    {
        private Link _link;
        private Link _link2;
        public Texture2D Sprite { get; private set; }
        public Rectangle[] SourceRectangles { get; private set; }
        public Vector2 Position;
        public Vector2 OriginalPosition { get; set; }
        private int itemFrame;
        public Vector2 _scale;
        private float timePerFrame = 0.5f; // 100ms per frame
        private float timeElapsed;
        private int currentFrame;
        public ItemType CurrentItemType => ItemType.bow;
        public ItemType currentItemType { get; set; }
        public bool TwoPlayer;
        public Bow(Vector2 position, Link link, Link link2)
        {
            TwoPlayer = false;
            Position = position;
            OriginalPosition = position;
            _link = link;
            if (link2 != null)
            {
                _link2 = link2;
                TwoPlayer = true;
            }
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


            SourceRectangles = SpriteSheetHelper.CreateBowItemFrames();
            currentItemType = ItemType.bow;

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

           
            Rectangle itemBoundingBox = GetScaledRectangle((int)Position.X, (int)Position.Y, 16, 16, _link._scale);
            if (!TwoPlayer)
            {
                Rectangle playerBoundingBox = GetScaledRectangle((int)_link._position.X, (int)_link._position.Y, 16, 16, _link._scale);
                if (playerBoundingBox.Intersects(itemBoundingBox))
                {
                    Position.X += 20000;
                    Position.Y += 20000;
                    _link.hasBow = true;
                    _link.inventory.AddItem(this);
                }
            } else
            {
                Rectangle playerBoundingBox = GetScaledRectangle((int)_link._position.X, (int)_link._position.Y, 16, 16, _link._scale);
                if (playerBoundingBox.Intersects(itemBoundingBox))
                {
                    Position.X += 20000;
                    Position.Y += 20000;
                    _link.hasBow = true;
                    _link.inventory.AddItem(this);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(Sprite, Position, SourceRectangles[currentFrame], Color.White, 0f, Vector2.Zero, _scale, SpriteEffects.None, 0f);

        }
    }
}
