using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Sprint0.Interfaces;

namespace Sprint0.Classes
{
    public class LinkRightState : ILinkState
    {
        private Link link;
        private float moveSpeed = 3f;
        private int currentFrame;
        private float frameTime;
        private const float TIME_PER_FRAME = 0.2f;

        public LinkRightState(Link link)
        {
            this.link = link;
            currentFrame = 0;
            frameTime = 0;
            SetTextureIndex(2); // Set initial right-facing sprite
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
                SetTextureIndex(2 + currentFrame); // 2 and 3 are right-facing sprites
                frameTime = 0;
            }
        }

        public void MoveUp()
        {
            link.ChangeState(new LinkUpState(link));
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
            link.Position += new Vector2(moveSpeed, 0);
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