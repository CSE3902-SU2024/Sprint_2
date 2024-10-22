using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;
using Microsoft.Xna.Framework.Content;
using Sprint2.Enemy;
using Sprint2.Classes;
using static Sprint2.Classes.Iitem;
using Sprint0.Player;
using static System.Formats.Asn1.AsnWriter;

namespace Sprint0.Classes
{
    internal class Item: Iitem
    {
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
        
        public ItemType currentItemType { get; set; }
        
        public Item(Vector2 position, Link link)
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

        public void LoadContent(ContentManager content, string texturePath, GraphicsDevice graphicsdevice, ItemType itemType)
        {
            Sprite = content.Load<Texture2D>(texturePath);

            if (itemType == ItemType.health)
            {
                SourceRectangles = SpriteSheetHelper.CreateHealthItemFrames(); 
                currentItemType = ItemType.health;
            }
            else if (itemType == ItemType.fire)
            {
                SourceRectangles = SpriteSheetHelper.CreateFireItemFrames(); 
                currentItemType = ItemType.fire;
            }
            else if (itemType == ItemType.bow)
            {
                SourceRectangles = SpriteSheetHelper.CreateBowleItemFrames();
                currentItemType = ItemType.bow;
            }
            else if (itemType == ItemType.boom)
            {
                SourceRectangles = SpriteSheetHelper.CreateBoomleItemFrames();
                currentItemType = ItemType.boom;
            }
            else if (itemType == ItemType.compass)
            {
                SourceRectangles = SpriteSheetHelper.CreateCompassleItemFrames();
                currentItemType = ItemType.compass;
            }
            else if (itemType == ItemType.triangle)
            {
                SourceRectangles = SpriteSheetHelper.CreateTriangleItemFrames();
                currentItemType = ItemType.triangle;
            }
            else if (itemType == ItemType.key)
            {
                SourceRectangles = SpriteSheetHelper.CreateKeyItemFrames();
                currentItemType = ItemType.key;
            }
            else if (itemType == ItemType.fairy)
            {
                SourceRectangles = SpriteSheetHelper.CreateFairyItemFrames();
                currentItemType = ItemType.fairy;
            }
            else if (itemType == ItemType.clock)
            {
                SourceRectangles = SpriteSheetHelper.CreateClockItemFrames();
                currentItemType = ItemType.clock;
            }
            else if (itemType == ItemType.diamond)
            {
                SourceRectangles = SpriteSheetHelper.CreateDiamondItemFrames();
                currentItemType = ItemType.diamond;
            }
            else if (itemType == ItemType.potion)
            {
                SourceRectangles = SpriteSheetHelper.CreatePotionItemFrames();
                currentItemType = ItemType.potion;
            }
            else if (itemType == ItemType.map)
            {
                SourceRectangles = SpriteSheetHelper.CreateMapItemFrames();
                currentItemType = ItemType.map;
            }
            else if (itemType == ItemType.heart)
            {
                SourceRectangles = SpriteSheetHelper.CreateHeartItemFrames();
                currentItemType = ItemType.heart;
            }
            _scale.X = (float)graphicsdevice.Viewport.Width / 256.0f;
            _scale.Y = (float)graphicsdevice.Viewport.Height / 176.0f;
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
            if (playerBoundingBox.Intersects(itemBoundingBox) && currentItemType == ItemType.fire)
            {
                _link.TakeDamage();
            }
            else if (playerBoundingBox.Intersects(itemBoundingBox) && currentItemType != ItemType.fire)
            {
                Position.X += 20000;
                Position.Y += 20000;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (currentItemType == ItemType.fire && currentFrame == 1)
            {
                spriteBatch.Draw(Sprite, Position, SourceRectangles[currentFrame], Color.White, 0f, Vector2.Zero, _scale, SpriteEffects.FlipHorizontally, 0f);
            }
            else
            {
                spriteBatch.Draw(Sprite, Position, SourceRectangles[currentFrame], Color.White, 0f, Vector2.Zero, _scale, SpriteEffects.None, 0f);
            }
        }
    }
}
