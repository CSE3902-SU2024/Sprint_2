using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Sprint0.Interfaces;

namespace Sprint0.Classes
{
    public class LinkLeftState : ILinkState
    {
        private Link link;
        private float moveSpeed = 3f;
        private int currentFrame;
        private float frameTime;
        private const float TIME_PER_FRAME = 0.2f;

        public LinkLeftState(Link link)
        {
            this.link = link;
            currentFrame = 0;
            frameTime = 0;
            SetTextureIndex(6); // Set initial left-facing sprite
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
                SetTextureIndex(6 + currentFrame); // 6 and 7 are left-facing sprites
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
            link.Position += new Vector2(-moveSpeed, 0);
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