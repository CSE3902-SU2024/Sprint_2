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

namespace Sprint0.Classes
{
    internal class Item
    {
        public Texture2D Sprite;
        public Rectangle[][] SourceRectangles;
        public Vector2 Position { get; set; }
        public Vector2 OriginalPosition { get; set; }
        public float Speed { get; set; }
        private int itemFrame;
        private float distanceMoved;
        private const float MovementThreshold = 500f;
        private const float scale = 4.0f;
        private float timePerFrame = 0.5f; // 100ms per frame
        private float timeElapsed;
        private int currentFrame;
        public ItemType currentItemType { get; set; }
        public enum ItemType
        {
            attackable,
            unattackable
        }
        public Item(Vector2 position, float speed)
        {

            Position = position;
            OriginalPosition = position;
            Speed = speed;
            itemFrame = 0;
            distanceMoved = 0f;
            
        }

        public void LoadContent(ContentManager content, string texturePath, ItemType itemType)
        {
            Sprite = content.Load<Texture2D>(texturePath);
            
            if (itemType == ItemType.unattackable)
            {
                SourceRectangles = SpriteSheetHelper.CreateUnattackItemFrames(); 
                currentItemType = ItemType.unattackable;
            }
            else if (itemType == ItemType.attackable)
            {
                SourceRectangles = SpriteSheetHelper.CreateAttackItemFrames(); 
                currentItemType = ItemType.attackable;

            }
        }

        public void Update(GameTime gameTime, KeyboardController keyboardController)
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

            if (keyboardController.previousItem)
            {
                itemFrame = (itemFrame - 1 + SourceRectangles.Length) % SourceRectangles.Length;
            }

            if (keyboardController.nextItem)
            {
                itemFrame = (itemFrame + 1) % SourceRectangles.Length;
            }

            timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timeElapsed > timePerFrame)
            {
                currentFrame = (currentFrame + 1) % SourceRectangles[itemFrame].Length;
                timeElapsed = 0f;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (currentItemType == ItemType.attackable && currentFrame == 1)
            {
                spriteBatch.Draw(Sprite, Position, SourceRectangles[itemFrame][currentFrame], Color.White, 0f, Vector2.Zero, scale, SpriteEffects.FlipHorizontally, 0f);
            }
            else
            {
                spriteBatch.Draw(Sprite, Position, SourceRectangles[itemFrame][currentFrame], Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);

            }
            
            
            
        }
    }
}
