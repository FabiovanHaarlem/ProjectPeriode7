using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    [SerializeField]private Pause s_Bool;
    [SerializeField] private List<GameObject> s_Buttons = new List<GameObject>();
    [SerializeField] private GameObject s_Main;

	void Start ()
    {
		
	}
	
	void Update ()
    {
		if(s_Bool.s_IsPaused)
        {
            Time.timeScale = 0;
            s_Main.SetActive(true);
        }
	}
    public void Resume()
    {
        s_Bool.s_IsPaused = false;
        if(s_Bool.s_IsPaused == false)
        {
            Time.timeScale = 1;
            foreach (GameObject buttons in s_Buttons)
            {
                buttons.SetActive(false);
            }
        }
    }
    public void Options()
    {
        foreach (GameObject buttons in s_Buttons)
        {
            buttons.SetActive(false);
        }
    }
    public void Quit()
    {
        Application.Quit();
    }
}
