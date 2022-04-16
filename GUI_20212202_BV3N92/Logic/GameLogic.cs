﻿using GUI_20212202_BV3N92.Models;
using GUI_20212202_BV3N92.Windows;
using GUI_20212202_BV3N92.Windows.Ending;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_20212202_BV3N92.Logic
{
    public class GameLogic : IGameModel, IGameControl
    {

        MainWindow window;

        public enum MapItem
        {
            player, wall, floor, ammo, opponent, brick, health, locked, exit, finish
        }

        public enum Controls
        {
            moveUp, moveLeft, moveDown, moveRight, rotateUp, rotateLeft, rotateDown, rotateRight, shoot, menu
        }

        private Player player;
        private List<Opponent> opponents;
        private Queue<string> levels;

        public MapItem[,] Map { get; set; }

        public GameLogic(MainWindow window)
        {
            this.window = window;

            string[] lvls = Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), "levels"), "*.lvl");
            player = new Player();
            opponents = new List<Opponent>();

            levels = new Queue<string>();
            foreach (var lvl in lvls)
            {
                levels.Enqueue(lvl);
            }

            if (levels.Count > 0)
                LoadLevel(levels.Dequeue());
        }

        public void Control(Controls control)
        {
            int i = player.Position[0];
            int j = player.Position[1];
            int old_i = i;
            int old_j = j;

            switch (control)
            {
                case Controls.moveUp:
                    if (i - 1 >= 0)
                        i--;
                    break;
                case Controls.moveLeft:
                    if (j - 1 >= 0)
                        j--;
                    break;
                case Controls.moveDown:
                    if (i + 1 < Map.GetLength(0))
                        i++;
                    break;
                case Controls.moveRight:
                    if (j + 1 < Map.GetLength(1))
                        j++;
                    break;
                case Controls.rotateUp:
                    player.Direction = Directions.up;
                    break;
                case Controls.rotateLeft:
                    player.Direction = Directions.left;
                    break;
                case Controls.rotateDown:
                    player.Direction = Directions.down;
                    break;
                case Controls.rotateRight:
                    player.Direction = Directions.right;
                    break;
                case Controls.shoot:
                    player.Shoot(player.Direction);
                    break;
                case Controls.menu:
                    MenuWindow menu = new MenuWindow(window);
                    menu.ShowDialog();
                    // TODO: menu
                    break;
            }

            if (control == Controls.moveUp || control == Controls.moveLeft || control == Controls.moveDown || control == Controls.moveRight)
            {
                if (Map[i, j] == MapItem.floor)
                {
                    Map[i, j] = MapItem.player;
                    Map[old_i, old_j] = MapItem.floor;
                }
                if (Map[i, j] == MapItem.health)
                {
                    if (player.Health < 3)
                        player.Health++;

                    Map[i, j] = MapItem.player;
                    Map[old_i, old_j] = MapItem.floor;
                }
                if (Map[i, j] == MapItem.ammo)
                {
                    player.Ammo += 3;

                    Map[i, j] = MapItem.player;
                    Map[old_i, old_j] = MapItem.floor;
                }
                if (Map[i, j] == MapItem.exit)
                {
                    if (levels.Count > 0)
                    {
                        LoadLevel(levels.Dequeue());
                    }
                    else
                    {
                        EndingWindow ending = new EndingWindow();
                        ending.ShowDialog();

                        if (ending.ShowDialog() == true)
                        {
                            GameLogic restart = new GameLogic(window);
                        }
                        else
                        {
                            window.Close();
                            //TODO: close the MainWindow
                        }
                    }

                }

            }
            else if (control == Controls.shoot)
            {
                player.Shoot(player.Direction);

                // TODO: implement map changes upon hitting objects
            }

            // TODO: implement opponent shooting at player
        }

        private void LoadLevel(string lvlPath)
        {
            string[] lines = File.ReadAllLines(lvlPath);
            Map = new MapItem[int.Parse(lines[1]), int.Parse(lines[0])];
            for (int i = 0; i < Map.GetLength(0); i++)
            {
                for (int j = 0; j < Map.GetLength(1); j++)
                {
                    Map[i, j] = ConvertToEnum(lines[i + 2][j]);

                    if (Map[i, j] == MapItem.player)
                    {
                        player.Position = new int[] { i, j };
                    }
                    else if (Map[i, j] == MapItem.opponent)
                        opponents.Add(new Opponent() { Position = new int[] { i, j } });
                }
            }
        }

        private MapItem ConvertToEnum(char mapItem)
        {
            switch (mapItem)
            {
                case 'p':
                    return MapItem.player;
                case 'w':
                    return MapItem.wall;
                case 'a':
                    return MapItem.ammo;
                case 'o':
                    return MapItem.opponent;
                case 'b':
                    return MapItem.brick;
                case 'h':
                    return MapItem.health;
                case 'l':
                    return MapItem.locked;
                case 'e':
                    return MapItem.exit;
                case 'f':
                    return MapItem.finish;
                default:
                    return MapItem.floor;
            }
        }
    }
}
