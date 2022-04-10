using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_20212202_BV3N92.Models
{
    public class Player : IModel
    {
        public int[] Position { get; set; }
        public Directions Direction { get; set; }
        public int Health { get; set; }
        public int Ammo { get; set; }

        public Player()
        {
            Health = 3;
            Ammo = 3;
            Direction = Directions.right;
        }

        public void Shoot(Directions direction)
        {
            // TODO: implement shooting
        }
    }
}
