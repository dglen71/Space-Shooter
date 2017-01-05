using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {

	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue;
	private GameController gameController;
	public bool destroyed;

	//private PlayerController player;

	// Use this for initialization
	void Start () {

		destroyed = false;
		//GameObject playerObject = GameObject.FindWithTag ("Player");
		//player = playerObject.GetComponent<PlayerController> ();
		
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent<GameController> ();
		}
		if (gameControllerObject == null) {

			Debug.Log ("Cannot find 'GameController' script!");
		}
	}

	void OnTriggerEnter(Collider other)
	{	
		
		if (other.tag == "Boundary") {
			return;
		}

		if (other.tag == "Player") {
			Instantiate (playerExplosion, other.transform.position, other.transform.rotation);
			gameController.GameOver ();

		}

		if (other.tag == "Bolt") {
			destroyed = true;
		}

		gameController.AddScore (scoreValue);
		Instantiate(explosion, transform.position, transform.rotation);
		Destroy (other.gameObject);


	}
	
	// Update is called once per frame at the end
	void LateUpdate () {
		//Debug.Log (destroyed);

		if (destroyed == true) {
			
			Destroy (gameObject);
		}
		
	}
}
