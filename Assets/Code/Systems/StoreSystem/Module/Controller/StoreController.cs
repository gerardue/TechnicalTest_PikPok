using System;
using Game.Components.ItemsComponent.Data;
using Game.Store.Data;
using UnityEngine;

namespace Code.Systems.StoreSystem.Controller
{
    public class StoreController : MonoBehaviour
    {
        [SerializeField]
        private StoreItemLibrary storeItemLibrary;

        private Action<int> onPurchase;
        private Action<int> onSell; 
        
        #region Public Methods
        
        public void Initialize(Action<int> aOnPurchase)
        {
            storeItemLibrary.InitializeItems();
            onPurchase = aOnPurchase;
        }
        
        public void BuyItem(int itemId)
        {
            int price = storeItemLibrary.StoreItems[itemId].Price;
            onPurchase?.Invoke(-price);
        }
        
        public ItemSetup GetItem(int aItemId)
        {
            return storeItemLibrary.StoreItems[aItemId];
        }
        
        #endregion
    }
}