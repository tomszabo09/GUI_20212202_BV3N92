using GUI_20212202_BV3N92.Models;
using static GUI_20212202_BV3N92.Logic.GameLogic;

namespace GUI_20212202_BV3N92.Logic
{
    public interface IGameModel
    {
        MapItem[,] Map { get; set; }
    }
}