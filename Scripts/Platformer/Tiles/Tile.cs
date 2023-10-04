using GameDesignLearningAppPrototype.Scripts.Engine;
using GameDesignLearningAppPrototype.Scripts.Engine.Utility;
using GameDesignLearningAppPrototype.Scripts.Platformer.Components;
using OpenTK.Windowing.Desktop;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameDesignLearningAppPrototype.Scripts.Platformer.Tiles
{
    class Tile : GameObject
    {
        private Quad quad;
        private Texture texture;

        //make it so width height are always 90 and scale is always 1

        public Tile() : base()
        {
            quad = AddComponent<Quad>();
            texture = AddComponent<Texture>();

            quad.Width = 90.0f;
            quad.Height = 90.0f;

            texture.TextureID = 26 - 1;
        }

        public Tile(int textureID) : this()
        {
            texture.TextureID = textureID - 1;
        }

        public Tile(int textureID, float x, float y) : this(textureID)
        {
            transform.X = x;
            transform.Y = y;
        }

        private void Move(float x, float y)
        {
            //transform.X = x;
            //transform.Y = y;
            Console.WriteLine("implementDIFFERENTLY");
        }

        public override float[] AssembleVertexData()
        {
            //20 wide 9 high tile map
            int tileX = texture.TextureID % 20 + 1;// move to calulate in texture??
            int tileY = texture.TextureID / 20 + 1;

            float[] verticies = new float[] {
            //Positions         //UV /Textureslot (should all be the same)
             transform.X + (quad.Width*transform.ScaleX),  transform.Y + (quad.Height*transform.ScaleY), 0, 0.05f*tileX, 0.1112f*tileY, 0.0f, //top right
             transform.X + (quad.Width*transform.ScaleX),  transform.Y, 0, 0.05f*tileX, 0.1112f*tileY-0.1112f, 0.0f,                          //bottom right
             transform.X,  transform.Y, 0, 0.05f*tileX-0.05f, 0.1112f*tileY-0.1112f, 0.0f,                                                    //bottom left
             transform.X,  transform.Y + (quad.Height*transform.ScaleY), 0, 0.05f*tileX-0.05f, 0.1112f*tileY, 0.0f                            //top left
            }; ;

            float[] rotatedVerticies = VertexUtilities.RotateQuad(quad.RotationAngle, quad.Width, quad.Height, transform.X, transform.Y, verticies, 6);

            Array.Copy(rotatedVerticies, verticies, rotatedVerticies.Length);

            //PrintArrayFormatted<float>(verticies, 6);

            return verticies;
        }

        /*
        static void PrintArrayFormatted<T>(T[] array, int valuesPerRow)
        {
            for (int i = 0; i < array.Length; i++)
            {
                Console.Write(array[i] + " ");

                if ((i + 1) % valuesPerRow == 0)
                {
                    Console.WriteLine();
                }
            }
        }*/


        public override void Update(GameWindow gameWindow, GameTime gameTime)
        {
        }
    }
}