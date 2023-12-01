using System;
using Game.Core.CharacterEntity;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Systems.TurnBasedMechanicSystem.View
{
    public class EnemyEntityView : MonoBehaviour
    {
        [SerializeField]
        private Image enemyIcon;
        
        [SerializeField]
        private Button enemyButton;

        [SerializeField]
        private GameObject descriptionEnemyUI;

        [SerializeField]
        private TextMeshProUGUI nameEnemyText;
        
        [SerializeField]
        private TextMeshProUGUI enemyStatsText;
        
        private ICharacterEntity enemyEntity;

        #region Public Methods
        
        public void UpdateEnemyUI(ICharacterEntity aEnemyEntity)
        {
            enemyEntity = aEnemyEntity;
            enemyIcon.sprite = enemyEntity.GetCharacterIcon();
            enemyButton.onClick.RemoveAllListeners();
            enemyButton.onClick.AddListener(OpenEnemyDescription);
        }
        
        #endregion

        #region Private Methods
        
        private void OpenEnemyDescription()
        {
            nameEnemyText.text = enemyEntity.GetName();
            enemyStatsText.text = enemyEntity.GetStats(); 
            descriptionEnemyUI.SetActive(true);
        }
        
        #endregion
    }
}