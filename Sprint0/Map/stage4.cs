﻿using System;
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
        DrawDungeon _DrawDungeon;
        StageManager _StageManager;
        static int[,] room;
        static int[] doors;
        private Link _link;
        DungeonMap _map;
        DoorMap _DoorMap;
        private bool Left = false;
        private Rectangle LeftDoor;
        private Rectangle playerBoundingBox;

        public Stage4(StageManager stageManager, DungeonMap map,DoorMap doorMap, Link link, DrawDungeon drawDungeon)
        {
            room = map.GetRoom(3);
            doors = doorMap.GetDoors(3);
            _StageManager = stageManager;
            _link = link;
            _map = map;
            _DoorMap = doorMap;
            _link._position.X = 32 * _StageManager._scale.X;
            _link._position.Y = 75 * _StageManager._scale.Y;
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
            _DrawDungeon.Draw(room, doors); 
        }

        public void LeftStage()
        {
            _link._position.X = 210 * _StageManager._scale.X;
            _link._position.Y = 75 * _StageManager._scale.Y;
            _StageManager.currentStage = new Stage1(_StageManager, _map, _DoorMap, _link, _DrawDungeon);
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
            return true;
        }
    }
}