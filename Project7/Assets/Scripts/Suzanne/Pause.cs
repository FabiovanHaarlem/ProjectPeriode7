using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    private float s_TimerStartPausePressed;
    private float s_StartTimer;
    [SerializeField] private GameObject s_Text;
    [SerializeField] private PauseMenu s_Menu;
    public bool s_IsPaused;

    private void LateUpdate()
    {
        Debug.Log("uhm : " + s_TimerStartPausePressed);
        Debug.Log("timer : " + s_StartTimer);


        //check if button is pushed for atleast 5 seconds.
        if (s_StartTimer > 5)
        {
            s_IsPaused = true;
            s_Text.SetActive(true);
            s_TimerStartPausePressed = 0;
            s_StartTimer = 0;
        }
        if(s_IsPaused == false)
        {
            s_Text.SetActive(false);
        }

    }
    public void OnMouseDown()
    {
        if (s_IsPaused == false)
        {
            s_TimerStartPausePressed = Time.time;
        }
    }
    public void OnMouseUp()
    {
        if (s_IsPaused == false)
        {
            s_StartTimer = Time.time - s_TimerStartPausePressed;
        }
    }

}
