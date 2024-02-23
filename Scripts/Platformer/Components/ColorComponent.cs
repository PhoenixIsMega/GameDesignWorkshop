using GameDesignLearningAppPrototype.Scripts.Engine.Rendering.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDesignLearningAppPrototype.Scripts.Platformer.Components
{
    public class ColorComponent : ComponentBase
    {
        public Color color;
        public ColorComponent() : base() { }
        public ColorComponent(Color color) : this()
        {
            this.color = color;
        }

        public void SetColor(Color color)
        {
            this.color = color;
        }

        public Color GetColor()
        {
            return color;
        }


    }
}
