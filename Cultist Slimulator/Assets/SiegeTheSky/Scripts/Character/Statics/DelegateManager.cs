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
        public static List<GameObject> currentCraftingMaterials;

        public delegate void UpdateCurrentCraftingMaterials();
        public static UpdateCurrentCraftingMaterials updateCurrentCraftingMaterials;

        // Drag Drop

        public static Canvas dragDropCanvas;
        public static float dragAlpha;
        public static float minDistance;

        public delegate void DeselectAll();
        public static DeselectAll deselectAll;

        // Rift

        public static Transform parentPanel;

        // ThingRuntimeSets

        public static ThingRuntimeSet allUIObjects;

        #endregion

        #region General Stuffs

        static DelegateManager()
        {
            currentCraftingMaterials = new List<GameObject>();
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

        public static RectTransform GetNearestUIObject(ThingRuntimeSet thingRuntimeSet, float detectionRadius, RectTransform origin)
        {
            if (thingRuntimeSet.Items.Count < 1)
                return null;

            List<RectTransform> things = new List<RectTransform>();

            for (int i = 0; i < thingRuntimeSet.Items.Count; i++)
            {
                RectTransform thing = thingRuntimeSet.Items[i].GetComponent<RectTransform>();

                if (thing == null)
                    continue;

                if (thing == origin)
                    continue;

                //Debug.Log(GetDistance(origin, thing));

                if (GetDistance(origin, thing) <= detectionRadius)
                    things.Add(thing);
            }

            things = things.OrderBy(x => GetDistance(origin, x)).ToList();

            //Debug.Log(things[0].name.ToString());

            if (things.Count > 0)
                return things[0];
            else
                return null;
        }

        public static float GetDistance(RectTransform r1, RectTransform r2)
        {
            float distance = Mathf.Sqrt(Mathf.Pow(r2.anchoredPosition.x - r1.anchoredPosition.x, 2) + Mathf.Pow(r2.anchoredPosition.y - r1.anchoredPosition.y, 2));
            return distance; 
        }

        #endregion

        #region

        public static void AvoidOverlap(ThingRuntimeSet _thingRuntimeSet, float _detectionRadius, RectTransform _origin)
        {
            RectTransform nearestTransform = DelegateManager.GetNearestUIObject(_thingRuntimeSet, _detectionRadius, _origin);

            if (nearestTransform != null &&
                nearestTransform != _origin)
            {
                Debug.Log(nearestTransform.name.ToString());

                float newX = _origin.anchoredPosition.x - (_origin.anchoredPosition.x - nearestTransform.anchoredPosition.x > 0 ? _detectionRadius : _detectionRadius);
                float newY = _origin.anchoredPosition.y - (_origin.anchoredPosition.y - nearestTransform.anchoredPosition.y > 0 ? _detectionRadius : _detectionRadius);

                _origin.anchoredPosition = new Vector3(newX, newY);

                AvoidOverlap(_thingRuntimeSet, _detectionRadius, _origin);
            }
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