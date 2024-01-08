using GameDesignLearningAppPrototype.Scripts.Engine;
using GameDesignLearningAppPrototype.Scripts.Engine.Rendering.DataTypes;
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
    class Coin : Tile
    {

        //could add random offset to make look cooler??
        private TextureAnimator animator;
        public Coin(TileManager tileManager, int x, int y) : base(tileManager, x, y, TileType.COIN) {
            animator = AddComponent<TextureAnimator>();
            if (this.GetComponent<TextureComponent>() == null)
            {
                Console.WriteLine("Error : No texture component found");
            }
            //work out new way to do this below
            animator.AssignVariables(this.GetComponent<TextureComponent>(), new int[2] { 32, 33 }, new TimeSpan(0, 0, 0, 0, 0, 250));//figure out better method?
        }

        public override void Update(GameWindow gameWindow, GameTime gameTime)
        {
            base.Update(gameWindow, gameTime);
            animator.Update(gameTime.ElapsedGameTime);
        }
    }
}
