using System;
using System.Collections;
using System.Collections.Generic;
using Game.Core.CharacterEntity;
using Game.Systems.TurnBasedMechanicSystem.Data;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Systems.TurnBasedMechanicSystem.Controller
{
    public class TurnBasedForEnemy : MonoBehaviour
    {
        [SerializeField]
        private TurnBaseSetup turnBasedSetup;
        
        private Func<List<GameObject>> onGetPlayersAlive;
        private Func<List<GameObject>> onGetEnemiesAlive;
        
        #region Public Methods

        public void Initialize(Func<List<GameObject>> aOnGetPlayersAlive, Func<List<GameObject>> aOnGetEnemiesAlive)
        {
            onGetPlayersAlive = aOnGetPlayersAlive;
            onGetEnemiesAlive = aOnGetEnemiesAlive;
        }

        public void TurnBasedEnemy()
        {
            StartCoroutine(TurnBased());
        }
        
        #endregion

        #region Private Methods

        private IEnumerator TurnBased()
        {
            yield return new WaitForSeconds(2);
            
            var enemiesAlive = onGetEnemiesAlive();
            var playersAlive = onGetPlayersAlive();

            if (enemiesAlive.Count == 0)
                yield break;
            
            for (int i = 0; i < turnBasedSetup.PlaysPerTurn; i++)
            {
                int randEnemy = Random.Range(0, enemiesAlive.Count);
                int randPlayer = Random.Range(0, playersAlive.Count);

                var playerEntity = playersAlive[randPlayer].GetComponent<ICharacterEntity>();
                var enemyEntity = enemiesAlive[randEnemy].GetComponent<ICharacterEntity>();
                var enemyAttack = enemyEntity.GetAttack();

                enemyAttack(playerEntity);
                
                yield return new WaitForSeconds(1.5f);
            }
        }

        #endregion
    }
}