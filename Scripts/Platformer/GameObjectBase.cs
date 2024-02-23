using GameDesignLearningAppPrototype.Scripts.Engine;
using GameDesignLearningAppPrototype.Scripts.Platformer.Components;
using OpenTK.Windowing.Desktop;
using System;
using System.Collections.Generic;

namespace GameDesignLearningAppPrototype.Scripts.Platformer.GameObjects
{
    public abstract class GameObjectBase
    {
        public Transform transform;
        protected List<ComponentBase> components;
        protected List<GameObjectBase> children;
        private GameObjectBase parent;

        public GameObjectBase()
        {
            components = new List<ComponentBase>();
            transform = AddComponent<Transform>();
        }

        public T AddComponent<T>(params object[] constructorArgs) where T : ComponentBase, new()
        {
            //check for duplicate components


            //T component = new T();
            T component = (T)Activator.CreateInstance(typeof(T), constructorArgs);
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

        public bool hasParent()
        {
            return (parent != null);
        }

        public void addChild(GameObjectBase child)
        {
            if (children == null)
                children = new List<GameObjectBase>();
            children.Add(child);
            child.parent = this;
        }

        public (float x, float y) getWorldPosition()
        {
            if (!hasParent())
                return (transform.X, transform.Y);
            return (transform.X + parent.getWorldPosition().x, transform.Y + parent.getWorldPosition().y);
        }

        public abstract void Update(GameWindow gameWindow, GameTime gameTime);

        public abstract float[] AssembleVertexData();
    }
}
