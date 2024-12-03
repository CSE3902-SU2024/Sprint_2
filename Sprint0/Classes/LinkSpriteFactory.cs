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
                new Rectangle (172,185,16,16),       //26 BOMB 4

                new  Rectangle (245,220,36,14),       //27 AK Right 1
                new  Rectangle (282,220,33,14),       //28 AK Right 2
                new  Rectangle (318,218,38,15),       //29 AK Right 3

                 new Rectangle (325,158,3,6),       //30 BULLET Up 1

                new Rectangle (310,158,6,3),       //31 BULLET Right 1

                new  Rectangle (297,95,13,29),       //32 AK Up 1
                new  Rectangle (316,92,11,32),       //33 AK Up 2
                new  Rectangle (333,92,15,32),       //34 AK Up 3

                new  Rectangle (297,47,15,27),       //35 AK Down 1
                new  Rectangle (328,47,12,30),       //36 AK Down 2
                new  Rectangle (333,47,32,32),       //37 AK Down 3








                          
             };

            return sourceRectangles;
        }
    }
}

