using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace cc.tools
{
    public class Timer : MonoBehaviour
    {
        public float tic = .25f;
     
        public TextMeshProUGUI txt;
        void Start()
        {

        }

        public void startCountDown(float _d)
        {
            StartCoroutine(Countdown(_d));
        }
        IEnumerator Countdown(float duration)
        {
            float curTime = 0;
            float _duration = duration;
            txt.text = curTime.ToString();
            while (_duration>0.01)
            {
                curTime += tic;
                txt.text = string.Format("{0:0.00}", duration - curTime); 
                yield return new WaitForSeconds(tic);
                _duration -= tic;
            }

            Destroy(gameObject);
        }
        
    }

}
