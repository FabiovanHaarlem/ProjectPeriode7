using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListenToTouch : MonoBehaviour {

    public Touch[] m_Touches;
    [SerializeField] private GameObject s_Player;
    [SerializeField] private Transform s_Startpos;
	void Start ()
    {
		
	}
	
	void Update ()
    {
        m_Touches = Input.touches;
		if(Input.touchCount > 0)
        {
            for(int i =0; i < Input.touchCount; i++)
            {
                if (Input.GetTouch(i).phase == TouchPhase.Began)
                {
                    //StaticInstanceManager.m_Instance.GetNoteChecker(this);
                    //multiple input.
                    //Instantiate(s_Player, s_Startpos);

                }
            }
        }
	}
    
}
