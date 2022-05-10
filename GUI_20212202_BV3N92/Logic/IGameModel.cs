using GUI_20212202_BV3N92.Models;
using System;
using System.Collections.Generic;
using static GUI_20212202_BV3N92.Logic.GameLogic;

namespace GUI_20212202_BV3N92.Logic
{
    public interface IGameModel
    {
        MapItem[,] Map { get; set; }
        Player player { get; set; }
        event EventHandler Changed;
        List<Bullet> bullets { get; set; }
        List<Wall> walls { get; set; }
        List<Health> healths { get; set; }
        List<Ammo> ammos { get; set; }
        List<Brick> bricks { get; set; }
        List<Exit> exits { get; set; }
        List<Finish> finishes { get; set; }
        List<Lock> locks { get; set; }
        List<Opponent> opponents { get; set; }
    }
}