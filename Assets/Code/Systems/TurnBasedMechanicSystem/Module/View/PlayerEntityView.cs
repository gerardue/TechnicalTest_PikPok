using System;
using Game.Core.CharacterEntity;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Systems.TurnBasedMechanicSystem.View
{
    public class PlayerEntityView : MonoBehaviour
    {
        [SerializeField]
        private EquippedItemView[] slotEquippedItems;

        [SerializeField]
        private TextMeshProUGUI nameText;
        
        [SerializeField]
        private TextMeshProUGUI statsText;

        [SerializeField]
        private Button attackButton; 
        
        [Header("Potion")]
        [SerializeField]
        private Button potionButton;
        [SerializeField]
        private Image potionIcon;

        private ICharacterEntity playerEntity;
        private ICharacterEntity enemyEntity;
        private int currentSlotUsedId;

        private Action onPlayerTurn; 
        
        #region Public Methods

        public void Initialize(Action aOnOpenInventory, Action aOnPlayerTurn)
        {
            for (int i = 0; i < slotEquippedItems.Length; i++)
            {
                slotEquippedItems[i].Initialize(aOnOpenInventory, SetCurrentSlotUsed, i);
            }
            
            onPlayerTurn = aOnPlayerTurn;
        }

        public void UpdatePlayerUI(ICharacterEntity aPlayerEntity)
        {
            playerEntity = aPlayerEntity;
            UpdateSlotItems();
            SetupStats();
            SetupEquippedItems();
            SetupPotionButton();
            SetupAttackButton();
        }

        public void SetEnemyEntity(ICharacterEntity aEnemyEntity)
        {
            enemyEntity = aEnemyEntity;
        }

        public void SetupStats()
        {
            nameText.text = playerEntity.GetName();
            statsText.text = playerEntity.GetStats();
        }
        
        public void SetupEquippedItems()
        {
            var itemsIcon = playerEntity.GetItemsIcon();  

            for (int i = 0; i < itemsIcon.Count; i++)
            {
                slotEquippedItems[i].SetSprite(itemsIcon[i]);
            }
        }

        public int GetCurrentSlotUsedId()
        {
            return currentSlotUsedId; 
        }
        
        public void EnablePlayerButtons()
        {
            attackButton.gameObject.SetActive(true);
            potionButton.gameObject.SetActive(true);
        }
        
        public void DisablePlayerButtons()
        {
            attackButton.gameObject.SetActive(false);
            potionButton.gameObject.SetActive(false);
        }
        
        #endregion

        #region Private Methods

        private void SetupPotionButton()
        {
            Sprite potionSprite;
            Action onPotion;
            playerEntity.GetPotion(out potionSprite, out onPotion);

            potionIcon.sprite = potionSprite; 
            potionButton.onClick.RemoveAllListeners();
            potionButton.onClick.AddListener(() => onPotion());
        }

        private void SetupAttackButton()
        {
            Action<ICharacterEntity> onAttack = playerEntity.GetAttack();
            attackButton.onClick.RemoveAllListeners();
            attackButton.onClick.AddListener(() =>
            {
                onPlayerTurn?.Invoke();
                onAttack?.Invoke(enemyEntity);
            });
        }

        private void SetCurrentSlotUsed(int aId)
        {
            currentSlotUsedId = aId;
        }

        private void UpdateSlotItems()
        {
            int[] itemsId = playerEntity.GetItemsId();
            for (int i = 0; i < itemsId.Length; i++)
            {
                if (itemsId[i] == -1)
                    slotEquippedItems[i].EnableEquipButton(true);
                else
                    slotEquippedItems[i].EnableEquipButton(false);
            }
        }

        #endregion
    }
}