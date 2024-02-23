using GameDesignLearningAppPrototype.Scripts.Engine;
using GameDesignLearningAppPrototype.Scripts.Engine.Rendering.Managers;
using GameDesignLearningAppPrototype.Scripts.Engine.Rendering.Managers.RenderLayerManagers;
using GameDesignLearningAppPrototype.Scripts.Platformer.Components;
using GameDesignLearningAppPrototype.Scripts.Platformer.GameObjects;
using GameDesignLearningAppPrototype.Scripts.Platformer.Managers;
using GameDesignLearningAppPrototype.Scripts.Platformer.Tiles;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Linq;

namespace GameDesignLearningAppPrototype.Scripts.Menu.Cursors
{
    public class Cursor : GameObjectBase
    {
        private Quad quad;
        private TextureComponent texture;
        private bool holdingTile;
        public TileType tileType;

        private bool mouseDown = false;

        //make it so width height are always 90 and scale is always 1
        private readonly ClassManager classManager;
        private readonly TileManager tileManager;
        public Cursor(ClassManager classManager) : base()
        {
            this.classManager = classManager;
            this.tileManager = classManager.TileManager;
            quad = AddComponent<Quad>();
            texture = AddComponent<TextureComponent>();

            quad.Width = 80.0f;
            quad.Height = 80.0f;
        }

        public void Move(float x, float y)
        {
            transform.X = x;
            transform.Y = y;
        }

        public override float[] AssembleVertexData()
        {
            int tileX = 1;
            float tileY = 7.0f;
            texture.TextureID = tileType.GetTileID()-1;
            float[] verticies;
            if (tileType.Equals(TileType.AIR))
            {
                verticies = new float[] {
            //Positions         //UV /Textureslot (should all be the same)
             transform.X + (quad.Width*transform.ScaleX),  transform.Y + (quad.Height*transform.ScaleY), 0.0f, 0.029412f*tileX, 0.041667f*tileY, 0.0f, //top right
             transform.X + (quad.Width*transform.ScaleX),  transform.Y, 0.0f, 0.029412f*tileX, 0.041667f*tileY-0.041667f, 0.0f,                          //bottom right
             transform.X,  transform.Y, 0.0f, 0.029412f*tileX-0.029412f, 0.041667f*tileY-0.041667f, 0.0f,                                                    //bottom left
             transform.X,  transform.Y + (quad.Height*transform.ScaleY), 0.0f, 0.029412f*tileX-0.029412f, 0.041667f*tileY, 0.0f                           //top left
            };
            } else
            {
                verticies = new float[] {
            //Positions         //UV /Textureslot (should all be the same)
             transform.X + (quad.Width*transform.ScaleX),  transform.Y + (quad.Height*transform.ScaleY), 0, 0.05f*texture.TileX,       0.1111f*texture.TileY, 1.0f, //top right
             transform.X + (quad.Width*transform.ScaleX),  transform.Y,                                  0, 0.05f*texture.TileX,       0.1111f*texture.TileY-0.111f, 1.0f,                          //bottom right
             transform.X,                                  transform.Y,                                  0, 0.05f*texture.TileX-0.049f, 0.1111f*texture.TileY-0.111f, 1.0f,                                                    //bottom left
             transform.X,                                  transform.Y + (quad.Height*transform.ScaleY), 0, 0.05f*texture.TileX-0.049f, 0.1111f*texture.TileY, 1.0f                            //top left
            }; ;
            }
            //Console.WriteLine(string.Join(" , ", verticies));
            return verticies;
        }
        public override void Update(GameWindow gameWindow, GameTime gameTime)
        {
            if (gameWindow.IsMouseButtonDown(MouseButton.Left))
            {
                mouseDown = true;
                transform.ScaleX = 0.8f;
                transform.ScaleY = 0.8f;
                float tileX = (gameWindow.MousePosition.X + classManager.CameraManager.GetCameraLocation().X) / 90;
                float tileY = (float)(((classManager.CameraManager.GetCameraLocation().Y + 300) + (-gameWindow.MousePosition.Y + 720 - (3.5 * 90))) / 90);
                if(!tileManager.GetTileType((int)tileX, (int)tileY).Equals(tileType)) {
                    tileManager.TrySetTile((int)tileX, (int)tileY, tileType);
                    tileManager.UpdateSurroundingState((int)tileX, (int)tileY);
                }
            } else if (gameWindow.IsMouseButtonDown(MouseButton.Right))
            {
                transform.ScaleX = 0.8f;
                transform.ScaleY = 0.8f;
                float tileX = (gameWindow.MousePosition.X + classManager.CameraManager.GetCameraLocation().X) / 90;
                float tileY = (float)(((classManager.CameraManager.GetCameraLocation().Y + 300) + (-gameWindow.MousePosition.Y + 720 - (3.5 * 90))) / 90);
                tileManager.TrySetTile((int)tileX, (int)tileY, TileType.AIR);
                tileManager.UpdateSurroundingState((int)tileX, (int)tileY);
            }
            else if (gameWindow.IsAnyMouseButtonDown) //THIS WILL NOT WORK IF WINDOW RESIZED DUE TO MAGIC NUMBERS
            {
                mouseDown = true;
            }
            else
            {
                mouseDown = false;
                transform.ScaleX = 1.0f;
                transform.ScaleY = 1.0f;
            }

            Vector2 mousePosition = gameWindow.MousePosition;
            transform.X = mousePosition.X - ((5 * transform.ScaleX) * 6);

            int[] viewport = new int[4];
            GL.GetInteger(GetPName.Viewport, viewport); // Get the viewport dimensions

            transform.Y = (viewport[3] - mousePosition.Y) - ((5 * transform.ScaleY) * 13);
        }
    }
}
