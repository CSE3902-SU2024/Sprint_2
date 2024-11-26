﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2.Classes
{
    public class MovableBlock
    {
        public Rectangle[] SourceRectangles;
        private float scale;
        public Texture2D texture;
        private Rectangle boundingBox;
        private bool isMoving;
        private float speed = 1.0f;
        private Vector2 playerPosition;
        public Vector2 blockPosition;
        private int playerWidth;
        private int playerHeight;
        public int blockWidth;
        public int blockHeight;

        public Rectangle BoundingBox => boundingBox;

        public MovableBlock(Vector2 playerPos, Vector2 blockPos, int pWidth, int pHeight, int bWidth, int bHeight) 
        {
            playerPosition = playerPos;
            blockPosition = blockPos;
            playerWidth = pWidth;
            playerHeight = pHeight;
            blockWidth = bWidth;
            blockHeight = bHeight;
            isMoving = false;
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

        public void LoadContent(ContentManager content, string texturePath, Rectangle sourceRectangle)
        {
            texture = content.Load<Texture2D>(texturePath);
            SourceRectangles = new Rectangle[] { sourceRectangle };
            boundingBox = new Rectangle((int)blockPosition.X, (int)blockPosition.Y, texture.Width, texture.Height);
        }

        public void Update(ref Vector2 spritePosition, Vector2 scale)
        {
            Rectangle blockBoundingBox = GetScaledRectangle((int)blockPosition.X, (int)blockPosition.Y, blockWidth, blockHeight, scale);
            Rectangle playerBoundingBox = GetScaledRectangle((int)playerPosition.X, (int)playerPosition.Y, playerWidth, playerHeight, scale);

            if (blockBoundingBox.Intersects(playerBoundingBox))
            {
                Rectangle intersection = Rectangle.Intersect(playerBoundingBox, blockBoundingBox);

                // First, resolve vertical collisions  
                if (intersection.Height < intersection.Width)
                {
                    if (spritePosition.Y < blockBoundingBox.Y) // Coming from the top  
                    {
                        spritePosition.Y = blockBoundingBox.Top - (playerHeight * scale.Y);
                        isMoving = true; // Set isMoving to true when player is on top of the block  
                    }
                    else if (spritePosition.Y > blockBoundingBox.Y) // Coming from below  
                    {
                        spritePosition.Y = blockBoundingBox.Bottom;
                        isMoving = false;
                    }
                }
                // Then, resolve horizontal collisions  
                else
                {
                    if (spritePosition.X < blockBoundingBox.X) // Coming from the left  
                    {
                        spritePosition.X -= intersection.Width;
                        isMoving = false;
                    }
                    else if (spritePosition.X > blockBoundingBox.X) // Coming from the right  
                    {
                        spritePosition.X = blockBoundingBox.Right;
                        isMoving = false;
                    }
                }
            }
            else
            {
                isMoving = false;
            }

            // Update the block's position if the player is moving downwards and the block is not already moving  
            if (isMoving)
            {
                blockPosition.Y += speed;
                blockBoundingBox = GetScaledRectangle((int)blockPosition.X, (int)blockPosition.Y, blockWidth, blockHeight, scale);
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 blockPosition2, Vector2 scale)
        {
            spriteBatch.Draw(texture, blockPosition2, SourceRectangles[0], Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            //_spriteBatch.Draw(pauseScreen, position, sourceRectangle, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        }
    }
}
