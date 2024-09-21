using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.Interfaces
{
    public interface ISprite
    {
        void Update(GameTime gameTime, KeyboardController keyboardController);
        void Draw(SpriteBatch spriteBatch);
    }
}
