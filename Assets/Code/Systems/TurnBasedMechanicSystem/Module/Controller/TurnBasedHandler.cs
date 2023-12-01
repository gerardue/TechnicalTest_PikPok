using System;
using Game.Core.CharacterEntity;
using Game.Systems.TurnBasedMechanicSystem.View;
using UnityEngine;

namespace Game.Systems.TurnBasedMechanicSystem.Controller
{
    public class TurnBasedHandler : MonoBehaviour
    {
        [SerializeField]
        private SelectCharacterController characterSelector;

        private Func<GameObject> onGetPlayer; 
        private Func<GameObject> onGetEnemy; 

        #region Public Methods

        public void Initialize(Func<GameObject> aOnGetPlayer, Func<GameObject> aOnGetEnemy)
        {
            onGetPlayer = aOnGetPlayer;
            onGetEnemy = aOnGetEnemy;
            
            characterSelector.SelectEnemy(onGetEnemy());
            characterSelector.SelectPlayer(onGetPlayer());
        }

        #endregion
    }
}