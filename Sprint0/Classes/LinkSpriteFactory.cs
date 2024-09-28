using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Sprint0.Classes
{
    public class LinkSpriteFactory : ISpriteFactory
    {
        private readonly GraphicsDevice graphicsDevice;
        private readonly ContentManager content;
        private Texture2D link_Sheet;
        private Rectangle[] sourceRectangles;

        public LinkSpriteFactory(GraphicsDevice graphicsDevice, ContentManager content, String sheetName)
        {
            this.graphicsDevice = graphicsDevice;
            this.content = content;
            LoadTexture(sheetName);
        }
        public void LoadTexture(String sheetName)
        {
            try
            {
                link_Sheet = content.Load<Texture2D>(sheetName);
            }
            catch (ContentLoadException e)
            {
                Console.WriteLine($"Error loading content: {e.Message}");
                throw;
            }
        }

        public Rectangle[] CreateFrames()
        {
            sourceRectangles = new Rectangle[]
             {
                //https://pixspy.com/
                new Rectangle(1, 11, 16, 16),     // 0 animation frame 1 {down 1}
                new Rectangle(18, 11, 16, 16),    // 1 animation frame 2 {down 2}
                new Rectangle(35, 11, 16, 16),    // 2 animation frame 1 {right 1}
                new Rectangle(52, 11, 16, 16),    // 3 animation frame 2 {right 2}
                new Rectangle(69, 11, 16, 16),    // 4 animation frame 1 {up 1}
                new Rectangle(86, 11, 16, 16),    // 5 animation frame 2 {up 2}
                new Rectangle(35, 11, 16, 16),    // 6 animation frame 1 {left 1}  //gotta flip these horizontally --> did this
                new Rectangle(52, 11, 16, 16),    // 7 animation frame 2 {left 2}
                new Rectangle(107,11,16,16),      // 8 Link Down attack
                new Rectangle (124, 11, 16, 16),  // 9 Link Right attack
                new Rectangle (124, 11, 16, 16),  // 10 Link Left attack          //gotta flip these horizontally --> did this
                new Rectangle (141, 11, 16, 16),   // 11 Link Up attack

                new Rectangle (36,154,7,13),      //12 SWORD Up 1
                new Rectangle (36,154,7,11),       //13 SWORD Up 2
                new Rectangle (36,154,7,6),       //14 SWORD Up 3

                new Rectangle (45,159,16,7),       //15 SWORD Right 1
                new Rectangle (50,159,11,7),       //16 SWORD Right 2
                new Rectangle (55,159,6,7),       //17 SWORD Right 3

                new Rectangle (3,185,5,16),       //18 ARROW Up 1

                new Rectangle (10,190,16,5),       //19 ARROW Right 1

                new Rectangle (65,189,5,8),       //20 BOOMERANG 1
                new Rectangle (73,189,8,8),       //21 BOOMERANG 2
                new Rectangle (82,189,8,8),       //22 BOOMERANG 3

                new Rectangle (129,185,8,16),       //23 BOMB 1
                new Rectangle (138,185,16,16),      //24 BOMB 2
                new Rectangle (155,185,16,16),      //25 BOMB 3 
                new Rectangle (172,185,16,16)       //26 BOMB 4




                //new Rectangle(94, 77, 15, 15),    // 8 Sword Right 1
                //new Rectangle(111, 78, 27, 14),   // 9 Sword Right 2
                //new Rectangle(139, 78, 23, 14),   // 10 Sword Right 3
                //new Rectangle(163, 77, 18, 15),   // 11 Sword Right 4
                //new Rectangle(94,109,16,16),      // 12 Sword Up 1                 //#12
                //new Rectangle(111,97,16,28),      // 13  Sword Up 2
                //new Rectangle(128,98,16, 28),     // 14 Sword up 3
                //new Rectangle(145,106,16, 19),    // 15 Sword up 4
                //new Rectangle(94,47,16,16),       // 16 Sword down 1
                //new Rectangle(111,47,16,27),      // 17 Sword down 2
                //new Rectangle(128,47,16,23),      // 18 Sword down 3
                //new Rectangle(145,47,16,19),      // 19 Sword down 4
                //new Rectangle(35,232,16,16)       // 20 hurt animation              
             };

            return sourceRectangles;
        }
    }
}

