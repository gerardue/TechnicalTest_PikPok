using System;
using Code.Systems.StoreSystem.Controller;
using Game.Components.ItemsComponent.Data;
using Game.Systems.StoreSystem.View;
using Game.Store.Data;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Systems.StoreSystem.Handler
{
    public class StoreHandler : MonoBehaviour
    {
        [SerializeField]
        private StoreController storeController;
        
        [SerializeField]
        private StoreLevelSetup storeLevelSetup;

        [SerializeField]
        private StoreView store;

        private int currentLvl = 0; 
        
        private Func<int> onGetLevelStore; // It takes the lvl player to configure the store
        private Action<int> onBuyItem;
        
        #region Public Methods
        
        public void Initialize(Func<int> aOnGetLevelStore, Func<int, bool> aOnBuyItem, Action<int> aOnAdditem)
        {
            onGetLevelStore = aOnGetLevelStore;
            storeController.Initialize(aOnBuyItem, aOnAdditem, store.OpenPopUp);
        }

        public void OpenStore()
        {
            SetupStore();
            store.Initialize();
        }

        public void CloseStore()
        {
            store.Dispose();
        }

        public void UpdateCoins(int aCurrentAmountCoins)
        {
            store.UpdateCoins(aCurrentAmountCoins);
        }
        
        #endregion

        #region Private Methods
        
        private void SetupStore()
        {
            if (onGetLevelStore() == currentLvl)
            {
                return;
            }
            
            currentLvl = onGetLevelStore();
            store.Dispose();
            
            StoreLevelData storeLevelData = storeLevelSetup.GetStore(currentLvl);
            
            foreach (ItemSetup item in storeLevelData.Items)
            {
                store.CreateItem(item.Id, item.NameItem, item.Description, item.Price, storeController.BuyItem);
            }
        }
        
        #endregion
    }
}