using GameDesignLearningAppPrototype.Scripts.Engine;
using GameDesignLearningAppPrototype.Scripts.Platformer.GameObjects;
using OpenTK.Windowing.Desktop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDesignLearningAppPrototype.Scripts.Platformer
{
    public class EmptyGameObject : GameObjectBase
    {
        public override float[] AssembleVertexData()
        {
            throw new NotImplementedException();
        }

        public override void Update(GameWindow gameWindow, GameTime gameTime)
        {
            
        }
    }
}
