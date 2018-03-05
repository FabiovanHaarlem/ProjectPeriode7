using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListenToTouch : MonoBehaviour {

    public Touch[] m_Touches;
  // [SerializeField] private List<Node> s_Node = new List<Node>();
     [SerializeField]private Node s_Node;
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
                    RaycastHit hit;
                    
                    if (Physics.Raycast(ray, out hit))
                    {
                        Debug.Log(hit.transform.gameObject);
                        //input when touched
                        //StaticInstanceManager.m_Instance.GetNoteChecker.CheckIfMiddleNote(this.gameObject);
                        s_Node.s_IfNotPressed = false;
                    }
                }
            }
        }
	}
    

}
