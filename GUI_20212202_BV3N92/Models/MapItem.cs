using GUI_20212202_BV3N92.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GUI_20212202_BV3N92.Models
{
    public abstract class MapItem
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double displayWidth { get; set; }
        public double displayHeight { get; set; }
        public ItemType type { get; set; }
        public virtual Rect CalcArea()
        {
            return new Rect(X, Y, displayWidth, displayHeight);
        }

        public bool IsColliding(MapItem other)
        {
            var thisArea = this.CalcArea();
            var otherArea = other.CalcArea();
            return thisArea.IntersectsWith(otherArea);
        }
    }
}
