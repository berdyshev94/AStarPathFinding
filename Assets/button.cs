﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button : MonoBehaviour {

    public int but = 0;
	void Start () {
		
	}
	
	void Update () {
		
	}

    private void OnMouseUpAsButton()
    {
        if (but == 0)
        {
            Debug.Log("start1");
            main.main01.start_calc();
        }
        if (but == 1)
        {
            main.main01.clear();
        }
    }
}
