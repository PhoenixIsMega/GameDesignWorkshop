using GameDesignLearningAppPrototype.Scripts.Engine;
using GameDesignLearningAppPrototype.Scripts.Engine.Utility;
using GameDesignLearningAppPrototype.Scripts.Platformer.Components;
using OpenTK.Windowing.Desktop;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameDesignLearningAppPrototype.Scripts.Platformer.Particles
{
    class Particle : GameObject
    {
        private Quad quad;
        public int lifetime = 0;
        //make it so width height are always 90 and scale is always 1

        public Particle() : base()
        {
            quad = AddComponent<Quad>();

            quad.Width = 90.0f;
            quad.Height = 90.0f;

        }

        public void Move(float x, float y)
        {
            transform.X = x;
            transform.Y = y;
        }

        public (float, float) getCoords()
        {
            return (transform.X, transform.Y);
        }

        public override float[] AssembleVertexData()
        {

            float[] verticies = new float[] {
            //Positions         //UV /Textureslot (should all be the same)
             transform.X + (quad.Width*transform.ScaleX),  transform.Y + (quad.Height*transform.ScaleY), 0.0f,//top right
             transform.X + (quad.Width*transform.ScaleX),  transform.Y, 0.0f,  //bottom right
             transform.X,  transform.Y, 0.0f,                                               //bottom left
             transform.X,  transform.Y + (quad.Height*transform.ScaleY), 0.0f //top left
            }; ;

            float[] rotatedVerticies = VertexUtilities.RotateQuad(quad.RotationAngle, quad.Width, quad.Height, transform.X, transform.Y, verticies, 3);

            Array.Copy(rotatedVerticies, verticies, rotatedVerticies.Length);

            return verticies;
        }

        public override void Update(GameWindow gameWindow, GameTime gameTime)
        {
            quad.RotationAngle = ((float)gameTime.TotalGameTime.TotalMilliseconds * 120) % 360;
        }
    }
}
