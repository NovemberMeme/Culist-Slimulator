using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BoolVariable : ScriptableObject
{
    public bool value = true;

    public void MakeTrue()
    {
        value = true;
    }

    public void MakeFalse()
    {
        value = false;
    }

    public void MakeOpposite()
    {
        value = !value;
    }
}
