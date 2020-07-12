using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using SiegeTheSky;

namespace Slimulator
{
    public class CraftingSlot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
    {
        private RectTransform rectTransform;

        private GameObject myMat;
        private RectTransform myMatRect;
        private DragDrop myMatDragDrop;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        private void Update()
        {
            if(myMat != null)
            {
                if(myMatDragDrop != null)
                {
                    if (myMatDragDrop.isDragging)
                    {
                        myMat = null;
                        myMatRect = null;
                        myMatDragDrop = null;
                    }
                }
            }

            if(myMat != null)
            {
                if(myMatRect != null)
                    myMatRect.anchoredPosition = rectTransform.anchoredPosition;
            }
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag == null)
                return;

            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = rectTransform.anchoredPosition;

            myMat = eventData.pointerDrag;
            myMatRect = myMat.GetComponent<RectTransform>();
            myMatDragDrop = myMat.GetComponent<DragDrop>();

            if(DelegateManager.currentCraftingMaterials.Count < 3)
            {
                DelegateManager.currentCraftingMaterials.Add(myMat);
                DelegateManager.updateCurrentCraftingMaterials();
                //Debug.Log(DelegateManager.currentCraftingMaterials.Count);
            }
            else
            {
                //Debug.Log(DelegateManager.currentCraftingMaterials.Count);
                Debug.Log("Crafting Materials at Max!");
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            //Debug.Log(eventData.pointerEnter.name.ToString());

            if (eventData.pointerEnter.GetComponent<CraftingMaterial>() != null)
            {
                DelegateManager.shouldRandomize = false;
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            //Debug.Log(eventData.pointerEnter.name.ToString());

            if (eventData.pointerEnter.GetComponent<CraftingMaterial>() != null)
            {
                DelegateManager.shouldRandomize = true;
            }
        }
    }
}