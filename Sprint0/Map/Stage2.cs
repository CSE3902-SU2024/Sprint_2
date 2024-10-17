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
        DrawDungeon _DrawDungeon;
        StageManager _StageManager;
        static int[,] room;
        static int[] doors;
        private Link _link;
        DungeonMap _map;
        DoorMap _DoorMap;
        private bool Down = false;
        private Rectangle playerBoundingBox;
        private Rectangle BottomDoor;

        public Stage2(StageManager stageManager, DungeonMap map, DoorMap doorMap, Link link, DrawDungeon drawDungeon)
        {
             room = map.GetRoom(1);
            doors = doorMap.GetDoors(1);
            _DoorMap = doorMap;
            _StageManager = stageManager;
            _link = link;
            _map = map;
            _link._position.X = 120 * _StageManager._scale.X;
            _link._position.Y = 115 * _StageManager._scale.Y;
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
            _link._position.X = 115 * _StageManager._scale.X;
            _link._position.Y = 20 * _StageManager._scale.Y;
            _StageManager.currentStage = new Stage1(_StageManager, _map, _DoorMap, _link, _DrawDungeon);
            
        }

        public void Draw()
        {
            _DrawDungeon.Draw(room, doors);
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
            return true;
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