using Microsoft.Xna.Framework.Graphics;

namespace Sprint0.Classes
{
    public interface ILinkState
    {
        void Draw(SpriteBatch spriteBatch);
        void setTextureIndex(int index);
        void TakeDamage();
        void Update();
        void MoveUp();
        void MoveDown();
        void MoveLeft();
        void MoveRight();
        void UsePrimary();
        void UseSecondary();
    }
}