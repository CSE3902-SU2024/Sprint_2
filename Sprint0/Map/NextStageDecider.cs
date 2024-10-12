using Microsoft.Xna.Framework;
using Sprint0.Player;
using System.Diagnostics;

namespace Sprint2.Map
{
    public class NextStageDecider
    {
        static Link _link;
        static Vector2 _scale;
        static StageManager _stageManager;
        public NextStageDecider(Link link, Vector2 scale, StageManager stageManager) 
        {
            _link = link;
            _scale = scale;
            _stageManager = stageManager;
        }

        public void DecideStage()
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
                        
                        currentStage.UpStage();
                    }
                }
                // bottom middle
                else if ((_link._position.Y >= 110 * _scale.Y && _link._position.Y <= 146 * _scale.Y))
                {
                    if (currentStage.canDown())
                    {
                        currentStage.DownStage();  
                    }

                }
            }
           else if (_link._position.X >= 20 * _scale.X && (_link._position.X <= _scale.X * 70))
            {     
                if (_link._position.Y >= 65 * _scale.Y && _link._position.Y <= 115 * _scale.Y)
                {  
                    if (currentStage.canLeft())
                    {
                        currentStage.LeftStage();
                    }
                }
            }
            else if (_link._position.X >= 180 * _scale.X && (_link._position.X <= _scale.X * 220))
            {
                if (_link._position.Y >= 65 * _scale.Y && _link._position.Y <= 115 * _scale.Y)
                {
                    if (currentStage.canRight())
                    {
                        currentStage.RightStage();
                    }
                }
            }



        }
       
    }
}
