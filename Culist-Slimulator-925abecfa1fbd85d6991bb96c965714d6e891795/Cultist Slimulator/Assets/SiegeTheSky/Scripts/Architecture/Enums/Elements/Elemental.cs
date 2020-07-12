using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elemental : MonoBehaviour
{
    [Tooltip("Element represented by this elemental.")]
    public AttackElement Element;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Elemental e = other.gameObject.GetComponent<Elemental>();
        if (e != null)
        {
            if (e.Element.DefeatedElements.Contains(Element))
                Destroy(gameObject);
        }
    }
}
