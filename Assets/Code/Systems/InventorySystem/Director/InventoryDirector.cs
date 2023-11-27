using System;
using Game.Components.ItemsComponent.Data;
using Game.Systems.InventorySystem.Handler;
using UnityEngine;

namespace Game.Systems.InventorySystem.Director
{
    public class InventoryDirector : MonoBehaviour
    {
        [SerializeField]
        private InventoryHandler inventoryHandler;

        #region Public Methods

        public void Initialize(Func<int, ItemSetup> aOnGetItem, Action<int> aOnSell, Action aOnEquip)
        {
            inventoryHandler.Initialize(aOnGetItem, aOnSell, aOnEquip);
        }

        public void OpenInventory()
        {
            inventoryHandler.OpenInventory();
        }
        
        public void CloseInventory()
        {
            inventoryHandler.CloseInventory();
        }
        
        #endregion
    }
}