using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Utilities
{
    /// <summary>
    /// This class is responsible for Object Pool pattern behavior. 
    /// This class must be used with ObjectPoolInitializer and to initialize it at awake of every scene to init correctly the Object Pool system
    /// </summary>
    public class ObjectPool
    {
        private const string POOL_NAME_SUFFIX = "_Pool";

        // Parent for all Pools
        private Transform mainContentPools;

        // Data
        private Dictionary<string, List<MonoBehaviour>> objectsCreated = new Dictionary<string, List<MonoBehaviour>>();
        private Dictionary<string, Transform> poolsParents = new Dictionary<string, Transform>();

        // Temp string to save pool names
        private string tempPoolName;

        // Singleton
        private static ObjectPool instance;
        public static ObjectPool Instance => instance ?? (instance = new ObjectPool());

        #region Public Methods

        public void Initialize(Transform mainContentPools)
        {
            this.mainContentPools = mainContentPools;
            objectsCreated.Clear();
        }

        /// <summary>
        /// Create am object with specific position and rotation
        /// </summary>
        public T CreateObject<T>(T typeObject, Vector3 position, Quaternion rotation) where T : MonoBehaviour
        {
            if (objectsCreated.TryGetValue(typeObject.name, out var elements))
            {
                Object objectPooleable = elements.FirstOrDefault(IsAvaliable);

                if (objectPooleable == null)
                {
                    return InstantiateObject(typeObject, elements, position, rotation);
                }

                var _object = objectPooleable as MonoBehaviour;

                _object.transform.SetParent(null);
                _object.transform.position = position;
                _object.gameObject.SetActive(true);

                return (T)objectPooleable;
            }
            
            CreatePool(typeObject);
            return InstantiateObject(typeObject, objectsCreated[typeObject.name], position, rotation);
        }

        /// <summary>
        /// Create an object with specific parent
        /// </summary>
        public T CreateObject<T>(T typeObject, Transform transform) where T : MonoBehaviour
        {
            if (objectsCreated.TryGetValue(typeObject.name, out var elements))
            {
                Object objectPooleable = elements.FirstOrDefault(IsAvaliable);

                if (objectPooleable == null)
                {
                    return InstantiateObject(typeObject, elements, transform);
                }

                var _object = objectPooleable as MonoBehaviour;

                _object.transform.SetParent(transform);
                _object.transform.localPosition = Vector3.zero;
                _object.gameObject.SetActive(true);

                return (T)objectPooleable;
            }
            else
            {
                CreatePool(typeObject);
                return InstantiateObject(typeObject, objectsCreated[typeObject.name], transform);
            }
        }

        /// <summary>
        /// Recycle an object
        /// </summary>
        public void Recycle(GameObject typeObject)
        {
            tempPoolName = typeObject.name.Replace("(Clone)", "") + POOL_NAME_SUFFIX;

            typeObject.gameObject.SetActive(false);
            typeObject.transform.SetParent(GetPoolParent(tempPoolName));
        }

        /// <summary>
        /// Destroy an object
        /// </summary>
        public void DestroyObject(MonoBehaviour typeObject)
        {
            tempPoolName = typeObject.name.Replace("(Clone)", "");
            if (objectsCreated.TryGetValue(tempPoolName, out List<MonoBehaviour> elements))
            {
                elements.Remove(typeObject);
                Object.Destroy(typeObject);
            }
        }

        #endregion

        #region Private Methods

        private void CreatePool(Object typeObject)
        {
            objectsCreated.Add(typeObject.name, new List<MonoBehaviour>());
            tempPoolName = typeObject.name + POOL_NAME_SUFFIX;

            if (!poolsParents.TryGetValue(tempPoolName, out var pool))
            {
                Transform parent = new GameObject(tempPoolName).transform;
                parent.SetParent(mainContentPools);
                poolsParents.Add(tempPoolName, parent);
            }
        }

        /// <summary>
        /// Instantiate an object with specific positiona and rotation
        /// </summary>
        private T InstantiateObject<T>(T typeObject, List<MonoBehaviour> objectPool, Vector3 pos, Quaternion rotation) where T : MonoBehaviour
        {
            var objectCreated = Object.Instantiate(typeObject, pos, rotation);

            objectPool.Add(objectCreated);
            return objectCreated;
        }

        /// <summary>
        /// Instantiate an object with specific parent
        /// </summary>
        private T InstantiateObject<T>(T typeObject, List<MonoBehaviour> objectPool, Transform parent) where T : MonoBehaviour
        {
            var objectCreated = Object.Instantiate(typeObject, parent);

            objectPool.Add(objectCreated);
            return objectCreated;
        }

        /// <summary>
        /// Check if an object from pool is avaliable
        /// </summary>
        private bool IsAvaliable(MonoBehaviour obj)
        {
            if (obj == null)
                return false;

            return !obj.gameObject.activeSelf;
        }

        /// <summary>
        /// Get pool parent
        /// </summary>
        private Transform GetPoolParent(string poolName)
        {
            if (poolsParents.TryGetValue(poolName, out var pool))
                return pool;
            else
                return new GameObject(poolName).transform;
        }

        #endregion
    }
}