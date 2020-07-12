using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SiegeTheSky;

namespace Slimulator
{
    [CreateAssetMenu(fileName = "New Recipe Type", menuName = "New Recipe Type")]
    public class RecipeType : ScriptableObject
    {
        [SerializeField] private List<CraftingMaterialType> recipe = new List<CraftingMaterialType>();

        [SerializeField] private GameObject craftedObject;

        public GameObject CraftedObject { get => craftedObject; set => craftedObject = value; }

        public List<int> GetRecipe()
        {
            List<int> intRecipe = new List<int>();

            for (int i = 0; i < recipe.Count; i++)
            {
                intRecipe.Add((int)recipe[i].atomicNumber);
            }

            return intRecipe;
        }
    }
}