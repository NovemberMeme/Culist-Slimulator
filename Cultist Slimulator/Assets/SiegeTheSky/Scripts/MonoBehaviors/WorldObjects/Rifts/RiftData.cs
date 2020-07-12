using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Slimulator;

namespace SiegeTheSky
{
    [System.Serializable]
    public class RiftData
    {
        public GameObject objectToSpawn;
        public RandomEvent randomEvent;
        public int spawnChance;
    }
}