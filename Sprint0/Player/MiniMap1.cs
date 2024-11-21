using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2.Player
{
    public class MiniMap1
    {
        Texture2D _MiniMap;

        Vector2 _scale;
        Vector2 Base;
        Rectangle map;
        public MiniMap1(Vector2 scale)
        {
            Base = new Vector2(10 * scale.X, 10 * scale.Y);
            _scale = scale;
            // Corner = (0,1) 64x40
            map = new Rectangle(1,0,63,40);
            List<Vector2> StagePosition = new List<Vector2>();
            StagePosition.Add(new Vector2(24, 33));
            StagePosition.Add(new Vector2(14, 33));
            StagePosition.Add(new Vector2(34, 33));
            StagePosition.Add(new Vector2(24, 27));
            StagePosition.Add(new Vector2(14, 21));
            StagePosition.Add(new Vector2(24, 21));
            StagePosition.Add(new Vector2(34, 21));
            StagePosition.Add(new Vector2(14, 15));
            StagePosition.Add(new Vector2(4, 15));
            StagePosition.Add(new Vector2(14, 15));
            StagePosition.Add(new Vector2(24, 15));
            StagePosition.Add(new Vector2(34, 15));
            StagePosition.Add(new Vector2(24, 9));
            StagePosition.Add(new Vector2(24, 3));
            StagePosition.Add(new Vector2(14, 3));
            StagePosition.Add(new Vector2(44, 15));
            StagePosition.Add(new Vector2(44, 9));
            StagePosition.Add(new Vector2(54, 9));
         
        }

        public void LoadMap(ContentManager content)
        {
            _MiniMap = content.Load<Texture2D>("MiniMap_Level1");
        }

        public void Update()
        {

        }

        public void DecodePosition()
        {
            //
        }   
        
        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(_MiniMap, Base, map, Color.White, 0f, Vector2.Zero, _scale, SpriteEffects.None, 0f);
        }
            }
}
