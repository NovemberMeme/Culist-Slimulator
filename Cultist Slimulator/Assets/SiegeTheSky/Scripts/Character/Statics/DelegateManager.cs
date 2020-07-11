using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace SiegeTheSky
{
    public static class DelegateManager
    {
        #region Fields and Statics

        // Audio

        public static AudioSource audioPlayer;

        // Hover Info Panel
        public static GameObject hoveredObject;
        public static GameObject infoPanel;

        // Rift

        public delegate void SpawnFromObjectPooler(GameObject poolabledObject, Vector3 startPosition);
        public static SpawnFromObjectPooler spawnFromObjectPooler;

        // Crafting

        public static GameObject marker;

        public delegate void DeselectAll();
        public static DeselectAll deselectAll;

        #endregion

        #region General Stuffs

        static DelegateManager()
        {

        }

        #endregion

        #region Detection

        public static List<Transform> GetObjectList(ThingRuntimeSet thingRuntimeSet, float detectionRadius, Vector3 origin)
        {
            if (thingRuntimeSet.Items.Count < 1)
                return null;

            List<Transform> things = new List<Transform>();

            for (int i = 0; i < thingRuntimeSet.Items.Count; i++)
            {
                Transform thing = thingRuntimeSet.Items[i].transform;

                if (thing == null)
                    continue;

                if (Vector3.Distance(origin, thing.transform.position) <= detectionRadius)
                    things.Add(thing);
            }

            if (things.Count > 0)
                return things;
            else
                return null;
        }

        public static Transform GetNearestObject(ThingRuntimeSet thingRuntimeSet, float detectionRadius, Vector3 origin)
        {
            if (thingRuntimeSet.Items.Count < 1)
                return null;

            List<Transform> things = new List<Transform>();

            for (int i = 0; i < thingRuntimeSet.Items.Count; i++)
            {
                Transform thing = thingRuntimeSet.Items[i].transform;

                if (thing == null)
                    continue;

                if (Vector3.Distance(origin, thing.transform.position) <= detectionRadius)
                    things.Add(thing);
            }

            things = things.OrderBy(x => Vector3.Distance(origin, x.transform.position)).ToList();

            if (things.Count > 0)
                return things[0];
            else
                return null;
        }

        #endregion

        #region Audio

        public static void PlayAudio(AudioClip clipToPlay)
        {
            audioPlayer.clip = clipToPlay;

            audioPlayer.Play();
        }

        public static void PlayAudio(AudioSource audioSource, AudioClip clipToPlay)
        {
            audioSource.clip = clipToPlay;

            audioSource.Play();
        }

        #endregion
    }
}