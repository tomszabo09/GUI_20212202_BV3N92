using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GUI_20212202_BV3N92.Models
{
    public class Bullet:MapItem
    {
        public Bullet(double X,double Y, double speed, double displayWidth,double displayHeight,Directions dir)
        {
            this.X = X;
            this.Y = Y;
            Speed = speed;
            this.displayWidth = displayWidth;
            this.displayHeight = displayHeight;
            this.Direction = dir;
        }
        public Directions Direction { get; set; }
        public double Speed;       
        public void Move()
        {
            switch (this.Direction)
            {
                case Directions.up:
                    this.Y -= 20;                    
                    break;
                case Directions.left:
                    this.X -= 20;                   
                    break;
                case Directions.down:
                    this.Y += 20;
                    break;
                case Directions.right:
                    this.X += 20;
                    break;
                default:
                    break;
            }
        }
    }
}
