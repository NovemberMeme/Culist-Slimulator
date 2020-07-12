using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SiegeTheSky;

namespace Slimulator
{
    public class BoundedObject : MonoBehaviour
    {
        RectTransform rectTransform;

        [SerializeField] private float resetInterval = 1;

        [SerializeField] private Vector2 origPosition = new Vector2(500, 500);

        [SerializeField] private float randomizePositionMultiplier = 10;

        float timer;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        private void Update()
        {
            timer += Time.deltaTime;

            if(timer > resetInterval)
            {
                timer = 0;
                ResetPosition();
            }
        }

        public void ResetPosition()
        {
            rectTransform.anchoredPosition = origPosition;
            DelegateManager.RandomizePosition(rectTransform, randomizePositionMultiplier);
        }
    }
}