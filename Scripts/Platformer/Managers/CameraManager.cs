using GameDesignLearningAppPrototype.Scripts.Engine;
using GameDesignLearningAppPrototype.Scripts.Platformer.Cameras;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;

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

        public Vector2 GetCameraLocation()
        {
            return new Vector2(mainCamera.transform.X, mainCamera.transform.Y);
        }

        public Vector2 GetCameraScale()
        {
            return new Vector2(mainCamera.transform.ScaleX, mainCamera.transform.ScaleY);
        }

        public void SetCameraScale(float scale)
        {
            mainCamera.transform.ScaleX = scale;
            mainCamera.transform.ScaleY = scale;
        }

        public void SetCameraUniform(int programId)
        {
            GL.Uniform2(GL.GetUniformLocation(programId, "CameraLocation"), GetCameraLocation().X, GetCameraLocation().Y); // Set the "ViewportSize" uniform in the shader
        }

        public void SetCameraScaleUniform(int programId)
        {
            GL.Uniform2(GL.GetUniformLocation(programId, "Scale"), GetCameraScale().X, GetCameraScale().Y); // Set the "ViewportSize" uniform in the shader
        }

        public void Update(GameWindow gameWindow, GameTime gameTime)
        {
            mainCamera.Update(gameWindow, gameTime);
        }

        public void SetCameraPosition(float x, float y)
        {
            mainCamera.SetPosition(x, y);
        }
    }
}
