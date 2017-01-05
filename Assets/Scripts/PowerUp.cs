using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

	private PlayerController player;
	private DestroyByContact destroy;

	// Use this for initialization
	void Start () {

		GameObject playerObject = GameObject.FindWithTag ("Player");
		if (playerObject != null) {

			player = playerObject.GetComponent<PlayerController> ();
		} else if (playerObject == null) {

			Debug.Log ("Cannot Find Player!");
		}

		destroy = GetComponent<DestroyByContact> ();
		
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (destroy.destroyed);
		
		if (destroy.destroyed == true) {

			player.poweredUpSpread = true;

		}
		
	}
}
