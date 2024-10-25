using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Sprint0.Player;
using Sprint2.Collisions;
using System.Collections.Generic;
using Sprint2.Enemy;
using Sprint0.Collisions;
using System;
using static System.Formats.Asn1.AsnWriter;
using Sprint2.Classes;

namespace Sprint2
{
    public class GameHUD
    {
        private SpriteBatch _spriteBatch;
        private Texture2D _hudTexture;
        private Rectangle _hudBackground;
        private Vector2 _scale;
        private Link _link;

        private Rectangle _healthBarPosition;
        private const int HEART_WIDTH = 8;
        private const int HEART_HEIGHT = 8;
        private const int MAX_HEALTH = 3;
        public GameHUD(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, ContentManager content, Link link, Vector2 scale)
        {
            _spriteBatch = spriteBatch;
            _link = link;
            _scale = scale;
            LoadContent(content);
            InitializeHUDPositions();
        }
        private void LoadContent(ContentManager content)
        {
            _hudTexture = content.Load<Texture2D>("NES - The Legend of Zelda - HUD & Pause Screen");
        }
        private void InitializeHUDPositions()
        {
            _hudBackground = new Rectangle(0, 0, (int)(256 * _scale.X), (int)(48 * _scale.Y));
            _healthBarPosition = new Rectangle((int)(176 * _scale.X), (int)(32 * _scale.Y), (int)(32 * _scale.Y), (int)(HEART_HEIGHT * _scale.Y));

        }
        public void Draw()
        {
            //background
            _spriteBatch.Begin();
            _spriteBatch.Draw(_hudTexture, _hudBackground, new Rectangle(0, 0, 256, 48), Color.Black);

            //healthbar
            for (int i = 0; i < MAX_HEALTH; i++)
            {
                Rectangle heartSource = new Rectangle(i < _link.Health ? 0 : HEART_WIDTH, 0, HEART_WIDTH, HEART_HEIGHT);
                _spriteBatch.Draw(_hudTexture, new Rectangle(_healthBarPosition.X + (i * (int)(HEART_WIDTH * 1.5f * _scale.X)), _healthBarPosition.Y, _healthBarPosition.Width, _healthBarPosition.Height), heartSource, Color.White);
            }
            _spriteBatch.End();
        }
    }
}