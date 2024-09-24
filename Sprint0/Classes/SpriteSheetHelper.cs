using Microsoft.Xna.Framework;

namespace Sprint0.Classes
{
    public static class SpriteSheetHelper
    {
        public static Rectangle[] CreateDragonFrames()
        {
            // Define frames based on your sprite sheet dimensions
            return new Rectangle[]
            {
            new Rectangle(1, 11, 24, 32),   // Frame 1  
            new Rectangle(26, 11, 24, 32),   // Frame 2
            new Rectangle(51, 11, 24, 32),   // Frame 3
            new Rectangle(76, 11, 24, 32),   // Frame 4
           
            };
        }
        public static Rectangle[] CreateProjectileFrames()
        {
            return new Rectangle[]
            {
            new Rectangle(101, 14, 8, 10), // Projectile frame 1
            new Rectangle(110, 14, 8, 10), // Projectile frame 2
            new Rectangle(119, 14, 8, 10), // Projectile frame 3
            new Rectangle(128, 14, 8, 10), // Projectile frame 4
            };
        }
        public static Rectangle[] CreateGoriyaFrames()
        {
            return new Rectangle[]
            {
            new Rectangle(222, 11, 16, 16),   // Goriya Frame 1
            new Rectangle(239, 11, 16, 16),// Goriya Frame 2
            new Rectangle(256, 11, 16, 16),  // Goriya Frame 3
            new Rectangle(273, 11, 16, 16),  // Goriya Frame 4
            };
        }
        // Boomerang frames for Goriya
        public static Rectangle[] CreateBoomerangFrames()
        {
            return new Rectangle[]
            {
            new Rectangle(291, 15, 5, 8),   // Boomerang Frame 1
            new Rectangle(299, 15, 8, 8),// Boomerang Frame 2
            new Rectangle(308, 17, 8, 5),  // Boomerang Frame 3
            };
        }
    }
}
