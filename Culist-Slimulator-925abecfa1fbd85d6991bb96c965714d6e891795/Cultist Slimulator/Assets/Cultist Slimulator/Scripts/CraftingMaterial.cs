using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SiegeTheSky;
using TMPro;
using cc.tools;

namespace Slimulator
{
    public class CraftingMaterial : MonoBehaviour
    {
        [SerializeField] private bool shouldAssignNames = false;
        [SerializeField] private TextMeshProUGUI myText;
        [SerializeField] private TextMeshProUGUI atomicNumber;

        [SerializeField] private CraftingMaterialType myCraftingMaterialType;

        [SerializeField] private bool isSelected = false;

        [SerializeField] private bool isDecaying = false;
        [SerializeField] private float lifeSpan = 120;

        public bool isCrafted = false;

        private Timer timer;
        private RectTransform rectTransform;

        public CraftingMaterialType MyCraftingMaterialType { get => myCraftingMaterialType; set => myCraftingMaterialType = value; }
        public bool IsSelected { get => isSelected; set => isSelected = value; }

        private void Start()
        {
            CheckForOverpopulation();

            rectTransform = GetComponent<RectTransform>();

            AssignMaterial();
            DelegateManager.AvoidOverlap(DelegateManager.allUIObjects, DelegateManager.minDistance, GetComponent<RectTransform>());

            if (isDecaying)
            {
                timer = GetComponent<Timer>();
                timer.startCountDown(lifeSpan);
            }
        }

        private void Update()
        {
            float x = Mathf.Abs(rectTransform.anchoredPosition.x);
            float y = Mathf.Abs(rectTransform.anchoredPosition.y);

            if (x > 2000 && x < 5000 ||
                y > 2000 && y < 5000)
            {
                Destroy(gameObject);
            }
        }

        private void OnDrawGizmos()
        {
            AssignMaterial();
        }

        private void AssignMaterial()
        {
            if (shouldAssignNames)
            {
                name = myCraftingMaterialType.materialName;
                myText.text = myCraftingMaterialType.materialName;
                atomicNumber.text = (myCraftingMaterialType.atomicNumber).ToString();
                shouldAssignNames = false;
            }
        }

        private void CheckForOverpopulation()
        {
            int randomIndex = Random.Range(0, 20);

            if(randomIndex < DelegateManager.allUIObjects.Items.Count &&
                !isCrafted)
            {
                Debug.Log("I Died during Childbirth!");
                Destroy(gameObject);
            }
        }
    }
}