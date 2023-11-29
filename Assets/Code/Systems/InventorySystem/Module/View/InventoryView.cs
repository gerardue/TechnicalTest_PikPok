using System;
using System.Collections.Generic;
using System.Data.Common;
using Game.Components.ItemsComponent.Data;
using Game.Components.ItemsComponent.View;
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
        
        public void CreateItem(int aItemId, string aNameItem, string aDescriptionItem, int price, ItemSetup itemMerged, Action<int> aOnSell, 
            Action aOnEquip, Action<int> onRemoveItem)
        {
            var item = ObjectPool.Instance.CreateObject(inventoryItem, parentItems);
            item.Initialize(aItemId, aNameItem, aDescriptionItem, price, itemMerged, aOnSell, aOnEquip, itemDescription.OpenItemDescription, onRemoveItem);
            items.Add(item);
        }
        
        #endregion
    }
}