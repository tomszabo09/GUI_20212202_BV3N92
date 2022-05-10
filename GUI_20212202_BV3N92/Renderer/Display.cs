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
            this.model.Changed+=(sender,eventargs)=>this.InvalidateVisual();
        }
        #region brushes
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
        public Brush BulletBrush { get { return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images","bullet.bmp"), UriKind.RelativeOrAbsolute))); } }
        #endregion
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            if (model != null && size.Width>50 && size.Height>50)
            {
                drawingContext.DrawRectangle(FloorBrush, null, new Rect(0, 0, size.Width, size.Height));

                rectWidth = size.Width / model.Map.GetLength(1);
                rectHeight = size.Height / model.Map.GetLength(0);
                model.player.Calcangle();
                
                foreach (var item in model.walls)
                {
                    drawingContext.DrawRectangle(WallBrush, null, item.CalcArea());
                }
                foreach (var item in model.bricks)
                {
                    drawingContext.DrawRectangle(BrickBrush, null, item.CalcArea());
                }
                foreach (var item in model.ammos)
                {
                    drawingContext.DrawRectangle(AmmoBrush, null, item.CalcArea());
                }
                foreach (var item in model.opponents)
                {
                    drawingContext.DrawRectangle(OpponentBrush, null, item.CalcArea());
                }
                foreach (var item in model.healths)
                {
                    drawingContext.DrawRectangle(HealthBrush, null, item.CalcArea());
                }
                foreach (var item in model.locks)
                {
                    drawingContext.DrawRectangle(LockedBrush, null, item.CalcArea());
                }
                foreach (var item in model.exits)
                {
                    drawingContext.DrawRectangle(ExitBrush, null, item.CalcArea());
                }
                foreach (var item in model.finishes)
                {
                    drawingContext.DrawRectangle(FinishBrush, null, item.CalcArea());
                }
                drawingContext.PushTransform(new RotateTransform(model.player.Angle, model.player.X+model.player.displayWidth/2, model.player.Y+model.player.displayHeight/2));
                drawingContext.DrawRectangle(PlayerBrush, null, model.player.CalcArea());
                drawingContext.Pop();
                foreach (var item in model.bullets)
                {
                    drawingContext.DrawRectangle(BulletBrush, null, item.CalcArea());
                }

            }
        }
    }
}
