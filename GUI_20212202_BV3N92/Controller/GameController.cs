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
                    control.Control(GameLogic.Controls.moveUp);
                    break;
                case Key.A:
                    control.Control(GameLogic.Controls.moveLeft);
                    break;
                case Key.S:
                    control.Control(GameLogic.Controls.moveDown);
                    break;
                case Key.D:
                    control.Control(GameLogic.Controls.moveRight);
                    break;
                case Key.Up:
                    control.Control(GameLogic.Controls.rotateUp);
                    break;
                case Key.Left:
                    control.Control(GameLogic.Controls.rotateLeft);
                    break;
                case Key.Down:
                    control.Control(GameLogic.Controls.rotateDown);
                    break;
                case Key.Right:
                    control.Control(GameLogic.Controls.rotateRight);
                    break;
                case Key.Space:
                    control.Control(GameLogic.Controls.shoot);
                    break;
            }
        }
    }
}
