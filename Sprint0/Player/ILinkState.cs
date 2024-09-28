
﻿using Microsoft.Xna.Framework.Graphics;
using System;
﻿using Microsoft.Xna.Framework;


namespace Sprint0.Player
{
    public interface ILinkState
    {
        void Draw(SpriteBatch spriteBatch);
        void Update();

        void MoveUp();
        void MoveDown();
        void MoveLeft();
        void MoveRight();
        void UseSword();
        void UseArrow();
        void UseBoomerang();
        void UseBomb();


        void IsDamaged();
    }
}
