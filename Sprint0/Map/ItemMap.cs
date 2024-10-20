﻿using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Sprint0.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Sprint2.Classes;
using static Sprint2.Classes.Iitem;
using Sprint0.Player;

namespace Sprint2.Map
{
    public class ItemMap
    {
        public List<List<Iitem>> _itemMap;
        private List<int[,]> rooms;
        private int roomHeight;
        private int roomWidth;
        private Vector2 _scale;
        private GraphicsDevice _GraphicsDevice;
        private ContentManager _ContentManager;
        public Link _link;
        public ItemMap(String filename, Vector2 scale, GraphicsDevice graphicsDevice, ContentManager content, Link link)
        {
            string[] lines = File.ReadAllLines(filename);

            rooms = new List<int[,]>();
            _itemMap = new List<List<Iitem>>();
            roomHeight = 7;
            roomWidth = 12;
            _scale = scale;
            _GraphicsDevice = graphicsDevice;
            _ContentManager = content;
            _link = link;

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
                List<Iitem> items = GetItemsInRoom(_currentRoom);
                _itemMap.Add(items);
            }


        }
        public List<Iitem> GetItems(int roomNum)
        {
            if (roomNum < 0 || roomNum > _itemMap.Count)
            {
                throw new ArgumentOutOfRangeException("Room out of range!");
            }
            return _itemMap.ElementAt(roomNum);
        }

        public int[,] GetRoom(int roomNum)
        {
            if (roomNum < 0 || roomNum > rooms.Count)
            {
                throw new ArgumentOutOfRangeException("Room out of range!");
            }
            return rooms.ElementAt(roomNum);
        }

        public List<Iitem> GetItemsInRoom(int[,] room)
        {
            List<Iitem> ItemsInRoom = new List<Iitem>();

            Vector2 ItemPosition = new Vector2(32 * _scale.X, 32 * _scale.Y);
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    int tileIdx = room[i, j];

                    switch (tileIdx)
                    {
                        case 1:
                            Item fire = new Item(ItemPosition, 0, _link);
                            fire.LoadContent(_ContentManager, "zeldaLink", _GraphicsDevice, ItemType.fire);
                            ItemsInRoom.Add(fire);
                            break;
                        case 2:
                            Item unattack = new Item(ItemPosition, 10, _link);
                            unattack.LoadContent(_ContentManager, "NES - The Legend of Zelda - Items & Weapons", _GraphicsDevice, ItemType.unattackable);
                            ItemsInRoom.Add(unattack);
                            break;
                            //case 3:
                            //    Goriya goriya = new Goriya(EnemyPosition);
                            //    goriya.LoadContent(_ContentManager, "Dungeon1", _GraphicsDevice);
                            //    EnemiesInRoom.Add(goriya);
                            //    break;
                            //case 4:
                            //    Dragon dragon = new Dragon(EnemyPosition);
                            //    dragon.LoadContent(_ContentManager, "Bosses1", _GraphicsDevice);
                            //    EnemiesInRoom.Add(dragon);
                            //    break;
                    }
                    ItemPosition.X += 16 * _scale.X;
                }
                ItemPosition.X = 32 * _scale.X;
                ItemPosition.Y += 16 * _scale.Y;
            }
            return ItemsInRoom;
        }

        public void Update(int currentStage, GameTime gameTime)
        {
            List<Iitem> items = GetItems(currentStage);
            foreach (Iitem item in items)
            {
                item.Update(gameTime);
            }
        }
    }
}