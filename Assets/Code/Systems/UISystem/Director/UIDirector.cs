using System;
using Game.Systems.UISystem.View;
using UnityEngine;

namespace Code.Systems.UISystem.Director
{
    public class UIDirector : MonoBehaviour
    {
        [SerializeField]
        private MenusHandlerView menusHandlerView;

        #region Public Methods

        public void Initialize(Action aOnStore, Action aOnInventory)
        {
            menusHandlerView.Initialize(aOnStore, aOnInventory);
        }

        #endregion
    }
}