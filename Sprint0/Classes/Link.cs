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
    public class Link : ISprite, IMove, IAttack
    {
        private Texture2D _texture;
        private Vector2 _position;
        private Direction _facingDirection;
        private bool _isAttacking;

        public Link(Texture2D texture, Vector2 initialPosition)
        {
            _texture = texture;
            _position = initialPosition;
            _facingDirection = Direction.Down;
            _isAttacking = false;
        }

        public void Move(Direction direction)
        {
            _facingDirection = direction;

            switch (direction)
            {
                case Direction.Up:
                    _position.Y -= 5;
                    break;
                case Direction.Down:
                    _position.Y += 5;
                    break;
                case Direction.Left:
                    _position.X -= 5;
                    break;
                case Direction.Right:
                    _position.X += 5;
                    break;
            }
        }

        public void Attack()
        {
            _isAttacking = true;
            // Logic for sword attack animation
        }

        public void TakeDamage()
        {

        }

        public void Update(GameTime gameTime)
        {
            // Update the player state 
            if (_isAttacking)
            {
                // Reset attack flag after attack is done
                _isAttacking = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
