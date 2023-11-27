using System;
using Code.Systems.UISystem.Director;
using Game.Store.Director;
using Game.Systems.GameStateSystem.Director;
using Game.Systems.InventorySystem.Director;
using UnityEngine;

namespace Game.GameDirectors.GameplayDirector
{
    public class GameplayDirector : MonoBehaviour
    {
        [SerializeField]
        private StoreDirector storeDirector;

        [SerializeField]
        private InventoryDirector inventoryDirector; 

        [SerializeField]
        private GameStateDirector gameStateDirector;

        [SerializeField]
        private UIDirector uiDirector;
        
        #region Unity Methods
        
        private void Awake()
        {
            inventoryDirector.Initialize(storeDirector.GetItem, gameStateDirector.UpdateCoin, () => {});
            storeDirector.Initialize(gameStateDirector.GetCurrentLevel, gameStateDirector.UpdateCoin);
            uiDirector.Initialize(storeDirector.OpenStore, inventoryDirector.OpenInventory);
        }

        public void Test(int i)
        {
            
        }
        
        private void OnEnable()
        {
            gameStateDirector.OnUpdateCoin += storeDirector.UpdateCoins;
        }

        private void OnDisable()
        {
            gameStateDirector.OnUpdateCoin -= storeDirector.UpdateCoins;
        }

        #endregion
        
        #region Public Methods

        

        #endregion

        #region Private Methods

        

        #endregion
    }
}