using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2.Enemy
{
    public interface IEnemy
    {
        Vector2 Position { get; set; }
        int Width { get; }
        int Height { get; }
        void LoadContent(ContentManager content, string texturePath, GraphicsDevice graphicsdevice);
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
        void TakeDamage();

       
        void Reset();

    }
}
