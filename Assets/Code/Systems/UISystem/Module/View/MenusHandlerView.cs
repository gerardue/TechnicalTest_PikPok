using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Systems.UISystem.View
{
    public class MenusHandlerView : MonoBehaviour
    {
        [SerializeField]
        private MenuView store;

        [SerializeField]
        private MenuView inventory;
        
        private List<MenuView> menuViews;
        
        // Events
        private Action onStore;
        private Action onInventory; 
        
        #region Public Methods

        public void Initialize(Action aOnStore, Action aOnInventory)
        {
            onStore = aOnStore;
            onInventory = aOnInventory; 
            
            AddMenus();
            SetupMenus();
        }

        #endregion

        #region Private Methods

        private void SetupMenus()
        {
            store.Initialize(OpenStore);
            inventory.Initialize(OpenInventory);
        }

        private void OpenStore()
        {
            CloseAllMenus();
            store.Open(true);
            onStore?.Invoke();
        }

        private void OpenInventory()
        {
            CloseAllMenus();
            inventory.Open(true);
            onInventory?.Invoke();
        }
        
        private void AddMenus()
        {
            menuViews = new List<MenuView>();
            menuViews.Add(store);
            menuViews.Add(inventory);
        }

        private void CloseAllMenus()
        {
            foreach (MenuView menu in menuViews)
            {
                menu.Open(false);
            }
        }

        #endregion
    }
}