using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tank_script : MonoBehaviour {

	public GameObject shell;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void fire(){
		RaycastHit hit;
		Physics.Raycast (transform.position + transform.forward * 1.1F, transform.forward, out hit, 16);
		if (hit.collider != null) {
			showShell (hit.collider.gameObject);
		}
	}

	void showShell(GameObject thingThatGotHit){
		GameObject shell_instance = (GameObject)Instantiate (shell, transform.position + transform.forward * 1.1F, transform.rotation);
		shell_instance.GetComponent<shell_script> ().target = thingThatGotHit;
	}
}
