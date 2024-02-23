using GameDesignLearningAppPrototype.Scripts.Engine;
using GameDesignLearningAppPrototype.Scripts.Platformer.Components;
using GameDesignLearningAppPrototype.Scripts.Platformer.Managers;
using OpenTK.Windowing.Desktop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDesignLearningAppPrototype.Scripts.Platformer.Tiles
{
    public class GrassBlock : Tile
    {
        private TileManager tileManager;

        private enum GrassBlockState
        {
            STANDALONE,
            LEFT,
            MIDDLE,
            RIGHT,
            TOP_LEFT,
            TOP_MIDDLE,
            TOP_RIGHT,
            MIDDLE_LEFT,
            CENTER,
            MIDDLE_RIGHT,
            BOTTOM_LEFT,
            BOTTOM_MIDDLE,
            BOTTOM_RIGHT,
            STANDALONE_WITH_BOTTOM,
            CENTER_WITH_BOTTOM,
            BOTTOM_STANDALONE,
        }
        private GrassBlockState state = GrassBlockState.STANDALONE;

        public GrassBlock(TileManager tileManager, int x, int y) : base(tileManager, x, y, TileType.GRASS)
        {
            this.tileManager = tileManager;
            SetState(GrassBlockState.LEFT);
        }

        public override void Update(GameWindow gameWindow, GameTime gameTime)
        {
            base.Update(gameWindow, gameTime);
        }

        private void SetState(GrassBlockState state)
        {
            this.state = state;
            switch (state)
            {
                case GrassBlockState.STANDALONE:
                    GetComponent<TextureComponent>().TextureID = 160; 
                    break;
                case GrassBlockState.LEFT:
                    GetComponent<TextureComponent>().TextureID = 161; 
                    break;
                case GrassBlockState.MIDDLE:
                    GetComponent<TextureComponent>().TextureID = 162;
                    break;
                case GrassBlockState.RIGHT:
                    GetComponent<TextureComponent>().TextureID = 163;
                    break;
                case GrassBlockState.TOP_LEFT:
                    GetComponent<TextureComponent>().TextureID = 141;
                    break;
                case GrassBlockState.TOP_MIDDLE:
                    GetComponent<TextureComponent>().TextureID = 142;
                    break;
                case GrassBlockState.TOP_RIGHT:
                    GetComponent<TextureComponent>().TextureID = 143;
                    break;
                case GrassBlockState.MIDDLE_LEFT:
                    GetComponent<TextureComponent>().TextureID = 41;
                    break;
                case GrassBlockState.CENTER:
                    GetComponent<TextureComponent>().TextureID = 42;
                    break;
                case GrassBlockState.MIDDLE_RIGHT:
                    GetComponent<TextureComponent>().TextureID = 43;
                    break;
                case GrassBlockState.BOTTOM_LEFT:
                    GetComponent<TextureComponent>().TextureID = 21;
                    break;
                case GrassBlockState.BOTTOM_MIDDLE:
                    GetComponent<TextureComponent>().TextureID = 22;
                    break;
                case GrassBlockState.BOTTOM_RIGHT:
                    GetComponent<TextureComponent>().TextureID = 23;
                    break;
                case GrassBlockState.STANDALONE_WITH_BOTTOM:
                    GetComponent<TextureComponent>().TextureID = 140;
                    break;
                case GrassBlockState.CENTER_WITH_BOTTOM:
                    GetComponent<TextureComponent>().TextureID = 40;
                    break;
                case GrassBlockState.BOTTOM_STANDALONE:
                    GetComponent<TextureComponent>().TextureID = 20;
                    break;
            }
        }

        public override void UpdateState(bool updateSurrounding)
        {
            if (tileManager.GetTile(X, Y - 1) is GrassBlock)
            {
                if (tileManager.GetTile(X, Y + 1) is GrassBlock)
                {
                    //middle dirt
                    if (tileManager.GetTile(X - 1, Y) is GrassBlock && tileManager.GetTile(X + 1, Y) is GrassBlock)
                    {
                        SetState(GrassBlockState.CENTER);
                    }
                    else if (tileManager.GetTile(X + 1, Y) is GrassBlock)
                    {
                        SetState(GrassBlockState.MIDDLE_LEFT);
                    }
                    else if (tileManager.GetTile(X - 1, Y) is GrassBlock)
                    {
                        SetState(GrassBlockState.MIDDLE_RIGHT);
                    }
                    else
                    {
                        SetState(GrassBlockState.CENTER_WITH_BOTTOM);
                    }
                }
                else
                {
                    //grass with bottom
                    if (tileManager.GetTile(X - 1, Y) is GrassBlock && tileManager.GetTile(X + 1, Y) is GrassBlock)
                    {
                        SetState(GrassBlockState.TOP_MIDDLE);
                    }
                    else if (tileManager.GetTile(X + 1, Y) is GrassBlock)
                    {
                        SetState(GrassBlockState.TOP_LEFT);
                    }
                    else if (tileManager.GetTile(X - 1, Y) is GrassBlock)
                    {
                        SetState(GrassBlockState.TOP_RIGHT);
                    }
                    else
                    {
                        SetState(GrassBlockState.STANDALONE_WITH_BOTTOM);
                    }
                }
            }
            else
            {
                //bottom block
                if (tileManager.GetTile(X, Y + 1) is GrassBlock)
                {
                    //bottom dirt
                    if (tileManager.GetTile(X - 1, Y) is GrassBlock && tileManager.GetTile(X + 1, Y) is GrassBlock)
                    {
                        SetState(GrassBlockState.BOTTOM_MIDDLE);
                    }
                    else if (tileManager.GetTile(X + 1, Y) is GrassBlock)
                    {
                        SetState(GrassBlockState.BOTTOM_LEFT);
                    }
                    else if (tileManager.GetTile(X - 1, Y) is GrassBlock)
                    {
                        SetState(GrassBlockState.BOTTOM_RIGHT);
                    }
                    else
                    {
                        SetState(GrassBlockState.BOTTOM_STANDALONE);
                    }
                }
                else
                {
                    //standalones
                    if (tileManager.GetTile(X - 1, Y) is GrassBlock && tileManager.GetTile(X + 1, Y) is GrassBlock)
                    {
                        SetState(GrassBlockState.MIDDLE);
                    }
                    else if (tileManager.GetTile(X + 1, Y) is GrassBlock)
                    {
                        SetState(GrassBlockState.LEFT);
                    }
                    else if (tileManager.GetTile(X - 1, Y) is GrassBlock)
                    {
                        SetState(GrassBlockState.RIGHT);
                    }
                    else
                    {
                        SetState(GrassBlockState.STANDALONE);
                    }
                }
            }
            if (updateSurrounding)
            {
                tileManager.UpdateSurroundingState(this);
            }
        }
    }
}
