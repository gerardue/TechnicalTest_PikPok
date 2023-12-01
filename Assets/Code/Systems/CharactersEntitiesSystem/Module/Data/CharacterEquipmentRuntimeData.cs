using UnityEngine;

namespace Game.Systems.CharactersEntitiesSystem.Data
{
    [System.Serializable]
    public struct CharacterEquipmentRuntimeData
    {
        [SerializeField]
        private int[] equippedItemsId;
        [SerializeField]
        private int[] equippedPotionsId;
        
        public int[] EquippedItemsId
        {
            get => equippedItemsId;
            set => equippedItemsId = value; 
        }

        public int[] EquippedPotionsId
        {
            get => equippedPotionsId;
            set => equippedPotionsId = value;
        }

        public CharacterEquipmentRuntimeData(int aMaxEquippedItems, int aMaxEquippedPotions)
        {
            equippedItemsId = new int[aMaxEquippedItems];
            equippedPotionsId = new int[aMaxEquippedPotions];

            for (int i = 0; i < equippedItemsId.Length; i++)
                equippedItemsId[i] = -1;
            
            for (int i = 0; i < equippedPotionsId.Length; i++)
                equippedPotionsId[i] = -1;
        }
    }
}