using GUI_20212202_BV3N92.Logic;
using GUI_20212202_BV3N92.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GUI_20212202_BV3N92.Renderer
{
    public class Display : FrameworkElement
    {
        IGameModel model;
        public Size size;
        public double rectWidth;
        public double rectHeight;

        public void Resize(Size size)
        {
            this.size = size;
        }
        public void SetupModel(IGameModel model)
        {
            this.model = model;
        }
        public Brush FloorBrush { get { return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "blank.bmp"), UriKind.RelativeOrAbsolute))); } }
        public Brush PlayerBrush { get { return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "player.bmp"), UriKind.RelativeOrAbsolute))); } }
        public Brush WallBrush { get { return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "wall.bmp"), UriKind.RelativeOrAbsolute))); } }
        public Brush AmmoBrush { get { return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "ammosingle.bmp"), UriKind.RelativeOrAbsolute))); } }
        public Brush OpponentBrush { get { return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "opponent.bmp"), UriKind.RelativeOrAbsolute))); } }
        public Brush BrickBrush { get { return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "brick.bmp"), UriKind.RelativeOrAbsolute))); } }
        public Brush HealthBrush { get { return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "hp.bmp"), UriKind.RelativeOrAbsolute))); } }
        public Brush LockedBrush { get { return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "lock.bmp"), UriKind.RelativeOrAbsolute))); } }
        public Brush ExitBrush { get { return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "exit.bmp"), UriKind.RelativeOrAbsolute))); } }
        public Brush FinishBrush { get { return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "finish.bmp"), UriKind.RelativeOrAbsolute))); } }
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            if (model != null && size.Width>50 && size.Height>50)
            {
                drawingContext.DrawRectangle(FloorBrush, null, new Rect(0, 0, size.Width, size.Height));
                rectWidth = size.Width / model.Map.GetLength(1);
                rectHeight = size.Height / model.Map.GetLength(0);
                for (int i = 0; i < model.Map.GetLength(0); i++)
                {
                    for (int j = 0; j < model.Map.GetLength(1); j++)
                    {
                        
                        switch (model.Map[i, j].type)
                        {
                            case ItemType.player:
                                drawingContext.DrawRectangle(PlayerBrush, null, model.Map[i,j].CalcArea());
                                break;
                            case ItemType.wall:
                                drawingContext.DrawRectangle(WallBrush, null, model.Map[i, j].CalcArea());
                                break;
                            case ItemType.floor:
                                break;
                            case ItemType.ammo:
                                drawingContext.DrawRectangle(AmmoBrush, null, model.Map[i, j].CalcArea());
                                break;
                            case ItemType.opponent:
                                drawingContext.DrawRectangle(OpponentBrush, null, model.Map[i, j].CalcArea());
                                break;
                            case ItemType.brick:
                                drawingContext.DrawRectangle(BrickBrush, null, model.Map[i, j].CalcArea());
                                break;
                            case ItemType.health:
                                drawingContext.DrawRectangle(HealthBrush, null, model.Map[i, j].CalcArea());
                                break;
                            case ItemType.locked:
                                drawingContext.DrawRectangle(LockedBrush, null, model.Map[i, j].CalcArea());
                                break;
                            case ItemType.exit:
                                drawingContext.DrawRectangle(ExitBrush, null, model.Map[i, j].CalcArea());
                                break;
                            case ItemType.finish:
                                drawingContext.DrawRectangle(FinishBrush, null, model.Map[i, j].CalcArea());
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }
    }
}
