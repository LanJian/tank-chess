﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wall_script : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter (Collision col) {
		print ("jfkdsla");
		if (col.gameObject.tag == "tank") {
			
			Destroy (gameObject);
		}
	}
}
