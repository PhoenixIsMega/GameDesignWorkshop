using System;
using System.Collections.Generic;
using System.Text;

namespace GameDesignWorkshop.Scripts.Game.Objects.Components
{
    class Rigidbody : Component
    {
        //gravity
        public float Mass { get; set; }
        public float VelocityX { get; set; }
        public float VelocityY { get; set; }

        public Rigidbody()
        {
            Mass = 1f;
            VelocityX = 0f;
            VelocityY = 0f;
        }

        public void Update()
        {
            // Update physics simulation logic
        }
    }
}
