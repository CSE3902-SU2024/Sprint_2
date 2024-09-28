//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using Sprint0.Link;
//using System;
//using System.Collections.Generic;

//namespace Sprint0.Classes
//{
//    public class LinkArrowHandler
//    {
//        private const int MaxArrows = 5; // Adjust as needed
//        private List<Arrow> _arrows = new List<Arrow>();


//        public void FireArrow(Link link, Texture2D arrowTexture, Rectangle arrowSourceRectangle)
//        {
//            //if (_arrows.Count >= MaxArrows) return;

//            //Vector2 direction = GetDirectionFromState(link.GetCurrentState);

//            //if (direction != Vector2.Zero)
//            //{
//            //    Arrow arrow = new Arrow(arrowTexture, arrowSourceRectangle);
//            //    arrow.Fire(link.Position, direction);
//            //    _arrows.Add(arrow);
//            //}
//        }

//        public void UpdateArrows(GameTime gameTime)
//        {
//            for (int i = _arrows.Count - 1; i >= 0; i--)
//            {
//                _arrows[i].Update(gameTime);
//                if (!_arrows[i].IsActive)
//                {
//                    _arrows.RemoveAt(i);
//                }
//            }
//        }

//        public void DrawArrows(SpriteBatch spriteBatch)
//        {
//            foreach (var arrow in _arrows)
//            {
//                arrow.Draw(spriteBatch);
//            }
//        }

//        private Vector2 GetDirectionFromState(LinkStateMachine.State state)
//        {
//            switch (state)
//            {
//                case LinkStateMachine.State.MovingLeft:
//                    return new Vector2(-1, 0);
//                case LinkStateMachine.State.MovingRight:
//                    return new Vector2(1, 0);
//                case LinkStateMachine.State.MovingUp:
//                    return new Vector2(0, -1);
//                case LinkStateMachine.State.MovingDown:
//                    return new Vector2(0, 1);
//                default:
//                    return Vector2.Zero;
//            }
//        }
//    }
//}