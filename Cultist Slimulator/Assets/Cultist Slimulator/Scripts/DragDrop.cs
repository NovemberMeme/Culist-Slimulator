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
        }

        public void OnDrag(PointerEventData eventData)
        {
            rectTransform.anchoredPosition += eventData.delta / DelegateManager.dragDropCanvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1f;

            RectTransform nearestTransform = DelegateManager.GetNearestUIObject(DelegateManager.allUIObjects, DelegateManager.minDistance, rectTransform);

            if (nearestTransform != null &&
                nearestTransform != transform)
            {
                Debug.Log(nearestTransform.name.ToString());

                float newX = rectTransform.anchoredPosition.x - (rectTransform.anchoredPosition.x - nearestTransform.anchoredPosition.x > 0 ? -DelegateManager.minDistance : DelegateManager.minDistance);
                float newY = rectTransform.anchoredPosition.y - (rectTransform.anchoredPosition.y - nearestTransform.anchoredPosition.y > 0 ? -DelegateManager.minDistance : DelegateManager.minDistance);

                rectTransform.anchoredPosition = new Vector3(newX, newY);
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            
        }
    }
}