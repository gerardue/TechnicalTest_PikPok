using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Systems.UISystem.View
{
    public class MenuView : MonoBehaviour
    {
        [SerializeField]
        private Button openButton;

        [SerializeField]
        private GameObject ui;

        #region Public Methods

        public void Initialize(Action onPressedButton)
        {
            openButton.onClick.AddListener(() => onPressedButton());
        }

        public void Open(bool value)
        {
            ui.gameObject.SetActive(value);
        }

        #endregion
    }
}