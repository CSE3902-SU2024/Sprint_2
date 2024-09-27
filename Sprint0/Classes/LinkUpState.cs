using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprint0.Interfaces;

namespace Sprint0.Classes
{
    public class LinkUpState : ILinkState
    {
        private Link link;
        private float moveSpeed = 3f;
        private int currentFrame;
        private float frameTime;
        private const float TIME_PER_FRAME = 0.2f;

        public LinkUpState(Link link)
        {
            this.link = link;
            currentFrame = 0;
            frameTime = 0;
            SetTextureIndex(4);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            link.Draw(spriteBatch);
        }

        public void Update(GameTime gameTime)
        {
            frameTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (frameTime >= TIME_PER_FRAME)
            {
                currentFrame = (currentFrame + 1) % 2;
                SetTextureIndex(currentFrame);
                frameTime = 0;
            }
        }

        public void MoveUp()
        {
            link.Position += new Vector2(0, -moveSpeed);
        }

        public void MoveDown()
        {
            link.ChangeState(new LinkDownState(link));
        }

        public void MoveLeft()
        {
            link.ChangeState(new LinkLeftState(link));
        }

        public void MoveRight()
        {
            link.ChangeState(new LinkRightState(link));
        }

        public void UsePrimary()
        {
            //sworddown class
        }

        public void UseSecondary()
        {
            //arrowdown class
        }
        public void TakeDamage()
        {
            //takedamage class
        }
        private void SetTextureIndex(int index)
        {
            link.CurrentTextureIndex = index;
        }
    }
}