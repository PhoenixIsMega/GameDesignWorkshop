using GameDesignLearningAppPrototype.Scripts.Engine.Rendering.DataTypes;
using GameDesignLearningAppPrototype.Scripts.Engine.Rendering.Managers;
using GameDesignLearningAppPrototype.Scripts.Engine.Rendering.Managers.RenderLayerManagers;
using GameDesignLearningAppPrototype.Scripts.Menu.Managers;
using GameDesignLearningAppPrototype.Scripts.Platformer;
using GameDesignLearningAppPrototype.Scripts.Platformer.Components;
using GameDesignLearningAppPrototype.Scripts.Platformer.GameObjects;
using GameDesignLearningAppPrototype.Scripts.Platformer.Managers;
using GameDesignLearningAppPrototype.Scripts.Platformer.Particles;
using GameDesignLearningAppPrototype.Scripts.Platformer.Players;
using GameDesignLearningAppPrototype.Scripts.Platformer.Tiles;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;

namespace GameDesignLearningAppPrototype.Scripts.Engine.Versions
{
    public class PrototypeVersion1 : EngineBase
    {
        public PrototypeVersion1(string windowTitle, int initialWindowWidth, int initialWindowHeight) : base(windowTitle, initialWindowWidth, initialWindowHeight)
        {
        }

        private bool renderWireframe = false;
        private ClassManager classManager;

        private Player player;

        //make camera manager not a singleton
        protected override void Initialise()
        {
            Console.WriteLine("Initalise");
            classManager = new ClassManager(this);
            //cameraManager = new CameraManager();
        }

        protected override void LoadContent()
        {
            Console.WriteLine("Load");
            classManager.LayerManager.LoadContent();
            gameWindow.CursorState = OpenTK.Windowing.Common.CursorState.Hidden;

            //load scene
            player = classManager.PlayerManager.createPlayer(); //creates player if hasnt already been made
            GameObjectBase cameraFollow = new EmptyGameObject();
            player.addChild(cameraFollow);
            cameraFollow.GetComponent<Transform>().X = player.GetSize().sizeX/2;
            cameraFollow.GetComponent<Transform>().Y = player.GetSize().sizeY/2 + 150;
            classManager.CameraManager.SetTarget(cameraFollow);
            ParticleEmitter snowfall = classManager.ParticleManager.createParticleEmitter(classManager, 4000.0f, 0.01f);
            snowfall.GetComponent<Transform>().Y = 740;
            snowfall.randomVelocityXMax = 6;
            snowfall.randomVelocityXMin = -6;
            Particle snowflake = new Particle();
            snowflake.startSize = 9.5f;
            snowflake.endSize = 9.5f;
            snowflake.lifetimeMax = 75;
            snowflake.rotationspeed = 15;
            snowflake.startColor = new Color(255, 255, 255, 255);
            snowflake.endColor = new Color(255, 255, 255, 0);
            snowflake.GetComponent<PhysicsComponent>().gravity = true;
            snowfall.SetParticleToSpawn(snowflake);
            classManager.CameraManager.GetMainCamera().addChild(snowfall);

        }


        protected override void Update(GameTime gameTime)
        {
            KeyboardState input = gameWindow.KeyboardState;
            if (input.IsKeyPressed(Keys.Escape))
            {
                gameWindow.Close();
            }

            if (input.IsKeyPressed(Keys.B))
            {
                renderWireframe = !renderWireframe;
            }

            if (input.IsKeyPressed(Keys.P))
            {
                player.GetComponent<PhysicsComponent>().gravity = !player.GetComponent<PhysicsComponent>().gravity;
                player.GetComponent<BoxCollider>().enabled = !player.GetComponent<BoxCollider>().enabled;
            }

            classManager.Update(gameWindow, gameTime);
            }

        float scale = 1;

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            //Console.WriteLine("Mouse Wheel");

            //ZOOM
            /*
            float delta = e.OffsetY;
            scale *= 1 + (delta / 30);
            //Console.WriteLine(scale);
            CameraManager.Instance.SetCameraScale(scale);
            */

            classManager.CursorManager.cursor.tileType = classManager.CursorManager.cursor.tileType + (int)e.OffsetY;
            if (classManager.CursorManager.cursor.tileType < TileType.AIR)
            {
                classManager.CursorManager.cursor.tileType = TileType.AIR;
            } else if (classManager.CursorManager.cursor.tileType > TileType.WATERFALL)
            {
                classManager.CursorManager.cursor.tileType = TileType.WATERFALL;
            }
        }

        protected override void Render(GameTime gameTime)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit); // Clear the color 

            classManager.LayerManager.Render(renderWireframe);


        }

        protected override void UnloadContent()
        {
            //unload and dispose of everything here
            Console.WriteLine("Unload");
        }

        /*public (float, float) getPlayerLocation()
        {
            return classManager.PlayerManager.GetPlayerPosition();
        }*/
    }
}
