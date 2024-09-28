<<<<<<<< HEAD:Sprint0/Player/ILinkState.cs
﻿using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.Player
========
﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0.Interfaces
>>>>>>>> c7a0e7e552f991c8aabde8265d1c08ff6c756edf:Sprint0/Interfaces/ILinkState.cs
{
    public interface ILinkState
    {
        void Draw(SpriteBatch spriteBatch);
<<<<<<<< HEAD:Sprint0/Player/ILinkState.cs
        void Update();
========
        void Update(GameTime gameTime);
        void TakeDamage();
>>>>>>>> c7a0e7e552f991c8aabde8265d1c08ff6c756edf:Sprint0/Interfaces/ILinkState.cs
        void MoveUp();
        void MoveDown();
        void MoveLeft();
        void MoveRight();
     //   void UseSword();
       // void UseBow();

        //void TakeDamge();
    }
}
