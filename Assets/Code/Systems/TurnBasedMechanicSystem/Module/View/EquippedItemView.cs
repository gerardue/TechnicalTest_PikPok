using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Systems.TurnBasedMechanicSystem.View
{
    public class EquippedItemView : MonoBehaviour
    {
        [SerializeField]
        private Image itemIcon;

        [SerializeField]
        private Button equipItem;

        private int id;
        
        #region Public Methods

        public void Initialize(Action aOnOpenInventory, Action<int> aOnCurrentSlotUsed, int aId)
        {
            id = aId;
            equipItem.gameObject.SetActive(true);
            equipItem.onClick.RemoveAllListeners();
            equipItem.onClick.AddListener(() =>
            {
                aOnCurrentSlotUsed?.Invoke(id);
                aOnOpenInventory();
                equipItem.gameObject.SetActive(false);
            });
        }

        public void SetSprite(Sprite aSprite)
        {
            itemIcon.sprite = aSprite;
        }

        public void EnableEquipButton(bool value)
        {
            equipItem.gameObject.SetActive(value);
        }
        
        #endregion
    }
}