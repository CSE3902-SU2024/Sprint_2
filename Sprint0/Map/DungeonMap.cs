using System;
using System.Linq;
using System.IO;
using System.Diagnostics;


namespace Sprint2.Map
{
    public class DungeonMap
    {
        static int[,,] rooms;
        static int roomHeight;
        static int roomWidth;
        public DungeonMap(String filename)
        {
            string[] lines;
            try
            {
                lines = File.ReadAllLines(filename);
            }
            catch (Exception)
            {
                throw new IOException("CSV does not exist!!!");
            }
            roomWidth = 12;
            roomHeight = 7;
            int roomCount = lines.Length / (roomWidth * roomHeight + 1);

            rooms = new int[roomCount, roomHeight, roomWidth];
            Debug.WriteLine(lines[0]);
            int roomIdx = 0;
            for (int i = 0; i < roomCount; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i])) continue;

                int rowIndex = (i - roomIdx * (roomWidth * roomHeight + 1)) / roomWidth;
                int colIndex = (i - roomIdx * (roomWidth * roomHeight + 1)) % roomWidth;

                rooms[roomIdx, rowIndex, colIndex] = int.Parse(lines[i]);
                if ((i + 1) % (roomWidth * roomHeight + 1) == 0) roomIdx++;
            }
        }
        public int[,] GetRoom(int roomNum)
        {
            Debug.WriteLine("room num ", roomNum);
            if (roomNum < 0 || roomNum > 2)
            {
                throw new ArgumentOutOfRangeException("Room out of range!");
            }

            int[,] room = new int[roomHeight, roomWidth];
            for (int i = 0; i < roomHeight; i++)
            {
                for (int j = 0; j < roomWidth; j++)
                {
                    room[i, j] = rooms[roomNum, i, j];
                }
            }
            return room;
        }

    }
}
