using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
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
        private Rectangle TopDoor;
        private Rectangle LeftDoor;
        private Rectangle RightDoor;
        private Rectangle BottomDoor;
        private Rectangle playerBoundingBox;
        private bool Up = false;
        private bool Down = false;
        private bool Left = false;
        private bool Right = false;

        public Stage1(StageManager stageManager, DungeonMap map, Link link)
        {
              room = map.GetRoom(0);
            _StageManager = stageManager;
            _link = link;
            _map = map;
            
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
            _StageManager.currentStage = new Stage3(_StageManager, _map, _link);
        }

        public void Draw()
        {
            //_link._position.X = 120 * _StageManager._scale.X;
            //_link._position.Y = 115 * _StageManager._scale.Y;
            int[] doorCodes = { 1, 1, 1, 1 };
           _StageManager.DrawTiles(room);
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
            Up = false;
            TopDoor = new Rectangle((int)(112 * _link._scale.X), 0, (int)(32 * _link._scale.X), (int)(35 * _link._scale.X));
            playerBoundingBox = GetScaledRectangle((int)_link._position.X, (int)_link._position.Y, 16, 16, _link._scale);
            if (playerBoundingBox.Intersects(TopDoor))
            {
                Up = true;
            }
            return Up;
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
            Right = false;
            RightDoor = new Rectangle((int)(221 * _link._scale.X), (int)(72 * _link._scale.Y), (int)(32 * _link._scale.X), (int)(32 * _link._scale.X));
            playerBoundingBox = GetScaledRectangle((int)_link._position.X, (int)_link._position.Y, 16, 16, _link._scale);
            if (playerBoundingBox.Intersects(RightDoor))
            {
                Right = true;
            }
            return Right;
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
