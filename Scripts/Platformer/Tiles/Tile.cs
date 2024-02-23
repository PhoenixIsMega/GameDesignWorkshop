using GameDesignLearningAppPrototype.Scripts.Engine;
using GameDesignLearningAppPrototype.Scripts.Engine.Utility;
using GameDesignLearningAppPrototype.Scripts.Platformer.Components;
using GameDesignLearningAppPrototype.Scripts.Platformer.GameObjects;
using GameDesignLearningAppPrototype.Scripts.Platformer.Managers;
using OpenTK.Windowing.Desktop;
using System;
using System.Transactions;

namespace GameDesignLearningAppPrototype.Scripts.Platformer.Tiles
{
    public class Tile : GameObjectBase
    {
        protected Quad quad;
        public TextureComponent texture;
        public BoxCollider boxCollider;
        private readonly TileType tileType;
        private readonly int x, y;
        private readonly TileManager tileManager;
        public static readonly float TILE_SIZE = 90.0f;
        public int X
        {
            get { return x; }
        }

        public int Y
        {
            get { return y; }
        }

        public TileType TileType
        {
            get { return tileType; }
        }

        //make it so width height are always 90 and scale is always 1

        protected Tile() : base()
        {
            quad = AddComponent<Quad>();
            texture = AddComponent<TextureComponent>();
            boxCollider = AddComponent<BoxCollider>(transform, quad, null);

            quad.Width = TILE_SIZE;
            quad.Height = TILE_SIZE;

            texture.TextureID = 26 - 1;

            boxCollider.staticObject = true;
        }

        private Tile(TileManager tileManager, TileType tileType) : this() //remove need for texture id and just get from tiletype
        {
            texture.TextureID = tileType.GetTileID() - 1;
            this.tileManager = tileManager;
            this.tileType = tileType;
            //TileExtensions.GetTileID(tileType)
        }

        public Tile(TileManager tileManager, int x, int y, TileType tileType) : this(tileManager, tileType)
        {
            transform.X = x * 90;
            transform.Y = y * 90;
            this.x = x;
            this.y = y;
        }

        float[] verticies;

        public override float[] AssembleVertexData()
        {
            //float x = transform.X;
            //float y = transform.Y;
            float x = this.getWorldPosition().x;
            float y = this.getWorldPosition().y;
            float width = quad.Width * transform.ScaleX;
            float height = quad.Height * transform.ScaleY;
            float tileX = 0.05f * texture.TileX;
            float tileY = 0.1111f * texture.TileY;
            float tileYMinus = 0.111f;

            verticies = new float[] {
                x + width, y + height, 0, tileX, tileY, 0.0f, //top right
                x + width, y, 0, tileX, tileY - tileYMinus, 0.0f, //bottom right
                x, y, 0, tileX - 0.049f, tileY - tileYMinus, 0.0f, //bottom left
                x, y + height, 0, tileX - 0.049f, tileY, 0.0f //top left
            };

            return verticies;
        }

        public virtual void UpdateState(bool updateSurrounding) { }

        public override void Update(GameWindow gameWindow, GameTime gameTime)
        {
        }
    }
}