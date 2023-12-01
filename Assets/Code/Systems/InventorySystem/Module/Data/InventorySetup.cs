using System.Collections.Generic;
using UnityEngine;

namespace Game.Systems.InventorySystem.Data
{
    [CreateAssetMenu(fileName = "InventorySetup", menuName = "PikPok/InventorySetup")]
    public class InventorySetup : ScriptableObject
    {
        [SerializeField]
        private List<int> itemsId;

        public List<int> Items => itemsId;

        public void AddItem(int itemId)
        {
            itemsId.Add(itemId);
        }
    }
}