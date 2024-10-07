using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2.Map
{
    public class DoorDecoder
    {
        public int DecodeDoor(int direction, int door)
        {
            return  9 + (direction * 5) + door;
            
        }
    }
}
