﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonData
{
    public class Map
    {
        public Map()
        {
            PlayerList = new List<Player>();
        }
        public List<Player> PlayerList { get; set; }
    }
}
