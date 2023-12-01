using UnityEngine;

namespace Game.Components.StatsComponent.Data
{
    [System.Serializable]
    public class StatData
    {
        [SerializeField]
        private StatSetup stat;
        [SerializeField]
        private int value;
        [SerializeField]
        private int maxValue;
        
        
        public StatSetup Stat => stat;

        public int Value
        {
            get => value;
            set
            {
                this.value = value;
                if (this.value > maxValue)
                {
                    maxValue = this.value;
                }
            }
        }

        public int MaxValue => maxValue; 

        public StatData(StatSetup aStat, int aValue)
        {
            stat = aStat;
            value = aValue;
            maxValue = aValue;
        }
    }
}