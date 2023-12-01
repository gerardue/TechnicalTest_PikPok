using UnityEngine;

namespace Game.Systems.CharactersEntitiesSystem.Data
{
    public class CharacterRuntimeDataStorage : MonoBehaviour
    {
        [SerializeField]
        private CharacterSetup characterSetup;
        
        [SerializeField]
        private CharacterStatsRuntimeData statsRuntime;

        [SerializeField]
        private CharacterEquipmentRuntimeData equippedItemsRuntime;

        public CharacterSetup CharacterSetup => characterSetup; 
        public CharacterStatsRuntimeData StatsRuntime => statsRuntime;
        public CharacterEquipmentRuntimeData EquippedItemsRuntime => equippedItemsRuntime; 
        

        #region Public Methods
        
        public void Initialize()
        {
            equippedItemsRuntime = new CharacterEquipmentRuntimeData(characterSetup.MaxAmountEquippedItems, characterSetup.MaxAmountEquippedPotions);
            statsRuntime = new CharacterStatsRuntimeData(characterSetup.Stats); 
        }

        #endregion
    }
}