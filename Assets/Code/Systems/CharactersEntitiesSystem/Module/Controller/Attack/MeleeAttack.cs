using System;
using System.Collections;
using Game.Core.CharacterEntity;
using UnityEngine;

namespace Game.Systems.CharactersEntitiesSystem.Controller.Attack
{
    public class MeleeAttack : AttackBase
    {
        [SerializeField]
        private float duration = 0.5f;
        
        private Vector3 initialPosition;


        #region Unity Methods

        private void OnEnable()
        {
            initialPosition = transform.position;
        }

        #endregion
        
        #region Public Methods

        public override void Attack(ICharacterEntity aTargetAttacked, int aDamage)
        {
            StartCoroutine(AttackMovement(aTargetAttacked, aDamage));
        }

        #endregion

        #region Private Methods

        private IEnumerator AttackMovement(ICharacterEntity targetAttacked, int aDamage)
        {
            yield return StartCoroutine(GoTo(initialPosition, targetAttacked.GetGameObject().transform.position));
            targetAttacked.ReceiveDamage(aDamage);
            yield return new WaitForSeconds(0.5f);
            yield return StartCoroutine(GoTo(targetAttacked.GetGameObject().transform.position, initialPosition));
        }

        private IEnumerator GoTo(Vector3 initialPosition, Vector3 targetPosition)
        {
            float elapsedTime = 0;

            while (elapsedTime < 1)
            {
                elapsedTime += Time.deltaTime / duration;
                transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime);

                yield return null;
            }
        }

        #endregion
    }
}