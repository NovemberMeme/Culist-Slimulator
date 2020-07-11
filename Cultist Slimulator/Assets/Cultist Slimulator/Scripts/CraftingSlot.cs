using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using SiegeTheSky;

namespace Slimulator
{
    public class CraftingSlot : MonoBehaviour, IDropHandler
    {
        private RectTransform rectTransform;

        private GameObject myMat;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag == null)
                return;

            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = rectTransform.anchoredPosition;

            myMat = eventData.pointerDrag;

            if(DelegateManager.currentCraftingMaterials.Count < 3)
            {
                DelegateManager.currentCraftingMaterials.Add(myMat);
                DelegateManager.updateCurrentCraftingMaterials();
            }
            else
            {
                Debug.Log("Crafting Materials at Max!");
            }
        }
    }
}