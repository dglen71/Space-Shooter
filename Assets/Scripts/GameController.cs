using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public GameObject hazard;
	public GameObject hazardPowerUp;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	public GUIText scoreText;
	public GUIText restartText;
	public GUIText gameOverText;
	public GUIText highscoreCountText;

	private bool gameOver;
	private bool restart;
	private int score;
	private static int highscore;

	private int index;
	public float initHazardSpeed;
	public float maxHazardSpeed;
	public float increaseSpeed;

	// Use this for initialization
	void Start () {
		
		score = 0;
		UpdateScore ();
		StartCoroutine (SpawnWaves ());

		gameOver = false;
		restart = false;

		restartText.text = "";
		gameOverText.text = "";
		highscoreCountText.text = "" + highscore;
		highscoreCountText.color = Color.grey;

		//initHazardSpeed = -5.0f;





	}

	IEnumerator SpawnWaves()
	{
		index = 0;
		yield return new WaitForSeconds (spawnWait);
		while (true) {
			
			bool alreadySpawned = false;

			if (index % 2 != 0) {
				
				hazardCount++; // Increases number of hazards in a wave by 1 every odd numbered turn
				if ((initHazardSpeed * increaseSpeed) > maxHazardSpeed) {
					
					initHazardSpeed = initHazardSpeed * increaseSpeed;
				} else if ((initHazardSpeed * increaseSpeed) <= maxHazardSpeed) {

					initHazardSpeed = maxHazardSpeed;
				}
	
			}

			for (int i = 0; i < hazardCount; i++) {

				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
			

				int spawnPowerUp = Random.Range(0,6);

				if (spawnPowerUp == 1 && alreadySpawned == false) {
					Instantiate (hazardPowerUp, spawnPosition, spawnRotation);
					alreadySpawned = true;
				} else if (spawnPowerUp != 1 || alreadySpawned == true) {
					Instantiate (hazard, spawnPosition, spawnRotation);
				}

				yield return new WaitForSeconds (spawnWait);



			}




			yield return new WaitForSeconds (waveWait);

			index++;

			if (gameOver) {

				restartText.text = "Press 'R' to Restart";
				restart = true;
				break;

			}
		}


		
	}

	public void AddScore(int newScoreValue)
	{

		score += newScoreValue;

		if (score > highscore) {
			highscore = score;
			highscoreCountText.color = Color.green;
		}

		UpdateScore ();

	}

	void UpdateScore()
	{

		scoreText.text = "Score: " + score;
		highscoreCountText.text = "" + highscore;

	}

	public void GameOver()
	{
		gameOverText.text = "Game Over!";
		gameOver = true;
	}


	
	// Update is called once per frame
	void Update () {

		if (restart) {

			if (Input.GetKeyDown (KeyCode.R)) {

				SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
				//Application.LoadLevel (Application.loadedLevel);
			}
		}
		
	}
}
