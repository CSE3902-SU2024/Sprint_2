using Microsoft.Xna.Framework;
using Sprint0.Player;
using System.Diagnostics;

namespace Sprint2.Map
{
    public class NextStageDecicer
    {
        static Link _link;
        static Vector2 _scale;
        static StageManager _stageManager;
        public NextStageDecicer(Link link, Vector2 scale, StageManager stageManager) 
        {
            _link = link;
            _scale = scale;
            _stageManager = stageManager;
        }

        public int DecideStage()
        {
            IStage currentStage = _stageManager.currentStage;
            // check if link is in middle of screen
           if (_link._position.X >= 110*_scale.X && _link._position.X <= 150 * _scale.X)
            {
                
                //top middle
                if ((_link._position.Y >= 0 * _scale.Y && _link._position.Y <= 60 * _scale.Y))             
                {
                    if (currentStage.canUp())
                    {
                        return 0;
                    }
                }
                else if ((_link._position.Y >= 110 * _scale.Y && _link._position.Y <= 146 * _scale.Y))
                {
                    if (currentStage.canDown())
                        {
                        return 3;
                       
                    }

                }
            }

            return 5;
            
            
        }
       
    }
}
