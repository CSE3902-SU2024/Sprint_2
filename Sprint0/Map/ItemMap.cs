using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.Classes;
using Sprint0.Player;
using Sprint2.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static Sprint0.Player.ILinkState;
using static Sprint2.Classes.Iitem;

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
        private Fairy fairy;
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

            Vector2 ItemPosition = new Vector2(32 * _scale.X, 87 * _scale.Y);
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    int tileIdx = room[i, j];

                    switch (tileIdx)
                    {
                        case 1:
                            Fire fire = new Fire(ItemPosition, _link);
                            fire.LoadContent(_ContentManager, "zeldaLink", _GraphicsDevice, ItemType.fire, _scale);
                            ItemsInRoom.Add(fire);
                            break;
                        case 2:
                            Health health = new Health(ItemPosition, _link);
                            health.LoadContent(_ContentManager, "NES - The Legend of Zelda - Items & Weapons", _GraphicsDevice, ItemType.health, _scale);
                            ItemsInRoom.Add(health);
                            break;
                        case 3:
                            Heart heart = new Heart(ItemPosition, _link);
                            heart.LoadContent(_ContentManager, "NES - The Legend of Zelda - Items & Weapons", _GraphicsDevice, ItemType.heart, _scale);
                            ItemsInRoom.Add(heart);
                            break;
                        case 4:
                            Clock clock = new Clock(ItemPosition, _link);
                            clock.LoadContent(_ContentManager, "NES - The Legend of Zelda - Items & Weapons", _GraphicsDevice, ItemType.clock, _scale);
                            ItemsInRoom.Add(clock);
                            break;
                        case 5:
                            fairy = new Fairy(ItemPosition, _link);
                            fairy.LoadContent(_ContentManager, "NES - The Legend of Zelda - Items & Weapons", _GraphicsDevice, ItemType.fairy, _scale);
                            ItemsInRoom.Add(fairy);
                            break;
                        case 6:
                            Diamond diamond = new Diamond(ItemPosition, _link);
                            diamond.LoadContent(_ContentManager, "NES - The Legend of Zelda - Items & Weapons", _GraphicsDevice, ItemType.diamond, _scale);
                            ItemsInRoom.Add(diamond);
                            break;
                        case 7:
                            Potion potion = new Potion(ItemPosition, _link);
                            potion.LoadContent(_ContentManager, "NES - The Legend of Zelda - Items & Weapons", _GraphicsDevice, ItemType.potion, _scale);
                            ItemsInRoom.Add(potion);
                            break;
                        case 8:
                            Triangle triangle = new Triangle(ItemPosition, _link);
                            triangle.LoadContent(_ContentManager, "NES - The Legend of Zelda - Items & Weapons", _GraphicsDevice, ItemType.triangle, _scale);
                            ItemsInRoom.Add(triangle);
                            break;
                        case 9:
                            Maps map = new Maps(ItemPosition, _link);
                            map.LoadContent(_ContentManager, "NES - The Legend of Zelda - Items & Weapons", _GraphicsDevice, ItemType.map, _scale);
                            ItemsInRoom.Add(map);
                            break;
                        case 10:
                            Key key = new Key(ItemPosition, _link);
                            key.LoadContent(_ContentManager, "NES - The Legend of Zelda - Items & Weapons", _GraphicsDevice, ItemType.key, _scale);
                            ItemsInRoom.Add(key);
                            break;
                        case 11:
                            Bow bow = new Bow(ItemPosition, _link);
                            bow.LoadContent(_ContentManager, "NES - The Legend of Zelda - Items & Weapons", _GraphicsDevice, ItemType.bow, _scale);
                            ItemsInRoom.Add(bow);
                            break;
                        case 12:
                            Boom boom = new Boom(ItemPosition, _link);
                            boom.LoadContent(_ContentManager, "NES - The Legend of Zelda - Items & Weapons", _GraphicsDevice, ItemType.boom, _scale);
                            ItemsInRoom.Add(boom);
                            break;
                        case 13:
                            Compass compass = new Compass(ItemPosition, _link);
                            compass.LoadContent(_ContentManager, "NES - The Legend of Zelda - Items & Weapons", _GraphicsDevice, ItemType.compass, _scale);
                            ItemsInRoom.Add(compass);
                            break;

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
            if (fairy.follow && _link.transitioning)
            {
                items.Add(fairy);
                fairy.Position.X = _link._position.X;
                fairy.Position.Y = _link._position.Y;
            }
            foreach (Iitem item in items)
            {
                item.Update(gameTime);
                
                
            }
        }
    }
}
