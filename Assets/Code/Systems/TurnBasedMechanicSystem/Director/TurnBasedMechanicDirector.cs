using System;
using System.Collections.Generic;
using Game.Components.StatsComponent.Data;
using Game.Systems.TurnBasedMechanicSystem.Controller;
using Game.Systems.TurnBasedMechanicSystem.View;
using UnityEngine;

namespace Game.Systems.TurnBasedMechanicSystem.Director
{
    public class TurnBasedMechanicDirector : MonoBehaviour
    {
        [SerializeField]
        private TurnBasedHandler turnBasedHandler;

        [SerializeField]
        private TurnBasedRules turnBasedRules;
        
        [SerializeField]
        private EquipItemController equipItem;

        [SerializeField]
        private TurnBasedView turnBasedView;

        #region Public Methods

        public void Initialize(Func<GameObject> aOnGetPlayer, Func<GameObject> aOnGetEnemy, 
            Func<List<GameObject>> aOnGetPlayersAlive, Func<List<GameObject>> aOnGetEnemiesAlive, Action aOnWin)
        {
            equipItem.Initialize();
            turnBasedHandler.Initialize(aOnGetPlayer, aOnGetEnemy);
            turnBasedRules.Initialize(aOnGetPlayersAlive, aOnGetEnemiesAlive, aOnWin);
        }

        public void OpenInventory(Action aOnOpenInventory)
        {
            turnBasedView.Initialize(aOnOpenInventory);
        }
        
        public void EquipItem(StatData[] statsData, int aItemId)
        {
            equipItem.EquipItem(statsData, aItemId, turnBasedView.GetCurrentSlotUsed());
        }

        public int GetCurrentSlotUsedId()
        {
            return turnBasedView.GetCurrentSlotUsed();
        }

        public void Win()
        {
            turnBasedRules.Win();
        }

        public void GameOver()
        {
            turnBasedRules.Defeated();
        }
        
        #endregion
    }
}