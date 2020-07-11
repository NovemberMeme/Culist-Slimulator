using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Void_Rain.Events
{
    [CreateAssetMenu]
    public class Event : ScriptableObject
    {
        [Tooltip("The elements that are defeated by this element.")]
        public GameEvent eventType;
    }
}

