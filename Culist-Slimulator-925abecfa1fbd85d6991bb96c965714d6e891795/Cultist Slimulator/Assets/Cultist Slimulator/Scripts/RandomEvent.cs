using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SiegeTheSky;

namespace Slimulator
{
    [CreateAssetMenu(fileName = "New Random Event", menuName = "New Random Event")]
    public class RandomEvent : ScriptableObject
    {
        public void MoveRandom()
        {
            int randomIndex = Random.Range(0, 2);

            switch (randomIndex)
            {
                case 0:
                    MoveRandomObject();
                    MoveSpawner();
                    break;
                case 1:
                    MoveSpawner();
                    break;
            }
        }

        private void MoveRandomObject()
        {
            if (DelegateManager.allUIObjects.Items.Count < 1)
                return;

            int randomIndex = Random.Range(0, DelegateManager.allUIObjects.Items.Count);

            DelegateManager.RandomizePosition(DelegateManager.allUIObjects.Items[randomIndex].GetComponent<RectTransform>());
        }

        private void MoveSpawner()
        {
            DelegateManager.RandomizePosition(DelegateManager.spawner.GetComponent<RectTransform>());
        }
    }
}