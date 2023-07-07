using System;
using System.Collections.Generic;
using System.Text;

namespace GameDesignWorkshop.Scripts.Game.Objects.Components.For_Later
{
    class SpriteAnimator : Component
    {
        public string CurrentAnimation { get; set; }
        public float AnimationSpeed { get; set; }

        public SpriteAnimator()
        {
            CurrentAnimation = "";
            AnimationSpeed = 1f;
        }

        public void Update()
        {
            // Update animation logic
        }

        public void PlayAnimation(string animationName)
        {
            // Play the specified animation
        }

        public void Start()
        {
            throw new NotImplementedException();
        }

        public void OnDestroy()
        {
            throw new NotImplementedException();
        }
    }
}
