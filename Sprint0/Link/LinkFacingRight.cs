using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.Classes;

namespace Sprint0.Link
{

    internal class LinkFacingRight : ILinkState
    {
        private Rectangle _animationFrame;
        private Vector2 _position;
        private ILinkState _currentState;
        private Vector2 movespeed;
        public PlayerIndex link; //how do I made this our Link class?
        
        public LinkFacingRight (Rectangle[] animationFrames, ILinkState currentState, Vector2 position)
        {
            _animationFrame = animationFrames[1]; //this 1 will have to be switched to like an i or whatever should be right facing dirction
            _currentState = currentState;
            _position = position;
            movespeed = Vector2.One; //idk we can later implement a movement speed
        }
        void Draw(SpriteBatch spriteBatch) { }

        void Update() { }
        void TakeDamage() { }

        void MoveUp() { 
        // make current state LinkFacingUp
        }
        void MoveDown() { 
        //make current state LinkFacingDown
        
        }
        void MoveLeft() { 
        //make current State LinkfacingLeft
        }
        void MoveRight() {
            _position += movespeed;
            //next frame for animation?
        }

        void Attack() { 
        //make currentstate new LinkAttackRight
        }

        void UseItem() { 
        //make currentState new LinkItemRight
        }

        void setTexture(Texture2D texture) { 
        //switch if currentState= FacingUp, switch to that texture. etc etc
        }

        void ILinkState.Draw(SpriteBatch spriteBatch)
        {
            //link.LinkSpriteFactory(spriteBatch, _animationFrame);
        }

        void ILinkState.Update()
        {
            throw new NotImplementedException();
        }

        void ILinkState.TakeDamage()
        {
            throw new NotImplementedException();
        }

        void ILinkState.MoveUp()
        {
            throw new NotImplementedException();
        }

        void ILinkState.MoveDown()
        {
            throw new NotImplementedException();
        }

        void ILinkState.MoveLeft()
        {
            throw new NotImplementedException();
        }

        void ILinkState.MoveRight()
        {
            throw new NotImplementedException();
        }

        void ILinkState.Attack()
        {
            throw new NotImplementedException();
        }

        void ILinkState.UseItem()
        {
            throw new NotImplementedException();
        }

        void ILinkState.setTexture(Texture2D texture)
        {
            throw new NotImplementedException();
        }
    }
}
