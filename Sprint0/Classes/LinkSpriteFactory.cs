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
                new Rectangle(1, 11, 16, 16),   //animation frame 1 {down 1}
                new Rectangle(18, 11, 16, 16),  //animation frame 2 {down 2}
                new Rectangle(35, 11, 16, 16),  //animation frame 1 {right 1}
                new Rectangle(52, 11, 16, 16),  //animation frame 2 {right 2}
                new Rectangle(69, 11, 16, 16),  //animation frame 1 {up 1}
                new Rectangle(86, 11, 16, 16),  //animation frame 2 {up 2}
                new Rectangle(35, 11, 16, 16),  //animation frame 1 {left 1}  //gotta flip these horizontally --> did this
                new Rectangle(52, 11, 16, 16),  //animation frame 2 {left 2}
                new Rectangle(94, 77, 15, 15),   // Sword Right 1
                new Rectangle(111, 78, 26, 14),  // Sword Right 2
                new Rectangle(139, 78, 22, 14),  // Sword Right 3
                new Rectangle(163, 77, 18, 15),   // Sword Right 4
                new Rectangle(94,109,16,28), //Sword Up 1                 //#12
                new Rectangle(111,97,16,28), // Sword Up 2
                new Rectangle(128,98,16, 28), // Sword up 3
                new Rectangle(145,106,16, 28), //Sword up 4
                new Rectangle(94,128,16,23), // Sword down 1
                new Rectangle(111,128,16,23) // Sword down 2
             };

            return sourceRectangles;
        }
    }
}

