using GameDesignLearningAppPrototype.Scripts.Engine;
using GameDesignLearningAppPrototype.Scripts.Engine.Rendering.Managers;
using GameDesignLearningAppPrototype.Scripts.Engine.Utility;
using GameDesignLearningAppPrototype.Scripts.Platformer.Cameras;
using GameDesignLearningAppPrototype.Scripts.Platformer.Components;
using GameDesignLearningAppPrototype.Scripts.Platformer.GameObjects;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;

namespace GameDesignLearningAppPrototype.Scripts.Platformer.Managers
{
    public class CameraManager
    {
        private readonly ClassManager classManager;
        public CameraManager(ClassManager classManager)
        {
            this.classManager = classManager;
            mainCamera = new MainCamera();
        }

        private MainCamera mainCamera;
        private GameObjectBase target;
        private float smoothTime = 0.1f;

        public void SetTarget(GameObjectBase target)
        {
            this.target = target;
        }

        public void SetCamera(MainCamera camera)
        {
            mainCamera = camera;
        }

        public MainCamera GetMainCamera()
        {
            return mainCamera;
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

        public void SetCameraScaleUniform(int programId, float multiplier)
        {
            GL.Uniform2(GL.GetUniformLocation(programId, "Scale"), GetCameraScale().X * multiplier, GetCameraScale().Y * multiplier); // Set the "ViewportSize" uniform in the shader
        }
        public void Update(GameWindow gameWindow, GameTime gameTime)
        {
            float targetX = target.getWorldPosition().x - gameWindow.Size.X / 2;
            float targetY = target.getWorldPosition().y - gameWindow.Size.Y / 2;
            SetCameraPosition(MathUtilities.Lerp(mainCamera.getWorldPosition().x, targetX, smoothTime), MathUtilities.Lerp(mainCamera.getWorldPosition().y, targetY, smoothTime));
            mainCamera.Update(gameWindow, gameTime);
        }

        public void SetCameraPosition(float x, float y)
        {
            mainCamera.SetPosition(x, y);
        }
    }
}
