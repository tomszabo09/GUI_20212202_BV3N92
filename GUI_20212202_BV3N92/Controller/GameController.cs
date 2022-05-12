using GUI_20212202_BV3N92.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GUI_20212202_BV3N92.Controller
{
    public class GameController
    {
        IGameControl control;

        public GameController(IGameControl control)
        {
            this.control = control;
        }

        public void KeyPressed(Key key)
        {
            switch (key)
            {
                case Key.W:
                    control.Control(Controls.moveUp);
                    break;
                case Key.A:
                    control.Control(Controls.moveLeft);
                    break;
                case Key.S:
                    control.Control(Controls.moveDown);
                    break;
                case Key.D:
                    control.Control(Controls.moveRight);
                    break;
                case Key.Space:
                    control.Control(Controls.shoot);
                    break;
                case Key.Escape:
                    control.Control(Controls.menu);
                    break;
            }
        }
    }
}
