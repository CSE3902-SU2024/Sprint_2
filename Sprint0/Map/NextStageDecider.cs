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

            if (currentStage.canUp())
            {
                currentStage.UpStage();
            }
                
                // bottom middle
                
            if (currentStage.canDown())
            {
                currentStage.DownStage();  
            }

            if (currentStage.canLeft())
            {
                currentStage.LeftStage();
            }
            
            if (currentStage.canRight())
            {
                currentStage.RightStage();
            }
        }
       
    }
}
