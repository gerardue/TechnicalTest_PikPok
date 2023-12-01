using System.Collections.Generic;
using Game.Components.StatsComponent.Data;
using UnityEngine;

namespace Game.Systems.CharactersEntitiesSystem.Data
{
    [System.Serializable]
    public struct CharacterStatsRuntimeData
    {
        [SerializeField]
        private List<StatData> statsData;
        
        public List<StatData> StatsData
        {
            get => statsData;
            set => statsData = value;
        }

        public CharacterStatsRuntimeData(StatData[] aStats)
        {
            statsData = new List<StatData>();

            foreach (var stat in aStats)
            {
                statsData.Add(new StatData(stat.Stat, stat.Value));
            }
        }
    }
}