using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Components.ItemsComponent.View
{
    public class ItemDescriptionView : MonoBehaviour
    {
        [SerializeField]
        private GameObject ui;

        [SerializeField]
        private TextMeshProUGUI title;

        [SerializeField]
        private TextMeshProUGUI description;

        [SerializeField]
        private Button closeButton;

        #region Unity Methods

        private void OnEnable()
        {
            closeButton.onClick.AddListener(() => ui.SetActive(false));
        }

        private void OnDisable()
        {
            closeButton.onClick.RemoveAllListeners();
        }

        #endregion
        
        #region Public Methods

        public void OpenItemDescription(string aTitle, string aDescription)
        {
            title.text = aTitle;
            description.text = aDescription;
            ui.SetActive(true);
        }

        #endregion
    }
}