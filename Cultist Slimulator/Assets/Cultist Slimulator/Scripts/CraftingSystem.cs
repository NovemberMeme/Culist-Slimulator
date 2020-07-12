using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SiegeTheSky;

namespace Slimulator
{
    public enum CraftingMode
    {
        Fusion,
        Microwave
    }

    public class CraftingSystem : MonoBehaviour
    {
        [Header("Drag Drop Logic: ")]

        [SerializeField] private Canvas _DragDropCanvas;
        [SerializeField] private float _DragAlpha;
        [SerializeField] private float _minDistance = 10;

        [SerializeField] private float _minRandomDistance = 100;
        [SerializeField] private float _maxRandomDistance = 200;

        [Header("Crafting Logic: ")]

        [SerializeField] private CraftingMode craftingMode = CraftingMode.Fusion;

        [SerializeField] private Image craftingButtonImage;

        [SerializeField] private Sprite fusionSprite;
        [SerializeField] private Sprite microwaveSprite;

        [SerializeField] private GameObject _marker;
        [SerializeField] private GameObject _spawner;

        [SerializeField] private Transform _parentPanel;

        [SerializeField] private ThingRuntimeSet allWorldObjects;
        [SerializeField] private ThingRuntimeSet _allUIObjects;

        [SerializeField] private List<GameObject> _currentCraftingSlots = new List<GameObject>();
        [SerializeField] private List<GameObject> _currentCraftingMaterials = new List<GameObject>();
        [SerializeField] private List<GameObject> allCraftingMaterials = new List<GameObject>();
        [SerializeField] private List<RecipeType> allCraftRecipes = new List<RecipeType>();

        private void Awake()
        {
            DelegateManager.updateCurrentCraftingMaterials += UpdateCraftingMaterials;

            DelegateManager.dragDropCanvas = _DragDropCanvas;
            DelegateManager.dragAlpha = _DragAlpha;
            DelegateManager.allUIObjects = _allUIObjects;
            DelegateManager.minDistance = _minDistance;
            DelegateManager.marker = _marker;
            DelegateManager.parentPanel = _parentPanel;
            DelegateManager.currentCraftingMaterials = _currentCraftingMaterials;

            DelegateManager.minRandomDistance = _minRandomDistance;
            DelegateManager.maxRandomDistance = _maxRandomDistance;

            DelegateManager.spawner = _spawner;
        }

        private void Update()
        {
            
        }

        #region Crafting Code

        private void UpdateCraftingMaterials()
        {
            _currentCraftingMaterials = DelegateManager.currentCraftingMaterials;
        }

        public void AttemptCraft()
        {
            List<int> currentRecipe = new List<int>();

            if (_currentCraftingMaterials.Count < 1)
                return;

            for (int i = 0; i < _currentCraftingMaterials.Count; i++)
            {
                if (_currentCraftingMaterials[i].GetComponent<CraftingMaterial>() == null)
                    return;

                currentRecipe.Add(_currentCraftingMaterials[i].GetComponent<CraftingMaterial>().MyCraftingMaterialType.atomicNumber);
            }

            switch (craftingMode)
            {
                case CraftingMode.Fusion:
                    CheckRecipe(currentRecipe);
                    FusionCraft(currentRecipe);
                    break;
                case CraftingMode.Microwave:
                    MicrowaveCraft(currentRecipe);
                    break;
            }
        }

        private void MicrowaveCraft(List<int> _currentRecipe)
        {
            for (int i = 0; i < _currentRecipe.Count; i++)
            {
                ConsumeSpecificType(_currentRecipe[i]);
            }

            FixCraftingList();
        }

        private void MassCraft(int atomicNumber, int amount)
        {
            while(amount > 3 && atomicNumber < allCraftingMaterials.Count)
            {
                atomicNumber++;
                amount /= 2;
            }

            Craft(atomicNumber);
        }

        private void ConsumeSpecificType(int _atomicNumber)
        {
            List<GameObject> allOfType = new List<GameObject>();

            for (int i = 0; i < DelegateManager.allUIObjects.Items.Count; i++)
            {
                if(DelegateManager.allUIObjects.Items[i].GetComponent<CraftingMaterial>().MyCraftingMaterialType.atomicNumber == _atomicNumber)
                {
                    allOfType.Add(DelegateManager.allUIObjects.Items[i].gameObject);
                }
            }

            MassCraft(_atomicNumber, allOfType.Count);

            for (int i = allOfType.Count - 1; i >= 0; i--)
            {
                Destroy(allOfType[i]);
            }
        }

        private void CheckRecipe(List<int> _currentRecipe)
        {
            for (int i = 0; i < allCraftRecipes.Count; i++)
            {
                if (DoRecipesMatch(_currentRecipe, allCraftRecipes[i].GetRecipe()))
                {
                    Craft(allCraftRecipes[i]);
                    return;
                }
            }
        }

        private void FusionCraft(List<int> _currentRecipe)
        {
            int totalAtomicPower = 0;

            for (int i = 0; i < _currentRecipe.Count; i++)
            {
                totalAtomicPower += (int)Mathf.Pow(2, _currentRecipe[i]);
            }

            _currentRecipe.Sort();

            int highestAtomicPower = (int)Mathf.Pow(2, _currentRecipe[0]);

            List<int> elementsToCraft = new List<int>();

            if(totalAtomicPower >= 2 * highestAtomicPower)
            {
                elementsToCraft.Add(_currentRecipe[0] + 1);
            }
            else
            {
                elementsToCraft.Add(_currentRecipe[0]);
            }

            int repeatTimes = _currentRecipe.Count;

            int coinFlip = Random.Range(0, 2);

            if(coinFlip > 0)
            {
                repeatTimes++;
            }

            for (int i = 0; i < repeatTimes; i++)
            {
                elementsToCraft.Add(RandomTraceElement(elementsToCraft[0]));
            }

            CraftMultiple(elementsToCraft);

            FixCraftingList();
        }

        private int RandomTraceElement(int atomicNumber)
        {
            atomicNumber = (int)Mathf.Ceil(atomicNumber / 2);

            return Random.Range(1, atomicNumber + 1);
        }

        private void CraftMultiple(List<int> toCraftList)
        {
            for (int i = 0; i < toCraftList.Count; i++)
            {
                Craft(toCraftList[i]);
            }
        }

        private void Craft(int atomicNumber)
        {
            Debug.Log(atomicNumber);
            GameObject craftedObject = Instantiate(allCraftingMaterials[atomicNumber-1], DelegateManager.marker.transform.position, Camera.main.transform.rotation);

            CraftingMaterial craftedMaterial = craftedObject.GetComponent<CraftingMaterial>();
            craftedMaterial.isCrafted = true;

            craftedObject.transform.SetParent(DelegateManager.parentPanel);
            craftedObject.GetComponent<RectTransform>().localScale = Vector3.one;
        }

        private void Craft(RecipeType objectToCraft)
        {
            // Marker

            GameObject craftedObject = Instantiate(objectToCraft.CraftedObject, DelegateManager.marker.transform.position, Camera.main.transform.rotation);

            CraftingMaterial craftedMaterial = craftedObject.GetComponent<CraftingMaterial>();
            craftedMaterial.isCrafted = true;

            craftedObject.transform.SetParent(DelegateManager.parentPanel);
            craftedObject.GetComponent<RectTransform>().localScale = Vector3.one;

            FixCraftingList();
        }

        private void FixCraftingList()
        {
            //Debug.Log("Fixed Crafting List");

            ////Module newModule = craftedObject.GetComponent<Module>();

            ////if (newModule == null)
            ////    return;

            ////newModule.RecipeType = objectToCraft;

            for (int i = _currentCraftingMaterials.Count - 1; i >= 0; i--)
            {
                //Debug.Log(_currentCraftingMaterials[i].name);
                //Destroy(_currentCraftingMaterials[i]);
                Destroy(_currentCraftingMaterials[i]);
                //_currentCraftingMaterials[i].SetActive(false);
            }

            DeselectCraftingMaterials();
        }

        private bool DoRecipesMatch(List<int> list1, List<int> list2)
        {
            if (list1.Count != list2.Count)
                return false;

            list1.Sort();
            list2.Sort();

            for (int i = 0; i < list1.Count; i++)
            {
                if (list2[i] != list1[i])
                {
                    return false;
                }
            }

            return true;
        }

        private void DeselectCraftingMaterials()
        {
            UnHighlightCraftingMaterials();

            _currentCraftingMaterials.Clear();
        }

        private void HighlightCraftingMaterials()
        {
            for (int i = 0; i < _currentCraftingMaterials.Count; i++)
            {
                ////DelegateManager.OnClickSelect(_currentCraftingMaterials[i].transform);
            }
        }

        private void UnHighlightCraftingMaterials()
        {
            //for (int i = 0; i < _currentCraftingMaterials.Count; i++)
            //{
            //    DelegateManager.StopClickSelect(_currentCraftingMaterials[i].transform);
            //}

            DelegateManager.deselectAll?.Invoke();
        }

        public void SetCraftingMode(int _craftingModeIndex)
        {
            craftingMode = (CraftingMode)_craftingModeIndex;
            switch (_craftingModeIndex)
            {
                case 0:
                    craftingButtonImage.sprite = fusionSprite;
                    break;
                case 1:
                    craftingButtonImage.sprite = microwaveSprite;
                    break;
            }
        }

        #endregion
    }
}