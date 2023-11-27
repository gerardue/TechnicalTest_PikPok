using System;
using Game.Systems.InventorySystem.View;
using UnityEngine;
using UnityEngine.EventSystems;
using Utilities;

namespace Game.Systems.InventorySystem.Controller
{
    public class MergeItemController : MonoBehaviour, IBeginDragHandler, IDragHandler, IDropHandler
    {
        [SerializeField]
        private RectTransform rectTransform;

        [SerializeField]
        private InventoryItemView item;

        private int childId;
        private Vector2 startPosition;
        private bool isReadyToCheck;
        private bool isReadyToMerge;

        private InventoryItemView otherItem;
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            startPosition = rectTransform.position;
            isReadyToCheck = true;
            // childId = transform.GetSiblingIndex();
            // transform.SetAsLastSibling();
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            rectTransform.position = eventData.position; 
        }
        
        public void OnDrop(PointerEventData eventData)
        {
            isReadyToCheck = false;
            
            if (isReadyToMerge)
            {
                Debug.Log("It has been merge");
                // transform.SetSiblingIndex(childId);
                ObjectPool.Instance.Recycle(item.gameObject);
                otherItem.MergeItem();
                return;
            }
            
            rectTransform.position = startPosition;
            // transform.SetSiblingIndex(childId);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if(!isReadyToCheck)
                return;

            if (other.TryGetComponent(out InventoryItemView otherItem))
            {
                if(otherItem.ItemId != item.ItemId && item.ItemMerged == null)
                    return;

                this.otherItem = otherItem;
                childId = otherItem.transform.GetSiblingIndex();
                
                isReadyToMerge = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            isReadyToMerge = false;
        }
    }
}