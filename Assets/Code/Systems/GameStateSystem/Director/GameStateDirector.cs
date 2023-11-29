using System;
using Game.Systems.GameStateSystem.Data;
using UnityEngine;

namespace Game.Systems.GameStateSystem.Director
{
    public class GameStateDirector : MonoBehaviour
    {
        [SerializeField]
        private GameDataSetup gameDataSetup;

        public Action<int> OnUpdateCoin; 
        
        #region Public Methods

        public void UpdateCoin(int aAmountCoins)
        {
            gameDataSetup.Coins += aAmountCoins;
            OnUpdateCoin?.Invoke(gameDataSetup.Coins);
        }
        
        public bool Debit(int aCost)
        {
            bool canDebit = gameDataSetup.Coins > aCost;
            
            if(canDebit)
                UpdateCoin(-aCost);
            
            return canDebit; 
        }
        
        public void IncreaseLevel()
        {
            gameDataSetup.Level++;
        }

        public int GetCurrentLevel()
        {
            return gameDataSetup.Level; 
        }

        #endregion
    }
}