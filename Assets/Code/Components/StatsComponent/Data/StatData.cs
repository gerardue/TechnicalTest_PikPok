using UnityEngine;

namespace Game.Components.StatsComponent.Data
{
    [System.Serializable]
    public struct StatData
    {
        [SerializeField]
        private StatSetup stat;
        [SerializeField]
        private int value;
        
        public StatSetup Stat => stat;
        public int Value => value;
    }
}