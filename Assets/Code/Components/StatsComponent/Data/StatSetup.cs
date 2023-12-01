using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Components.StatsComponent.Data
{
    [CreateAssetMenu(fileName = "Stat", menuName = "PikPok/Stats")]
    public class StatSetup : ScriptableObject
    {
        [FormerlySerializedAs("nameStat")]
        [SerializeField]
        private string nameNameStat;

        public string NameStat => nameNameStat;
    }
}