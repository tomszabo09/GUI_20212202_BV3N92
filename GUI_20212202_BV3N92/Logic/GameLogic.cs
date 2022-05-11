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
        public event EventHandler Changed;

        MainWindow mainWindow;

        System.Windows.Size size;

        public double rectWidth;

        public double rectHeight;

        private Queue<string> levels;

        private string currentLevel;

        public Player player { get; set; }
        public List<Opponent> opponents { get; set; }
        public List<Wall> walls { get; set; }
        public List <Health> healths { get; set; }
        public List<Ammo> ammos { get; set; }
        public List<Brick> bricks { get; set; }
        public List<Exit> exits { get; set; }
        public List<Finish> finishes { get; set; }
        public List<Lock> locks { get; set; }
        public List<Bullet> bullets { get; set; }
        public MapItem[,] Map { get; set; }
        public Player Player { get => player; }
        public string CurrentLevel { get => currentLevel.Substring(10, 2); }

        public enum Controls
        {
            moveUp, moveLeft, moveDown, moveRight, shoot, menu
        }

        public GameLogic()
        {
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
                while (i < lvls.Length && !lvls[i].Contains(currentLvl))
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

        public GameLogic(MainWindow window, System.Windows.Size size)
        {
            this.mainWindow = window;
            this.size = size;
            levels = new Queue<string>();
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

            if (saved != null)
            {
                //TODO: too much // in currentLvl
                StreamReader sr = new StreamReader(saved);
                string currentLvl = sr.ReadLine();

                int i = 0;
                while (i < lvls.Length && !lvls[i].Contains(currentLvl))
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

        public void Control(Controls control)
        {
            switch (control)
            {
                case Controls.moveUp:
                    if (Collides(Directions.up))
                    {
                        break;
                    }                   
                    player.Y -= 10;
                    player.Direction = Directions.up;
                    break;
                case Controls.moveLeft:
                    if (Collides(Directions.left))
                    {
                        break;
                    }
                    player.X -= 10;
                    player.Direction=Directions.left;
                    break;
                case Controls.moveDown:
                    if (Collides(Directions.down))
                    {
                        break;
                    }
                    player.Y += 10;
                    player.Direction=Directions.down;
                    break;
                case Controls.moveRight:
                    if (Collides(Directions.right))
                    {
                        break;
                    }
                    player.X += 10;
                    player.Direction = Directions.right;
                    break;                
                case Controls.shoot:
                    if (player.Ammo > 0)
                    {
                        player.Ammo--;
                        NewShoot(player.Direction);
                    }
                    break;
                case Controls.menu:
                    MenuWindow menu = new MenuWindow(mainWindow);
                    menu.ShowDialog();

                    if (menu.ShowDialog() == true)
                    {
                        //save
                        SaveLevel();
                    }
                    else
                    {
                        //restart
                        GameLogic restart = new GameLogic(mainWindow, size);
                    }
                    break;
                default:
                    break;
            }
            Changed?.Invoke(this, null);
        }

        private bool CollisionLogic(Player tmpplayer)
        {
            foreach (var item in walls)
            {
                if (tmpplayer.IsColliding(item))
                {
                    return true;
                }
            }
            foreach (var item in healths)
            {
                if (tmpplayer.IsColliding(item))
                {
                    player.Health++;
                    healths.Remove(item);
                    return true;
                }
            }
            foreach (var item in ammos)
            {
                if (tmpplayer.IsColliding(item))
                {
                    player.Ammo += 3;
                    ammos.Remove(item);
                    return true;
                }
            }
            foreach (var item in bricks)
            {
                if (tmpplayer.IsColliding(item))
                {
                    return true;
                }
            }
            foreach (var item in exits)
            {
                if (tmpplayer.IsColliding(item))
                {
                    if (levels.Count > 0)
                    {
                        currentLevel = levels.Dequeue();
                        LoadLevel(currentLevel, false);
                    }
                    return true;
                }
            }
            foreach (var item in finishes)
            {
                if (tmpplayer.IsColliding(item))
                {
                    MessageBox.Show("YOU WIN!");
                    mainWindow.Close();
                    return true;
                }
            }
            foreach (var item in locks)
            {
                if (tmpplayer.IsColliding(item))
                {
                    return true;
                }
            }
            foreach (var item in opponents)
            {
                if (tmpplayer.IsColliding(item))
                {
                    opponents.Remove(item);
                    player.Health--;
                    return true;
                }
            }
            return false;
        }

        private bool Collides(Directions dir)
        {
            Player tmpplayer = new Player()
            {
                Direction = player.Direction,
                X = player.X,
                Y = player.Y,
                displayWidth = player.displayWidth,
                displayHeight = player.displayHeight,

            };            
            switch (dir)
            {
                case Directions.up:
                    tmpplayer.Y -= 10;
                    return CollisionLogic(tmpplayer);
                case Directions.left:
                    tmpplayer.X -= 10;
                    return CollisionLogic(tmpplayer);
                case Directions.down:
                    tmpplayer.Y += 10;
                    return CollisionLogic(tmpplayer);
                case Directions.right:
                    return CollisionLogic(tmpplayer);
                default:
                    return true;
            }
        }

        private void NewShoot(Directions dir)
        {
            bullets.Add(new Bullet(player.X, player.Y, 20, player.displayWidth, player.displayHeight, player.Direction));
        }

        public void TimeStep()
        {
            for (int i = 0; i < bullets.Count; i++)
            {
                bullets[i].Move();
                if (BulletCollides(bullets[i]))
                {
                    bullets.RemoveAt(i);
                }
            }
            List<Lock> dellocks = new List<Lock>();
            if (opponents.Count == 0)
            {
                foreach (var item in locks)
                {
                    dellocks.Add(item);
                }
                foreach (var item in dellocks)
                {
                    locks.Remove(item);
                }
            }
            
            Changed?.Invoke(this, null);
        }

        private bool BulletCollisionLogic(Bullet bullet)
        {
            foreach (var item in walls)
            {
                if (bullet.IsColliding(item))
                {
                    return true;
                }
            }
            foreach (var item in healths)
            {
                if (bullet.IsColliding(item))
                {
                    return true;
                }
            }
            foreach (var item in ammos)
            {
                if (bullet.IsColliding(item))
                {
                    return true;
                }
            }
            foreach (var item in bricks)
            {
                if (bullet.IsColliding(item))
                {
                    bricks.Remove(item);
                    return true;
                }
            }
            foreach (var item in exits)
            {
                if (bullet.IsColliding(item))
                {
                    return true;
                }
            }
            foreach (var item in finishes)
            {
                if (bullet.IsColliding(item))
                {
                    return true;
                }
            }
            foreach (var item in locks)
            {
                if (bullet.IsColliding(item))
                {
                    return true;
                }
            }
            foreach (var item in opponents)
            {
                if (bullet.IsColliding(item))
                {
                    opponents.Remove(item);
                    return true;
                }
            }
            return false;
        }

        private bool BulletCollides(Bullet bullet)
        {
            switch (bullet.Direction)
            {
                case Directions.up:
                    bullet.Y -= 10;
                    return BulletCollisionLogic(bullet);
                case Directions.left:
                    bullet.X -= 10;
                    return BulletCollisionLogic(bullet);
                case Directions.down:
                    bullet.Y += 10;
                    return BulletCollisionLogic(bullet);
                case Directions.right:
                    return BulletCollisionLogic(bullet);
                default:
                    return true;
            }
        }
        private void LoadLevel(string lvlPath, bool saved)
        {
            player = new Player();
            opponents = new List<Opponent>();
            walls = new List<Wall>();
            healths = new List<Health>();
            ammos = new List<Ammo>();
            bricks = new List<Brick>();
            exits = new List<Exit>();
            finishes = new List<Finish>();
            locks = new List<Lock>();

            bullets = new List<Bullet>();

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
                                var pl = new Player()
                                {
                                    X = (rectWidth * j) + 5,
                                    Y = (rectHeight * i) + 5,
                                    displayWidth = rectWidth - 10,
                                    displayHeight = rectHeight - 10
                                };
                                Map[i, j] = pl;
                                player = pl;
                                break;
                            case 'w':
                                var wall = new Wall()
                                {
                                    X = rectWidth * j,
                                    Y = rectHeight * i,
                                    displayWidth = rectWidth,
                                    displayHeight = rectHeight
                                };
                                Map[i, j] = wall;
                                walls.Add(wall);
                                break;
                            case 'a':
                                var ammo = new Ammo()
                                {
                                    X = rectWidth * j,
                                    Y = rectHeight * i,
                                    displayWidth = rectWidth,
                                    displayHeight = rectHeight
                                };
                                Map[i, j] = ammo;
                                ammos.Add(ammo);
                                break;
                            case 'o':
                                var op = new Opponent()
                                {
                                    X = rectWidth * j,
                                    Y = rectHeight * i,
                                    displayWidth = rectWidth,
                                    displayHeight = rectHeight
                                };
                                Map[i, j] = op;
                                opponents.Add(op);
                                break;
                            case 'b':
                                var brick = new Brick()
                                {
                                    X = rectWidth * j,
                                    Y = rectHeight * i,
                                    displayWidth = rectWidth,
                                    displayHeight = rectHeight
                                };
                                Map[i, j] = brick;
                                bricks.Add(brick);
                                break;
                            case 'h':
                                var hp = new Health()
                                {
                                    X = rectWidth * j,
                                    Y = rectHeight * i,
                                    displayWidth = rectWidth,
                                    displayHeight = rectHeight
                                };
                                Map[i, j] = hp;
                                healths.Add(hp);
                                break;
                            case 'l':
                                var locked = new Lock()
                                {
                                    X = rectWidth * j,
                                    Y = rectHeight * i,
                                    displayWidth = rectWidth,
                                    displayHeight = rectHeight
                                };
                                Map[i, j] = locked;
                                locks.Add(locked);
                                break;
                            case 'e':
                                var exit = new Exit()
                                {
                                    X = rectWidth * j,
                                    Y = rectHeight * i,
                                    displayWidth = rectWidth,
                                    displayHeight = rectHeight
                                };
                                Map[i, j] = exit;
                                exits.Add(exit);
                                break;
                            case 'f':
                                var finish = new Finish()
                                {
                                    X = rectWidth * j,
                                    Y = rectHeight * i,
                                    displayWidth = rectWidth,
                                    displayHeight = rectHeight
                                };
                                Map[i, j] = finish;
                                finishes.Add(finish);
                                break;
                            default:
                                Map[i, j] = new Floor()
                                {
                                    X = rectWidth * j,
                                    Y = rectHeight * i,
                                    displayWidth = rectWidth,
                                    displayHeight = rectHeight
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
                                var pl = new Player()
                                {
                                    X = (rectWidth * j)+5,
                                    Y = (rectHeight * i)+5,
                                    displayWidth = rectWidth-10,
                                    displayHeight = rectHeight-10
                                };
                                Map[i, j] = pl;
                                player = pl;
                                break;
                            case 'w':
                                var wall = new Wall()
                                {
                                    X = rectWidth * j,
                                    Y = rectHeight * i,
                                    displayWidth = rectWidth,
                                    displayHeight = rectHeight
                                };
                                Map[i, j] = wall;
                                walls.Add(wall);
                                break;
                            case 'a':
                                var ammo = new Ammo()
                                {
                                    X = rectWidth * j,
                                    Y = rectHeight * i,
                                    displayWidth = rectWidth,
                                    displayHeight = rectHeight
                                };
                                Map[i, j] = ammo;
                                ammos.Add(ammo);
                                break;
                            case 'o':
                                var op = new Opponent()
                                {
                                    X = rectWidth * j,
                                    Y = rectHeight * i,
                                    displayWidth = rectWidth,
                                    displayHeight = rectHeight
                                };
                                Map[i, j] = op;
                                opponents.Add(op);
                                break;
                            case 'b':
                                var brick = new Brick()
                                {
                                    X = rectWidth * j,
                                    Y = rectHeight * i,
                                    displayWidth = rectWidth,
                                    displayHeight = rectHeight
                                };
                                Map[i, j] = brick;
                                bricks.Add(brick);
                                break;
                            case 'h':
                                var hp = new Health()
                                {
                                    X = rectWidth * j,
                                    Y = rectHeight * i,
                                    displayWidth = rectWidth,
                                    displayHeight = rectHeight
                                };
                                Map[i, j] = hp;
                                healths.Add(hp);
                                break;
                            case 'l':
                                var locked = new Lock()
                                {
                                    X = rectWidth * j,
                                    Y = rectHeight * i,
                                    displayWidth = rectWidth,
                                    displayHeight = rectHeight
                                };
                                Map[i, j] = locked;
                                locks.Add(locked);
                                break;
                            case 'e':
                                var exit = new Exit()
                                {
                                    X = rectWidth * j,
                                    Y = rectHeight * i,
                                    displayWidth = rectWidth,
                                    displayHeight = rectHeight
                                };
                                Map[i, j] = exit;
                                exits.Add(exit);
                                break;
                            case 'f':
                                var finish = new Finish()
                                {
                                    X = rectWidth * j,
                                    Y = rectHeight * i,
                                    displayWidth = rectWidth,
                                    displayHeight = rectHeight
                                };
                                Map[i, j] = finish;
                                finishes.Add(finish);
                                break;
                            default:                                
                                Map[i, j] = new Floor()
                                {
                                    X = rectWidth * j,
                                    Y = rectHeight * i,
                                    displayWidth = rectWidth,
                                    displayHeight = rectHeight
                                };
                                break;
                        }
                    }
                }
                ;
            }
            
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
                GameLogic restart = new GameLogic(mainWindow,size);
            }
        }
    }
}
