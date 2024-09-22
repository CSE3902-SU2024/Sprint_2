using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0;

public class AnimatedBlock
{
    private Texture2D[] blocks;
    private Vector2 position;
    private int currentBlock;
    private float timePerBlock;
    private float timer;

    public AnimatedBlock(Texture2D[] blockTextures, Vector2 startPosition, float frameDuration)
    {
        blocks = blockTextures;
        position = startPosition;
        timePerBlock = frameDuration;
        currentBlock = 0;
        timer = 0f;
    }

    public void Update(GameTime gameTime)
    {
        // Update the timer
        timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (timer >= timePerBlock)
        {
            // Move to the next block
            currentBlock++;

            // Loop back to the first block if we reached the end
            if (currentBlock >= blocks.Length)
            {
                currentBlock = 0;
            }

            timer = 0f; // Reset the timer
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        // Draw the current block at the specified position
        spriteBatch.Draw(blocks[currentBlock], position, Color.White);
    }
}