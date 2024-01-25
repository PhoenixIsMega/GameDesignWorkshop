using GameDesignLearningAppPrototype.Scripts.Engine.Rendering.DataTypes;
using GameDesignLearningAppPrototype.Scripts.Platformer.Components;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDesignLearningAppPrototype.Scripts.Platformer.Managers
{
    public enum Backgrounds
    {
        PLAINS_1,
        PLAINS_2, 
        DESERT,
        LUSH
    }

    internal class BackgroundManager
    {
        private Backgrounds currentBackground = Backgrounds.PLAINS_1;
        float[] verticies;

        public float[] AssembleVertexData()
        {
            int[] viewport = new int[4];
            GL.GetInteger(GetPName.Viewport, viewport); // Get the viewport dimensions
                                                        //reset if viewport changes
                                                        //Console.WriteLine("Viewport: " + viewport[2] + " " + viewport[3]);
            float width = viewport[2];
            float x =  width * ((int)CameraManager.Instance.GetCameraLocation().X / (viewport[2])-1);
            float x2 = width * (((int)CameraManager.Instance.GetCameraLocation().X / (viewport[2])));
            float x3 = width * (((int)CameraManager.Instance.GetCameraLocation().X / (viewport[2])) + 1);
            float x4 = width * (((int)CameraManager.Instance.GetCameraLocation().X / (viewport[2])) + 2);
            float x5 = width * (((int)CameraManager.Instance.GetCameraLocation().X / (viewport[2])) + 3);
            float x6 = width * (((int)CameraManager.Instance.GetCameraLocation().X / (viewport[2])) -2);

            //float x = width * 2;
            //Console.WriteLine("X: " + x);
            float tileX = 0.25f;
            float tileY = 1.0f;
            float UVXmin = (int)currentBackground * tileX;
            float UVXmax = UVXmin + tileX;
            float UVXmin2 = ((int)currentBackground) * tileX;
            float UVXmax2 = UVXmin2 + tileX;


            verticies = new float[] {
                x, 1.0f, 0, UVXmax, tileY, 0.0f, //top right
                x, 0.0f, 0, UVXmax, 0.0f , 0.0f, //bottom right
                x - width, 0.0f, 0, UVXmin, 0.0f, 0.0f, //bottom left
                x - width, 1.0f, 0, UVXmin, tileY, 0.0f, //top left

                x2, 1.0f, 0, UVXmax2, tileY, 0.0f, //top right
                x2, 0.0f, 0, UVXmax2, 0.0f , 0.0f, //bottom right
                x2 - width, 0.0f, 0, UVXmin2, 0.0f, 0.0f, //bottom left
                x2 - width, 1.0f, 0, UVXmin2, tileY, 0.0f, //top left

                x3, 1.0f, 0, UVXmax, tileY, 0.0f, //top right
                x3, 0.0f, 0, UVXmax, 0.0f , 0.0f, //bottom right
                x3 - width, 0.0f, 0, UVXmin, 0.0f, 0.0f, //bottom left
                x3 - width, 1.0f, 0, UVXmin, tileY, 0.0f, //top left

                x4, 1.0f, 0, UVXmax2, tileY, 0.0f, //top right
                x4, 0.0f, 0, UVXmax2, 0.0f , 0.0f, //bottom right
                x4 - width, 0.0f, 0, UVXmin2, 0.0f, 0.0f, //bottom left
                x4 - width, 1.0f, 0, UVXmin2, tileY, 0.0f, //top left

                x5, 1.0f, 0, UVXmax2, tileY, 0.0f, //top right
                x5, 0.0f, 0, UVXmax2, 0.0f , 0.0f, //bottom right
                x5 - width, 0.0f, 0, UVXmin2, 0.0f, 0.0f, //bottom left
                x5 - width, 1.0f, 0, UVXmin2, tileY, 0.0f, //top left

                x6, 1.0f, 0, UVXmax2, tileY, 0.0f, //top right
                x6, 0.0f, 0, UVXmax2, 0.0f , 0.0f, //bottom right
                x6 - width, 0.0f, 0, UVXmin2, 0.0f, 0.0f, //bottom left
                x6 - width, 1.0f, 0, UVXmin2, tileY, 0.0f, //top left
            };
            //make into objects?

            return verticies;
        }
    }
}
