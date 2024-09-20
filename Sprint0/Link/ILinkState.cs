using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;


namespace Sprint0.Link
{
    public interface ILinkState
    {
        void Draw(SpriteBatch spriteBatch);

        void Update();
        void TakeDamage();

        void MoveUp();
        void MoveDown();
        void MoveLeft();
        void MoveRight();

        void Attack();

        void UseItem();

        void setTexture(Texture2D texture);


    }
}
