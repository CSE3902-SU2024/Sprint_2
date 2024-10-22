using Microsoft.Xna.Framework;
using Sprint0.Player;
using Sprint2.Enemy;
using System.Diagnostics;
using System.Transactions;

namespace Sprint2.Map
{
    public class NextStageDecider
    {
        private Link _link;
        private Vector2 _scale;
        private int _stage;
        private DoorMap _doorMap;
        


        public NextStageDecider(Link link, Vector2 scale, DoorMap doorMap)
        {
            _link = link;
            _scale = scale;
            _doorMap = doorMap;

        }
        public void Update(int stage)
        {
            _stage = stage;
        }

        

        public int DecideStage()
        {
            int[] doors = _doorMap.GetDoors(_stage);

            if (_link._position.X >= 110 * _scale.X && _link._position.X <= 150 * _scale.X)
            {
                // top middle
                if ((_link._position.Y >= 0 * _scale.Y && _link._position.Y <= 60 * _scale.Y))
                {

                    if (doors[0] == 1)
                    {
                        _link._position.X = 120 * _scale.X;
                        _link._position.Y = 125 * _scale.Y;
                        if (_stage > 0)
                        {
                            return 0;
                        }
                        else
                        {
                            return 1;
                        }
                    }
                }
                // bottom middle
                else if ((_link._position.Y >= 110 * _scale.Y && _link._position.Y <= 146 * _scale.Y))
                {
                    if (doors[3] == 1)
                    {
                        _link._position.X = 120 * _scale.X;
                        _link._position.Y = 32 * _scale.Y;
                        if (_stage > 0)
                        {
                            return 0;
                        }
                        else
                        {
                            return 2;
                        }
                    }
                }
            }
            else if (_link._position.X >= 20 * _scale.X && (_link._position.X <= _scale.X * 70))
            {
                if (_link._position.Y >= 65 * _scale.Y && _link._position.Y <= 115 * _scale.Y)
                {
                    if (doors[1] == 1)
                    {
                        _link._position.X = 210 * _scale.X;
                        _link._position.Y = 80 * _scale.Y;
                        if (_stage > 0)
                        {
                            return 0;
                        }
                        else
                        {
                            return 4;
                        }
                    }
                }

            }
            else if (_link._position.X >= 180 * _scale.X && (_link._position.X <= _scale.X * 220))
            {
                if (_link._position.Y >= 64 * _scale.Y && _link._position.Y <= 115 * _scale.Y)
                {
                    if (doors[2] == 1)
                    {
                        _link._position.X = 32 * _scale.X;
                        _link._position.Y = 80 * _scale.Y;
                        if (_stage > 0)
                        {
                            return 0;
                        }
                        else
                        {
                            return 3;
                        }
                    }
                }
            }
            return _stage;
        }
    }
}

