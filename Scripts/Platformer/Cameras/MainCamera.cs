using GameDesignLearningAppPrototype.Scripts.Engine;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameDesignLearningAppPrototype.Scripts.Platformer.Cameras
{
    public class MainCamera : GameObject
    {
        float velocityX = 0;
        float velocityY = 0;
        float speed = 1.5f;
        float airResistance = 1.0f;
        float maxVelocity = 15.0f;
        float threshold = 1.0f;

        //make it so width height are always 90 and scale is always 1

        public MainCamera() : base()
        {
        }

        public override float[] AssembleVertexData()
        {
            return null;
        }

        public override void Update(GameWindow gameWindow, GameTime gameTime)
        {
            // Player movement based on keyboard input
            /*KeyboardState input = gameWindow.KeyboardState;
            if (input.IsKeyDown(Keys.Right)) velocityX = Math.Min(velocityX + speed, maxVelocity);
            if (input.IsKeyDown(Keys.Left)) velocityX = Math.Max(velocityX - speed, -maxVelocity);
            if (input.IsKeyDown(Keys.Up)) velocityY = Math.Min(velocityY + speed, maxVelocity);
            if (input.IsKeyDown(Keys.Down)) velocityY = Math.Max(velocityY - speed, -maxVelocity);
            transform.X += velocityX;
            transform.Y += velocityY;

            // Apply air resistance to velocity
            if (Math.Abs(velocityX) < threshold)
            {
                velocityX = 0.0f;
            }
            else if (velocityX > 0.0f)
            {
                velocityX -= airResistance;
            }
            else if (velocityX < 0.0f)
            {
                velocityX += airResistance;
            }

            if (Math.Abs(velocityY) < threshold)
            {
                velocityY = 0.0f;
            }
            else if (velocityY > 0.0f)
            {
                velocityY -= airResistance;
            }
            else if (velocityY < 0.0f)
            {
                velocityY += airResistance;
            }*/
        }

        public void setPosition (float x , float y)
        {
            transform.X = x;
            transform.Y = y;
        }
    }
}
