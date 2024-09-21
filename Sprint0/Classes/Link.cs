using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Sprint0.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprint0.Interfaces;


namespace Sprint0.Classes
{
    public class Link : ISprite
    {
        private Texture2D _texture;
        private Vector2 _position;
        private Rectangle[] _sourceRectangles;
        private int _currentFrame;
        private float _frameTime;
        private float _frameTimeCounter;
        private LinkStateMachine _stateMachine;
        private const float MovementSpeed = 100f; // pixels per second
        private Vector2 _scale;
        private SpriteEffects spriteEffects;


        public Link(Texture2D texture, Vector2 initialPosition, Rectangle[] sourceRectangles)
        {
            _texture = texture;
            _position = initialPosition;
            _sourceRectangles = sourceRectangles;
            _currentFrame = 0;
            _frameTime = 0.2f; // 5 frames per second
            _frameTimeCounter = 0f; // time tracker for frame updates
            _stateMachine = new LinkStateMachine(this);
            _scale = new Vector2(4.0f, 4.0f);
            spriteEffects = SpriteEffects.None;
        }

        public void Update(GameTime gameTime, KeyboardController keyboardController)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (keyboardController.IsMovingLeft)
            {
                _stateMachine.ChangeState(LinkStateMachine.State.MovingLeft);
                _position.X -= MovementSpeed * deltaTime;
            }
            else if (keyboardController.IsMovingRight)
            {
                _stateMachine.ChangeState(LinkStateMachine.State.MovingRight);
                _position.X += MovementSpeed * deltaTime;
            }
            else if (keyboardController.IsMovingUp)
            {
                _stateMachine.ChangeState(LinkStateMachine.State.MovingUp);
                _position.Y -= MovementSpeed * deltaTime;
            }
            else if (keyboardController.IsMovingDown)
            {
                _stateMachine.ChangeState(LinkStateMachine.State.MovingDown);
                _position.Y += MovementSpeed * deltaTime;
            }
            else
            {
                _stateMachine.ChangeState(LinkStateMachine.State.Idle);
                _currentFrame = 0;
            }

            //animation logic separate from above switch block <-- adding it into that switch block not only delayed animations
            //but delayed how quickly different keys were pressed for switching input
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
                }

                _frameTimeCounter = 0f;
            }

            _stateMachine.Update(gameTime);
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            if (_stateMachine.GetCurrentState() == LinkStateMachine.State.MovingLeft)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            } else
            {
                spriteEffects = SpriteEffects.None;
            }
                spriteBatch.Draw(_texture, _position, _sourceRectangles[_currentFrame], Color.White, 0f, Vector2.Zero, _scale, spriteEffects, 0f);

        }
    }
}
