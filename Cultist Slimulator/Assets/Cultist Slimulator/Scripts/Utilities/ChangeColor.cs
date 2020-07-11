using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace cc.tools
{
    public class ChangeColor : MonoBehaviour
    {
        Image img;
        public Color tint = Color.white;
        void Start()
        {
            img = GetComponent<Image>();
        }

        public void colorChange()
        {
            img.color = tint;
        }
    }

}
