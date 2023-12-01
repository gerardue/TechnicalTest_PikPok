using System;
using System.Collections.Generic;
using Game.Components.ItemsComponent.Data;
using Game.Components.StatsComponent.Data;
using Game.Systems.CharactersEntitiesSystem.Controller;
using UnityEngine;

namespace Game.Systems.CharactersEntitiesSystem.Director
{
    public class CharacterDirector : MonoBehaviour
    {
        [SerializeField]
        private CharacterCreator characterCreator;

        #region Public Methods

        public void Initialize(Func<int, ItemSetup> aOnGetItem, Action aOnAllPlayersDefeated, Action aOnAllEnemiesDefeated)
        {
            characterCreator.Initialize(aOnGetItem, aOnAllPlayersDefeated, aOnAllEnemiesDefeated);
        }

        public void CreateCharacters(int aCurrentLevel)
        {
            characterCreator.CreateCharacters(aCurrentLevel);
        }

        public GameObject GetOnePlayer()
        {
            return characterCreator.GetOnePlayer();
        }

        public GameObject GetOneEnemy()
        {
            return characterCreator.GetOneEnemy();
        }

        public List<GameObject> GetPlayersAlive()
        {
            return characterCreator.GetPlayersAlive();
        }

        public List<GameObject> GetEnemiesAlive()
        {
            return characterCreator.GetEnemiesAlive();
        }

        #endregion
    }
}