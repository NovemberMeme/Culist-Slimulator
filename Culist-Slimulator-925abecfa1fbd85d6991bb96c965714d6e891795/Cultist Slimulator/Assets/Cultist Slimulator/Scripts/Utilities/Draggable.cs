using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace cc.tools
{
    public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] Canvas canvas;
        RectTransform rectTransform;

        [SerializeField] MainMenuControl mmc;
        Vector3 DefaultPos = Vector3.zero;
     
        [SerializeField]  command _type = command.NONE;

        CanvasGroup cg;

        private void Start()
        {
            DefaultPos = transform.position;
            cg = GetComponent<CanvasGroup>();
            rectTransform = GetComponent<RectTransform>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            mmc.Selection = this;
            mmc.type = _type;
          
            cg.interactable = false;
            cg.blocksRaycasts = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
            //Vector3 screenPoint = Input.mousePosition;
            //screenPoint.z = transform.position.z;
            //transform.position = Camera.main.ScreenToWorldPoint(screenPoint);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            transform.position = DefaultPos;
            mmc.Selection = null;
            mmc.type = command.NONE;

            cg.interactable = true;
            cg.blocksRaycasts = true;
        }
    }
}

