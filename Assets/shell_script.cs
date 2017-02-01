using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shell_script : MonoBehaviour {

	private GameObject target;
	private Vector3 origin;
	// Use this for initialization
	void Start () {
		origin = transform.position;

		// get target
		RaycastHit hit;
		Physics.Raycast (transform.position, transform.forward, out hit);
		if (hit.collider != null) {
			target = hit.collider.gameObject;
		}
	}
	
	// Update is called once per frame
	void Update () {
		// if too far away kill the bullet
		if (Vector3.Distance (transform.position, origin) > 16) {
			if (gameObject != null) {
				Destroy (gameObject);
				return;
			}
		}
			
		if (target == null) {
			// get new target if original target is gone
			RaycastHit hit;
			Physics.Raycast (transform.position, transform.forward, out hit);
			if (hit.collider != null) {
				target = hit.collider.gameObject;
			}
		}

		transform.position += transform.forward * 0.5F;

		// if we didnt get a target, just return and let the bullet die of natural causes
		if (target == null) {
			return;
		}

		if (target != null) {
			if (Vector3.Distance (transform.position, origin) > Vector3.Distance (target.transform.position, origin)) {
				// collision: kill everything !!! muhahahahaha
				Destroy (target);
				Destroy (gameObject);
			}
		}
	}
}
