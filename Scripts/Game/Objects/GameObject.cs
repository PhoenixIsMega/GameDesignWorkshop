using GameDesignWorkshop.Scripts.Game.Objects.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameDesignWorkshop.Scripts.Game.Objects
{
    class GameObject
    {
        //needs capacity to carry componenets
        //always has transform component
        private Transform transform;
        private List<Component> components;

        public GameObject()
        {
            transform = AddComponent<Transform>();
            components = new List<Component>();
        }

        public T AddComponent<T>() where T : Component, new()
        {
            T component = new T();
            components.Add(component);
            return component;
        }

        public T GetComponent<T>() where T : Component
        {
            foreach (var component in components)
            {
                if (component is T desiredComponent)
                    return desiredComponent;
            }
            return null;
        }

        public void Update()
        {
            foreach (var component in components)
            {
                component.Update();
            }
        }
    }
}
