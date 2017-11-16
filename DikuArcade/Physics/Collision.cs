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
        public static bool Aabb(GameObject obj, StaticGameObject sta)
        {
            throw new System.NotImplementedException();
        }
    }
}