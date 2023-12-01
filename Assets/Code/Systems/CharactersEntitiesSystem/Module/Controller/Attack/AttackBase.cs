using System;
using Game.Core.CharacterEntity;
using UnityEngine;

namespace Game.Systems.CharactersEntitiesSystem.Controller.Attack
{
    public abstract class AttackBase : MonoBehaviour
    {
        public abstract void Attack(ICharacterEntity aTargetAttacked, int aDamage);
    }
}