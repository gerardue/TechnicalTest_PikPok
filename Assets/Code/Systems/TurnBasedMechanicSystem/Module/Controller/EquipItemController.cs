using System;
using System.Collections.Generic;
using Game.Components.StatsComponent.Data;
using Game.Core.CharacterEntity;
using UnityEngine;

namespace Game.Systems.TurnBasedMechanicSystem.Controller
{
    public class EquipItemController : MonoBehaviour
    {
        [SerializeField]
        private SelectCharacterController characterSelector;

        private ICharacterEntity characterEntity;

        public Action OnUpdateStats;
        
        #region Public Methods

        public void Initialize()
        {
            characterSelector.OnGetPlayerEntity += SetCharacterSelected;
        }

        public void EquipItem(StatData[] statsData, int aItemId, int slotItemId)
        {
            Dictionary<string, int> stats = new Dictionary<string, int>();

            foreach (var data in statsData)
            {
                stats.Add(data.Stat.NameStat, data.Value);
            }
            
            characterEntity.UpdateStats(stats);
            characterEntity.AddItem(aItemId, slotItemId);
            OnUpdateStats?.Invoke();
        }

        #endregion

        #region Private Methods

        private void SetCharacterSelected(ICharacterEntity aCharacter)
        {
            characterEntity = aCharacter;
        }

        #endregion
    }
}