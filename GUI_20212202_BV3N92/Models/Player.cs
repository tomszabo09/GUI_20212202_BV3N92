using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_20212202_BV3N92.Models
{
    public class Player : MapItem
    {
        public Directions Direction { get; set; }
        public int Health { get; set; }
        public int Ammo { get; set; }
        public int Angle { get; set; }

        public Player()
        {
            Health = 3;
            Ammo = 3;
            Direction = Directions.right;
            type = Logic.ItemType.player;
        }

        public void Calcangle()
        {
            Directions dir = this.Direction;
            switch (dir)
            {
                case Directions.right:
                    this.Angle = 0;
                    break;
                case Directions.left:
                    this.Angle = 180;
                    break;
                case Directions.up:
                    this.Angle = -90;
                    break;
                case Directions.down:
                    this.Angle = 90;
                    break;
            }
        }
        
    }
}
