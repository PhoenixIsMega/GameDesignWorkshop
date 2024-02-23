using GameDesignLearningAppPrototype.Scripts.Engine;
using GameDesignLearningAppPrototype.Scripts.Engine.Rendering.DataTypes;
using GameDesignLearningAppPrototype.Scripts.Engine.Utility;
using GameDesignLearningAppPrototype.Scripts.Platformer.Components;
using GameDesignLearningAppPrototype.Scripts.Platformer.GameObjects;
using GameDesignLearningAppPrototype.Scripts.Platformer.Tiles;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;

namespace GameDesignLearningAppPrototype.Scripts.Platformer.Players
{
    public class Player : GameObjectBase
    {
        private Quad quad;
        private TextureComponent texture;
        private BoxCollider collider;
        private PhysicsComponent physicsComponent;
        //float velocityX = 0;
        //float velocityY = 0;
        float speed = 0.6f;
        //float airResistance = 1.0f;
        //float maxVelocity = 15.0f;
        Color colorMultiplier = new Color(255, 255, 255, 255);

        //make it so width height are always 90 and scale is always 1

        public Player() : base()
        {
            quad = AddComponent<Quad>();
            texture = AddComponent<TextureComponent>();
            physicsComponent = AddComponent<PhysicsComponent>(transform);
            collider = AddComponent<BoxCollider>(transform, quad, physicsComponent);

            quad.Width = 120.0f;
            quad.Height = 120.0f;

            collider.enabled = false;
        }

        public void Move(float x, float y)
        {
            transform.X = x;
            transform.Y = y;
        }

        public (float x, float y) GetPosition()
        {
            return (this.getWorldPosition().x, this.getWorldPosition().y);
        }

        public (float sizeX, float sizeY) GetSize()
        {
            return (quad.Width, quad.Height);
        }

        public override float[] AssembleVertexData()
        {
            float x = this.getWorldPosition().x;
            float y = this.getWorldPosition().y;
            //20 wide 9 high tile map
            int tileX = Math.Abs(physicsComponent.VelocityX) < (physicsComponent.velocityThreshold + 0.00f) ? 1 : (int)(Math.Abs(x) / 42.857) % 2 + 1; //50 / 140 * 120
            float tileY = 3.0f;

            float[] verticies = new float[] {
            //Positions         //UV /Textureslot (should all be the same)
             x + (quad.Width*transform.ScaleX),  y + (quad.Height*transform.ScaleY), 0.0f, 0.1111f*tileX, 0.33333f*tileY, MathUtilities.Normalise(0, 255, colorMultiplier.R), MathUtilities.Normalise(0, 255, colorMultiplier.G), MathUtilities.Normalise(0, 255, colorMultiplier.B), 0.0f, //top right
             x + (quad.Width*transform.ScaleX),  y, 0.0f, 0.1111f*tileX, 0.33333f*tileY-0.3333f, MathUtilities.Normalise(0, 255, colorMultiplier.R), MathUtilities.Normalise(0, 255, colorMultiplier.G), MathUtilities.Normalise(0, 255, colorMultiplier.B), 0.0f,                          //bottom right
             x,  y, 0.0f, 0.1111f*tileX-0.1111f, 0.33333f*tileY-0.3333f, MathUtilities.Normalise(0, 255, colorMultiplier.R), MathUtilities.Normalise(0, 255, colorMultiplier.G), MathUtilities.Normalise(0, 255, colorMultiplier.B), 0.0f,                                                    //bottom left
             x,  y + (quad.Height*transform.ScaleY), 0.0f, 0.1111f*tileX-0.1111f, 0.33333f*tileY, MathUtilities.Normalise(0, 255, colorMultiplier.R), MathUtilities.Normalise(0, 255, colorMultiplier.G), MathUtilities.Normalise(0, 255, colorMultiplier.B), 0.0f                           //top left
            }; ;

            VertexUtilities.RotateQuad(quad.RotationAngle, quad.Width, quad.Height, x, y, ref verticies, 9);

            //Array.Copy(rotatedVerticies, verticies, rotatedVerticies.Length);

            return verticies;
        }
        public override void Update(GameWindow gameWindow, GameTime gameTime) //basic movement
        {
            // Player movement based on keyboard input
            KeyboardState input = gameWindow.KeyboardState;
            if (input.IsKeyDown(Keys.D)) physicsComponent.ApplyForce(speed, 0.0f);
            if (input.IsKeyDown(Keys.A)) physicsComponent.ApplyForce(-speed, 0.0f);
            if (input.IsKeyDown(Keys.W)) physicsComponent.ApplyForce(0.0f, speed);
            if (input.IsKeyDown(Keys.S)) physicsComponent.ApplyForce(0.0f, -speed);
            if (input.IsKeyDown(Keys.Space)) physicsComponent.ApplyForce(0.0f, 10.0f);
            physicsComponent.Update(gameWindow, gameTime);

            quad.RotationAngle = (float)(-physicsComponent.VelocityX * 1.5f);
        }
    }
}
