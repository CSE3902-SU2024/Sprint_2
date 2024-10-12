using System;
using System.Linq;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;


namespace Sprint2.Map
{
    public class DungeonMap
    {
        static List<int[,]> rooms;
        static int roomHeight;
        static int roomWidth;
        public DungeonMap(String filename)
        {
            string[] lines = File.ReadAllLines(filename); // replace "input.csv" with your file path

            rooms = new List<int[,]>();
            roomHeight = 7;
            roomWidth = 12;
            int[,] currentRoom = new int[roomHeight, roomWidth];
            int row = 0;

            foreach (string line in lines)
            {
                if (line.Trim() == ",,,,,,,")
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
                        string value = values[col].Trim(); // remove leading/trailing whitespace
                        if (int.TryParse(value, out int intValue))
                        {
                            currentRoom[row, col] = intValue;
                        }
                        else
                        {
                            // handle the error, e.g., set the value to 0 or a default value
                            currentRoom[row, col] = 0;
                        }
                    }
                    row++;
                }
            }

            rooms.Add(currentRoom);

        }
        public int[,] GetRoom(int roomNum)
        {
            Debug.WriteLine("room num ", roomNum);
            if (roomNum < 0 || roomNum > 2)
            {
                throw new ArgumentOutOfRangeException("Room out of range!");
            }

            return rooms[roomNum];

            //int[,] room = new int[roomHeight, roomWidth];
            //for (int i = 0; i < roomHeight; i++)
            //{
            //    for (int j = 0; j < roomWidth; j++)
            //    {
            //        room[i, j] = rooms[roomNum, i, j];
            //    }
            //}
            //return room;
        }

    }
}
