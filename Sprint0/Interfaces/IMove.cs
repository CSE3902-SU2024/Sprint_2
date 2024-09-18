using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.Interfaces
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    internal interface IMove
    {
        void Move(Direction direction);
    }
}
