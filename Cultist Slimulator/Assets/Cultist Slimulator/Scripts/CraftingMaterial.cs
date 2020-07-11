using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SiegeTheSky;
using TMPro;

namespace Slimulator
{
    public class CraftingMaterial : MonoBehaviour
    {
        [SerializeField] private bool shouldAssignNames = false;

        [SerializeField] private CraftingMaterialType myCraftingMaterialType;

        [SerializeField] private bool isSelected = false;

        public CraftingMaterialType MyCraftingMaterialType { get => myCraftingMaterialType; set => myCraftingMaterialType = value; }
        public bool IsSelected { get => isSelected; set => isSelected = value; }

        private void Start()
        {
            AssignMaterial();
            DelegateManager.AvoidOverlap(DelegateManager.allUIObjects, DelegateManager.minDistance, GetComponent<RectTransform>());
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