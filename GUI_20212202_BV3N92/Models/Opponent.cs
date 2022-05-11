using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_20212202_BV3N92.Models
{
    public class Opponent : MapItem
    {
        public Directions Direction { get; set; }
        public bool PlayerInSight { get; set; }

        public Opponent()
        {
            PlayerInSight = false;
            Direction = Directions.right;
            type = Logic.ItemType.opponent;
        }
    }
}
