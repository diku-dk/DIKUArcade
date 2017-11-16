using DikuArcade.Math;

namespace DikuArcade.Entities
{
    public class Entity
    {
        /// <summary>
        /// Basic Entity properties
        /// </summary>
        public Vec2f Position;
        public Vec2f Direction; // TODO: move this elsewhere
        public Vec2f Extent;
        
        /// <summary>
        /// Angle measured in radians.
        /// TODO: could be called Orientation?
        /// </summary>
        public float Angle;

        /// <summary>
        /// Used for GameObjectContainer.
        /// TODO: Is this good software design principle?
        /// </summary>
        private bool _markedForDeletion = false;

        public void DeleteEntity()
        {
            _markedForDeletion = true;
        }

        public bool IsDeleted()
        {
            return _markedForDeletion;
        }
        
        public void Scale(Vec2f scalar)
        {
            // This is doing pairwise vector multiplication!
            Extent *= scalar;
        }

        public void Move(Vec2f scalar)
        {
            Position += scalar;
        }

        public void Rotate(float angleRadians)
        {
            Angle += angleRadians;
        }
    }
}