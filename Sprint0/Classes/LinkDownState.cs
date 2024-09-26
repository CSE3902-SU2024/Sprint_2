using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.Classes
{
    public class LinkDownState : ILinkState
    {
        private Link link;
        private float moveSpeed = 3f;

        public LinkDownState(Link link)
        {
            this.link = link;
            setTextureIndex(0);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            link.Draw(spriteBatch);
        }

        public void setTextureIndex(int index)
        {
          //  link.SetTextureIndex(index);
        }

        public void TakeDamage()
        {
            //takedamage class
        }

        public void Update()
        {
            //animation goes here
        }

        public void MoveUp()
        {
            // link.ChangeState(new LinkUpState(link));
        }

        public void MoveDown()
        {
            //link.Position += moveSpeed;    //error ig fuck me

        }

        public void MoveLeft()
        {
            //link.ChangeState(new LinkLeftState(link));
        }

        public void MoveRight()
        {
            // link.ChangeState(new LinkRightState(link));
        }

        public void UsePrimary()
        {
            //sworddown class
        }

        public void UseSecondary()
        {
            //arrowdown class
        }
    }
}
