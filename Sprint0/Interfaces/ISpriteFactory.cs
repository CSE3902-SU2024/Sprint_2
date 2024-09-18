using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Sprint0.Classes
{

    internal interface ISpriteFactory
    {
        void LoadTexture(String sheetName);

       Rectangle[] CreateFrames();
    }
}
