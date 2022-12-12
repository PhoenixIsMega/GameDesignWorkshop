using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace GameDesignWorkshop.game
{
    class Rectangle
    {
        // The pivot point of the rectangle
        public PivotPoint PivotPoint { get; set; }

        // The dimensions of the rectangle
        public Vector2 Size { get; set; }

        // Calculate the position of the rectangle based on the pivot point and the dimensions of the rectangle
        public Vector2 Position => PivotPoint.GetObjectPosition(Size);

        // Calculate the coordinates of the corners of the rectangle
        public int Left => (int)Position.X;
        public int Right => (int)Position.X + (int)Size.X;
        public int Top => (int)Position.Y;
        public int Bottom => (int)Position.Y + (int)Size.Y;

        // Check if the rectangle contains a given point
        public bool Contains(int x, int y)
        {
            return (x >= Left && x <= Right && y >= Top && y <= Bottom);
        }

        // Check if the rectangle intersects with another rectangle
        public bool Intersects(Rectangle rect)
        {
            return !(Left > rect.Right || Right < rect.Left || Top > rect.Bottom || Bottom < rect.Top);
        }
    }
}
