using System;
using System.Collections.Generic;
using System.Data.Common;
using Game.Components.ItemsComponent.Data;
using Game.Components.ItemsComponent.View;
using Game.Components.StatsComponent.Data;
using TMPro;
using UnityEngine;
using Utilities;

namespace Game.Systems.InventorySystem.View
{
    public class InventoryView : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField]
        private GameObject inventoryUI;
        
        [Header("Item Prefab")]
        [SerializeField]
        private InventoryItemView inventoryItem;

        [Header("Item Description UI")]
        [SerializeField]
        private ItemDescriptionView itemDescription;
        
        [Header("Other components")]
        [SerializeField]
        private Transform parentItems;
        
        private List<InventoryItemView> items = new List<InventoryItemView>();

        private Action<StatData[], int> onEquipItem;
        
        #region Unity Methods

        private void OnDisable()
        {
            // Dispose();
        }

        #endregion
        
        #region Public Methods
        
        public void Initialize()
        {
            inventoryUI.SetActive(true);
        }
        
        public void Dispose()
        {
            foreach (InventoryItemView item in items)
            {
                ObjectPool.Instance.Recycle(item.gameObject);
            }
            items.Clear();
        }
        
        public void CreateItem(int aItemId, string aNameItem, string aDescriptionItem, int price, StatData[] statsData, ItemSetup itemMerged, 
            Action<int> aOnSell, Action<StatData[], int> aOnEquip, Action<int> onRemoveItem)
        {
            onEquipItem = aOnEquip;
            var item = ObjectPool.Instance.CreateObject(inventoryItem, parentItems);
            item.Initialize(aItemId, aNameItem, aDescriptionItem, price, statsData, itemMerged, aOnSell, EquipItem, itemDescription.OpenItemDescription, onRemoveItem);
            items.Add(item);
        }
        
        #endregion

        #region Private Methods

        private void EquipItem(StatData[] statsData, int aItemId)
        {
            Dispose();
            inventoryUI.SetActive(false);
            onEquipItem.Invoke(statsData, aItemId);
        }

        #endregion
    }
}