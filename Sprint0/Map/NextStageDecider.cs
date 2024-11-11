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
        private StageManager _stageManager;
        


        public NextStageDecider(Link link, Vector2 scale, DoorMap doorMap, StageManager stageManager)
        {
            _link = link;
            _scale = scale;
            _doorMap = doorMap;
            _stageManager = stageManager;

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
                if ((_link._position.Y >= 0 * _scale.Y && _link._position.Y <= 115 * _scale.Y))
                {

                    if (doors[0] == 1)
                    {
                        _link._position.X = 120 * _scale.X;
                        _link._position.Y = 180 * _scale.Y;
                        switch (_stage)
                        {
                            case 0:
                                _stageManager.Animate(0, 1, 3);
                                return 1;
                            case 1:
                                _stageManager.Animate(1, 4, 3);
                                return 4;
                            case 4:
                                _stageManager.Animate(4, 5, 3);
                                return 5;
                            default:
                                break;
                        }
                        
                    }
                }
                // bottom middle
                else if ((_link._position.Y >= 110 * _scale.Y && _link._position.Y <= 201 * _scale.Y))
                {
                    if (doors[3] == 1)
                    {
                        _link._position.X = 120 * _scale.X;
                        _link._position.Y = 87 * _scale.Y;
                        switch (_stage)
                        {
                            case 4:
                                _stageManager.Animate(4, 1, 4);
                                return 1;
                            case 5:
                                _stageManager.Animate(5, 4, 3);
                                return 4;
                            default:
                                break;
                        }
                    }
                }
            }
            //Left
            else if (_link._position.X >= 20 * _scale.X && (_link._position.X <= _scale.X * 70))
            {
                if (_link._position.Y >= 110 * _scale.Y && _link._position.Y <= 170 * _scale.Y)
                {
                    if (doors[1] == 1)
                    {
                        _link._position.X = 210 * _scale.X;
                        _link._position.Y = 135 * _scale.Y;
                        switch (_stage)
                        {
                            case 1:
                                _stageManager.Animate(1, 2, 2);
                                return 2;
                            case 3:
                                _stageManager.Animate(3, 1, 2);
                                return 1;
                            default:
                                break;
                        }
                    }
                }

            }
            // Right
            else if (_link._position.X >= 180 * _scale.X && (_link._position.X <= _scale.X * 220))
            {
                if (_link._position.Y >= 120 * _scale.Y && _link._position.Y <= 170 * _scale.Y)
                {
                    if (doors[2] == 1)
                    {
                        _link._position.X = 32 * _scale.X;
                        _link._position.Y = 135 * _scale.Y;
                        switch (_stage)
                        {
                            case 2:
                                _stageManager.Animate(2, 1, 1);
                                return 1;
                            case 1:
                                _stageManager.Animate(1, 3, 1);
                                return 3;
                            default:
                                break;
                        }
                    }
                }
            }
            return _stage;
        }
    }
}

