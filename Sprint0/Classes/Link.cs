using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Sprint0.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Sprint0.Classes
{
    public class Link 
    {
        public Vector2 Position { get; set; }
        public int CurrentTextureIndex { get; set; }

        private Texture2D spriteSheet;
        private ILinkState currentState;
        private int currentTextureIndex;
        private Rectangle[] sourceRectangles;


        private LinkStateMachine _stateMachine;
        private LinkAnimation _animator;
        private const float MovementSpeed = 100f; // pixels per second
        private Vector2 _scale;
        private bool _isAttackInputHandled;
        private LinkArrowHandler _arrowHandler;



        public Link(Texture2D spriteSheet, Vector2 initialPosition, Rectangle[] sourceRectangles)
        {
            this.spriteSheet = spriteSheet;
            Position = initialPosition;
            this.sourceRectangles = sourceRectangles;
            currentState = new LinkDownState(this);
            CurrentTextureIndex = 0;


            _stateMachine = new LinkStateMachine(this);
            _animator = new LinkAnimation(sourceRectangles, _stateMachine, spriteSheet);
            _scale = new Vector2(4.0f, 4.0f);
            _arrowHandler = new LinkArrowHandler();

        }
        public void Update(GameTime gameTime)
        {
            currentState.Update(gameTime);
        }
        public void ChangeState(ILinkState newState)
        {
            currentState = newState;
        }
        public void MoveUp() => currentState.MoveUp();
        public void MoveDown() => currentState.MoveDown();
        public void MoveLeft() => currentState.MoveLeft();
        public void MoveRight() => currentState.MoveRight();
        public void UsePrimary() => currentState.UsePrimary();
        public void UseSecondary() => currentState.UseSecondary();
        public void TakeDamage() => currentState.TakeDamage();

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


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(spriteSheet, Position, sourceRectangles[currentTextureIndex], Color.White, 0f, Vector2.Zero, _scale, SpriteEffects.None, 0f);

            //_animator.Draw(spriteBatch, _position, _scale);
          _arrowHandler.DrawArrows(spriteBatch);


        }


    }
}
