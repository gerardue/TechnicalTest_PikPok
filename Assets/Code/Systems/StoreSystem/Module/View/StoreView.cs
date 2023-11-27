using System;
using System.Collections.Generic;
using Game.Components.ItemsComponent.View;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace Game.Systems.StoreSystem.View
{
    public class StoreView : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField]
        private GameObject storeUI;
        [SerializeField]
        private TextMeshProUGUI coinText;
        
        [Header("Item Prefab")]
        [SerializeField]
        private StoreItemView storeItem;

        [Header("Item Description UI")]
        [SerializeField]
        private ItemDescriptionView itemDescription;
        
        [Header("Other components")]
        [SerializeField]
        private Transform parentItems;

        private List<StoreItemView> items = new List<StoreItemView>();

        #region Unity Methods

        private void OnDisable()
        {
            // Dispose();
        }

        #endregion
        
        #region Public Methods
        
        public void Initialize()
        {
            storeUI.SetActive(true);
        }

        public void Dispose()
        {
            foreach (StoreItemView item in items)
            {
                ObjectPool.Instance.Recycle(item.gameObject);
            }
            
            items.Clear();
            storeUI.SetActive(false);
        }
        
        public void CreateItem(int aItemId, string aNameItem, string aDescriptionItem, int price, Action<int> aOnBuy)
        {
            var item = ObjectPool.Instance.CreateObject(storeItem, parentItems);
            item.Initialize(aItemId, aNameItem, aDescriptionItem, price, aOnBuy, itemDescription.OpenItemDescription);
            items.Add(item);
        }

        public void UpdateCoins(int currentAmountCoins)
        {
            coinText.text = currentAmountCoins.ToString(); 
        }
        
        #endregion

        #region Private Methods
        
        
        #endregion
    }
}