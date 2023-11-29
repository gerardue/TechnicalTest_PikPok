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
        private TextMeshProUGUI nameItemText;
        
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
            nameItemText.text = aNameItem;
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
            descriptionButton.onClick.AddListener(() => aOnDescription?.Invoke(nameItemText.text, descriptionItem));
        }

        public void MergeItem()
        {
            onRemoveItem?.Invoke(itemId);
            itemId = itemMerged.Id;
            nameItemText.text = itemMerged.NameItem;
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