using GameDesignLearningAppPrototype.Scripts.Engine;
using GameDesignLearningAppPrototype.Scripts.Engine.Utility;
using GameDesignLearningAppPrototype.Scripts.Platformer;
using GameDesignLearningAppPrototype.Scripts.Platformer.Components;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameDesignLearningAppPrototype.Scripts.Menu.Cursors
{
    public class Cursor : GameObject
    {
        private Quad quad;
        private Texture texture;

        private bool mouseDown = false;

        //make it so width height are always 90 and scale is always 1

        public Cursor() : base()
        {
            quad = AddComponent<Quad>();
            texture = AddComponent<Texture>();

            quad.Width = 80.0f;
            quad.Height = 80.0f;
        }

        public void Move(float x, float y)
        {
            transform.X = x;
            transform.Y = y;
        }

        public override float[] AssembleVertexData()
        {
            int tileX = 1;
            float tileY = 7.0f;

            float[] verticies = new float[] {
            //Positions         //UV /Textureslot (should all be the same)
             transform.X + (quad.Width*transform.ScaleX),  transform.Y + (quad.Height*transform.ScaleY), 0.0f, 0.029412f*tileX, 0.041667f*tileY, 0.0f, //top right
             transform.X + (quad.Width*transform.ScaleX),  transform.Y, 0.0f, 0.029412f*tileX, 0.041667f*tileY-0.041667f, 0.0f,                          //bottom right
             transform.X,  transform.Y, 0.0f, 0.029412f*tileX-0.029412f, 0.041667f*tileY-0.041667f, 0.0f,                                                    //bottom left
             transform.X,  transform.Y + (quad.Height*transform.ScaleY), 0.0f, 0.029412f*tileX-0.029412f, 0.041667f*tileY, 0.0f                           //top left
            };

            return verticies;
        }
        public override void Update(GameWindow gameWindow, GameTime gameTime)
        {
            if (gameWindow.IsAnyMouseButtonDown)
            {
                mouseDown = true;
                transform.ScaleX = 0.8f;
                transform.ScaleY = 0.8f;
            }
            else
            {
                mouseDown = false;
                transform.ScaleX = 1.0f;
                transform.ScaleY = 1.0f;
            }

            Vector2 mousePosition = gameWindow.MousePosition;
            transform.X = mousePosition.X - ((5 * transform.ScaleX) * 6);

            int[] viewport = new int[4];
            GL.GetInteger(GetPName.Viewport, viewport); // Get the viewport dimensions

            transform.Y = (viewport[3] - mousePosition.Y) - ((5*transform.ScaleY)*13);
        }
    }
}
