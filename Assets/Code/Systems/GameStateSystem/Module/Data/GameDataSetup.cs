using UnityEngine;

namespace Game.Systems.GameStateSystem.Data
{
    [CreateAssetMenu(fileName = "GameData", menuName = "PikPok/GameData")]
    public class GameDataSetup : ScriptableObject
    {
        [SerializeField]
        private int coins;

        [SerializeField]
        private int level;

        public int Coins
        {
            get => coins;
            set => coins = value;
        }

        public int Level
        {
            get => level;
            set => level = value;
        }
    }
}