using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0.Interfaces
{
    public interface ILinkState
    {
        void Draw(SpriteBatch spriteBatch);
        void Update(GameTime gameTime);
        void TakeDamage();
        void MoveUp();
        void MoveDown();
        void MoveLeft();
        void MoveRight();
        void UsePrimary();
        void UseSecondary();
    }
}