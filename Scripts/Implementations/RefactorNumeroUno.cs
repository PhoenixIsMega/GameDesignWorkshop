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

        float velocityX = 0;
        float velocityY = 0;
        float speed = 1.5f;
        float airResistance = 1.0f;
        float maxVelocity = 15.0f;
        static float x = 100f;
        static float y = 100f;
        static float w = 140f;
        static float h = 140f;
        static float gap = 200;
        static float z = 0;

        float[] _verticies = {
            //Positions         //UV        //Colors          //Textureslot (should all be the same)
             x + w,  y + h, z, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 0.0f, //top right
             x + w,  y, z, 1.0f, 0.0f, 1.0f, 1.0f, 1.0f, 0.0f, //bottom right
             x,  y, z, 0.0f, 0.0f, 1.0f, 1.0f, 1.0f, 0.0f,//bottom left
             x,  y + h, z, 0.0f, 1.0f, 1.0f, 1.0f, 1.0f, 0.0f,//top left

            //Positions         //UV        //Colors          //Textureslot (should all be the same)
            x + w + gap,  y + h+ gap, z, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, //top right
            x + w + gap,  y + gap, z, 1.0f, 0.0f, 1.0f, 1.0f, 1.0f, 1.0f, //bottom right
            x + gap,  y + gap, z, 0.0f, 0.0f, 1.0f, 1.0f, 1.0f, 1.0f,//bottom left
            x + gap,  y + h + gap, z, 0.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f,//top left
            //Positions         //UV        //Colors          //Textureslot (should all be the same)
            x + w - gap,  y + h- gap, z, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, //top right
            x + w - gap,  y - gap, z, 1.0f, 0.0f, 1.0f, 1.0f, 1.0f, 1.0f, //bottom right
            x - gap,  y - gap, z, 0.0f, 0.0f, 1.0f, 1.0f, 1.0f, 1.0f,//bottom left
            x - gap,  y + h - gap, z, 0.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f,//top left
        };

        private uint[] _indices =
        {
            0, 1, 3, //tri 1
            1, 2, 3, //tri 2
            4, 5, 7,
            5, 6, 7,
            8, 9, 11,
            9, 10, 11
        };

        private VertexBuffer vertexBuffer;
        private IndexBuffer indexBuffer; //could move to be local
        private VertexArray vertexArray;

        private Shader shader;

        //OnEnable
        protected override void Initialise()
        {
            this.ClassManager = new GameClassManager();

            this.ClassManager.GetMusicManager().Initialise(); //dispose at end aswell
            this.ClassManager.GetSoundEffectManager().LoadSound("exit","Assets/sounds/exit.wav");

            float[] uvCoords = new SpritesheetManager().getSpriteSheetUV(spritesheetProperties0, 1, 5);
            Console.WriteLine(uvCoords[0].ToString() + '+' + uvCoords[1].ToString());
        }

        protected override void LoadContent()
        {
            Console.WriteLine(1);
            shader = new Shader(Shader.ParseShader("Assets/shaders/ViewportTexture.glsl"));
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
            var textureSampleUniformLocation = shader.GetUniformLocation("u_Texture[0]");
            int[] samplers = new int[2] { 0, 1 };
            GL.Uniform1(textureSampleUniformLocation, 2, samplers);

            ResourceManager.Instance.LoadTexture("Assets/textures/platformer/alienPink.png");
            ResourceManager.Instance.LoadTexture("Assets/textures/platformer/ice_sheet.png");
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState input = gameWindow.KeyboardState;
            if (input.IsKeyDown(Keys.D)) velocityX = Math.Min(velocityX + speed, maxVelocity);
            if (input.IsKeyDown(Keys.A)) velocityX = Math.Max(velocityX - speed, -maxVelocity);
            if (input.IsKeyDown(Keys.W)) velocityY = Math.Min(velocityY + speed, maxVelocity);
            if (input.IsKeyDown(Keys.S)) velocityY = Math.Max(velocityY - speed, -maxVelocity);
            x += velocityX;
            y += velocityY;

            if(velocityX > 0.0f)
            {
                velocityX -= airResistance;
            } else if (velocityX < 0.0f)
            {
                velocityX += airResistance;
            }

            if (velocityY > 0.0f)
            {
                velocityY -= airResistance;
            }
            else if (velocityY < 0.0f)
            {
                velocityY += airResistance;
            }

            float colourRed = Math.Abs(Math.Max(Math.Abs(velocityX), Math.Abs(velocityY)) / (maxVelocity + speed));
            //float colourRed = Math.Sin((float)gameTime.ElapsedGameTime.TotalSeconds);

            _verticies = new float[] {
            //Positions         //UV        //Colors          //Textureslot (should all be the same)
             x + w,  y + h*2, z, 0.60f, 0.25f, 1.0f, 1.0f-colourRed, 1.0f-colourRed, 0.0f, //top right
             x + w,  y-h, z, 0.60f, 0.0f, 1.0f, 1.0f-colourRed, 1.0f-colourRed, 0.0f, //bottom right
             x,  y-h, z, 0.25f, 0.0f, 1.0f, 1.0f-colourRed, 1.0f-colourRed, 0.0f,//bottom left
             x,  y + h*2, z, 0.25f, 0.25f, 1.0f, 1.0f-colourRed, 1.0f-colourRed, 0.0f,//top left

            //Positions         //UV        //Colors          //Textureslot (should all be the same)
            x + w + gap,  y + h+ gap, z, 0.5f, 0.5f, 1.0f, 1.0f-colourRed, 1.0f-colourRed, 1.0f, //top right
            x + w + gap,  y + gap, z, 0.5f, 0.0f, 1.0f, 1.0f-colourRed, 1.0f-colourRed, 1.0f, //bottom right
            x + gap,  y + gap, z, 0.0f, 0.0f, 1.0f, 1.0f-colourRed, 1.0f-colourRed, 1.0f,//bottom left
            x + gap,  y + h + gap, z, 0.0f, 0.5f, 1.0f, 1.0f-colourRed, 1.0f-colourRed, 1.0f,//top left

             //Positions         //UV        //Colors          //Textureslot (should all be the same)
            x + w - gap,  y + h- gap, z, 0.5f, 0.5f, 1.0f, 1.0f-colourRed, 1.0f-colourRed, 1.0f, //top right
            x + w - gap,  y - gap, z, 0.5f, 0.0f, 1.0f, 1.0f-colourRed, 1.0f-colourRed, 1.0f, //bottom right
            x - gap,  y - gap, z, 0.0f, 0.0f, 1.0f, 1.0f-colourRed, 1.0f-colourRed, 1.0f,//bottom left
            x - gap,  y + h - gap, z, 0.0f, 0.5f, 1.0f, 1.0f-colourRed, 1.0f-colourRed, 1.0f,//top left
            };
        }

        protected override void Render(GameTime gameTime)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit); //swap?
            GL.ClearColor(new Color4(0.35f, 0.75f, 0.45f, 1f));
            shader.Use();
            vertexArray.Bind();
            GL.BufferSubData(BufferTarget.ArrayBuffer,(IntPtr) 0, _verticies.Length * sizeof(float), _verticies);
            indexBuffer.Bind();
            GL.Uniform1(GL.GetUniformLocation(shader.ProgramId, "blackness"), 1.0f);
            int[] viewport = new int[4];    
            GL.GetInteger(GetPName.Viewport, viewport);
            GL.Uniform2(GL.GetUniformLocation(shader.ProgramId, "ViewportSize"), (float) viewport[2], (float) viewport[3]);
            GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);
        }

        public GameClassManager GetClassManager()
        {
            return ClassManager;
        }

        protected override void UnloadContent()
        {
            //unload and dispose of everything here
        }
    }
}
