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

namespace Sprint0.Classes
{
    internal class Item
    {
        public Texture2D[] Sprite { get; private set; }
        public Vector2 Position { get; set; }
        public Vector2 OriginalPosition { get; set; }
        public float Speed { get; set; }
        private int itemFrame;
        private float distanceMoved;
        private const float MovementThreshold = 500f;
        public Item(Texture2D[] sprite, Vector2 position, float speed)
        {
            Sprite = sprite;
            Position = position;
            OriginalPosition = position;
            Speed = speed;
            itemFrame = 0;
            distanceMoved = 0f;

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
                itemFrame = (itemFrame - 1 + Sprite.Length) % Sprite.Length;
            }

            if (keyboardController.nextItem)
            {
                itemFrame = (itemFrame + 1) % Sprite.Length;
            }


        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite[itemFrame], Position, Color.White);
        }
    }
}
