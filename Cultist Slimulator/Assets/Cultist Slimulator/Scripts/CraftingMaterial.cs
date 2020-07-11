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

        [SerializeField] private CraftingMaterialType myCraftingMaterialType;

        [SerializeField] private bool isSelected = false;

        [SerializeField] private bool isDecaying = false;
        [SerializeField] private float lifeSpan = 120;

        private Timer timer;

        public CraftingMaterialType MyCraftingMaterialType { get => myCraftingMaterialType; set => myCraftingMaterialType = value; }
        public bool IsSelected { get => isSelected; set => isSelected = value; }

        private void Start()
        {
            AssignMaterial();
            DelegateManager.AvoidOverlap(DelegateManager.allUIObjects, DelegateManager.minDistance, GetComponent<RectTransform>());

            if (isDecaying)
            {
                timer = GetComponent<Timer>();
                timer.startCountDown(lifeSpan);
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
                transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = myCraftingMaterialType.materialName;
                shouldAssignNames = false;
            }
        }
    }
}