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

	public float fireRate;
	private float nextFire;

	private AudioSource audioSource;

	public bool poweredUpSpread;
	public float initPowerUpTime;
	public float powerUpTime;
	
	void Start()
	{
		rb = GetComponent<Rigidbody> ();
		audioSource = GetComponent<AudioSource> ();
		poweredUpSpread = false;
		powerUpTime = 0;
	}

	void Update()
	{

		if (poweredUpSpread == true) {

			powerUpTime = initPowerUpTime;
			poweredUpSpread = false;
		}
		if (powerUpTime > 0) {

			powerUpTime -= Time.deltaTime;
		}
		if (powerUpTime < 0) {

			powerUpTime = 0;
		}

		if (Input.GetKey(KeyCode.Space) && Time.time > nextFire)
		{
			if (powerUpTime == 0) {

				nextFire = Time.time + fireRate;	
				Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
				audioSource.Play ();
			} else if (powerUpTime > 0) {
				
				SpreadShot ();
			}


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
		nextFire = Time.time + fireRate;
		Instantiate (spreadShot, shotSpawn.position, shotSpawn.rotation);
		Instantiate (spreadShot, shotSpawn.position, leftRotation);
		Instantiate (spreadShot, shotSpawn.position, rightRotation);
		audioSource.Play ();

	}
}
