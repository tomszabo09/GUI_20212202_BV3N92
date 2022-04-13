using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_20212202_BV3N92.Models
{
    public enum Directions
    {
        up, left, down, right
    }
    public interface IModel
    {
        int[] Position { get; set; }
        Directions Direction { get; set; }

        void Shoot(Directions direction);
    }
}
