using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Slimulator;

namespace SiegeTheSky
{
    public class Rift : MonoBehaviour
    {
        [SerializeField] private bool testBool = false;

        [SerializeField] private Vector3 spawnOffset = new Vector3(0, -3, 0);

        [SerializeField] private List<RiftData> spawnTableList = new List<RiftData>();
        [SerializeField] private List<int> weightedSpawnChances = new List<int>();
        [SerializeField] private List<GameObject> _allCraftingMaterials = new List<GameObject>();

        [SerializeField] private int totalWeight;

        private void Start()
        {
            InitializeSpawnTable();
            _allCraftingMaterials = GetComponent<CraftingSystem>().AllCraftingMaterials;
        }

        private void OnDrawGizmos()
        {
            if (testBool == true)
            {
                InitializeSpawnTable();

                testBool = false;
            }
        }

        private void InitializeSpawnTable()
        {
            totalWeight = 0;

            weightedSpawnChances.Clear();

            for (int i = 0; i < spawnTableList.Count; i++)
            {
                weightedSpawnChances.Add(spawnTableList[i].spawnChance);
            }

            for (int i = 0; i < weightedSpawnChances.Count; i++)
            {
                totalWeight += weightedSpawnChances[i];
            }
        }

        public void RandomWeightedSpawn()
        {
            int randomNumber = Random.Range(0, totalWeight);

            for (int i = 0; i < weightedSpawnChances.Count; i++)
            {
                if (randomNumber <= weightedSpawnChances[i])
                {
                    // Spawn

                    // Spawn enemies at a location x units in between headquarters and this rift

                    //DelegateManager.spawnFromObjectPooler(spawnTableList[i].objectToSpawn, transform.position + spawnOffset);

                    AttemptSpawn(i);

                    return;
                }
                else
                {
                    randomNumber -= weightedSpawnChances[i];
                }
            }
        }

        private void AttemptSpawn(int _i)
        {
            if (spawnTableList[_i].objectToSpawn != null)
                Spawn(_i);

            if(spawnTableList[_i].randomEvent != null)
                spawnTableList[_i].randomEvent.MoveRandom();
        }

        public void Spawn(int _i)
        {
            if (DelegateManager.allUIObjects.Items.Count < 1)
            {
                GameObject spawnedObject = Instantiate(_allCraftingMaterials[0], DelegateManager.marker.transform.position, Camera.main.transform.rotation);
                spawnedObject.transform.SetParent(DelegateManager.parentPanel);
                spawnedObject.GetComponent<RectTransform>().localScale = Vector3.one;
            }
            else
            {
                int randomIndex = Random.Range(0, DelegateManager.allUIObjects.Items.Count);

                GameObject spawnedObject = Instantiate(DelegateManager.allUIObjects.Items[randomIndex].gameObject, DelegateManager.marker.transform.position, Camera.main.transform.rotation);
                spawnedObject.transform.SetParent(DelegateManager.parentPanel);
                spawnedObject.GetComponent<RectTransform>().localScale = Vector3.one;
            }
        }

        public void Click()
        {
            RandomWeightedSpawn();
        }

        public void Click(Vector3 pos)
        {

        }

        public void UnClickSelect()
        {

        }
    }
}