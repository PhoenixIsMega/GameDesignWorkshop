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
    class Waterfall : Tile
    {
        private TextureAnimator animator;
        private TileManager tileManager;

        private enum WaterfallState
        {
            TOP,
            MIDDLE,
            BOTTOM
        }
        private WaterfallState state = WaterfallState.BOTTOM;

        public Waterfall(TileManager tileManager, int x, int y) : base(tileManager, x, y, TileType.WATERFALL)
        {
            this.tileManager = tileManager;
            animator = AddComponent<TextureAnimator>(GetComponent<TextureComponent>(), new int[2] { 115, 116 }, new TimeSpan(0, 0, 0, 0, 0, 125));
            //if (this.GetComponent<TextureComponent>() == null)
            //{
            //    Console.WriteLine("Error : No texture component found");
            //}
            //work out new way to do this below
            //animator.AssignVariables(this.GetComponent<TextureComponent>(), new int[2] { 115, 116 }, new TimeSpan(0, 0, 0, 0, 0, 125));//figure out better method?
        }

        public override void Update(GameWindow gameWindow, GameTime gameTime)
        {
            base.Update(gameWindow, gameTime);
            animator.Update(gameTime.ElapsedGameTime);
        }

        private void SetState(WaterfallState state)
        {
            this.state = state;
            switch (state)
            {
                case WaterfallState.TOP:
                    animator.SetFrameIDs(new int[2] { 155, 156 });
                    animator.Unpause();
                    break;
                case WaterfallState.MIDDLE:
                    animator.SetFrameIDs(new int[2] { 135, 136});
                    animator.Unpause();
                    break;
                case WaterfallState.BOTTOM:
                    animator.SetFrameIDs(new int[2] { 115, 116 });
                    animator.Unpause();
                    break;
            }
        }

        public override void UpdateState(bool updateSurrounding)
        {
            if (tileManager.GetTile(X, Y + 1) is Waterfall && tileManager.GetTile(X, Y - 1) is Waterfall)
            {
                SetState(WaterfallState.MIDDLE);
            }
            else if (tileManager.GetTile(X, Y - 1) is Waterfall)
            {
                SetState(WaterfallState.TOP);
            }
            else
            {
                SetState(WaterfallState.BOTTOM);
            }
            if (updateSurrounding)
            {
                tileManager.UpdateSurroundingState(this);
            }
        }
    }
}