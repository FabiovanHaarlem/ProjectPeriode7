using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    private float s_TimerStartPausePressed;

    public void OnPausedPressed()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began: 
                    
                    break;

                case TouchPhase.Ended:
                    break;
            }

        }
    }
}
