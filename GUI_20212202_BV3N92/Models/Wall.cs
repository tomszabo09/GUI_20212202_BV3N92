﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_20212202_BV3N92.Models
{
    public class Wall : MapItem
    {
        public Wall()
        {
            type = Logic.ItemType.wall;
        }
    }
}
