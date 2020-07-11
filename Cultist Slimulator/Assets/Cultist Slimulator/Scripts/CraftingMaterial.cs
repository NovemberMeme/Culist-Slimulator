using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SiegeTheSky;

namespace Slimulator
{
    public class CraftingMaterial : MonoBehaviour
    {
        [SerializeField] private CraftingMaterialType myCraftingMaterialType;

        [SerializeField] private bool isCursed = false;

        [SerializeField] private bool isSelected = false;

        public CraftingMaterialType MyCraftingMaterialType { get => myCraftingMaterialType; set => myCraftingMaterialType = value; }
        public bool IsSelected { get => isSelected; set => isSelected = value; }

        private void Start()
        {
            AssignMaterial();
        }

        private void OnDrawGizmos()
        {
            AssignMaterial();
        }

        private void AssignMaterial()
        {
            name = myCraftingMaterialType.ToString();
        }

        public bool IsCursed()
        {
            return isCursed;
        }
    }
}