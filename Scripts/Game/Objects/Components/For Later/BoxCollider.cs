using System;
using System.Collections.Generic;
using System.Text;

namespace GameDesignWorkshop.Scripts.Game.Objects.Components
{
    class BoxCollider : Component
    {
        public bool IsTrigger { get; set; }
        public bool IsColliding { get; set; }

        public BoxCollider()
        {
            IsTrigger = false;
            IsColliding = false;
        }

        public void Update()
        {
            // Update collision detection logic
        }

        public void OnTriggerEnter2D(BoxCollider other)
        {
            // Called when this collider enters a collision with another collider
        }

        public void OnTriggerExit2D(BoxCollider other)
        {
            // Called when this collider exits a collision with another collider
        }

        public void OnCollisionEnter2D(BoxCollider other)
        {
            // Called when this collider collides with another collider
        }

        public void OnCollisionExit2D(BoxCollider other)
        {
            // Called when this collider stops colliding with another collider
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
