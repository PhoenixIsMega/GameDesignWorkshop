using GameDesignLearningAppPrototype.Scripts.Engine;
using GameDesignLearningAppPrototype.Scripts.Platformer.Components;
using OpenTK.Windowing.Desktop;
using System.Collections.Generic;

namespace GameDesignLearningAppPrototype.Scripts.Platformer.GameObjects
{
    public abstract class GameObjectBase
    {
        public Transform transform;
        protected List<ComponentBase> components;
        protected List<GameObjectBase> children;

        public GameObjectBase()
        {
            components = new List<ComponentBase>();
            transform = AddComponent<Transform>();
        }

        public T AddComponent<T>() where T : ComponentBase, new()
        {
            //check for duplicate components


            T component = new T();
            components.Add(component);
            return component;
        }

        public T GetComponent<T>() where T : ComponentBase //assumes dupes arent added
        {
            foreach (var component in components)
            {
                if (component is T desiredComponent)
                    return desiredComponent;
            }
            return default;
        }

        public abstract void Update(GameWindow gameWindow, GameTime gameTime);

        public abstract float[] AssembleVertexData();
    }
}
