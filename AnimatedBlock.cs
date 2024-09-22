using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.Classes;
using Sprint0.Interfaces;

namespace Sprint0;

public class AnimatedBlock
{
    private Texture2D[] blocks;
    private Vector2 position;
    private int currentBlock;
    private float scale;
    //private float timePerBlock;
    //private float timer;

    public AnimatedBlock(Texture2D[] blockTextures, Vector2 startPosition)
    {
        blocks = blockTextures;
        position = startPosition;
        scale = 4.0f;
       
        currentBlock = 0;
        //timer = 0f;
    }

    public void Update(GameTime gameTime, KeyboardController keyboardController)
    {
        // Update the timer
        //timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

        //if (timer >= timePerBlock)
        //{
        //    // Move to the next block
        //    currentBlock++;

        //    // Loop back to the first block if we reached the end
        //    if (currentBlock >= blocks.Length)
        //    {
        //        currentBlock = 0;
        //    }

        //    timer = 0f; // Reset the timer
        //}

        if (keyboardController.previousBlock)
        {
            currentBlock = (currentBlock - 1 + blocks.Length) % blocks.Length;
        }

        if (keyboardController.nextBlock)
        {
            currentBlock = (currentBlock + 1) % blocks.Length;
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        // Draw the current block at the specified position
        spriteBatch.Draw(blocks[currentBlock], position,null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
    }
}