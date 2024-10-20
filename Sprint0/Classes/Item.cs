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
        public Rectangle[][] SourceRectangles;
        public Vector2 Position;
        public Vector2 OriginalPosition { get; set; }
        public float Speed { get; set; }
        private int itemFrame;
        private float distanceMoved;
        private const float MovementThreshold = 500f;
        public Vector2 _scale;
        private float timePerFrame = 0.5f; // 100ms per frame
        private float timeElapsed;
        private int currentFrame;
        
        public ItemType currentItemType { get; set; }
        
        public Item(Vector2 position, float speed, Link link)
        {

            Position = position;
            OriginalPosition = position;
            Speed = speed;
            itemFrame = 0;
            distanceMoved = 0f;
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
            
            if (itemType == ItemType.unattackable)
            {
                SourceRectangles = SpriteSheetHelper.CreateUnattackItemFrames(); 
                currentItemType = ItemType.unattackable;
            }
            else if (itemType == ItemType.fire)
            {
                SourceRectangles = SpriteSheetHelper.CreateAttackItemFrames(); 
                currentItemType = ItemType.fire;
            }
            _scale.X = (float)graphicsdevice.Viewport.Width / 256.0f;
            _scale.Y = (float)graphicsdevice.Viewport.Height / 176.0f;
        }

        public void Update(GameTime gameTime)
        {
            // simple movement reset the position when move for certain distance
            Vector2 movement = new Vector2(Speed, 0) * (float)gameTime.ElapsedGameTime.TotalSeconds;
            Position += movement;
            distanceMoved += movement.Length();
            if (distanceMoved >= MovementThreshold)
            {
                Position = OriginalPosition;
                distanceMoved = 0f;  // Reset the distance counter
            }

            //if (keyboardController.previousItem)
            //{
            //    itemFrame = (itemFrame - 1 + SourceRectangles.Length) % SourceRectangles.Length;
            //}

            //if (keyboardController.nextItem)
            //{
            //    itemFrame = (itemFrame + 1) % SourceRectangles.Length;
            //}

            timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timeElapsed > timePerFrame)
            {
                currentFrame = (currentFrame + 1) % SourceRectangles[itemFrame].Length;
                timeElapsed = 0f;
            }

            Rectangle playerBoundingBox = GetScaledRectangle((int)_link._position.X, (int)_link._position.Y, 16, 16, _link._scale);
            Rectangle itemBoundingBox = GetScaledRectangle((int)Position.X, (int)Position.Y, 16, 16, _link._scale);
            if (playerBoundingBox.Intersects(itemBoundingBox) && currentItemType == ItemType.fire)
            {
                _link.TakeDamage();
            }
            else if (playerBoundingBox.Intersects(itemBoundingBox) && currentItemType == ItemType.unattackable)
            {
                Position.X += 20000;
                Position.Y += 20000;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (currentItemType == ItemType.fire && currentFrame == 1)
            {
                spriteBatch.Draw(Sprite, Position, SourceRectangles[itemFrame][currentFrame], Color.White, 0f, Vector2.Zero, _scale, SpriteEffects.FlipHorizontally, 0f);
            }
            else
            {
                spriteBatch.Draw(Sprite, Position, SourceRectangles[itemFrame][currentFrame], Color.White, 0f, Vector2.Zero, _scale, SpriteEffects.None, 0f);

            }
            
            
            
        }
    }
}
