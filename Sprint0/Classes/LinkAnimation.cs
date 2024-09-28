using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Sprint0.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;


namespace Sprint0.Classes
{
    public class LinkAnimation
    {
        private Texture2D _texture;
        private Rectangle[] _sourceRectangles;
        private int _currentFrame;
        private float _frameTime;
        private float _frameTimeCounter;
        private LinkStateMachine _stateMachine;
        private SpriteEffects spriteEffects;

        public LinkAnimation(Rectangle[] sourceRectangles, LinkStateMachine stateMachine, Texture2D texture)
        {
            _sourceRectangles = sourceRectangles;
            _currentFrame = 0;
            _frameTime = 0.2f;
            _frameTimeCounter = 0f;
            _stateMachine = stateMachine;
            _texture = texture;
            spriteEffects = SpriteEffects.None;
        }

        public void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            _frameTimeCounter += deltaTime;
            if (_frameTimeCounter >= _frameTime)
            {
                switch (_stateMachine.GetCurrentState())
                {
                    case LinkStateMachine.State.MovingLeft:
                        _currentFrame = 2 + (_currentFrame + 1) % 2; // Left animation (flip horizontally when drawing)
                        break;
                    case LinkStateMachine.State.MovingRight:
                        _currentFrame = 2 + (_currentFrame + 1) % 2; // Right animation
                        break;
                    case LinkStateMachine.State.MovingUp:
                        _currentFrame = 4 + (_currentFrame + 1) % 2; // Up animation
                        break;
                    case LinkStateMachine.State.MovingDown:
                        _currentFrame = (_currentFrame + 1) % 2; // Down animation
                        break;
                    case LinkStateMachine.State.SwordAttackRight:
                        _currentFrame = 8 + (_currentFrame - 8 + 1) % 4;
                        if(_currentFrame == 11)
                        {
                            _currentFrame = 2;  
                            _stateMachine.ChangeState(LinkStateMachine.State.Idle);
                        }
                        break;
                    case LinkStateMachine.State.SwordAttackLeft:
                        _currentFrame = 8 + (_currentFrame - 8 + 1) % 4;
                        if (_currentFrame == 11)
                        {
                            _currentFrame = 2;
                            _stateMachine.ChangeState(LinkStateMachine.State.Idle); 
                        }
                        break;
                    case LinkStateMachine.State.SwordAttackUp:
                        if (_currentFrame < 12 || _currentFrame > 15)
                        {
                            _currentFrame = 12;
                        }
                        if (_currentFrame == 12)
                        {
                            _currentFrame = 13;
                        }
                        else if (_currentFrame == 13)
                        {
                            _currentFrame = 14;
                        }
                        else if (_currentFrame == 14)
                        {
                            _currentFrame = 15;
                        }
                        else if (_currentFrame == 15)
                        {
                            _currentFrame = 4;
                            _stateMachine.ChangeState(LinkStateMachine.State.Idle);
                        }
                        break;
                    case LinkStateMachine.State.SwordAttackDown:
                        if(_currentFrame < 16 || _currentFrame > 19)
                        {
                            _currentFrame = 16;
                        }
                        if (_currentFrame == 16)
                        {
                            _currentFrame = 17;
                        }
                        else if (_currentFrame == 17)
                        {
                            _currentFrame = 18;
                        }
                        else if (_currentFrame == 18)
                        {
                            _currentFrame = 19;
                        }
                        else if (_currentFrame == 19)
                        {
                            _currentFrame = 0;
                            _stateMachine.ChangeState(LinkStateMachine.State.Idle);
                        }
                        break;
                    case LinkStateMachine.State.TakeDamage:
                        _currentFrame = 20;
                        break;
                }

                _frameTimeCounter = 0f;
            }


        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, Vector2 scale)
        {
            if (
                _stateMachine.GetCurrentState() == LinkStateMachine.State.MovingLeft ||
                _stateMachine.GetPreviousState() == LinkStateMachine.State.MovingLeft ||
                _stateMachine.GetPreviousState() == LinkStateMachine.State.SwordAttackLeft ||
                _stateMachine.GetCurrentState() == LinkStateMachine.State.SwordAttackLeft
                )
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            else
            {
                spriteEffects = SpriteEffects.None;
            }

            spriteBatch.Draw(_texture, position, _sourceRectangles[_currentFrame], Color.White, 0f, Vector2.Zero, scale, spriteEffects, 0f);
        }

    }

    
}
