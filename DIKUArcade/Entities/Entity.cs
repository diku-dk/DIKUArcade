using DIKUArcade.Math;

namespace DIKUArcade.Entities
{
    public class Entity
    {
        /// <summary>
        /// Basic Entity properties
        /// TODO: Could (should) they be made private?
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

        // TODO: Should all manipulation methods (scale,translate,etc) be moved to EntityInfo class?
        public void Scale(Vec2f scalar)
        {
            // This is doing pairwise vector multiplication!
            Extent *= scalar;
        }
        public void ScaleX(float scale)
        {
            Extent.X *= scale;
        }
        public void ScaleY(float scale)
        {
            Extent.Y *= scale;
        }

        public void Move(Vec2f mover)
        {
            Position += mover;
        }
        public void MoveX(float move)
        {
            Position.X += move;
        }
        public void MoveY(float move)
        {
            Position.Y += move;
        }

        public void Rotate(float angleRadians)
        {
            Angle += angleRadians;
        }

        public void MoveToPosition(Vec2f newPosition)
        {
            Position = newPosition;
        }
    }
}