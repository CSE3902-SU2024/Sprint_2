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

        public static Rectangle[] CreateStalfosFrames()
        {
            return new Rectangle[]
            {
            new Rectangle(1, 59, 16, 16),    
          
            };
        }

        public static Rectangle[] CreateKeeseFrames()
        {
            return new Rectangle[]
            {
            new Rectangle(183, 11, 16, 16),    
            new Rectangle(200, 11, 16, 16), 
            
            };
        }

        public static Rectangle[] CreateGelFrames()
        {
            return new Rectangle[]
            {
            new Rectangle(1, 11, 8, 16),
            new Rectangle(10, 11, 8, 16),

            };
        }
        public static Rectangle[][] CreateItemFrames()
        {
            // Define frames based on your sprite sheet dimensions
            Rectangle[] heart = { new Rectangle(25, 0, 13, 14) , new Rectangle(25, 0, 13, 14) };
            Rectangle[] clock = { new Rectangle(58, 0, 11, 16) , new Rectangle(58, 0, 11, 16) };
            Rectangle[] diamond = { new Rectangle(72, 0, 8, 16), new Rectangle(72, 16, 8, 16) };
            Rectangle[] potion = { new Rectangle(81, 0, 6, 14), new Rectangle(81, 16, 6, 14) };
            Rectangle[][] itemList = { heart, clock, diamond, potion };
            return itemList;
        }

        public static Rectangle[] CreateBlockFrames()
        {

            return new Rectangle[]
             {
                 //https://pixspy.com/
                 new Rectangle(196, 307, 15, 15),
                 new Rectangle(212, 323, 15, 15),
                 new Rectangle(212, 272, 15, 15),
                 new Rectangle(212, 438, 15, 15),
                 new Rectangle(893, 799, 15, 15),
                 new Rectangle(421, 1009, 15, 15),
                 new Rectangle(469, 1009, 15, 15),
                 new Rectangle(533, 1040, 15, 15),
             }; 
        }
    }
}
