using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controls : MonoBehaviour {
	bool moveTankA = false;
	bool moveTankB = false;

	private GameObject currentTank;
	public GameObject tank_a1;
	public GameObject tank_a2;

	public int gamePhase = 0; // 0 for planning and 1 for movement

	public Vector3 currentDestination;
	public Vector3 destination_a1;
	public Vector3 destination_a2;

	public float angleBetween = 0.0F;
	public Transform target;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(gamePhase == 0){
			if (Input.GetKeyDown("1")) {
				currentTank = tank_a1;
			}
			if (Input.GetKeyDown("2")) {
				currentTank = tank_a2;
			}
			if (Input.GetMouseButtonDown(0)) {

				var plane = new Plane(Vector3.up, transform.position);
				var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				float distance;
				if (plane.Raycast(ray, out distance)){
					// some point of the plane was hit - get its coordinates
					currentDestination = ray.GetPoint(distance);

					// get angle to turn
					if (currentTank != null){
						if (currentTank.name == "tank_a1") {
							moveTankA = true;
							destination_a1 = currentDestination;
							destination_a1.y = 0.5F;

						}
						if (currentTank.name == "tank_a2") {
							moveTankB = true;
							destination_a2 = currentDestination;
							destination_a2.y = 0.5F;
						}
					}
				}
			}
			if (Input.GetKeyDown ("space")) {
				gamePhase = 1;
			}
		}

		if (gamePhase == 1) {
			// do things
			if (moveTankA) {
				if (shouldTankMove(tank_a1.transform.position,destination_a1)) {
					tank_a1.transform.LookAt (destination_a1);
					tank_a1.transform.position += (tank_a1.transform.forward * 0.1F);
				} else {
					moveTankA = false;
				}
			}
			if (moveTankB) {
				if (shouldTankMove(tank_a2.transform.position, destination_a2)) {
					tank_a2.transform.LookAt (destination_a2);
					tank_a2.transform.position += (tank_a2.transform.forward * 0.1F);
				} else {
					moveTankB = false;
				}
			}


			if (!moveTankA && !moveTankB) {
				tank_script script = (tank_script)tank_a1.GetComponent (typeof(tank_script));
				script.fire ();
				// reset things
				currentTank = null;

				// go to phase 0
				gamePhase = 0;
			}
		}
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
