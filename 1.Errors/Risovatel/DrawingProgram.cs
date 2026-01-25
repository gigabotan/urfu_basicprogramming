using System;
using Avalonia.Media;
using RefactorMe.Common;

namespace RefactorMe
{
    class DrawingProgram
    {
        static float x, y;
        static IGraphics graphics;

        public static void Initialize(IGraphics newGraphics)
        {
            graphics = newGraphics;
            //graphics.SmoothingMode = SmoothingMode.None;
            graphics.Clear(Colors.Black);
        }

        public static void SetPosition(float x0, float y0)
        { x = x0; y = y0; }

        public static void DrawLine(Pen pen, double length, double angle)
        {
            //Делает шаг длиной length в направлении angle и рисует пройденную траекторию
            var x1 = (float)(x + length * Math.Cos(angle));
            var y1 = (float)(y + length * Math.Sin(angle));
            graphics.DrawLine(pen, x, y, x1, y1);
            x = x1;
            y = y1;
        }

        public static void Move(double length, double angle)
        {
            x = (float)(x + length * Math.Cos(angle));
            y = (float)(y + length * Math.Sin(angle));
        }
    }

    public class ImpossibleSquare
    {
        public static void Draw(int width, int height, double rotationAngle, IGraphics graphics)
        {
            // rotationAngle пока не используется, но будет использоваться в будущем
            DrawingProgram.Initialize(graphics);
            var size = Math.Min(width, height);
            InitializeStartPosition(width, height, size);

            DrawSquareSide(size, 0);
            DrawSquareSide(size, -Math.PI / 2);
            DrawSquareSide(size, Math.PI);
            DrawSquareSide(size, Math.PI / 2);
        }

        private static void InitializeStartPosition(int width, int height, double size)
        {
            var diagonalLength = Math.Sqrt(2) * (size * 0.375f + size * 0.04f) / 2;
            var x0 = (float)(diagonalLength * Math.Cos(Math.PI / 4 + Math.PI)) + width / 2f;
            var y0 = (float)(diagonalLength * Math.Sin(Math.PI / 4 + Math.PI)) + height / 2f;
            DrawingProgram.SetPosition(x0, y0);
        }

        private static void DrawSquareSide(double size, double baseAngle)
        {
            DrawingProgram.DrawLine(new Pen(Brushes.Yellow), size * 0.375f, baseAngle);
            DrawingProgram.DrawLine(new Pen(Brushes.Yellow), size * 0.04f * Math.Sqrt(2), baseAngle + Math.PI / 4);
            DrawingProgram.DrawLine(new Pen(Brushes.Yellow), size * 0.375f, baseAngle + Math.PI);
            DrawingProgram.DrawLine(new Pen(Brushes.Yellow), size * 0.375f - size * 0.04f, baseAngle + Math.PI / 2);

            DrawingProgram.Move(size * 0.04f, baseAngle - Math.PI);
            DrawingProgram.Move(size * 0.04f * Math.Sqrt(2), baseAngle + 3 * Math.PI / 4);
        }
    }
}
