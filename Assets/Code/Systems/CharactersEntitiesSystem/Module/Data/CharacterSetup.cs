using Game.Components.StatsComponent.Data;
using UnityEngine;

namespace Game.Systems.CharactersEntitiesSystem.Data
{
    [CreateAssetMenu(fileName = "CharacterSetup", menuName = "PikPok/Characters/Character Setup")]
    public class CharacterSetup : ScriptableObject
    {
        [SerializeField]
        private string nameCharacter;

        [SerializeField]
        private Sprite icon;

        [SerializeField]
        private CharacterType characterType;

        [SerializeField]
        private StatData[] stats;

        [SerializeField]
        private int maxAmountEquippedItems;

        [SerializeField]
        private int maxAmountAmountEquippedPotions;

        public string NameCharacter => nameCharacter;
        public Sprite Icon => icon; 
        public CharacterType CharacterType => characterType;
        public StatData[] Stats => stats;
        public int MaxAmountEquippedItems => maxAmountEquippedItems;
        public int MaxAmountEquippedPotions => maxAmountAmountEquippedPotions; 
    }
}