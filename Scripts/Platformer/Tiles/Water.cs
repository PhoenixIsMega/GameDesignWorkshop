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
    class Water : Tile
    {
        private TextureAnimator animator;
        private TileManager tileManager;

        private enum WaterState
        {
            SURFACE,
            UNDER
        }
        private WaterState state = WaterState.SURFACE;

        public Water(TileManager tileManager, int x, int y) : base(tileManager, x, y, TileType.WATER)
        {
            this.tileManager = tileManager;
            animator = AddComponent<TextureAnimator>(this.GetComponent<TextureComponent>(), new int[2] { 134, 154 }, new TimeSpan(0, 0, 0, 0, 0, 250));
            /*if (this.GetComponent<TextureComponent>() == null)
            {
                Console.WriteLine("Error : No texture component found");
            }
            //work out new way to do this below
            animator.AssignVariables(this.GetComponent<TextureComponent>(), new int[2] { 134, 154 }, new TimeSpan(0, 0, 0, 0, 0, 250));//figure out better method?
            */
        }

        public override void Update(GameWindow gameWindow, GameTime gameTime)
        {
            base.Update(gameWindow, gameTime);
            animator.Update(gameTime.ElapsedGameTime);
        }

        private void SetState(WaterState state)
        {
            this.state = state;
            switch (state)
            {
                case WaterState.UNDER:
                    animator.SetFrameIDs(new int[1] { 114 });
                    animator.Pause();
                    break;
                case WaterState.SURFACE:
                    animator.SetFrameIDs(new int[2] { 134, 154});
                    animator.Unpause();
                    break;
            }
        }

        public override void UpdateState(bool updateSurrounding)
        {
            if (tileManager.GetTile(X, Y + 1) is Water)
            {
                SetState(WaterState.UNDER);
            }
            else
            {
                SetState(WaterState.SURFACE);
            }
            if (updateSurrounding)
            {

                /*if (tileManager.GetTile(X, Y - 1) == null)
                {
                    return;
                }
                tileManager.GetTile(X, Y - 1).UpdateState(false);*/
                //UpdateSurrounding();
                tileManager.UpdateSurroundingState(this);
            }
        }
    }
}
