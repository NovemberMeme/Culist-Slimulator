using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace cc.tools
{
    public class MainMenuMicrowave : MonoBehaviour, IDropHandler
    {

        [SerializeField] MainMenuControl mmc;
        public UnityEvent OnStart = new UnityEvent();
        public UnityEvent OnAbout = new UnityEvent();
        public UnityEvent OnExit = new UnityEvent();
     
      
        public void OnDrop(PointerEventData eventData)
        {
            Debug.Log("frap");
            if (eventData.pointerDrag != null)
            {
               switch (mmc.type)
                {
                    case command.START:
                        OnStart?.Invoke();
                        break;
                    case command.ABOUT:
                        OnAbout?.Invoke();
                        break;
                    case command.EXIT:
                        OnExit?.Invoke();
                        break;

                    default:
                        return;
                        

                }
            }

        }

    }


}

