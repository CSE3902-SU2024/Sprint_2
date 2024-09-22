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
    }
}
