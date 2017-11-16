using System.Collections.Generic;

namespace DIKUArcade
{
    public class GameObjectContainer
    {
        // TODO: Consider using IEnumerable interface.. (better data structure?)
        private List<GameObject> _gameObjects;
        private List<StaticGameObject> _staticGameObjects;
        
        public GameObjectContainer()
        {
            _gameObjects = new List<GameObject>();
            _staticGameObjects = new List<StaticGameObject>();
        }

        public void AddGameObject(GameObject obj)
        {
            
        }

        public void AddStaticGameObject(StaticGameObject obj)
        {
            
        }
        
        public void RemoveGameObject(GameObject obj)
        {
            
        }

        public void RemoveStaticGameObject(StaticGameObject obj)
        {
            
        }

        public void IterateGameObjects(int someDelegateOrFunctionPointer)
        {
            foreach (var obj in _gameObjects)
            {
                someDelegateOrFunctionPointer(obj);
            }
        }
    }
}
