using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touches : MonoBehaviour {

    [SerializeField] private Node s_Node;

    void Start () {
		
	}
	
	void Update ()
    {
		if(Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            s_Node.IsMiddleMusicNote();
            s_Node.s_IfNotPressed = false;
        }
	}
}
