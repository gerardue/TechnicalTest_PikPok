using System;
using Game.Systems.UISystem.View;
using UnityEngine;

namespace Code.Systems.UISystem.Director
{
    public class UIDirector : MonoBehaviour
    {
        [SerializeField]
        private MenusHandlerView menusHandlerView;

        [SerializeField]
        private HudView hudView;

        #region Public Methods

        public void Initialize(int aCoin, int aLevel)
        {
            hudView.UpdateHud(aCoin, aLevel);
        }

        #endregion
    }
}