using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WeaponState : ScriptableObject
{
    public bool playerState;
    public List<string> inputKeys = new List<string>();

    public bool affectsMovespeed;
    public float movespeedMultiplier;


}
