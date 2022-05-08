using GUI_20212202_BV3N92.Models;
using GUI_20212202_BV3N92.Windows;
using GUI_20212202_BV3N92.Windows.Ending;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GUI_20212202_BV3N92.Logic
{
    public enum ItemType
    {
        player, wall, floor, ammo, opponent, brick, health, locked, exit, finish
    }
    public class GameLogic : IGameModel
    {

        MainWindow mainWindow;
        Size size;
        public double rectWidth;
        public double rectHeight;

        public enum Controls
        {
            moveUp, moveLeft, moveDown, moveRight, rotateUp, rotateLeft, rotateDown, rotateRight, shoot, menu
        }

        private Player player;
        private List<Opponent> opponents;
        private Queue<string> levels;
        private string currentLevel;

        public MapItem[,] Map { get; set; }

        public GameLogic(MainWindow window)
        {
            this.mainWindow = window;

            string[] lvls = Directory.GetFiles(Path.Combine("levels"), "*.lvl");
            string saved;

            try
            {
                saved = Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), "saved"), "*.sav").First();
            }
            catch (Exception)
            {

                saved = null;
            }

            player = new Player();
            opponents = new List<Opponent>();
            levels = new Queue<string>();

            if (saved != null)
            {
                //TODO: too much // in currentLvl
                StreamReader sr = new StreamReader(saved);
                string currentLvl = sr.ReadLine();

                int i = 0;
                while (!lvls[i].Contains(currentLvl) && i < lvls.Length)
                {
                    i++;
                }
                while (i < lvls.Length)
                {
                    levels.Enqueue(lvls[i]);
                }

                sr.Close();

                if (levels.Count > 0)
                    currentLevel = levels.Dequeue();
                    LoadLevel(saved, true);
            }
            else
            {
                foreach (var lvl in lvls)
                {
                    levels.Enqueue(lvl);
                }

                if (levels.Count > 0)
                    currentLevel = levels.Dequeue();
                    LoadLevel(currentLevel, false);
            }
        }

        //public void Control(Controls control)
        //{
        //    int i = player.Position[0];
        //    int j = player.Position[1];
        //    int old_i = i;
        //    int old_j = j;

        //    switch (control)
        //    {
        //        case Controls.moveUp:
        //            if (i - 1 >= 0)
        //                i--;
        //            break;
        //        case Controls.moveLeft:
        //            if (j - 1 >= 0)
        //                j--;
        //            break;
        //        case Controls.moveDown:
        //            if (i + 1 < Map.GetLength(0))
        //                i++;
        //            break;
        //        case Controls.moveRight:
        //            if (j + 1 < Map.GetLength(1))
        //                j++;
        //            break;
        //        case Controls.rotateUp:
        //            player.Direction = Directions.up;
        //            break;
        //        case Controls.rotateLeft:
        //            player.Direction = Directions.left;
        //            break;
        //        case Controls.rotateDown:
        //            player.Direction = Directions.down;
        //            break;
        //        case Controls.rotateRight:
        //            player.Direction = Directions.right;
        //            break;
        //        case Controls.shoot:
        //            player.Shoot(player.Direction);
        //            break;
        //        case Controls.menu:
        //            MenuWindow menu = new MenuWindow(mainWindow);
        //            menu.ShowDialog();

        //            if (menu.ShowDialog() == true)
        //            {
        //                //save
        //                SaveLevel();
        //            }
        //            else
        //            {
        //                //restart
        //                GameLogic restart = new GameLogic(mainWindow);
        //            }

        //            break;
        //    }

        //    if (control == Controls.moveUp || control == Controls.moveLeft || control == Controls.moveDown || control == Controls.moveRight)
        //    {
        //        if (Map[i, j] == MapItem.floor)
        //        {
        //            Map[i, j] = MapItem.player;
        //            Map[old_i, old_j] = MapItem.floor;
        //        }
        //        if (Map[i, j] == MapItem.health)
        //        {
        //            if (player.Health < 3)
        //                player.Health++;

        //            Map[i, j] = MapItem.player;
        //            Map[old_i, old_j] = MapItem.floor;
        //        }
        //        if (Map[i, j] == MapItem.ammo)
        //        {
        //            player.Ammo += 3;

        //            Map[i, j] = MapItem.player;
        //            Map[old_i, old_j] = MapItem.floor;
        //        }
        //        if (Map[i, j] == MapItem.exit)
        //        {
        //            if (levels.Count > 0)
        //            {
        //                LoadLevel(levels.Dequeue(), false);
        //            }
        //            else
        //            {
        //                EndingWindow ending = new EndingWindow();
        //                ending.ShowDialog();

        //                if (ending.ShowDialog() == true)
        //                {
        //                    //restart

        //                    GameLogic restart = new GameLogic(mainWindow);
        //                }
        //                else
        //                {
        //                    //exit

        //                    mainWindow.Close();
        //                    File.Delete("save.sav");
        //                }
        //            }

        //        }

        //    }
        //    else if (control == Controls.shoot)
        //    {
        //        player.Shoot(player.Direction);

        //        // TODO: implement map changes upon hitting objects
        //    }

        //    // TODO: implement opponent shooting at player
        //}

        private void LoadLevel(string lvlPath, bool saved)
        {
            if (saved)
            {
                string[] lines = File.ReadAllLines(lvlPath);
                Map = new MapItem[int.Parse(lines[1]), int.Parse(lines[0])];
                rectWidth = size.Width / Map.GetLength(1);
                rectHeight = size.Height / Map.GetLength(0);
                for (int i = 0; i < Map.GetLength(0); i++)
                {
                    for (int j = 0; j < Map.GetLength(1); j++)
                    {
                        switch (lines[i + 2][j])
                        {
                            case 'p':
                                Map[i, j] = new Player()
                                {
                                    X = rectWidth * j,
                                    Y = rectHeight * i,
                                };
                                break;
                            case 'w':
                                Map[i, j] = new Wall()
                                {
                                    X = rectWidth * j,
                                    Y = rectHeight * i,
                                };
                                break;
                            case 'a':
                                Map[i, j] = new Ammo()
                                {
                                    X = rectWidth * j,
                                    Y = rectHeight * i,
                                };
                                break;
                            case 'o':
                                var op = new Opponent()
                                {
                                    X = rectWidth * j,
                                    Y = rectHeight * i,
                                };
                                Map[i, j] = op;
                                opponents.Add(op);
                                break;
                            case 'b':
                                Map[i, j] = new Brick()
                                {
                                    X = rectWidth * j,
                                    Y = rectHeight * i,
                                };
                                break;
                            case 'h':
                                Map[i, j] = new Health()
                                {
                                    X = rectWidth * j,
                                    Y = rectHeight * i,
                                };
                                break;
                            case 'l':
                                Map[i, j] = new Lock()
                                {
                                    X = rectWidth * j,
                                    Y = rectHeight * i,
                                };
                                break;
                            case 'e':
                                Map[i, j] = new Exit()
                                {
                                    X = rectWidth * j,
                                    Y = rectHeight * i,
                                };
                                break;
                            case 'f':
                                Map[i, j] = new Finish()
                                {
                                    X = rectWidth * j,
                                    Y = rectHeight * i,
                                };
                                break;
                            default:
                                Map[i, j] = new Floor()
                                {
                                    X = rectWidth * j,
                                    Y = rectHeight * i,
                                };
                                break;
                        }
                    }
                }
            }
            else
            {
                string[] lines = File.ReadAllLines(lvlPath);
                Map = new MapItem[int.Parse(lines[1]), int.Parse(lines[0])];
                rectWidth = size.Width / Map.GetLength(1);
                rectHeight = size.Height / Map.GetLength(0);
                for (int i = 0; i < Map.GetLength(0); i++)
                {
                    for (int j = 0; j < Map.GetLength(1); j++)
                    {
                        switch (lines[i + 2][j])
                        {
                            case 'p':
                                Map[i, j] = new Player()
                                {
                                    X = rectWidth * j,
                                    Y = rectHeight * i,
                                };
                                break;
                            case 'w':
                                Map[i, j] = new Wall()
                                {
                                    X = rectWidth * j,
                                    Y = rectHeight * i,
                                };
                                break;
                            case 'a':
                                Map[i, j] = new Ammo()
                                {
                                    X = rectWidth * j,
                                    Y = rectHeight * i,
                                };
                                break;
                            case 'o':
                                var op = new Opponent()
                                {
                                    X = rectWidth * j,
                                    Y = rectHeight * i,
                                };
                                Map[i, j] = op;
                                opponents.Add(op);
                                break;
                            case 'b':
                                Map[i, j] = new Brick()
                                {
                                    X = rectWidth * j,
                                    Y = rectHeight * i,
                                };
                                break;
                            case 'h':
                                Map[i, j] = new Health()
                                {
                                    X = rectWidth * j,
                                    Y = rectHeight * i,
                                };
                                break;
                            case 'l':
                                Map[i, j] = new Lock()
                                {
                                    X = rectWidth * j,
                                    Y = rectHeight * i,
                                };
                                break;
                            case 'e':
                                Map[i, j] = new Exit()
                                {
                                    X = rectWidth * j,
                                    Y = rectHeight * i,
                                };
                                break;
                            case 'f':
                                Map[i, j] = new Finish()
                                {
                                    X = rectWidth * j,
                                    Y = rectHeight * i,
                                };
                                break;
                            default:
                                Map[i, j] = new Floor()
                                {
                                    X = rectWidth * j,
                                    Y = rectHeight * i,
                                };
                                break;
                        }
                    }
                }
            }
            ;
        }

        private void SaveLevel()
        {
            StreamWriter sw = new StreamWriter("saved/save.sav");
            sw.WriteLine(currentLevel);
            sw.WriteLine(Map.GetLength(1));
            sw.WriteLine(Map.GetLength(0));     

            for (int i = 0; i < Map.GetLength(0); i++)
            {
                for (int j = 0; j < Map.GetLength(1); j++)
                {
                    sw.Write(Map[i + 2, j]);
                }
                sw.Write("\n");
            }

            sw.Close();
        }       

        private void OnDeath(string currentLevel)
        {
            if (player.Health > 0)
            {
                LoadLevel(currentLevel, false);
            }
            else
            {
                GameLogic restart = new GameLogic(mainWindow);
            }
        }
    }
}
