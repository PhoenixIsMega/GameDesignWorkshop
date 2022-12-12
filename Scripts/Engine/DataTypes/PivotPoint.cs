using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace GameDesignWorkshop.game
{
    class PivotPoint
    {
        // The position of the pivot point
        public Vector2 Position { get; set; }

        // Calculate the position of the object based on the pivot point and the dimensions of the object
        public Vector2 GetObjectPosition(Vector2 objectSize)
        {
            return Position - (objectSize / 2f);
        }
    }
}
