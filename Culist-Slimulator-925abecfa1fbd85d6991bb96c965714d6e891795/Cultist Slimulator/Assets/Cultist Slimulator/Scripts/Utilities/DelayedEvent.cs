using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace cc.tools
{
    public class DelayedEvent : MonoBehaviour
    {
        public List<UnityEvent> Eventlist = new List<UnityEvent>();
      
        public void CallEvent()
        {
            if (Eventlist.Count <= 0) return;

            for (int i =0; i < Eventlist.Count;i++)
            {
              if(Eventlist[i]!=null)
              {
                Eventlist[i].Invoke();
              }
            }           
              
        }

        public void CallDelayedEvent(float delay)
        {
            StartCoroutine(Delay(delay));

        }

        IEnumerator Delay(float delay)
        {
          
          for (int i = 0; i < Eventlist.Count; i++)
          {
            if (Eventlist[i] != null)
            {
                Eventlist[i].Invoke();
            }
            yield return new WaitForSeconds(delay);

          }
            StopAllCoroutines();
           
        }

    }

}
