using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonData
{
    public enum Operation
    {
        Join = 1,
        Update = 2,
        Exit = 3
    }

    public class GameInstance
    {
        public Operation opCode { get; set; }
        public Player Player { get; set; }
    }
}
