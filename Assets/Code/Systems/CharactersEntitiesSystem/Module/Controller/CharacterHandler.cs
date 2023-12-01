using System;
using System.Collections.Generic;
using System.Text;
using Game.Components.ItemsComponent.Data;
using Game.Components.StatsComponent.Data;
using Game.Core.CharacterEntity;
using Game.Systems.CharactersEntitiesSystem.Controller.Attack;
using Game.Systems.CharactersEntitiesSystem.Data;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Systems.CharactersEntitiesSystem.Controller
{
    public class CharacterHandler : MonoBehaviour, ICharacterEntity
    {
        [SerializeField]
        private AttackBase[] attacks;
        
        [Header("Data")]
        [SerializeField]
        private CharacterRuntimeDataStorage runtimeDataStorage;

        private Func<int, ItemSetup> onGetItem;
        private Action<CharacterHandler> onCharacterDefeated;
        private Action onAttack;
        
        public Action<int> OnUpdateHealth; 
        
        #region Public Methods
        
        public void Initialize(Func<int, ItemSetup> aOnGetItem, Action<CharacterHandler> aOnCharacterDefeated)
        {
            onGetItem = aOnGetItem;
            onCharacterDefeated = aOnCharacterDefeated; 
            runtimeDataStorage.Initialize();
        }

        public string GetName()
        {
            return runtimeDataStorage.CharacterSetup.NameCharacter;
        }

        public string GetStats()
        {
            StringBuilder statsBuilder = new StringBuilder();

            foreach (StatData stat in runtimeDataStorage.StatsRuntime.StatsData)
            {
                statsBuilder.Append($"{stat.Stat.NameStat.ToString()}: {stat.Value} \n");
            }
            
            return statsBuilder.ToString();
        }

        public Sprite GetCharacterIcon()
        {
            return runtimeDataStorage.CharacterSetup.Icon;
        }

        public List<Sprite> GetItemsIcon()
        {
            List<Sprite> icons = new List<Sprite>();

            foreach (int equippedItem in runtimeDataStorage.EquippedItemsRuntime.EquippedItemsId)
                icons.Add(equippedItem != -1 ? onGetItem(equippedItem).Icon : null);
            
            return icons;
        }
        
        public void AddItem(int aItemId, int aSlotId)
        {
            runtimeDataStorage.EquippedItemsRuntime.EquippedItemsId[aSlotId] = aItemId; 
        }

        public int[] GetItemsId()
        {
            return runtimeDataStorage.EquippedItemsRuntime.EquippedItemsId; 
        }

        public void GetPotion(out Sprite aIcon, out Action aOnInvokePotion)
        {
            int potionId = runtimeDataStorage.EquippedItemsRuntime.EquippedPotionsId[0];

            if (potionId != -1)
            {
                aIcon = onGetItem(potionId).Icon;
                aOnInvokePotion = UsePotion;
                
                return;
            }

            aIcon = null;
            aOnInvokePotion = null;
        }

        public Action<ICharacterEntity> GetAttack()
        {
            return DoAttack;
        }

        public void OnAttack(Action aOnAttack)
        {
            onAttack = aOnAttack; 
        }

        public void UpdateStats(Dictionary<string, int> stats)
        {
            var ownStats = runtimeDataStorage.StatsRuntime.StatsData; 
            
            foreach (var stat in stats)
            {
                var statId = ownStats.FindIndex(x => x.Stat.NameStat == stat.Key);
                ownStats[statId].Value += stat.Value;
            }
        }

        public void ReceiveDamage(int aDamage)
        {
            OnUpdateHealth?.Invoke(-aDamage);
            int currentHealth = runtimeDataStorage.StatsRuntime.StatsData.Find(x => x.Stat.NameStat == "Health").Value;
            if (currentHealth <= 0)
            {
                onCharacterDefeated?.Invoke(this);
            }
        }

        public GameObject GetGameObject()
        {
            return gameObject; 
        }

        #endregion

        #region Private Methods
        
        private void UsePotion()
        {
            var statsCharacter = runtimeDataStorage.StatsRuntime.StatsData;
            int potionId = runtimeDataStorage.EquippedItemsRuntime.EquippedPotionsId[0];
            ItemSetup potionSetup = onGetItem(potionId);

            foreach (var stat in potionSetup.StatData)
            {
                var ownStat = statsCharacter.Find(x => x.Stat.NameStat == stat.Stat.NameStat);
                ownStat.Value += stat.Value; 
            }
        }

        private void DoAttack(ICharacterEntity characterEntity)
        {
            int damage = runtimeDataStorage.StatsRuntime.StatsData.Find(x => x.Stat.NameStat == "Damage").Value;
            int randAttack = Random.Range(0, attacks.Length);
            attacks[randAttack].Attack(characterEntity, damage);
            onAttack?.Invoke();
        }
        
        #endregion

    }
}