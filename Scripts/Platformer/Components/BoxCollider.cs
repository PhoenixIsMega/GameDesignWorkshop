using GameDesignLearningAppPrototype.Scripts.Platformer.GameObjects;
using System;

namespace GameDesignLearningAppPrototype.Scripts.Platformer.Components
{
    public class BoxCollider : ComponentBase
    {
        private float scaleX = 1.0f;
        private float scaleY = 1.0f;
        public bool staticObject = false; //if static object, it will not move, move objects along axis with least overlap
        //public string collisionLayer = "default";
        //public bool ignoreSameCollisionLayer = true;
        //public bool solid = false;
        Transform transform = null;
        Quad quad = null;
        PhysicsComponent physicsComponent = null;

        public BoxCollider() : base()
        {
        }

        public BoxCollider(Transform transform, Quad quad, PhysicsComponent physicsComponent) : this()
        {
            this.quad = quad;
            this.transform = transform;
            this.physicsComponent = physicsComponent;
        }

        public BoxCollider(Transform transform, Quad quad, PhysicsComponent physicsComponent, float scaleX, float scaleY) : this(transform, quad, physicsComponent)
        {
            this.scaleX = scaleX;
            this.scaleY = scaleY;
        }
        public (float minX, float maxX, float minY, float maxY) getCorners()
        {
            float minX = transform.X;
            float minY = transform.Y;
            float maxY = transform.Y + (quad.Height * transform.ScaleY);
            float maxX = transform.X + (quad.Width * transform.ScaleX);
            return (minX, maxX, minY, maxY);
        }

        public bool detectCollisionYAxis(GameObjectBase otherObject)
        {
            if (!enabled) return false;
            BoxCollider otherBoxCollider = otherObject.GetComponent<BoxCollider>();
            if (otherBoxCollider == null) return false;
            if (!otherBoxCollider.enabled) return false;
            var corners = getCorners();
            float minY = corners.minY;
            float maxY = corners.maxY;
            var otherCorners = otherBoxCollider.getCorners();
            float otherMinY = otherCorners.minY;
            float otherMaxY = otherCorners.maxY;
            if (minY > otherMaxY) return false;
            if (maxY < otherMinY) return false;
            return true;
        }

        public bool detectCollisionXAxis(GameObjectBase otherObject)
        {
            if (!enabled) return false;
            BoxCollider otherBoxCollider = otherObject.GetComponent<BoxCollider>();
            if (otherBoxCollider == null) return false;
            if (!otherBoxCollider.enabled) return false;
            var corners = getCorners();
            float minX = corners.minX;
            float maxX = corners.maxX;
            var otherCorners = otherBoxCollider.getCorners();
            float otherMinX = otherCorners.minX;
            float otherMaxX = otherCorners.maxX;
            if (minX > otherMaxX) return false;
            if (maxX < otherMinX) return false;
            return true;
        }

        public bool detectCollision(GameObjectBase otherObject)
        {
            if (!enabled) return false;
            if (detectCollisionXAxis(otherObject) && detectCollisionYAxis(otherObject)) return true;
            return false;
        }

        public float getCollisionAmountX(GameObjectBase otherObject) //can also be used as proximity thingy, if value output is negatice
        {
            if (!enabled) return 0.0f;
            BoxCollider otherBoxCollider = otherObject.GetComponent<BoxCollider>();
            if (otherBoxCollider == null) return 0.0f;
            if (!otherBoxCollider.enabled) return 0.0f;
            var corners = getCorners();
            float minX = corners.minX;
            float maxX = corners.maxX;
            var otherCorners = otherBoxCollider.getCorners();
            float otherMinX = otherCorners.minX;
            float otherMaxX = otherCorners.maxX;
            float overlap = MathF.Min(maxX, otherMaxX) - MathF.Max(minX, otherMinX);
            return overlap;
        }

        public float getCollisionAmountY(GameObjectBase otherObject) //can also be used as proximity thingy, if value output is negative
        {
            if (!enabled) return 0.0f;
            BoxCollider otherBoxCollider = otherObject.GetComponent<BoxCollider>();
            if (otherBoxCollider == null) return 0.0f;
            if (!otherBoxCollider.enabled) return 0.0f;
            var corners = getCorners();
            float minY = corners.minY;
            float maxY = corners.maxY;
            var otherCorners = otherBoxCollider.getCorners();
            float otherMinY = otherCorners.minY;
            float otherMaxY = otherCorners.maxY;
            float overlap = MathF.Min(maxY, otherMaxY) - MathF.Max(minY, otherMinY);
            return overlap;
        }

        public void resolveCollision(GameObjectBase otherObject)
        {
            if(!enabled) return;
            BoxCollider otherBoxCollider = otherObject.GetComponent<BoxCollider>();
            if (otherBoxCollider == null) return;
            if (!otherBoxCollider.enabled) return;
            float overlapX = getCollisionAmountX(otherObject);
            float overlapY = getCollisionAmountY(otherObject);
            if (overlapX < overlapY)
            {
                //resolve on x axis
                if (otherBoxCollider.staticObject && staticObject) return;
                var corners = getCorners();
                float minX = corners.minX;
                float maxX = corners.maxX;
                float centreX = (maxX + minX) / 2;
                var otherCorners = otherBoxCollider.getCorners();
                float otherMinX = otherCorners.minX;
                float otherMaxX = otherCorners.maxX;
                float otherCentreX = (otherMaxX + otherMinX) / 2;
                if (centreX < otherCentreX)
                {
                    if (otherBoxCollider.staticObject)
                    {
                        transform.X -= overlapX;
                    }
                    else if (staticObject)
                    {
                        otherObject.GetComponent<Transform>().X += overlapX;
                    }
                    else
                    { //will be rarely used
                        transform.X -= overlapX / 2;
                        otherObject.GetComponent<Transform>().X += overlapX / 2;
                    }
                }
                else
                {
                    if (otherBoxCollider.staticObject)
                    {
                        transform.X += overlapX;
                    }
                    else if (staticObject)
                    {
                        otherObject.GetComponent<Transform>().X -= overlapX;
                    }
                    else
                    { //will be rarely used
                        transform.X += overlapX / 2;
                        otherObject.GetComponent<Transform>().X -= overlapX / 2;
                    }
                }
            } else
            {
                //resolve on y axis
                if (otherBoxCollider.staticObject && staticObject) return;
                var corners = getCorners();
                float minY = corners.minY;
                float maxY = corners.maxY;
                float centreY = (maxY + minY) / 2;
                var otherCorners = otherBoxCollider.getCorners();
                float otherMinY = otherCorners.minY;
                float otherMaxY = otherCorners.maxY;
                float otherCentreY = (otherMaxY + otherMinY) / 2;
                if (centreY < otherCentreY)
                {
                    if (otherBoxCollider.staticObject)
                    {
                        transform.Y -= overlapY;
                        if (physicsComponent != null)
                        {
                            physicsComponent.VelocityY = 0.0f;
                        }
                    }
                    else if (staticObject)
                    {
                        otherObject.GetComponent<Transform>().Y += overlapY;
                        if (otherObject.GetComponent<PhysicsComponent>() != null)
                        {
                            otherObject.GetComponent<PhysicsComponent>().VelocityY = 0.0f;
                        }
                    }
                    else
                    { //will be rarely used
                        transform.Y -= overlapY / 2;
                        if (physicsComponent != null)
                        {
                            physicsComponent.VelocityY = 0.0f;
                        }
                        otherObject.GetComponent<Transform>().Y += overlapY / 2;
                        if (otherObject.GetComponent<PhysicsComponent>() != null)
                        {
                            otherObject.GetComponent<PhysicsComponent>().VelocityY = 0.0f;
                        }
                    }
                }
                else
                {
                    if (otherBoxCollider.staticObject)
                    {
                        transform.Y += overlapY;
                        if (physicsComponent != null)
                        {
                            physicsComponent.VelocityY = 0.0f;
                        }
                    }
                    else if (staticObject)
                    {
                        otherObject.GetComponent<Transform>().Y -= overlapY;
                        if (otherObject.GetComponent<PhysicsComponent>() != null)
                        {
                            otherObject.GetComponent<PhysicsComponent>().VelocityY = 0.0f;
                        }
                    }
                    else
                    { //will be rarely used
                        transform.Y += overlapY / 2;
                        if (physicsComponent != null)
                        {
                            physicsComponent.VelocityY = 0.0f;
                        }
                        otherObject.GetComponent<Transform>().Y -= overlapY / 2;
                        if (otherObject.GetComponent<PhysicsComponent>() != null)
                        {
                            otherObject.GetComponent<PhysicsComponent>().VelocityY = 0.0f;
                        }
                    }
                }
            }
        }

        public float[] getLines()
        {
            if (!enabled) return null;
            var corners = getCorners();
            float minX = corners.minX;
            float minY = corners.minY;
            float maxY = corners.maxY;
            float maxX = corners.maxX;
            float[] lines = new float[24] 
            {minX, minY, 0f,
            minX, maxY, 0f,
            minX, maxY, 0f,
            maxX, maxY, 0f,
            maxX, maxY, 0f,
            maxX, minY, 0f,
            maxX, minY, 0f,
            minX, minY, 0f};

            return lines;
        }
            //add method to calculate lines for rendering

            //add collision bool method
        }
}
