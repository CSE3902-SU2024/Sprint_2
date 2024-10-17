using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Sprint0.Player;
using Sprint2.Collisions;


namespace Sprint2.Map
{
    public class Stage1: IStage
    {
        StageManager _StageManager;
        static int[,] room;
        static int[] doorCodes;
        private Link _link;
        DungeonMap _map;
        DrawDungeon _DrawDungeon;
        private Rectangle TopDoor;
        private Rectangle LeftDoor;
        private Rectangle RightDoor;
        private Rectangle BottomDoor;
        private Rectangle playerBoundingBox;
        private bool Up = false;
        private bool Down = false;
        private bool Left = false;
        private bool Right = false;

        public Stage1(StageManager stageManager, DungeonMap map, Link link, DrawDungeon drawDungeon)
        {
              room = map.GetRoom(0);
            _StageManager = stageManager;
            _link = link;
            _map = map;
            _DrawDungeon = drawDungeon;
        }
        private static Rectangle GetScaledRectangle(int x, int y, int width, int height, Vector2 scale)
        {
            return new Rectangle(
                x,
                y,
                (int)(width * scale.X),
                (int)(height * scale.Y)
            );
        }

        public void DownStage()
        {
            _StageManager.currentStage = new Stage3(_StageManager, _map, _link, _DrawDungeon);
        }

        public void Draw()
        {
            //_link._position.X = 120 * _StageManager._scale.X;
            //_link._position.Y = 115 * _StageManager._scale.Y;
            int[] doorCodes = { 1, 1, 1, 1 };
           _DrawDungeon.Draw(room, doorCodes);  
        }

        public void LeftStage()
        {
            _StageManager.currentStage = new Stage5(_StageManager, _map, _link, _DrawDungeon);
        }

        public void RightStage()
        {
            _StageManager.currentStage = new Stage4(_StageManager, _map, _link, _DrawDungeon);
        }

        public void UpStage()
        {
            _StageManager.currentStage = new Stage2(_StageManager, _map, _link, _DrawDungeon);
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
