using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    private float s_TimerStartPausePressed;
    private float s_StartTimer;
    [SerializeField] private GameObject s_Text;

    private void Update()
    {
        Debug.Log(s_TimerStartPausePressed);
    }
    public void OnMouseDown()
    {
        s_TimerStartPausePressed = Time.time;
        if (s_StartTimer > 5)
        {
            s_Text.SetActive(true);
            Debug.Log("yaay");
            s_TimerStartPausePressed = 5;
        }
    }
    public void OnMouseUp()
    {
        s_StartTimer = Time.time - s_TimerStartPausePressed;
    }

}
