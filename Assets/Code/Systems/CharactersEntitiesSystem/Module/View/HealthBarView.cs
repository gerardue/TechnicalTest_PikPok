using System;
using System.Collections.Generic;
using Game.Components.StatsComponent.Data;
using Game.Systems.CharactersEntitiesSystem.Controller;
using Game.Systems.CharactersEntitiesSystem.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Systems.CharactersEntitiesSystem.View
{
    public class HealthBarView : MonoBehaviour
    {
        [SerializeField]
        private Image healthbar;

        [SerializeField]
        private CharacterHandler characterHandler;
        
        [SerializeField]
        private CharacterRuntimeDataStorage runtimeDataStorage;

        public int increaeHealth;

        #region Unity Methods

        private void OnEnable()
        {
            characterHandler.OnUpdateHealth += UpdateHealth;
        }

        private void OnDisable()
        {
            characterHandler.OnUpdateHealth -= UpdateHealth;
        }

        #endregion
        
        #region Public Methods
        
        public void UpdateHealth(int aValue)  
        {
            int health = runtimeDataStorage.StatsRuntime.StatsData.FindIndex(x => x.Stat.NameStat == "Health");
            List<StatData> stats = runtimeDataStorage.StatsRuntime.StatsData;
            stats[health].Value = Mathf.Max(0, stats[health].Value + aValue); 
            healthbar.fillAmount = Mathf.Max(0,  (float)stats[health].Value / (float)stats[health].MaxValue);
        }

        #endregion
    }
}