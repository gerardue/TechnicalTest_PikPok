using System;
using Game.Systems.TurnBasedMechanicSystem.Controller;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.Systems.TurnBasedMechanicSystem.View
{
    public class TurnBasedView : MonoBehaviour
    {
        [Header("Controller")]
        [SerializeField]
        private SelectCharacterController characterSelector;
        [SerializeField]
        private EquipItemController equipItemController;
        [SerializeField]
        private TurnBasedRules turnBasedRules;

        [Header("Views")]
        [SerializeField]
        private PlayerEntityView playerEntityView;
        [SerializeField]
        private EnemyEntityView enemyEntityView;

        [Header("Defeated Window")]
        [SerializeField]
        private GameObject defeatedUI;
        [SerializeField]
        private Button continueButton;
        
        [Header("Win Window")]
        [SerializeField]
        private GameObject winUI;
        [SerializeField]
        private Button continueWinButton;
        
        #region Public Methods

        public void Initialize(Action aOnOpenInventory)
        {
            playerEntityView.Initialize(aOnOpenInventory, turnBasedRules.PlayerTurns);
            characterSelector.OnGetPlayerEntity += playerEntityView.UpdatePlayerUI; 
            characterSelector.OnGetEnemyEntity += enemyEntityView.UpdateEnemyUI;
            characterSelector.OnGetEnemyEntity += playerEntityView.SetEnemyEntity;
            equipItemController.OnUpdateStats += playerEntityView.SetupStats;
            equipItemController.OnUpdateStats += playerEntityView.SetupEquippedItems;
            turnBasedRules.OnPlayerEndTurns += playerEntityView.DisablePlayerButtons;
            turnBasedRules.OnEnemyEndTurns += playerEntityView.EnablePlayerButtons;
            turnBasedRules.OnWin += Win;
            turnBasedRules.OnDefeated += Defeated;
        }

        public int GetCurrentSlotUsed()
        {
            return playerEntityView.GetCurrentSlotUsedId();
        }
  
        #endregion

        #region Private Methods

        private void Win()
        {
            winUI.SetActive(true);
            continueWinButton.onClick.AddListener(LoadScene);
        }
        
        private void Defeated()
        {
            defeatedUI.SetActive(true);
            continueButton.onClick.AddListener(LoadScene);
        }

        private void LoadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        #endregion
    }
}