using UnityEngine;

namespace Game.Components.StatsComponent.Data
{
    [CreateAssetMenu(fileName = "Stat", menuName = "PikPok/Stats")]
    public class StatSetup : ScriptableObject
    {
        [SerializeField]
        private string nameStat;

        public string Stat => nameStat;
    }
}