using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

	private Rigidbody rb;
	public float speed;
	private GameController gameController;

	// Use this for initialization
	void Start () {

		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {

			gameController = gameControllerObject.GetComponent<GameController> ();

		} else if (gameControllerObject == null) {

			Debug.Log ("Cannot Find Game Controller!");
		}


		rb = GetComponent<Rigidbody> ();
		if (tag != "Hazard") {
			
			rb.velocity = transform.forward * speed;

		} else if (tag == "Hazard") {

			rb.velocity = transform.forward * gameController.initHazardSpeed;
		}

		
	}
	
	// Update is called once per frame
	void Update () {

	
	}
}
