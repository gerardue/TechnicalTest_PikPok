using System;
using System.Linq;
using Game.Components.ItemsComponent.Data;
using Game.Components.StatsComponent.Data;
using Game.Systems.InventorySystem.Data;
using Game.Systems.InventorySystem.View;
using UnityEngine;
using Task = System.Threading.Tasks.Task;

namespace Game.Systems.InventorySystem.Handler
{
    public class InventoryHandler : MonoBehaviour
    {
        [SerializeField]
        private InventorySetup inventorySetup;

        [SerializeField]
        private InventoryView inventoryView;

        private Func<int, ItemSetup> onGetItem;
        private Action<int> onSell;
        private Action<StatData[], int> onEquip; 
        
        #region Public Methods

        public void Initialize(Func<int, ItemSetup> aOnGetItem, Action<int> aOnSell, Action<StatData[], int> aOnEquip)
        {
            onGetItem = aOnGetItem;
            onSell = aOnSell;
            onEquip = aOnEquip; 
        }

        public void OpenInventory()
        { 
            SetupInventory();
            inventoryView.Initialize();
        }

        public void CloseInventory()
        {
            inventoryView.Dispose();
        }
        
        public void AddItemToInventory(int aId)
        {
            inventorySetup.AddItem(aId);
        }
        
        #endregion

        #region Private Methods

        private void SetupInventory()
        {
            inventoryView.Dispose();
            
            for (int i = 0; i < inventorySetup.Items.Count; i++)
            {
                var item = onGetItem(inventorySetup.Items[i]);
                inventoryView.CreateItem(item.Id, item.NameItem, item.Description, item.Price, item.StatData, item.ItemMerged, onSell, onEquip, RemoveItemFromInventory);
            }
        }

        private void RemoveItemFromInventory(int aItemId)
        {
            var item = inventorySetup.Items.Find(x => x == aItemId);
            inventorySetup.Items.Remove(item);
        }

        #endregion
    }
}