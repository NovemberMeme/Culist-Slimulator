using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SiegeTheSky;

namespace Slimulator
{
    public class AppearDisappear : MonoBehaviour
    {
        [SerializeField] private bool canAppearDisappear = false;

        [SerializeField] private bool isHiding = false;

        [SerializeField] private float minShowDuration = 0.5f;
        [SerializeField] private float maxShowDuration = 1;

        [SerializeField] private float minHideDuration = 0.5f;
        [SerializeField] private float maxHideDuration = 1;

        private float currentShowDuration;
        private float currentHideDuration;

        private Vector2 lastPos;

        private float timer = 0;

        private DragDrop dragDrop;
        private RectTransform rectTransform;

        private void Awake()
        {
            dragDrop = GetComponent<DragDrop>();
            rectTransform = GetComponent<RectTransform>();

            isHiding = false;
            RandomizeDurations();
        }

        private void Update()
        {
            if (!canAppearDisappear)
                return;

            timer += Time.deltaTime;

            if (!isHiding)
            {
                if(timer > currentShowDuration)
                {
                    Hide();
                }
            }
            else
            {
                if(timer > currentHideDuration)
                {
                    Show();
                }
            }
        }

        private void RandomizeDurations()
        {
            timer = 0;

            currentShowDuration = Random.Range(minShowDuration, maxShowDuration);
            currentHideDuration = Random.Range(minHideDuration, maxHideDuration);
        }

        private void Show()
        {
            isHiding = false;
            rectTransform.anchoredPosition = lastPos;

            if(!DelegateManager.currentCraftingMaterials.Contains(gameObject))
                DelegateManager.AvoidOverlap(rectTransform);

            RandomizeDurations();
        }

        private void Hide()
        {
            if (dragDrop.isDragging)
                return;

            if (DelegateManager.currentCraftingMaterials.Contains(gameObject))
                return;

            isHiding = true;
            lastPos = rectTransform.anchoredPosition;
            rectTransform.anchoredPosition = new Vector2(9000, 9000);
            RandomizeDurations();
        }
    }
}