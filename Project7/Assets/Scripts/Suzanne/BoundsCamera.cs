﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsCamera : MonoBehaviour {

    Vector3 screensize;
    public GameObject Right;
    public GameObject left;

    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        screensize = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        screensize = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        Right.transform.position = new Vector2(screensize.x + 0.65f, 0);
        left.transform.position = new Vector2((screensize.x - 0.65f) - Camera.main.orthographicSize * Camera.main.aspect * 1f, 0);
    }
}
