using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Sprint0.Player;
using Microsoft.Xna.Framework;
using System.Reflection.Metadata.Ecma335;


namespace Sprint2.Map
{
    public class Stage3 : IStage
    {
        DrawDungeon _DrawDungeon;
        StageManager _StageManager;
        static int[,] room;
        static int[] doors;
        private Link _link;
        DungeonMap _map;
        DoorMap _DoorMap;
        private Rectangle playerBoundingBox;
        private bool Up = false;
        private Rectangle TopDoor;

        public Stage3(StageManager stageManager, DungeonMap map,DoorMap doorMap, Link link, DrawDungeon drawDungeon)
        {
            room = map.GetRoom(2);
            doors = doorMap.GetDoors(2);
            _StageManager = stageManager;
            _link = link;
            _map = map;
            _DoorMap = doorMap;
            _link._position.X = 120 * _StageManager._scale.X;
            _link._position.Y = 30 * _StageManager._scale.Y;
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
            
        }

        public void Draw()
        {
            int[] doorCodes = { 1, 0, 0, 0 };
            _DrawDungeon.Draw(room, doorCodes);
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
            _link._position.X = 120 * _StageManager._scale.X;
            _link._position.Y = 130 * _StageManager._scale.Y;
            _StageManager.currentStage = new Stage1(_StageManager, _map, _DoorMap, _link, _DrawDungeon);
        }

        public bool canUp()
        {
            return true;
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
            return false;
        }
    }
}