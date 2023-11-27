using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Systems.StoreSystem.View
{
    public class StoreItemView : MonoBehaviour
    {
        [SerializeField]
        private Image icon;

        [SerializeField]
        private TextMeshProUGUI priceText;
        
        [SerializeField]
        private Button buyButton;

        [SerializeField]
        private Button descriptionButton;

        private int itemId;
        private string descriptionItem;
        private string nameItem;

        #region Public Methods

        public void Initialize(int aItemId, string aNameItem, string aDescriptionItem, int price, Action<int> aOnBuy, Action<string, string> aOnDescription)
        {
            itemId = aItemId;
            nameItem = aNameItem;
            descriptionItem = aDescriptionItem;

            priceText.text = price.ToString();
            
            buyButton.onClick.RemoveAllListeners();
            buyButton.onClick.AddListener(() => aOnBuy?.Invoke(itemId));
            
            descriptionButton.onClick.RemoveAllListeners();
            descriptionButton.onClick.AddListener(() => aOnDescription?.Invoke(nameItem, descriptionItem));
        }

        #endregion

        #region Private Methods

       

        #endregion
    }
}