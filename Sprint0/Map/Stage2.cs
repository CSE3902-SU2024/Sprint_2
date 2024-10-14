using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Sprint0.Player;
using Microsoft.Xna.Framework;


namespace Sprint2.Map
{
    public class Stage2 : IStage
    {
        StageManager _StageManager;
        static int[,] room;
        static int[] doorCodes;
        private Link _link;
        DungeonMap _map;
        private bool Down = false;
        private Rectangle playerBoundingBox;
        private Rectangle BottomDoor;

        public Stage2(StageManager stageManager, DungeonMap map, Link link)
        {
              room = map.GetRoom(1);
            _StageManager = stageManager;
            _link = link;
            _map = map;
            _link._position.X = 120 * _StageManager._scale.X;
            _link._position.Y = 115 * _StageManager._scale.Y;
            Debug.WriteLine("2");


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
            _link._position.X = 115 * _StageManager._scale.X;
            _link._position.Y = 20 * _StageManager._scale.Y;
            _StageManager.currentStage = new Stage1(_StageManager, _map, _link);
            
        }

        public void Draw()
        {
            int[] doorCodes = { 0, 0, 0, 1 };
            _StageManager.DrawTiles(room);
            _StageManager.DrawWalls();
            _StageManager.DrawDoors(doorCodes);
        }

        public void LeftStage()
        {

        }

        public void RightStage()
        {

        }

        //public void Update()
        //{

        //}

        public void UpStage()
        {

        }

        public bool canUp()
        {
            return false;
        }
        public bool canDown()
        {
            Down = false;
            BottomDoor = new Rectangle((int)(112 * _link._scale.X), (int)(140 * _link._scale.Y), (int)(32 * _link._scale.X), (int)(32 * _link._scale.X));
            playerBoundingBox = GetScaledRectangle((int)_link._position.X, (int)_link._position.Y, 16, 16, _link._scale);
            if (playerBoundingBox.Intersects(BottomDoor))
            {
                Down = true;
            }
            return Down;
        }
        public bool canRight()
        {
            return false;
        }
        public bool canLeft()
        {
            return false;
        }
    }
}