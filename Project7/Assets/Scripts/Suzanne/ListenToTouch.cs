using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListenToTouch : MonoBehaviour {

    public Touch[] m_Touches;
  // [SerializeField] private List<Node> s_Node = new List<Node>();
     [SerializeField]private NoteChecker s_Node;
     public RaycastHit hit;
    void Update ()
    {

        m_Touches = Input.touches;
		if(Input.touchCount > 0)
        {
            for(int i = 0; i < Input.touchCount; i++)
            {
                if (Input.GetTouch(i).phase == TouchPhase.Began)
                {
                    //multiple input.
                    Ray ray = Camera.main.ScreenPointToRay(new Vector3(m_Touches[i].position.x, m_Touches[i].position.y));
                            
                    if (Physics.Raycast(ray, out hit))
                    {
                        Debug.Log(hit.transform.gameObject);
                        //input when touched

                        StaticInstanceManager.m_Instance.GetNoteChecker.CheckIfMiddleNote(hit.transform.gameObject);
                        //s_Node.CheckIfMiddleNote(hit.transform.gameObject);
                    }
                }
            }
        }
	}
    

}
