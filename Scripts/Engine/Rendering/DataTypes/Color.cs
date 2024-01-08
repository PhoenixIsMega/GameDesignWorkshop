using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDesignLearningAppPrototype.Scripts.Engine.Rendering.DataTypes
{
    public class Color
    {
        private int r;
        private int g;
        private int b;
        private int a;

        public int R
        {
            get { return r; }
            set
            {
                if (value >= 0 && value <= 255)
                {
                    r = value;
                }
                else
                {
                    Console.WriteLine("Invalid Color R Error");
                }
            }
        }

        public int G
        {
            get { return g; }
            set
            {
                if (value >= 0 && value <= 255)
                {
                    g = value;
                }
                else
                {
                    Console.WriteLine("Invalid Color G Error");
                }
            }
        }
        public int B
        {
            get { return b; }
            set
            {
                if (value >= 0 && value <= 255)
                {
                    b = value;
                }
                else
                {
                    Console.WriteLine("Invalid Color B Error");
                }
            }
        }

        public int A
        {
            get { return a; }
            set
            {
                if (value >= 0 && value <= 255)
                {
                    a = value;
                }
                else
                {
                    Console.WriteLine("Invalid Color A Error");
                }
            }
        }

        public Color(int r, int g, int b, int a)
        {
            this.R = r;
            this.B = b;
            this.G = g;
            this.A = a;
        }
    }
}
