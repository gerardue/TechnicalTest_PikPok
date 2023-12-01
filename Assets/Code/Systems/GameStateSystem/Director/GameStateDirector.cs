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
        
        public void UpLevel()
        {
            gameDataSetup.Level = Mathf.Min(gameDataSetup.Level + 1, 3);
        }

        public void ResetLevel()
        {
            gameDataSetup.Level = 1; 
        }

        public int GetCurrentLevel()
        {
            return gameDataSetup.Level; 
        }

        public int GetCurrentCoins()
        {
            return gameDataSetup.Coins;
        }

        #endregion
    }
}