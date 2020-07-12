using UnityEngine;
using SiegeTheSky;

namespace Slimulator
{
    public class IsVisible : MonoBehaviour
    {
        Renderer m_Renderer;
        AppearDisappear appearDisappear;
        // Use this for initialization
        void Start()
        {
            m_Renderer = GetComponent<Renderer>();
            appearDisappear = GetComponent<AppearDisappear>();
        }

        // Update is called once per frame
        void Update()
        {
            if (!m_Renderer.isVisible)
            {
                if (!appearDisappear.IsHiding)
                {
                    Destroy(gameObject);
                }
            }

        }
    }
}