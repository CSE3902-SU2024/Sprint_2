using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprint0.Player;

namespace Sprint2.Map
{
    public interface IStage
    {
        // Boolean values to determin if door is valid
        //bool canUp { get; set; }
        //bool canDown { get; set; }
        //bool canRight { get; set; }
        //bool canLeft { get; set; }


     //   void Update();

        void UpStage();
        void DownStage();
        void RightStage();
        void LeftStage();

        bool canUp();
        bool canDown();
        bool canLeft();
        bool canRight();
        void Draw();
    }
}
