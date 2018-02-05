using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteChecker : MonoBehaviour {


	void Start ()
    {
		
	}
	

	void Update ()
    {
#if UNITY_ANDROID
        for (var i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position), Vector2.zero);

                if (hitInfo)
                {
                    
                }
            }
        }
#endif

        if (Input.GetMouseButtonDown(0))
        {
            //Raycast();
        }
    }

    //private void Raycast()
    //{
    //    Ray ray = (Camera.main.ScreenPointToRay(Input.mousePosition));

    //    RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
    //    //Physics.Raycast(ray, out hit, Mathf.Infinity);

    //    Debug.DrawRay(Camera.main.transform.position, ray.direction * 50, Color.red);

       
    //}
}
