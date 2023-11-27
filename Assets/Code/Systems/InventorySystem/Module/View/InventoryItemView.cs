using System;
using Game.Components.ItemsComponent.Data;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Utilities;

namespace Game.Systems.InventorySystem.View
{
    public class InventoryItemView : MonoBehaviour
    {
        [SerializeField]
        private Image icon;

        [SerializeField]
        private Button sellButton; 
        
        [SerializeField]
        private Button equipButton;

        [SerializeField]
        private Button descriptionButton;

        private int itemId;
        private string descriptionItem;
        private string nameItem;
        private int price;
        private ItemSetup itemMerged;

        private Action<int> onRemoveItem;
        
        public int ItemId => itemId;
        public ItemSetup ItemMerged => itemMerged;
        
        #region Public Methods

        public void Initialize(int aItemId, string aNameItem, string aDescriptionItem, int aPrice, ItemSetup aItemMerge, Action<int> aOnSell, 
            Action aOnEquip, Action<string, string> aOnDescription, Action<int> aOnRemoveItem)
        {
            itemId = aItemId;
            nameItem = aNameItem;
            descriptionItem = aDescriptionItem;
            price = aPrice;
            itemMerged = aItemMerge;
            onRemoveItem = aOnRemoveItem;
            
            sellButton.onClick.RemoveAllListeners();
            sellButton.onClick.AddListener(() =>
            {
                aOnSell?.Invoke(price);
                aOnRemoveItem?.Invoke(itemId);
                RecycleItem();
            });
            
            equipButton.onClick.RemoveAllListeners();
            equipButton.onClick.AddListener(() =>
            {
                aOnEquip?.Invoke();
                aOnRemoveItem?.Invoke(itemId);
                RecycleItem();
            });
            
            descriptionButton.onClick.RemoveAllListeners();
            descriptionButton.onClick.AddListener(() => aOnDescription?.Invoke(nameItem, descriptionItem));
        }

        public void MergeItem()
        {
            onRemoveItem?.Invoke(itemId);
            itemId = itemMerged.Id;
            nameItem = itemMerged.NameItem;
            descriptionItem = itemMerged.Description;
            price = itemMerged.Price;
            itemMerged = itemMerged.ItemMerged;
        }

        #endregion

        #region Private Methods

        private void RecycleItem()
        {
            ObjectPool.Instance.Recycle(gameObject);
        }

        #endregion
    }
}