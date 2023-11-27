using Game.Components.ItemsComponent.Data;
using UnityEngine;

namespace Game.Store.Data
{
    [CreateAssetMenu(fileName = "StoreItemLibrary", menuName = "PikPok/Items/StoreItemLibrary")]
    public class StoreItemLibrary : ScriptableObject
    {
        [SerializeField]
        private ItemSetup[] storeItems;
        
        public ItemSetup[] StoreItems => storeItems;

        public void InitializeItems()
        {
            for (int i = 0; i < storeItems.Length; i++)
            {
                storeItems[i].Id = i; 
            }
        }
    }
}