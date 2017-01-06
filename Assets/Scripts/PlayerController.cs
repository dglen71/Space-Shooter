using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {

	private Rigidbody rb;
	public Boundary boundary;

	public float tilt;
	public float speed;

	public GameObject shot;
	public GameObject spreadShot;
	public Transform shotSpawn;

	private float fireRate;
	private float nextFire;
	public float initFireRate;

	private AudioSource audioSource;

	public bool poweredUpSpread;
	public float initPowerUpTime;
	public float powerUpTime;

	public bool hindered;
	public float hinderFactor;
	public float initHinderTime;
	public float hinderTime;

	public Renderer render;

	public Material initMaterialMetal;
	public Material initMaterialGlass;

	public Material powerUpMaterialMetal;
	public Material powerUpMaterialGlass;

	public Material hinderMaterialMetal;
	public Material hinderMaterialGlass;

	
	void Start()
	{
		rb = GetComponent<Rigidbody> ();
		audioSource = GetComponent<AudioSource> ();
		render = GetComponent<Renderer> ();
		poweredUpSpread = false;
		powerUpTime = 0;
		hinderTime = 0;
		hindered = false;
		fireRate = initFireRate;

	}

	void Update()
	{

		//Power Up Timer Logic
		if (poweredUpSpread == true) {
			
			powerUpTime = initPowerUpTime;
			poweredUpSpread = false;
			changeMaterial (2);
		}
		if (powerUpTime > 0) {

			powerUpTime -= Time.deltaTime;
		}
		if (powerUpTime < 0) {
			
			powerUpTime = 0;
			changeMaterial (1);
		}

		//Hinder Timer Logic
		if (hindered == true) {
			
			hinderTime = initHinderTime;
			Hinder ();
			hindered = false;
			changeMaterial (3);
		}
		if (hinderTime > 0) {
			hinderTime -= Time.deltaTime;
		}
		if (hinderTime < 0) {

			hinderTime = 0;
			changeMaterial (1);
		}



		if (Input.GetKey(KeyCode.Space) && Time.time > nextFire)
		{

			if (hinderTime == 0) {
				fireRate = initFireRate;
			}

			if (powerUpTime == 0) {

				Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
				audioSource.Play ();
			} else if (powerUpTime > 0) {
				
				SpreadShot ();
			}

			nextFire = Time.time + fireRate;


		}

	}


	void FixedUpdate () {

		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rb.velocity = movement * speed;

		rb.position = new Vector3
			(
				Mathf.Clamp(rb.position.x,boundary.xMin, boundary.xMax),
				0.0f,
				Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
			);

		rb.rotation = Quaternion.Euler(0.0f,0.0f,rb.velocity.x * -tilt);
	}

	void SpreadShot()
	{
		Quaternion leftRotation = Quaternion.identity;
		leftRotation.eulerAngles = new Vector3 (0.0f, -45.0f, 0.0f);
		Quaternion rightRotation = Quaternion.identity;
		rightRotation.eulerAngles = new Vector3 (0.0f, 45.0f, 0.0f);
		Instantiate (spreadShot, shotSpawn.position, shotSpawn.rotation);
		Instantiate (spreadShot, shotSpawn.position, leftRotation);
		Instantiate (spreadShot, shotSpawn.position, rightRotation);
		audioSource.Play ();

	}

	void Hinder()
	{
		fireRate = fireRate * hinderFactor;

	}

	//1 is original, 2 is powerUp, and 3 is hinder
	void changeMaterial(int choice)
	{
		Material[] mats = render.materials;

		if (choice == 1) {

			mats [0] = initMaterialMetal;
			mats [1] = initMaterialGlass;
		}
		if (choice == 2) {

			mats [0] = powerUpMaterialMetal;
			mats [1] = powerUpMaterialGlass;
		}
		if (choice == 3) {
			mats[0] = hinderMaterialMetal;
			mats[1] = hinderMaterialGlass;
		}

		render.materials = mats;

	}
}
