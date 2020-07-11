using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SiegeTheSky
{
    public class Rift : MonoBehaviour
    {
        [SerializeField] private bool testBool = false;

        [SerializeField] private Vector3 spawnOffset = new Vector3(0, -3, 0);

        [SerializeField] private List<GameObject> spawnTableList = new List<GameObject>();
        [SerializeField] private List<int> weightedSpawnChances = new List<int>();

        [SerializeField] private int totalWeight;

        private void Start()
        {
            InitializeSpawnTable();
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

                    DelegateManager.spawnFromObjectPooler(spawnTableList[i].gameObject, transform.position + spawnOffset);

                    return;
                }
                else
                {
                    randomNumber -= weightedSpawnChances[i];
                }
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