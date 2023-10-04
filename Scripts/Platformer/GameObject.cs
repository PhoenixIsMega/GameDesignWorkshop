using GameDesignLearningAppPrototype.Scripts.Engine;
using GameDesignLearningAppPrototype.Scripts.Platformer.Components;
using OpenTK.Windowing.Desktop;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameDesignLearningAppPrototype.Scripts.Platformer
{
    public abstract class GameObject
    {
        public Transform transform;
        protected List<Component> components;
        protected List<GameObject> children;

        public GameObject()
        {
            components = new List<Component>();
            transform = AddComponent<Transform>();
        }

        public T AddComponent<T>() where T : Component, new()
        {
            //add check for dupes
            T component = new T();
            components.Add(component);
            return component;
        }

        public T GetComponent<T>() where T : Component //assumes dupes arent added
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
