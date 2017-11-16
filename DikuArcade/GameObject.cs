using System.Runtime.CompilerServices;
using DikuArcade.Math;

namespace DikuArcade
{
    public class GameObject : IGameObject // TODO: is this necessary/overcomplicated?
    {
        /// <summary>
        /// TODO: Should we use float values here instead?
        /// </summary>
        public Vec2i Position;
        public Vec2i Direction;
        public Vec2i Extent;
        
        /// <summary>
        /// Angle measured in radians
        /// </summary>
        public float Angle;

        /// <summary>
        /// Used for GameObjectContainer.
        /// TODO: Is this good software design principle?
        /// </summary>
        public bool MarkedForDeletion = false;
        
        // TODO: Should width and height be given a texture (image) level instead?
        public GameObject(int posX, int posY, int width, int height)
        {
            Position = new Vec2i(posX, posY);
            Direction = new Vec2i(0,0);
            Extent = new Vec2i(0,0);
        }

        public GameObject(Vec2i pos, Vec2i extent)
        {
            Position = pos;
            Extent = extent;
            Direction = new Vec2i();
        }
        
        public static explicit operator StaticGameObject(GameObject obj)
        {
            return new StaticGameObject(obj.Position, obj.Extent);
        }

        public void Scale(Vec2f scalar)
        {
            // TODO: It would seem using float vectors is a better option!?
            // This is doing pairwise vector multiplication!
            Extent *= scalar;
        }
    }
}