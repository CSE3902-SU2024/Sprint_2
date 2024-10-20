﻿using System;
using System.Linq;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using Sprint2.Enemy;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.Player;
using Sprint2.Collisions;
using Microsoft.Xna.Framework;
using System.Reflection.Metadata;
using Microsoft.Xna.Framework.Content;


namespace Sprint2.Map
{
    public class Enemy_Item_Map
    {
        public List<List<IEnemy>> _EnemyMap;
        private List<int[,]> rooms;
        private int roomHeight;
        private int roomWidth;
        private Vector2 _scale;
        private GraphicsDevice _GraphicsDevice;
        private ContentManager _ContentManager;
        public Enemy_Item_Map(String filename, Vector2 scale, GraphicsDevice graphicsDevice, ContentManager content)
        {
            string[] lines = File.ReadAllLines(filename);

            rooms = new List<int[,]>();
            _EnemyMap = new List<List<IEnemy>>();   
            roomHeight = 7;
            roomWidth = 12;
            _scale = scale;
            _GraphicsDevice = graphicsDevice;
            _ContentManager = content;
            
            int[,] currentRoom = new int[roomHeight, roomWidth];
            int row = 0;

            foreach (string line in lines)
            {
                if (line.Length < 20)
                {
                    rooms.Add(currentRoom);
                    currentRoom = new int[roomHeight, roomWidth];
                    row = 0;

                }
                else
                {
                    string[] values = line.Split(',');
                    for (int col = 0; col < 12; col++)
                    {
                        string value = values[col].Trim();
                        if (int.TryParse(value, out int intValue))
                        {
                            currentRoom[row, col] = intValue;
                        }
                        else
                        {
                            currentRoom[row, col] = 0;
                        }
                    }
                    row++;
                }
            }
            rooms.Add(currentRoom);

            for (int x = 0; x < rooms.Count; x++)
            {
                int[,] _currentRoom = GetRoom(x);
                List<IEnemy> enemies = GetEnemiesInRoom(_currentRoom);
                _EnemyMap.Add(enemies);
            }


        }
        public List<IEnemy> GetEnemies(int roomNum)
        {
            if (roomNum < 0 || roomNum > _EnemyMap.Count)
            {
                throw new ArgumentOutOfRangeException("Room out of range!");
            }
            return _EnemyMap.ElementAt(roomNum);
        }

        public int[,] GetRoom(int roomNum)
        {
            if (roomNum < 0 || roomNum > rooms.Count)
            {
                throw new ArgumentOutOfRangeException("Room out of range!");
            }
            return rooms.ElementAt(roomNum);
        }

        public List<IEnemy> GetEnemiesInRoom(int[,] room)
        {
            List<IEnemy> EnemiesInRoom = new List<IEnemy>();

            Vector2 EnemyPosition = new Vector2(32 * _scale.X, 32 * _scale.Y);
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    int tileIdx = room[i, j];

                    switch (tileIdx)
                    {
                        case 1:
                            Keese keese = new Keese(EnemyPosition);
                            keese.LoadContent(_ContentManager, "Dungeon1", _GraphicsDevice);
                            EnemiesInRoom.Add(keese);
                            break;
                        case 2:
                            Stalfos stalfos = new Stalfos(EnemyPosition);
                            stalfos.LoadContent(_ContentManager, "Dungeon1", _GraphicsDevice);
                            EnemiesInRoom.Add(stalfos);
                            break;
                        case 3:
                            Goriya goriya = new Goriya(EnemyPosition);
                            goriya.LoadContent(_ContentManager, "Dungeon1", _GraphicsDevice);
                            EnemiesInRoom.Add(goriya);
                            break;
                        case 4:
                            Dragon dragon = new Dragon(EnemyPosition);
                            dragon.LoadContent(_ContentManager, "Bosses1", _GraphicsDevice);
                            EnemiesInRoom.Add(dragon);
                            break;
                    }
                    EnemyPosition.X += 16 * _scale.X;
                }
                EnemyPosition.X = 32 * _scale.X;
                EnemyPosition.Y += 16 * _scale.Y;
            }
            return EnemiesInRoom;
        }

        public void Update(int currentStage, GameTime gameTime)
        {
            List<IEnemy> enemies = GetEnemies(currentStage);
            foreach (IEnemy enemy in enemies)
            {
                enemy.Update(gameTime);
            }
        }
    }
}

