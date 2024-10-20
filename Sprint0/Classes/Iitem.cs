using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprint0.Classes;

namespace Sprint2.Classes
{
    public interface Iitem
    {
        public enum ItemType
        {
            fire,
            unattackable
        }
        void LoadContent(ContentManager content, string texturePath, GraphicsDevice graphicsdevice, ItemType itemType);
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }
}
