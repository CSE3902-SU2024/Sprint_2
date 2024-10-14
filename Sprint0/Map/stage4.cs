using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Sprint0.Player;
using Microsoft.Xna.Framework;


namespace Sprint2.Map
{
    public class Stage4 : IStage
    {
        StageManager _StageManager;
        static int[,] room;
        static int[] doorCodes;
        private Link _link;
        DungeonMap _map;
        private bool Left = false;
        private Rectangle LeftDoor;
        private Rectangle playerBoundingBox;

        public Stage4(StageManager stageManager, DungeonMap map, Link link)
        {
            room = map.GetRoom(3);
            _StageManager = stageManager;
            _link = link;
            _map = map;
            _link._position.X = 32 * _StageManager._scale.X;
            _link._position.Y = 75 * _StageManager._scale.Y;



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

        }

        public void Draw()
        {
            int[] doorCodes = { 0, 1, 0, 0 };
            _StageManager.DrawTiles(room);
            _StageManager.DrawWalls();
            _StageManager.DrawDoors(doorCodes);
        }

        public void LeftStage()
        {
            _link._position.X = 210 * _StageManager._scale.X;
            _link._position.Y = 75 * _StageManager._scale.Y;
            _StageManager.currentStage = new Stage1(_StageManager, _map, _link);
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
            return false;
        }
        public bool canRight()
        {
            return false;
        }
        public bool canLeft()
        {
            Left = false;
            LeftDoor = new Rectangle(0, (int)(72 * _link._scale.Y), (int)(35 * _link._scale.X), (int)(32 * _link._scale.X));
            playerBoundingBox = GetScaledRectangle((int)_link._position.X, (int)_link._position.Y, 16, 16, _link._scale);
            if (playerBoundingBox.Intersects(LeftDoor))
            {
                Left = true;
            }
            return Left;
        }
    }
}