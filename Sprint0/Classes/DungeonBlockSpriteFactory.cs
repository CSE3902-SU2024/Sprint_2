using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.Classes
{
    public class DungeonBlockSpriteFactory : ISpriteFactory
    {
        private readonly GraphicsDevice graphicsDevice;
        private readonly ContentManager content;
        private Texture2D dungeonblock_Sheet;
        private Rectangle[] sourceRectangles;

        public DungeonBlockSpriteFactory(GraphicsDevice graphicsDevice, ContentManager content, String sheetName)
        {
            this.graphicsDevice = graphicsDevice;
            this.content = content;
            LoadTexture(sheetName);
        }
        public void LoadTexture(String sheetName)
        {
            try
            {
                dungeonblock_Sheet = content.Load<Texture2D>(sheetName);
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
                 new Rectangle(196, 307, 15, 15),
                 new Rectangle(212, 323, 15, 15),
                 new Rectangle(212, 272, 15, 15),
                 new Rectangle(212, 438, 15, 15),
                 new Rectangle(893, 799, 15, 15),
             };

            return sourceRectangles;
        }

    }
}
