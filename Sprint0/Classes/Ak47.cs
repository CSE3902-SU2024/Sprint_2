using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.Player;
using Sprint2.Classes;
using System;
using static Sprint2.Classes.Iitem;

namespace Sprint0.Classes
{
    internal class Ak47 : Iitem
    {
        public Link _link;
        public Texture2D Sprite { get; private set; }
        public Rectangle[] SourceRectangles { get; private set; }
        public Vector2 Position;
        public Vector2 OriginalPosition { get; set; }
        private int itemFrame;
        public Vector2 _scale;
        private float timePerFrame = 0.5f;
        private float timeElapsed;
        private int currentFrame;
        public ItemType CurrentItemType => ItemType.ak47;
        public ItemType currentItemType { get; set; }

        //ak47 specific variables
        private float nextFireTime = 0f;
        private float fireRate = 0.1f;
        private int ammoCount = 30;
        public int currentAmmo;
        private BulletManager _bulletManager;


        public Ak47(Vector2 position, Link link)
        {
            Position = position;
            OriginalPosition = position;
            _link = link;
            currentAmmo = ammoCount;
            //_bulletManager wip

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

            SourceRectangles = SpriteSheetHelper.CreateAk47ItemFrames();

           // _bulletManager wip

            currentItemType = ItemType.ak47;

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

            //pickup logic
            Rectangle playerBoundingBox = GetScaledRectangle((int)_link._position.X, (int)_link._position.Y, 16, 16, _link._scale);
            Rectangle itemBoundingBox = GetScaledRectangle((int)Position.X, (int)Position.Y, 16, 16, _link._scale);
            if (playerBoundingBox.Intersects(itemBoundingBox))
            {
                Position.X += 20000;
                Position.Y += 20000;
                _link.inventory.AddItem(this);
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite, Position, SourceRectangles[currentFrame], Color.White, 0f, Vector2.Zero, _scale, SpriteEffects.None, 0f);
        }
    }

}