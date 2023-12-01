using System;
using System.Collections;
using Game.Core.CharacterEntity;
using UnityEngine;
using Utilities;

namespace Game.Systems.CharactersEntitiesSystem.Controller.Attack
{
    public class RangeAttack : AttackBase
    {
        [SerializeField]
        private Bullet bulletPrefab;
        [SerializeField]
        private float duration = 0.5f;
        
        #region Public Methods

        public override void Attack(ICharacterEntity aTargetAttacked, int aDamage)
        {
            StartCoroutine(Shoot(aTargetAttacked, aDamage));
        }

        #endregion

        #region Private Methods

        private IEnumerator Shoot(ICharacterEntity targetAttacked, int aDamage)
        {
            float elapsedTime = 0;

            Transform bullet = ObjectPool.Instance.CreateObject(bulletPrefab, transform.position, Quaternion.identity).transform;
            
            while (elapsedTime < 1)
            {
                elapsedTime += Time.deltaTime / duration;
                bullet.position = Vector3.Lerp(transform.position, targetAttacked.GetGameObject().transform.position, elapsedTime);
            
                yield return null;
            }
            
            targetAttacked.ReceiveDamage(aDamage);
            ObjectPool.Instance.Recycle(bullet.gameObject);
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