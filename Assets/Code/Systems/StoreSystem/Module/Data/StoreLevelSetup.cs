using System.Linq;
using Game.Components.ItemsComponent.Data;
using UnityEngine;

namespace Game.Store.Data
{
    /// <summary>
    /// This Scriptable Object is responsible for change products on store depending its level.
    /// The store's level depends from the player advance.
    /// </summary>
    [CreateAssetMenu(fileName = "StoreLevelSetup", menuName = "PikPok/StoreLevel")]
    public class StoreLevelSetup : ScriptableObject
    {
        [SerializeField]
        private StoreLevelData[] storeLevels;

        #region Public Methods

        /// <summary>
        /// Get the configuration for the store
        /// </summary>
        public StoreLevelData GetStore(int aLevel)
        {
            int matchLevelId = GetTheClosestLevelId(aLevel);
            return storeLevels[matchLevelId];
        }

        #endregion

        #region Private Methods
        
        /// <summary>
        /// Get the closest (<=) level id
        /// </summary>
        private int GetTheClosestLevelId(int aTargetLevel)
        {
            var filteredNumbers = storeLevels.Where(x => x.Level <= aTargetLevel).ToArray();
            return filteredNumbers.Length - 1;
        }

        #endregion
    }

    [System.Serializable]
    public struct StoreLevelData
    {
        [SerializeField]
        private string name;
        [SerializeField]
        private int level;
        [SerializeField]
        private ItemSetup[] items;

        public int Level => level;
        public ItemSetup[] Items => items;
    }
}