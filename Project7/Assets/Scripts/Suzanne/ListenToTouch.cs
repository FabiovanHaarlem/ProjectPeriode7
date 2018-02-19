using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListenToTouch : MonoBehaviour {

    public Touch[] m_Touches;
    [SerializeField] private List<Renderer> s_Node = new List<Renderer>();

	void Update ()
    {

        m_Touches = Input.touches;
		if(Input.touchCount > 0)
        {
            for(int i = 0; i < Input.touchCount; i++)
            {
                if (Input.GetTouch(i).phase == TouchPhase.Began)
                {
                    //StaticInstanceManager.m_Instance.GetNoteChecker(this);
                    //multiple input.

                    Ray ray = Camera.main.ScreenPointToRay(new Vector3(m_Touches[i].position.x, m_Touches[i].position.y));
                    RaycastHit hit;
                    
                    if (Physics.Raycast(ray, out hit))
                    {
                        //input when touched
                        Destroy(hit.transform.gameObject);
                    }
                    
                }
            }
        }
	}
    

}
