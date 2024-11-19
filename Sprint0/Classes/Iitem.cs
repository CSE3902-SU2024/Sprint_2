using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint2.Classes
{
    public interface Iitem
    {
        public enum ItemType
        {
            fire,
            clock,
            heart, 
            health,
            compass,
            fairy,
            bow,
            boom,
            key,
            map,
            potion,
            triangle,
            diamond
        }
        void LoadContent(ContentManager content, string texturePath, GraphicsDevice graphicsdevice, ItemType itemType, Vector2 scale);
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }
}
