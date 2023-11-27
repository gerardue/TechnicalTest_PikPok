using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    /// <summary>
    /// This class is responsible for initializing Object Pool
    /// </summary>
    public class ObjectPoolInitializer : MonoBehaviour
    {
        [SerializeField]
        private Transform poolsParent; 

        private void Start()
        {
            ObjectPool.Instance.Initialize(poolsParent);
        }
    }
}