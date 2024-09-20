using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.Interfaces
{
    public interface IStateMachine {
        void Update(GameTime gameTime);
        void ChangeState(State newState); //idk how to get this to see my enum in LinkStateMachine
    }

}
