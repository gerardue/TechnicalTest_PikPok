using System;
using System.Collections.Generic;
using System.Linq;
using Game.Components.ItemsComponent.Data;
using Game.Core.CharacterEntity;
using Game.Systems.CharactersEntitiesSystem.Data;
using UnityEngine;
using Utilities;
using Random = UnityEngine.Random;

namespace Game.Systems.CharactersEntitiesSystem.Controller
{
    public class CharacterCreator : MonoBehaviour
    {
        [Header("Character Prefabs")]
        [SerializeField]
        private CharacterHandler[] players;
        [SerializeField]
        private CharacterHandler[] enemies;

        [Header("Positions")]
        [SerializeField]
        private Transform[] positionForPlayers;
        [SerializeField]
        private Transform[] positionsForEnemies;
        
        [Header("Setup")]
        [SerializeField]
        private CharacterCreatorSetup characterCreatorSetup;

        private List<CharacterHandler> playersCreated = new List<CharacterHandler>();
        private List<CharacterHandler> enemiesCreated = new List<CharacterHandler>();

        private Func<int, ItemSetup> onGetItem;
        private Action onAllPlayersDefeated;
        private Action onAllEnemiesDefeated;
        
        #region Public Methods

        public void Initialize(Func<int, ItemSetup> aOnGetItem, Action aOnAllPlayersDefeated, Action aOnAllEnemiesDefeated)
        {
            onGetItem = aOnGetItem;
            onAllPlayersDefeated = aOnAllPlayersDefeated;
            onAllEnemiesDefeated = aOnAllEnemiesDefeated; 
        }

        public void CreateCharacters(int aCurrentLevel)
        {
            RecycleAllCharactersCreated();
            
            CreatorData data = characterCreatorSetup.CreatorData.Where(x => x.level == aCurrentLevel).First();

            List<Transform> tempPositionForPlayer = new List<Transform>(positionForPlayers);
            List<Transform> tempPositionForEnemies = new List<Transform>(positionsForEnemies);

            for (int i = 0; i < data.amountPlayers; i++)
            {
                int randPlayer = Random.Range(0, players.Length);
                int randPos = Random.Range(0, tempPositionForPlayer.Count);

                CharacterHandler tempPlayer = ObjectPool.Instance.CreateObject(players[randPlayer], 
                    tempPositionForPlayer[randPos].position, Quaternion.identity);
                tempPlayer.Initialize(onGetItem, DefeatedPlayerCharacter);
                playersCreated.Add(tempPlayer);
                tempPositionForPlayer.RemoveAt(randPos);
            }
            
            for (int i = 0; i < data.amountEnemies; i++)
            {
                int randEnemy = Random.Range(0, enemies.Length);
                int randPos = Random.Range(0, tempPositionForEnemies.Count);

                CharacterHandler tempEnemy = ObjectPool.Instance.CreateObject(enemies[randEnemy], 
                    tempPositionForEnemies[randPos].position, Quaternion.identity);
                tempEnemy.Initialize(onGetItem, DefeatedEnemyCharacter);
                enemiesCreated.Add(tempEnemy);
                tempPositionForEnemies.RemoveAt(randPos);
            }
        }

        public GameObject GetOnePlayer()
        {
            return playersCreated[0].gameObject;
        }

        public GameObject GetOneEnemy()
        {
            return enemiesCreated[0].gameObject;
        }

        public List<GameObject> GetPlayersAlive()
        {
            List<GameObject> playersAlive = new List<GameObject>();

            foreach (CharacterHandler player in playersCreated)
                playersAlive.Add(player.gameObject);

            return playersAlive;
        }

        public List<GameObject> GetEnemiesAlive()
        {
            List<GameObject> enemiesAlive = new List<GameObject>();

            foreach (CharacterHandler enemies in enemiesCreated)
                enemiesAlive.Add(enemies.gameObject);

            return enemiesAlive;
        }

        #endregion

        #region Private Methods

        private void RecycleAllCharactersCreated()
        {
            foreach (CharacterHandler playerCreated in playersCreated)
                ObjectPool.Instance.Recycle(playerCreated.gameObject);
            
            foreach (CharacterHandler enemyCreated in enemiesCreated)
                ObjectPool.Instance.Recycle(enemyCreated.gameObject);
        }

        private void DefeatedPlayerCharacter(CharacterHandler aPlayerCharacter)
        {
            playersCreated.Remove(aPlayerCharacter); 
            ObjectPool.Instance.Recycle(aPlayerCharacter.gameObject);

            if (playersCreated.Count == 0)
                onAllPlayersDefeated?.Invoke();
        }

        private void DefeatedEnemyCharacter(CharacterHandler aEnemyCharacter)
        {
            enemiesCreated.Remove(aEnemyCharacter); 
            ObjectPool.Instance.Recycle(aEnemyCharacter.gameObject);
            
            if(enemiesCreated.Count == 0)
                onAllEnemiesDefeated?.Invoke();
        }
        
        #endregion
    }
}