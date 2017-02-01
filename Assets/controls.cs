using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controls : MonoBehaviour {
	public GameObject tank_a1;
	public GameObject tank_a2;
	public GameObject tank_b1;
	public GameObject tank_b2;

	private int gamePhase; // 0 for planning and 1 for movement

	public Vector3 destination;

	tank_script tank_a1_script;
	tank_script tank_a2_script;
	tank_script tank_b1_script;
	tank_script tank_b2_script;
	tank_script current_tank_script;

	// Use this for initialization
	void Start () {
		// do things
		gamePhase = 0;
		tank_a1_script = (tank_script)tank_a1.GetComponent (typeof(tank_script));
		tank_a2_script = (tank_script)tank_a2.GetComponent (typeof(tank_script));
		tank_b1_script = (tank_script)tank_b1.GetComponent (typeof(tank_script));
		tank_b2_script = (tank_script)tank_b2.GetComponent (typeof(tank_script));
	}
	
	// Update is called once per frame
	void Update () {
		// game phase 0, the phase to collect commands
		if(gamePhase == 0){
			print ("player 1 input");
			if (Input.GetKeyDown("1")) {
				current_tank_script = tank_a1_script;
			}
			if (Input.GetKeyDown("2")) {
				current_tank_script = tank_a2_script;
			}
			if (Input.GetKeyDown("f")) {
				if (current_tank_script != null) {
					if (current_tank_script.actions.Count < 6) {
						current_tank_script.actions.Enqueue ("fire");
						current_tank_script.actions.Enqueue ("dummy");
					}
				}
			}
			if (Input.GetMouseButtonDown(0)) {

				if (current_tank_script != null) {
					var plane = new Plane (Vector3.up, transform.position);
					var ray = Camera.main.ScreenPointToRay (Input.mousePosition);
					float distance;
					if (plane.Raycast (ray, out distance)) {
						// some point of the plane was hit - get its coordinates
						destination = ray.GetPoint (distance);
						destination.y = 0.5F;
						if (current_tank_script.actions.Count < 6) {
							current_tank_script.actions.Enqueue ("move");
							current_tank_script.actions.Enqueue (destination);
						}
					}
				}
			}
			if (Input.GetKeyDown ("space")) {
				gamePhase = 1;
			}
		}

		if (gamePhase == 1) {
			print ("Player 2 input");
			if (Input.GetKeyDown("1")) {
				current_tank_script = tank_b1_script;
			}
			if (Input.GetKeyDown("2")) {
				current_tank_script = tank_b2_script;
			}
			if (Input.GetKeyDown("f")) {
				if (current_tank_script != null) {
					if (current_tank_script.actions.Count < 6) {
						current_tank_script.actions.Enqueue ("fire");
						current_tank_script.actions.Enqueue ("dummy");
					}
				}
			}
			if (Input.GetMouseButtonDown(0)) {

				if (current_tank_script != null) {
					var plane = new Plane (Vector3.up, transform.position);
					var ray = Camera.main.ScreenPointToRay (Input.mousePosition);
					float distance;
					if (plane.Raycast (ray, out distance)) {
						// some point of the plane was hit - get its coordinates
						destination = ray.GetPoint (distance);
						destination.y = 0.5F;
						if (current_tank_script.actions.Count < 6) {
							current_tank_script.actions.Enqueue ("move");
							current_tank_script.actions.Enqueue (destination);
						}
					}
				}
			}
			if (Input.GetKeyDown ("return")) {
				gamePhase = 2;
			}
		}

		// game phase 1, the pahse to call execute for the commands
		if (gamePhase == 2) {
			tank_a1_script.doActions ();
			tank_a2_script.doActions ();
			tank_b1_script.doActions ();
			tank_b2_script.doActions ();
			gamePhase = 3;
		}

		// game phase 2, the phase where we wait for the tanks to stop doing things
		if(gamePhase == 3){
			if (!tank_a1_script.isActive && !tank_a2_script.isActive && !tank_b1_script.isActive && !tank_b2_script.isActive) {
				gamePhase = 0;
				current_tank_script = null;
			}
		}
	}
}
