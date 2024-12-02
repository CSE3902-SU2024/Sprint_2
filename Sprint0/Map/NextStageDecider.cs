using Microsoft.Xna.Framework;
using Sprint0.Player;
using Sprint2.Enemy;
using static Sprint2.Classes.Iitem;

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

            bool canUnlockDoor = _link.HasKey() && _link.inventory?.SelectedItem?.CurrentItemType == ItemType.key;
            if (_link._position.X >= 110 * _scale.X && _link._position.X <= 150 * _scale.X)
            {
                // top middle
                if ((_link._position.Y >= 0 * _scale.Y && _link._position.Y <= 115 * _scale.Y))
                {
                    if(canUnlockDoor && doors[0] == 2)
                    {
                        _link.DecrementKey();
                        _doorMap.KeyLogic(_stage);
                    }
                    else if (doors[0] == 1 || doors[0] ==4)
                    {
                        _link._position.X = 120 * _scale.X;
                        _link._position.Y = 180 * _scale.Y;
                        switch (_stage)
                        {
                            case 0:
                                _link.transitioning = true;
                                _stageManager.Animate(0, 1, 3);
                                return 1;
                            case 1:                                 
                                _link.transitioning = true;
                                _stageManager.Animate(1, 4, 3);                              
                                return 4;
                            case 4:
                                _link.transitioning = true;
                                _stageManager.Animate(4, 5, 3);
                                return 5;
                            case 5:
                                _link.transitioning = true;
                                _stageManager.Animate(5, 10, 3);
                                return 10;
                            case 6:
                                _link.transitioning = true;
                                _stageManager.Animate(6,8, 3);
                                return 8;
                            case 7:
                                _link.transitioning = true;
                                _stageManager.Animate(7, 11, 3);
                                return 11;
                            case 10:
                                _link.transitioning = true;
                                _stageManager.Animate(_stage, 12, 3);
                                return 12;
                            case 12:
                                _link.transitioning = true;
                                _stageManager.Animate(_stage, 13, 3);
                                return 13;
                            case 15:
                                _link.transitioning = true;
                                _stageManager.Animate(_stage, 16, 3);
                                return 16;
                            default:
                                break;
                        }
                        
                    }
                    if (doors[0] == 2 && canUnlockDoor)
                    {
                        doors[0] = 1;
                        _link.keyCount -= 1;
                    }
                }
                // bottom middle
                else if ((_link._position.Y >= 110 * _scale.Y && _link._position.Y <= 201 * _scale.Y))
                {
                    if (doors[3] == 1 || doors[3] == 4)
                    {
                        _link._position.X = 120 * _scale.X;
                        _link._position.Y = 87 * _scale.Y;
                        switch (_stage)
                        {
                            case 4:
                                _link.transitioning = true;
                                _stageManager.Animate(4, 1, 4);
                                return 1;
                            case 5:
                                _link.transitioning = true;
                                _stageManager.Animate(5, 4, 4);
                                return 4;
                            
                            case 8:
                                _link.transitioning = true;
                                _stageManager.Animate(8, 6, 4);
                                return 6;
                            case 10:
                                _link.transitioning = true;
                                _stageManager.Animate(10, 5, 4);
                                return 5;
                            case 11:
                                _link.transitioning = true;
                                _stageManager.Animate(11, 7, 4);
                                return 7;
                            case 12:
                                _link.transitioning = true;
                                _stageManager.Animate(12,10, 4);
                                return 10;
                            case 13:
                                _link.transitioning = true;
                                _stageManager.Animate(13, 12, 4);
                                return 12;
                            case 16:
                                _link.transitioning = true;
                                _stageManager.Animate(16,15, 4);
                                return 15;
                            default:
                                break;
                        }
                    }
                    if(doors[3] == 2 && canUnlockDoor)
                    {
                        doors[3] = 1;
                        _link.keyCount -= 1;
                    }
                }
            }
            //Left
            else if (_link._position.X >= 20 * _scale.X && (_link._position.X <= _scale.X * 70))
            {
                if (_link._position.Y >= 110 * _scale.Y && _link._position.Y <= 170 * _scale.Y)
                {
                    if (doors[1] == 1 || doors[1] ==4)
                    {
                        _link._position.X = 210 * _scale.X;
                        _link._position.Y = 135 * _scale.Y;
                        switch (_stage)
                        {
                            case 1:
                                _link.transitioning = true;
                                _stageManager.Animate(1, 2, 2);
                                return 2;
                            case 3:
                                _link.transitioning = true;
                                _stageManager.Animate(3, 1, 2);
                                return 1;
                            case 5:
                                _link.transitioning = true;
                                _stageManager.Animate(5, 6, 2);
                                return 6;
                            case 7:
                                _link.transitioning = true;
                                _stageManager.Animate(7, 5, 2);
                                return 5;
                            case 8:
                                _link.transitioning = true;
                                _stageManager.Animate(8, 9, 2);
                                return 9;
                            case 10:
                                _link.transitioning = true;
                                _stageManager.Animate(_stage, 11, 2);
                                return 8;
                            case 11:
                                _link.transitioning = true;
                                _stageManager.Animate(_stage, 10, 2);
                                return 10;
                            case 13:
                                _link.transitioning = true;
                                _stageManager.Animate(_stage, 14, 2);
                                return 14;
                            case 15:
                                _link.transitioning = true;
                                _stageManager.Animate(_stage, 11, 2);
                                return 11;
                            case 17:
                                _link.transitioning = true;
                                _stageManager.Animate(_stage, 16, 2);
                                return 16;
                            default:
                                break;
                        }
                    }
                    if(doors[1] == 2 && canUnlockDoor)
                    {
                        doors[1] = 1;
                        _link.keyCount -= 1;
                    }
                }

            }
            // Right
            else if (_link._position.X >= 180 * _scale.X && (_link._position.X <= _scale.X * 220))
            {
                if (_link._position.Y >= 120 * _scale.Y && _link._position.Y <= 170 * _scale.Y)
                {
                    if (doors[2] == 1 || doors[2] == 4)
                    {
                        _link._position.X = 32 * _scale.X;
                        _link._position.Y = 135 * _scale.Y;
                        switch (_stage)
                        {
                            case 2:
                                _link.transitioning = true;
                                _stageManager.Animate(2, 1, 1);
                                return 1;
                            case 1:
                                _link.transitioning = true;
                                _stageManager.Animate(1, 3, 1);
                                return 3;
                            case 5:
                                _link.transitioning = true;
                                _stageManager.Animate(5, 7, 1);
                                return 7;
                            case 6:
                                _link.transitioning = true;
                                _stageManager.Animate(6, 5, 1);
                                return 5;
                            case 8:
                                _link.transitioning = true;
                                _stageManager.Animate(8, 10, 1);
                                return 10;
                            case 10:
                                _link.transitioning = true;
                                _stageManager.Animate(10, 11, 1);
                                return 11;
                            case 9:
                                _link.transitioning = true;
                                _stageManager.Animate(9, 8, 1);
                                return 8;
                            case 11:
                                _link.transitioning = true;
                                _stageManager.Animate(_stage, 15, 1);
                                return 15;
                            case 14:
                                _link.transitioning = true;
                                _stageManager.Animate(14, 13, 1);
                                return 13;
                            case 16:
                                _link.transitioning = true;
                                _stageManager.Animate(16, 17, 1);
                                return 17;
                            
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

