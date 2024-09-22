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

using System.Threading;
using System.Timers;

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
            SwordAttackUp,
            SwordAttackDown,
            SwordAttackLeft,
            SwordAttackRight
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
        private System.Timers.Timer _attackTimer;
        private bool _isAttackTimerRunning;

        public LinkStateMachine(Link link)
        {
            _link = link;
            _currentState = State.Idle;
            _previousState = State.Idle;
            _attackTimer = new System.Timers.Timer();
            _attackTimer.Interval = 1100;
            _attackTimer.Elapsed += new ElapsedEventHandler(OnAttackTimerElapsed);
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
                case State.SwordAttackRight:
                case State.SwordAttackUp:
                case State.SwordAttackDown:
                case State.SwordAttackLeft:
                    if (!_isAttackTimerRunning)
                    {
                        ChangeState(State.Idle);
                    }
                    break;

              
            }
        }
        public void HandleAttack()
        {
            if (_previousState == State.MovingRight || _currentState == State.MovingRight)
            {
                _currentState = State.SwordAttackRight;
            } else if (_previousState == State.MovingLeft || _currentState == State.MovingLeft)
            {
                _currentState = State.SwordAttackLeft;
            }
            else if (_previousState == State.MovingUp || _currentState == State.MovingUp)
            {
                _currentState = State.SwordAttackUp;
            }
            else if (_previousState == State.MovingDown || _currentState == State.MovingDown)
            {
                _currentState = State.SwordAttackDown;
            }
            _attackTimer.Start();
            _isAttackTimerRunning = true;
                
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

        public void OnAttackTimerElapsed(object source, ElapsedEventArgs e)
        {
            _isAttackTimerRunning = false;
            _attackTimer.Stop();
        }
    }
}
   
