using System;
using System.Collections.Generic;
using Game.Core.CharacterEntity;
using Game.Systems.TurnBasedMechanicSystem.Data;
using UnityEngine;

namespace Game.Systems.TurnBasedMechanicSystem.Controller
{
    public class TurnBasedRules : MonoBehaviour
    {
        [Header("Controllers")]
        [SerializeField]
        private TurnBasedForEnemy turnBasedEnemy;
        
        [Header("Data")]
        [SerializeField]
        private TurnBaseSetup turnBaseSetup;

        
        public Action OnWin;
        public Action OnDefeated;
        public Action OnPlayerEndTurns;
        public Action OnEnemyEndTurns;
        
        
        #region Public Methods

        public void Initialize(Func<List<GameObject>> aOnGetPlayersAlive, Func<List<GameObject>> aOnGetEnemiesAlive, 
            Action aOnWin)
        {
            turnBasedEnemy.Initialize(aOnGetPlayersAlive, aOnGetEnemiesAlive);
            SetEventOnEnemyAttack(aOnGetEnemiesAlive());
            OnWin += aOnWin;
        }

        public void PlayerTurns()
        {
            turnBaseSetup.EnemyTurns = 0;
            turnBaseSetup.PlayerTurns++;

            if (turnBaseSetup.PlayerTurns >= turnBaseSetup.PlaysPerTurn)
            {
                OnPlayerEndTurns?.Invoke();
                turnBasedEnemy.TurnBasedEnemy();
                Debug.Log("End player turns");
            }
        }

        public void EnemyTurns()
        {
            turnBaseSetup.PlayerTurns = 0;
            turnBaseSetup.EnemyTurns++;

            if (turnBaseSetup.EnemyTurns >= turnBaseSetup.PlaysPerTurn)
            {
                OnEnemyEndTurns?.Invoke();
                Debug.Log("End player turns");
            }
        }

        public void Win()
        {
            OnWin?.Invoke();
        }

        public void Defeated()
        {
            OnDefeated?.Invoke();
        }

        #endregion

        #region Private Methods

        private void SetEventOnEnemyAttack(List<GameObject> enemies)
        {
            foreach (var enemy in enemies)
            {
                enemy.GetComponent<ICharacterEntity>().OnAttack(EnemyTurns);
            }
        }

        #endregion
    }
}