using GameDesignLearningAppPrototype.Scripts.Engine;
using GameDesignLearningAppPrototype.Scripts.Platformer.Cameras;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameDesignLearningAppPrototype.Scripts.Platformer.Managers
{
    public class CameraManager
    {
        private static CameraManager instance = null; // Singleton instance
        private static readonly object loc = new object(); // Lock object for thread safety
        
        public static CameraManager Instance
        {
            get
            {
                lock (loc) // Lock the access to the instance creation for thread safety
                {
                    if (instance is null) // If the instance is not yet created
                    {
                        instance = new CameraManager(); // Create a new instance of ResourceManager
                    }
                }
                return instance; // Return the instance
            }
        }

        MainCamera mainCamera;

        public CameraManager()
        {
            mainCamera = new MainCamera();
        }

        public Vector2 getCameraLocation()
        {
            return new Vector2(mainCamera.transform.X, mainCamera.transform.Y);
        }

        public Vector2 getCameraScale()
        {
            return new Vector2(mainCamera.transform.ScaleX, mainCamera.transform.ScaleY);
        }

        public void setCameraScale(float scale)
        {
            mainCamera.transform.ScaleX = scale;
            mainCamera.transform.ScaleY = scale;
        }

        public void setCameraUniform(int programId)
        {
            GL.Uniform2(GL.GetUniformLocation(programId, "CameraLocation"), getCameraLocation().X, getCameraLocation().Y); // Set the "ViewportSize" uniform in the shader
        }

        public void setCameraScaleUniform(int programId)
        {
            GL.Uniform2(GL.GetUniformLocation(programId, "Scale"), getCameraScale().X, getCameraScale().Y); // Set the "ViewportSize" uniform in the shader
        }

        public void Update(GameWindow gameWindow, GameTime gameTime)
        {
            mainCamera.Update(gameWindow, gameTime);
        }

        public void setCameraPosition(float x, float y)
        {
            mainCamera.setPosition(x, y);
        }
    }
}
