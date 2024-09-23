using Microsoft.Xna.Framework;

namespace Sprint0.Classes
{
    public static class SpriteSheetHelper
    {
        public static Rectangle[] CreateEnemyFrames()
        {
            // Define frames based on your sprite sheet dimensions
            return new Rectangle[]
            {
            new Rectangle(0, 0, 64, 64),   // Frame 1
            new Rectangle(64, 0, 64, 64),  // Frame 2
            new Rectangle(128, 0, 64, 64), // Frame 3
            new Rectangle(192, 0, 64, 64), // Frame 4

            };
        }

        public static Rectangle[] CreateItemFrames()
        {
            // Define frames based on your sprite sheet dimensions
            Rectangle heart = new Rectangle(25, 0, 13, 14);
            Rectangle clock = new Rectangle(58, 0, 11, 16);
            Rectangle diamond = new Rectangle(72, 0, 8, 16);
            Rectangle potion = new Rectangle(81, 0, 6, 14);
            Rectangle[] itemList = { heart, clock, diamond, potion };
            return itemList;
        }
    }
}
