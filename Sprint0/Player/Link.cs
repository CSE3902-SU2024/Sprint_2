using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;




namespace Sprint0.Player
{
    public class Link
    {

        public ILinkState currentState;
        public Rectangle[] _sourceRectangles;
        public Vector2 _position;
        public Vector2 _previousPosition;
        public Texture2D _texture;
        public Vector2 _scale;
        public float speed;
        public int framesPerStep;
        private SpriteEffects spriteEffects;


        public Link(Rectangle[] sourceRectangles, Texture2D texture)
        {
            currentState = new LinkFacingDown(this);
            _sourceRectangles = sourceRectangles;
            _position = new Vector2(200.0f, 200.0f);
            _scale = new Vector2(4.0f, 4.0f);
            _texture = texture;
            speed = 2.0f;
            spriteEffects = SpriteEffects.None;
            framesPerStep = 8;
            //   _previousPosition = new Vector2(200.0f, 200.0f);
        }

        public void MoveDown()
        {
            currentState.MoveDown();
        }

        public void MoveUp()
        {
            currentState.MoveUp();
        }

        public void MoveLeft()
        {
            currentState.MoveLeft();
        }

        public void MoveRight()
        {
            currentState.MoveRight();
        }

        public void Update()
        {
            currentState.Update();

           
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            currentState.Draw(_spriteBatch);
        }

        public void DrawSprite(SpriteBatch _spriteBatch, int frame, Boolean flipped)
        {
            if (flipped)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            else
            {
                spriteEffects = SpriteEffects.None;
            }
            _spriteBatch.Draw(_texture, _position, _sourceRectangles[frame], Color.White, 0f, Vector2.Zero, _scale, spriteEffects, 0f);
        }

        //private Texture2D _texture;
        //private Vector2 _position;
        //private Rectangle[] _sourceRectangles;

        //private LinkStateMachine _stateMachine;
        //private LinkAnimation _animator;
        //private const float MovementSpeed = 100f; // pixels per second
        //private Vector2 _scale;
        //private bool _isAttackInputHandled;
        //private LinkArrowHandler _arrowHandler;



        //public Link(Texture2D texture, Vector2 initialPosition, Rectangle[] sourceRectangles)
        //{
        //    _texture = texture;
        //    _position = initialPosition;
        //    _sourceRectangles = sourceRectangles;
        //    _stateMachine = new LinkStateMachine(this);
        //    _animator = new LinkAnimation(sourceRectangles, _stateMachine, _texture);
        //    _scale = new Vector2(4.0f, 4.0f);
        //    _arrowHandler = new LinkArrowHandler();

        //}

        //public void Update(GameTime gameTime, KeyboardController keyboardController)
        //{
        //    float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        //    if (keyboardController.IsMovingLeft)
        //    {
        //        _stateMachine.ChangeState(LinkStateMachine.State.MovingLeft);
        //        _position.X -= MovementSpeed * deltaTime;
        //    }
        //    else if (keyboardController.IsMovingRight)
        //    {
        //        _stateMachine.ChangeState(LinkStateMachine.State.MovingRight);
        //        _position.X += MovementSpeed * deltaTime;
        //    }
        //    else if (keyboardController.IsMovingUp)
        //    {
        //        _stateMachine.ChangeState(LinkStateMachine.State.MovingUp);
        //        _position.Y -= MovementSpeed * deltaTime;
        //    }
        //    else if (keyboardController.IsMovingDown)
        //    {
        //        _stateMachine.ChangeState(LinkStateMachine.State.MovingDown);
        //        _position.Y += MovementSpeed * deltaTime;
        //    }
        //    else if(keyboardController.SwordAttack && !_isAttackInputHandled)
        //    {
        //        _stateMachine.HandleAttack();
        //        _isAttackInputHandled = true;
        //    }
        //    else if(!_isAttackInputHandled)
        //    {
        //        _stateMachine.ChangeState(LinkStateMachine.State.Idle);
        //    }

        //    if (_stateMachine.GetCurrentState() == LinkStateMachine.State.Idle && _isAttackInputHandled)
        //    {
        //        _isAttackInputHandled = false;
        //    }
        //    if (keyboardController.TakeDamage)
        //    {
        //        _stateMachine.ChangeState(LinkStateMachine.State.TakeDamage);
        //    }
        //    if (keyboardController.IsFirePressed)
        //    {
        //        _arrowHandler.FireArrow(this, _texture, _sourceRectangles[8]); // Assuming arrow sprite is at index 8
        //    }

        //    // Update arrows
        //    _arrowHandler.UpdateArrows(gameTime);
        //    //animation logic separate from above switch block <-- adding it into that switch block not only delayed animations
        //    //but delayed how quickly different keys were pressed for switching input
        //    _animator.Update(gameTime);
        //    _stateMachine.Update(gameTime);
        //}


        //public void Draw(SpriteBatch spriteBatch)
        //{
        //  _animator.Draw(spriteBatch, _position, _scale);
        //  _arrowHandler.DrawArrows(spriteBatch);


        //}


    }
}
