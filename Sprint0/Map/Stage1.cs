using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Sprint0.Player;


namespace Sprint2.Map
{
    public class Stage1: IStage
    {
        StageManager _StageManager;
        static int[,] room;
        static int[] doorCodes;
        private Link _link;
        DungeonMap _map;

        public Stage1(StageManager stageManager, DungeonMap map, Link link)
        {
            //  room = map.GetRoom(0);
            _StageManager = stageManager;
            _link = link;
            _map = map;
        }

        public void DownStage()
        {
            _StageManager.currentStage = new Stage3(_StageManager, _map, _link);
        }

        public void Draw()
        {
            //_link._position.X = 120 * _StageManager._scale.X;
            //_link._position.Y = 115 * _StageManager._scale.Y;
            int[] doorCodes = { 1, 1, 1, 1 };
         //   _StageManager.DrawTiles(room);
            _StageManager.DrawWalls();
            _StageManager.DrawDoors(doorCodes);
        }

        public void LeftStage()
        {
            _StageManager.currentStage = new Stage5(_StageManager, _map, _link);
        }

        public void RightStage()
        {
            _StageManager.currentStage = new Stage4(_StageManager, _map, _link);
        }

        //public void Update()
        //{
            
        //}

        public void UpStage()
        {
            _StageManager.currentStage = new Stage2(_StageManager, _map, _link);
        }
        public bool canUp()
        {
            return true;
        }   
        public bool canDown()
        {
            return true;
        }
        public bool canRight()
        {
            return true;
        }
        public bool canLeft()
        {
            return true;
        }
    }
}
