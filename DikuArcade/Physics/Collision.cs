using DikuArcade.Entities;

namespace DikuArcade.Physics
{
    public class Collision
    {
        /// <summary>
        /// If checking collision between two dynamic game objects,
        /// one of them has to be converted (explicit type cast) to
        /// a static object first, such as to minimize confusion in
        /// movement order.
        /// </summary>
        public static bool Aabb(DynamicEntity obj, StaticEntity sta)
        {
            throw new System.NotImplementedException("Aabb (dynamic, static)");
        }
        
        public static bool Aabb(DynamicEntity obj1, DynamicEntity obj2)
        {
            throw new System.NotImplementedException("Aabb (dynamic, dynamic)");
        }
    }
}