using UnityEngine;

namespace Game.Systems.CharactersEntitiesSystem.Data
{
    [System.Serializable]
    public struct CreatorData
    {
        public int level;

        public int amountPlayers;
        
        public int amountEnemies;
    }
    
    [CreateAssetMenu(fileName = "CharacterCreatorSetup", menuName = "PikPok/Characters/Character Creator Setup")]
    public class CharacterCreatorSetup : ScriptableObject
    {
        [SerializeField]
        private CreatorData[] creatorData;

        public CreatorData[] CreatorData => creatorData;
    }
}