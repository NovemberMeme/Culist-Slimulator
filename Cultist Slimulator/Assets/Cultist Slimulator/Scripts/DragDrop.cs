using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using SiegeTheSky;

namespace Slimulator
{
    public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        private RectTransform rectTransform;
        private CanvasGroup canvasGroup;

        public bool isDragging = false;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = DelegateManager.dragAlpha;

            if (DelegateManager.currentCraftingMaterials.Contains(eventData.pointerDrag))
            {
                DelegateManager.currentCraftingMaterials.Remove(eventData.pointerDrag);
                DelegateManager.updateCurrentCraftingMaterials();
            }

            isDragging = true;
        }

        public void OnDrag(PointerEventData eventData)
        {
            rectTransform.anchoredPosition += eventData.delta / DelegateManager.dragDropCanvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1f;

            DelegateManager.AvoidOverlap(DelegateManager.allUIObjects, DelegateManager.minDistance, rectTransform);

            isDragging = false;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            
        }
    }
}