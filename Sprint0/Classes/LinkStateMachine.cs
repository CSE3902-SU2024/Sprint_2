using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
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
            //AttackingUp,
            //AttackingDown,
            SwordAttackLeft,
            //AttackingRight,
            //TakeDamage,
            //UseItem1Left,
            //UseItem1Right,
            //UseItem1Up,
            //UseItem1Down,
            //UseItem2Left,
            //UseItem2Right,
            //UseItem2Up,
            //UseItem2Down,
            //UseItem3Left,
            //UseItem3Right,
            //UseItem3Up,
            //UseItem3Down,
           
        }
        private Link _link;
        private State _currentState;
        private State _previousState;

        public LinkStateMachine(Link link)
        {
            _link = link;
            _currentState = State.Idle;
            _previousState = State.Idle;
        }

        public void Update(GameTime gametime)   
        {
            switch (_currentState)
            {
                case State.Idle:
                    // No movement
                    break;
                case State.MovingLeft:
                case State.MovingRight:
                case State.MovingUp:
                case State.MovingDown:
                    // Movement is handled in the Link class
                    break;
            }
        }

        public void ChangeState(State newState)
        {
            if (_currentState != State.Idle)
            {
                _previousState = _currentState;
            }
            _currentState = newState;
        }

        public State GetCurrentState()
        {
            return _currentState;
        }
        
        public State GetPreviousState()
        {
            return _previousState;
        }
    }
}
   
