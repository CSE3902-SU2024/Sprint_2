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
            link_Sheet = content.Load<Texture2D>(sheetName);
        }


        Rectangle[] ISpriteFactory.CreateFrames()
        {
            sourceRectangles = new Microsoft.Xna.Framework.Rectangle[]
            {
                //https://pixspy.com/
               new Microsoft.Xna.Framework.Rectangle(1, 11, 16, 16), //animation frame 1 {down 1}
               new Microsoft.Xna.Framework.Rectangle(18, 11, 16, 16), //animation frame 2 {down 2}
               new Microsoft.Xna.Framework.Rectangle(35, 11, 16, 16), //animation frame 2 {right 1}
               new Microsoft.Xna.Framework.Rectangle(52, 11, 16, 16), //animation frame 2 {right 2}
               new Microsoft.Xna.Framework.Rectangle(69, 11, 16, 16), //animation frame 2 {up 1}
               new Microsoft.Xna.Framework.Rectangle(86, 11, 16, 16), //animation frame 2 {up 2}
               new Microsoft.Xna.Framework.Rectangle(35, 11, 16, 16), //animation frame 2 {left 1}  //gotta flip these horizontally
               new Microsoft.Xna.Framework.Rectangle(52, 11, 16, 16), //animation frame 2 {left 2}
            };

            return sourceRectangles;
        }
    }
}

