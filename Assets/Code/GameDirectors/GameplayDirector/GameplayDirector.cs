using System;
using Code.Systems.UISystem.Director;
using Game.Components.StatsComponent.Data;
using Game.Store.Director;
using Game.Systems.CharactersEntitiesSystem.Director;
using Game.Systems.GameStateSystem.Director;
using Game.Systems.InventorySystem.Director;
using Game.Systems.TurnBasedMechanicSystem.Director;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.GameDirectors.GameplayDirector
{
    public class GameplayDirector : MonoBehaviour
    {
        [SerializeField]
        private TurnBasedMechanicDirector turnBasedMechanicDirector;
        
        [SerializeField]
        private CharacterDirector characterDirector;
        
        [SerializeField]
        private StoreDirector storeDirector;

        [SerializeField]
        private InventoryDirector inventoryDirector; 

        [SerializeField]
        private GameStateDirector gameStateDirector;

        [SerializeField]
        private UIDirector uiDirector;
        
        #region Unity Methods
        
        private void Awake()
        {
            characterDirector.Initialize(storeDirector.GetItem, turnBasedMechanicDirector.GameOver,
                turnBasedMechanicDirector.Win);
            characterDirector.CreateCharacters(gameStateDirector.GetCurrentLevel());
            
            inventoryDirector.Initialize(storeDirector.GetItem, gameStateDirector.UpdateCoin, turnBasedMechanicDirector.EquipItem);
            storeDirector.Initialize(gameStateDirector.GetCurrentLevel, gameStateDirector.Debit, inventoryDirector.AddItemToinventory);
            uiDirector.Initialize(gameStateDirector.GetCurrentCoins(), gameStateDirector.GetCurrentLevel());
            
            turnBasedMechanicDirector.OpenInventory(inventoryDirector.OpenInventory);
            turnBasedMechanicDirector.Initialize(characterDirector.GetOnePlayer, characterDirector.GetOneEnemy,
                characterDirector.GetPlayersAlive, characterDirector.GetEnemiesAlive, Win);
        }
        
        private void OnEnable()
        {
            gameStateDirector.OnUpdateCoin += storeDirector.UpdateCoins;
        }

        private void OnDisable()
        {
            gameStateDirector.OnUpdateCoin -= storeDirector.UpdateCoins;
        }

        #endregion  
        
        #region Public Methods

        public void ReturnToHome()
        {
            SceneManager.LoadScene(0);
        }

        #endregion

        #region Private Methods

        private void EquipItem(StatData[] aStatsData, int aItemId)
        {
            turnBasedMechanicDirector.EquipItem(aStatsData, aItemId);
        }

        private void Win()
        {
            gameStateDirector.UpLevel();
            storeDirector.OpenStore();
        }

        #endregion
    }
}