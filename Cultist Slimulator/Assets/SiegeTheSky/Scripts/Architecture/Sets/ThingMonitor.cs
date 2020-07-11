using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ThingMonitor : MonoBehaviour
{
    public ThingRuntimeSet Set;

    public TextMeshProUGUI Text;

    private int previousCount = -1;

    private void OnEnable()
    {
        UpdateText();
    }

    private void Update()
    {
        if (previousCount != Set.Items.Count)
        {
            UpdateText();
            previousCount = Set.Items.Count;
        }
    }

    public void UpdateText()
    {
        Text.text = "There are " + Set.Items.Count + " things.";
    }
}
