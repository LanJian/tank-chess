using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shell_script : MonoBehaviour {

	public GameObject target;
	private Vector3 origin;
	// Use this for initialization
	void Start () {
		print (target.name);
		origin = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		print (transform.position);
		transform.position += transform.forward * 0.5F;
		print (Vector3.Distance (transform.position, origin));
		print (Vector3.Distance (target.transform.position, origin));
		if (target != null) {
			if (Vector3.Distance (transform.position, origin) > Vector3.Distance (target.transform.position, origin)) {
				print ("abcdefg");
				// collision: kill everything !!! muhahahahaha
				Destroy (target);
				Destroy (gameObject);
			}
		}
	}
}
