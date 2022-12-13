using GameDesignWorkshop.game.managers;
using GameDesignWorkshop.management;
using GameDesignWorkshop.management.Textures;
using GameDesignWorkshop.me.phoenix.rendering.shaders;
using GameDesignWorkshop.rendering;
using GameDesignWorkshop.rendering.buffers;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameDesignWorkshop
{
    internal class MusicIntegration : Game
    {
        public MusicIntegration (string windowTitle, int initialWindowWidth, int initialWindowHeight) : base(windowTitle, initialWindowWidth, initialWindowHeight)
        {
        }

        private GameClassManager ClassManager;

        SpritesheetProperties spritesheetProperties0 = new SpritesheetProperties() 
        {leftMargin = 0, 
            bottomMargin = 2,
            rightMargin = 0, 
            topMargin = 0, 
            verticalSpacing = 2, 
            horizontalSpacing = 2, 
            tileSizeX = 64, 
            tileSizeY = 64, 
            imageSizeX = 914, 
            imageSizeY = 936};

        private readonly float[] _verticies = {
            //Positions         //UV        //Colors          //Textureslot (should all be the same)
             0.5f,  0.5f, 0.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 0.0f, //top right
             0.5f, -0.5f, 0.0f, 1.0f, 0.0f, 1.0f, 1.0f, 1.0f, 0.0f, //bottom right
            -0.5f, -0.5f, 0.0f, 0.0f, 0.0f, 1.0f, 1.0f, 1.0f, 0.0f,//bottom left
            -0.5f,  0.5f, 0.0f, 0.0f, 1.0f, 1.0f, 1.0f, 1.0f, 0.0f//top left
        };

        private uint[] _indices =
        {
            0, 1, 3, //tri 1
            1, 2, 3 //tri 2
        };

        private VertexBuffer vertexBuffer;
        private IndexBuffer indexBuffer; //could move to be local
        private VertexArray vertexArray;

        private Shader shader;

        //OnEnable
        protected override void Initialise()
        {
            //load classManager, which also loads all other non-static classes, that can then be referenced
            this.ClassManager = new GameClassManager();

            this.ClassManager.GetMusicManager().Initialise(); //dispose at end aswell
            this.ClassManager.GetSoundEffectManager().LoadSound("exit","Assets/sounds/exit.wav");
            //this.ClassManager.GetSoundEffectManager().LoadSound("exit", "Assets/sounds/exit.wav");

            float[] uvCoords = new SpritesheetManager().getSpriteSheetUV(spritesheetProperties0, 1, 5);
            Console.WriteLine(uvCoords[0].ToString() + '+' + uvCoords[1].ToString());
        }

        protected override void LoadContent()
        {
            Console.WriteLine(1);
            //load UVs
            //shader = new Shader(Shader.ParseShader("resources/shaders/TextureWithColorAndTextureSlot.glsl"));
            shader = new Shader(Shader.ParseShader("Assets/shaders/TextureWithColorAndTextureSlotAndFader.glsl"));
            if (!shader.CompileShader())
            {
                Console.WriteLine("Failed to compile shader");
                return;
            }
            vertexArray = new VertexArray();
            vertexBuffer = new VertexBuffer(_verticies);

            BufferLayout bufferLayout = new BufferLayout();
            bufferLayout.Add<float>(3); //xyz
            bufferLayout.Add<float>(2); //uv
            bufferLayout.Add<float>(3); //color
            bufferLayout.Add<float>(1); //textureslot


            vertexArray.AddBuffer(vertexBuffer, bufferLayout);

            shader.Use();

            indexBuffer = new IndexBuffer(_indices);
            var textureSamepleUniformLocation = shader.GetUniformLocation("u_Texture[0]");
            int[] samplers = new int[2] { 0, 1 };
            GL.Uniform1(textureSamepleUniformLocation, 2, samplers);

            ResourceManager.Instance.LoadTexture("Assets/textures/platformer/tiles_spritesheet.png");
            ResourceManager.Instance.LoadTexture("Assets/textures/platformer/sample.png");

            //texture = ResourceManager.Instance.LoadTexture("resources/textures/sample.png");
            //texture.Use();
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState input = gameWindow.KeyboardState;

            if (input.IsKeyPressed(Keys.F))
            {
                Console.WriteLine("INput");
                ClassManager.GetSoundEffectManager().PlaySoundEffect("exit"); //make into enum!!
            }
        }

        protected override void Render(GameTime gameTime)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit); //swap?
            GL.ClearColor(new Color4(0.8f, 0.4f, 0.5f, 1f));
            shader.Use();
            vertexArray.Bind();
            indexBuffer.Bind();

            GL.Uniform1(GL.GetUniformLocation(shader.ProgramId, "blackness"), (float)Math.Sin(gameTime.TotalGameTime.TotalMilliseconds) / (2.0f) + 0.5f);

            GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);
        }

        public GameClassManager GetClassManager()
        {
            return ClassManager;
        }
    }
}
