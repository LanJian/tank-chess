using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tank_script : MonoBehaviour {

	public GameObject shell;

	public bool moving;
	public bool firing;
	public bool isActive;

	private Vector3 destination;
	public Queue actions = new Queue();
	private GameObject shell_instance;

	// Use this for initialization
	void Start () {
		isActive = false;
		moving = false;
		firing = false;
		shell_instance = null;
	}
	
	// Update is called once per frame
	void Update () {
		if (!isActive) {
			return;
		}

		// move
		if (moving) {
			if (shouldTankMove (transform.position, destination)) {
				transform.LookAt (destination);
				transform.position += (transform.forward * 0.2F);
			} else {
				moving = false;
			}
		}

		// fire
		if (firing) {
			if (shell_instance == null) {
				firing = false;
			}
		} 

		if (isIdle ()) {
			if (actions.Count > 0) {
				doActions ();
			} else {
				isActive = false;
			}
		}
	}

	public void doActions() {
		if (actions.Count > 0) {
			isActive = true;

			string action = actions.Dequeue ().ToString ();
			if (action.Equals ("move")) {
				move ((Vector3)actions.Dequeue ());
			}
			if (action.Equals ("fire")) {
				actions.Dequeue ();
				fire ();
			}
		}
	}

	private void move(Vector3 dest) {
		if (gameObject.transform.Find ("engine") != null) {
			moving = true;
			destination = dest;		
		}
	}

	public void fire(){
		// check if we still have a turret
		if (gameObject.transform.Find ("turret") != null) {
			firing = true;
			shell_instance = (GameObject)Instantiate (shell, transform.position + transform.forward * 1.1F, transform.rotation);
		}
	}

	public bool isIdle() {
		return !moving && !firing;
	}

	bool shouldTankMove(Vector3 tankPos, Vector3 destination){
		bool result = true;
		// check x
		if (tankPos.x > (destination.x - 0.1) && tankPos.x < (destination.x + 0.1))
			//check y
		if (tankPos.z > (destination.z - 0.1) && tankPos.z < (destination.z + 0.1))
			result = false;

		return result;
	}
}
