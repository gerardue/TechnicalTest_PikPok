using UnityEngine;

namespace Game.Systems.TurnBasedMechanicSystem.Data
{
    [CreateAssetMenu(fileName = "TurnBaseSetup", menuName = "PikPok/Turn Base/Turn Base Setup")]
    public class TurnBaseSetup : ScriptableObject
    {
        [SerializeField]
        private int playsPerTurn;

        [SerializeField]
        private int playerTurns;

        [SerializeField]
        private int enemyTurns;

        public int PlaysPerTurn => playsPerTurn;

        public int PlayerTurns
        {
            get => playerTurns;
            set => playerTurns = value;
        }

        public int EnemyTurns
        {
            get => enemyTurns;
            set => enemyTurns = value;
        }
    }
}