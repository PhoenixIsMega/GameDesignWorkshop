using GameDesignLearningAppPrototype.Scripts.Engine;
using GameDesignLearningAppPrototype.Scripts.Engine.Rendering.DataTypes;
using GameDesignLearningAppPrototype.Scripts.Engine.Utility;
using GameDesignLearningAppPrototype.Scripts.Platformer.Components;
using GameDesignLearningAppPrototype.Scripts.Platformer.GameObjects;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;

namespace GameDesignLearningAppPrototype.Scripts.Platformer.Players
{
    public class Player : GameObjectBase
    {
        private Quad quad;
        private TextureComponent texture;

        float velocityX = 0;
        float velocityY = 0;
        float speed = 1.5f;
        float airResistance = 1.0f;
        float maxVelocity = 15.0f;
        float threshold = 1.0f;
        Color colorMultiplier = new Color(255, 100, 100, 255);

        //make it so width height are always 90 and scale is always 1

        public Player() : base()
        {
            quad = AddComponent<Quad>();
            texture = AddComponent<TextureComponent>();

            quad.Width = 120.0f;
            quad.Height = 120.0f;
        }

        public void Move(float x, float y)
        {
            transform.X = x;
            transform.Y = y;
        }

        public (float, float) GetPosition()
        {
            return (transform.X, transform.Y);
        }

        public (float, float) GetSize()
        {
            return (quad.Width, quad.Height);
        }

        public override float[] AssembleVertexData()
        {
            //20 wide 9 high tile map
            int tileX = Math.Abs(velocityX) < (threshold + 0.00f) ? 1 : (int)(Math.Abs(transform.X) / 42.857) % 2 + 1; //50 / 140 * 120
            float tileY = 1.0f;

            float[] verticies = new float[] {
            //Positions         //UV /Textureslot (should all be the same)
             transform.X + (quad.Width*transform.ScaleX),  transform.Y + (quad.Height*transform.ScaleY), 0.0f, 0.1111f*tileX, 0.33333f*tileY, MathUtilities.Normalise(0, 255, colorMultiplier.R), MathUtilities.Normalise(0, 255, colorMultiplier.G), MathUtilities.Normalise(0, 255, colorMultiplier.B), 0.0f, //top right
             transform.X + (quad.Width*transform.ScaleX),  transform.Y, 0.0f, 0.1111f*tileX, 0.33333f*tileY-0.3333f, MathUtilities.Normalise(0, 255, colorMultiplier.R), MathUtilities.Normalise(0, 255, colorMultiplier.G), MathUtilities.Normalise(0, 255, colorMultiplier.B), 0.0f,                          //bottom right
             transform.X,  transform.Y, 0.0f, 0.1111f*tileX-0.1111f, 0.33333f*tileY-0.3333f, MathUtilities.Normalise(0, 255, colorMultiplier.R), MathUtilities.Normalise(0, 255, colorMultiplier.G), MathUtilities.Normalise(0, 255, colorMultiplier.B), 0.0f,                                                    //bottom left
             transform.X,  transform.Y + (quad.Height*transform.ScaleY), 0.0f, 0.1111f*tileX-0.1111f, 0.33333f*tileY, MathUtilities.Normalise(0, 255, colorMultiplier.R), MathUtilities.Normalise(0, 255, colorMultiplier.G), MathUtilities.Normalise(0, 255, colorMultiplier.B), 0.0f                           //top left
            }; ;

            VertexUtilities.RotateQuad(quad.RotationAngle, quad.Width, quad.Height, transform.X, transform.Y, ref verticies, 9);

            //Array.Copy(rotatedVerticies, verticies, rotatedVerticies.Length);

            return verticies;
        }
        public override void Update(GameWindow gameWindow, GameTime gameTime) //basic movement
        {
            // Player movement based on keyboard input
            KeyboardState input = gameWindow.KeyboardState;
            if (input.IsKeyDown(Keys.D)) velocityX = Math.Min(velocityX + speed, maxVelocity);
            if (input.IsKeyDown(Keys.A)) velocityX = Math.Max(velocityX - speed, -maxVelocity);
            if (input.IsKeyDown(Keys.W)) velocityY = Math.Min(velocityY + speed, maxVelocity);
            if (input.IsKeyDown(Keys.S)) velocityY = Math.Max(velocityY - speed, -maxVelocity);
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
            }

            quad.RotationAngle = (float)(-velocityX / 1.8);
        }
    }
}
