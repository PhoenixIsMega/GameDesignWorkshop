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
    class Flag : Tile
    {
        private TextureAnimator animator;
        private TileManager tileManager;

        private enum FlagState
        {
            POLE,
            FLAG
        }
        private FlagState state = FlagState.FLAG;

        public Flag(TileManager tileManager, int x, int y) : base(tileManager, x, y, TileType.FLAG)
        {
            this.tileManager = tileManager;
            animator = AddComponent<TextureAnimator>();
            if (this.GetComponent<TextureComponent>() == null)
            {
                Console.WriteLine("Error : No texture component found");
            }
            //work out new way to do this below
            animator.AssignVariables(this.GetComponent<TextureComponent>(), new int[2] { 72, 73 }, new TimeSpan(0, 0, 0, 0, 0, 250));//figure out better method?
        }

        public override void Update(GameWindow gameWindow, GameTime gameTime)
        {
            base.Update(gameWindow, gameTime);
            animator.Update(gameTime.ElapsedGameTime);
        }

        private void SetState(FlagState state)
        {
            this.state = state;
            switch (state)
            {
                case FlagState.POLE:
                    animator.SetFrameIDs(new int[1] { 52 });
                    animator.Pause();
                    break;
                case FlagState.FLAG:
                    animator.SetFrameIDs(new int[2] { 72, 73 });
                    animator.Unpause();
                    break;
            }
        }

        public override void UpdateState(bool updateSurrounding)
        {
            if (tileManager.GetTile(X, Y + 1) is Flag)
            {
                SetState(FlagState.POLE);
            }
            else
            {
                SetState(FlagState.FLAG);
            }
            if (updateSurrounding)
            {

                /*if (tileManager.GetTile(X, Y - 1) == null)
                {
                    return;
                }
                tileManager.GetTile(X, Y - 1).UpdateState(false);*/
                tileManager.UpdateSurroundingState(this);
            }
        }
    }
}
