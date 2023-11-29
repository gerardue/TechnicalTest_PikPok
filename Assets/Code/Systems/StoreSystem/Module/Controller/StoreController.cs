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

        private Func<int, bool> onPurchase;
        private Action<int> onAddItem;
        private Action onNotDebit;
        
        #region Public Methods
        
        public void Initialize(Func<int, bool> aOnPurchase, Action<int> aOnAddItem, Action aOnNotDebit)
        {
            storeItemLibrary.InitializeItems();
            onPurchase = aOnPurchase;
            onAddItem = aOnAddItem; 
            onNotDebit = aOnNotDebit;
        }
        
        public void BuyItem(int itemId)
        {
            int price = storeItemLibrary.StoreItems[itemId].Price;
            bool isDebited = onPurchase.Invoke(price);

            if (isDebited)
                onAddItem?.Invoke(itemId);
            else
                onNotDebit?.Invoke();
        }
        
        public ItemSetup GetItem(int aItemId)
        {
            return storeItemLibrary.StoreItems[aItemId];
        }
        
        #endregion
    }
}