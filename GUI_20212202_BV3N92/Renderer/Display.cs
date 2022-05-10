using GUI_20212202_BV3N92.Logic;
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
        Size size;

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
                double rectWidth = size.Width / model.Map.GetLength(1);
                double rectHeight = size.Height / (model.Map.GetLength(0) + 1);
                for (int i = 0; i < model.Map.GetLength(0); i++)
                {
                    for (int j = 0; j < model.Map.GetLength(1); j++)
                    {
                        switch (model.Map[i, j])
                        {
                            case GameLogic.MapItem.player:
                                drawingContext.DrawRectangle(PlayerBrush, null, new Rect(rectWidth * j, rectHeight * (i+1), rectWidth, rectHeight));
                                break;
                            case GameLogic.MapItem.wall:
                                drawingContext.DrawRectangle(WallBrush, null, new Rect(rectWidth * j, rectHeight * (i + 1), rectWidth, rectHeight));
                                break;
                            case GameLogic.MapItem.floor:
                                break;
                            case GameLogic.MapItem.ammo:
                                drawingContext.DrawRectangle(AmmoBrush, null, new Rect(rectWidth * j, rectHeight * (i + 1), rectHeight, rectWidth));
                                break;
                            case GameLogic.MapItem.opponent:
                                drawingContext.DrawRectangle(OpponentBrush, null, new Rect(rectWidth * j, rectHeight * (i + 1), rectHeight, rectWidth));
                                break;
                            case GameLogic.MapItem.brick:
                                drawingContext.DrawRectangle(BrickBrush, null, new Rect(rectWidth * j, rectHeight * (i + 1), rectHeight, rectWidth));
                                break;
                            case GameLogic.MapItem.health:
                                drawingContext.DrawRectangle(HealthBrush, null, new Rect(rectWidth * j, rectHeight * (i + 1), rectHeight, rectWidth));
                                break;
                            case GameLogic.MapItem.locked:
                                drawingContext.DrawRectangle(LockedBrush, null, new Rect(rectWidth * j, rectHeight * (i + 1), rectHeight, rectWidth));
                                break;
                            case GameLogic.MapItem.exit:
                                drawingContext.DrawRectangle(ExitBrush, null, new Rect(rectWidth * j, rectHeight * (i + 1), rectHeight, rectWidth));
                                break;
                            case GameLogic.MapItem.finish:
                                drawingContext.DrawRectangle(FinishBrush, null, new Rect(rectWidth * j, rectHeight * (i + 1), rectHeight, rectWidth));
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
