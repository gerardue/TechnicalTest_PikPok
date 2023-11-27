using Game.Components.StatsComponent.Data;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Components.ItemsComponent.Data
{
    [CreateAssetMenu(fileName = "ItemStore", menuName = "PikPok/Items/ItemStore")]
    public class ItemSetup : ScriptableObject
    {
        [SerializeField]
        private int id;
        [SerializeField]
        private string nameItem;
        [SerializeField]
        private string description;
        [SerializeField]
        private int price;
        [SerializeField]
        private Sprite icon;
        [SerializeField]
        private int level;
        [SerializeField]
        private StatData[] statData;
        [SerializeField]
        private ItemSetup itemMerged;

        public int Id
        {
            get => id;
            set => id = value;   
        }
        
        public string NameItem => nameItem;
        public string Description => description;
        public int Price => price;
        public Sprite Icon => icon;
        public int Level => level;
        public StatData[] StatData => statData;
        public ItemSetup ItemMerged => itemMerged;
    }
}