using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SiegeTheSky;

namespace Slimulator
{
    [CreateAssetMenu(fileName = "New Crafting Material Type", menuName = "New Crafting Material Type")]
    public class CraftingMaterialType : ScriptableObject
    {
        public string materialName;

        public int atomicNumber;
    }
}