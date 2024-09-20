using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using Sprint0.Interfaces;
using Microsoft.Xna.Framework.Input;

namespace Sprint0.Classes
{
    public class LinkStateMachine : IStateMachine 
    {
        public enum State
        {
            Idle,
            MovingLeft,
            MovingRight,
            MovingUp,
            MovingDown,
            AttackingUp,
            AttackingDown,
            AttackingLeft,
            AttackingRight,
            TakeDamage,
            UseItem1Left,
            UseItem1Right,
            UseItem1Up,
            UseItem1Down,
            UseItem2Left,
            UseItem2Right,
            UseItem2Up,
            UseItem2Down,
            UseItem3Left,
            UseItem3Right,
            UseItem3Up,
            UseItem3Down,
        }
        private Link _link;
        private State _currentState;

        public LinkStateMachine(Link link)
        {
            _link = link;
            _currentState = State.Idle;
        }

        private void Update(GameTime gametime)
        {
            throw new NotImplementedException();

        }

        void IStateMachine.Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        //void ChangeState(State newState)
        //{
        //    _currentState = newState;

        //    switch (_currentState)
        //    {
        //        //i honestly don't know wtf to do here if we have Imove and movement in the Link class
        //        case State.Idle:

        //            break;
        //        case State.MovingUp:

        //            break;
        //        case State.MovingLeft:

        //            break;
        //        case State.MovingRight:

        //            break;
        //        case State.MovingDown:

        //            break;
        //        case State.AttackingUp:
        //            break;
        //        case State.AttackingLeft:
        //            break;
        //        case State.AttackingRight:
        //            break;
        //        case State.AttackingDown:
        //            break;
        //        case State.TakeDamage:
        //            break;
        //        case State.UseItem1Up: break;
        //        case State.UseItem1Down: break;
        //        case State.UseItem1Left: break;
        //        case State.UseItem1Right: break;
        //        case State.UseItem2Up: break;
        //        case State.UseItem2Down: break;
        //        case State.UseItem2Left: break;
        //        case State.UseItem2Right: break;
        //        case State.UseItem3Up: break;
        //        case State.UseItem3Down: break;
        //        case State.UseItem3Left: break;
        //        case State.UseItem3Right: break;


        //    }
    }


 

}
