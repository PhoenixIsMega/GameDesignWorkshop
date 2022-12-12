using System;
using System.Collections.Generic;
using System.Text;

namespace GameDesignWorkshop.game.objects.particles
{
    public class RGBAColor
    {
        // Red, green, blue, and alpha (transparency) values of the color
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }
        public byte A { get; set; }

        public RGBAColor() { }

            // Constructor that takes a hexadecimal string
            public RGBAColor(string value)
        {
            // Parse the hexadecimal string and extract the red, green, blue, and alpha values
            R = Convert.ToByte(value.Substring(1, 2), 16);
            G = Convert.ToByte(value.Substring(3, 2), 16);
            B = Convert.ToByte(value.Substring(5, 2), 16);
            A = Convert.ToByte(value.Substring(7, 2), 16);
        }

        // Constructor that takes four integer values
        public RGBAColor(int r, int g, int b, int a)
        {
            R = (byte)r;
            G = (byte)g;
            B = (byte)b;
            A = (byte)a;
        }
    }
}
