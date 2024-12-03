using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.Player;

namespace Sprint0.Classes
{
    //WORK IN PROGRESS
    public class BulletManager
    {
        private List<Bullet> _activeBullets;
        private Texture2D _bulletSprite;

        public BulletManager()
        {
            _activeBullets = new List<Bullet>();
        }

        public void LoadBulletSprite(Texture2D bulletSprite)
        {
            _bulletSprite = bulletSprite;
        }

        public void CreateBullet(Vector2 startPosition, Vector2 direction, Vector2 scale, ILinkState.Direction bulletDirection)
        {
            Bullet newBullet = new Bullet(_bulletSprite, startPosition, direction, scale, bulletDirection);
            _activeBullets.Add(newBullet);
        }

        public void Update(GameTime gameTime)
        {
            for (int i = _activeBullets.Count - 1; i >= 0; i--)
            {
                _activeBullets[i].Update(gameTime);

                if (_activeBullets[i].IsExpired())
                {
                    _activeBullets.RemoveAt(i);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Bullet bullet in _activeBullets)
            {
                bullet.Draw(spriteBatch);
            }
        }

        public List<Bullet> GetActiveBullets()
        {
            return _activeBullets;
        }
    }
}