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
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            if (model != null && size.Width>50 && size.Height>50)
            {
                double itemWidth = size.Width / model.Map.GetLength(0);
                double itemHeight = size.Height / model.Map.GetLength(1);
                drawingContext.DrawRectangle(Brushes.Black, new Pen(Brushes.Black, 0), new Rect(0, 0, size.Width, size.Height));
                for (int i = 0; i < model.Map.GetLength(0); i++)
                {
                    for (int j = 0; j < model.Map.GetLength(1); j++)
                    {
                        ImageBrush brush = new ImageBrush();
                        switch (model.Map[i,j])
                        {
                            case GameLogic.MapItem.player:
                                break;
                            case GameLogic.MapItem.wall:
                                brush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images","wall.bmp"),UriKind.RelativeOrAbsolute)));
                                break;
                            case GameLogic.MapItem.floor:
                                break;
                            case GameLogic.MapItem.ammo:
                                brush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "ammosingle.bmp"), UriKind.RelativeOrAbsolute)));
                                break;
                            case GameLogic.MapItem.opponent:
                                break;
                            case GameLogic.MapItem.brick:
                                brush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "brick.bmp"), UriKind.RelativeOrAbsolute)));
                                break;
                            case GameLogic.MapItem.health:
                                brush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "hp.bmp"), UriKind.RelativeOrAbsolute)));
                                break;
                            case GameLogic.MapItem.locked:
                                break;
                            case GameLogic.MapItem.exit:
                                break;
                            case GameLogic.MapItem.finish:
                                break;
                            default:
                                break;
                        }
                        drawingContext.DrawRectangle(brush, new Pen(Brushes.Black, 0),
                                    new Rect(i * itemWidth, j * itemHeight, itemWidth, itemHeight)
                                    );
                    }                   
                }
            }
        }
    }
}
