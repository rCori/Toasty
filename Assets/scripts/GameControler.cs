using UnityEngine;
using System.Collections;

public class GameControler : MonoBehaviour {


	public controls controls;
	public GUIText score;
	public GUIText time;
	public GUIText tears;

	bool gameOver;

	float currTime;
	float endTime;

	int finalScore;
	int tearCount;
	// Use this for initialization
	void Start ()
	{
		currTime = 0f;
		endTime = 20.0f;
		gameOver = false;
		tearCount = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (endTime - currTime > 0) {
			score.text = "Count is currently " + controls.grid.d_count;
			currTime += Time.deltaTime;
			//Implicit type conversions like a scrublord
			time.text = "" + (endTime - currTime);
			finalScore = controls.grid.d_count;

		}
		else
		{
			score.text = "Game Over. Final score was: " + finalScore;
			time.text = "Click to end";
			gameOver = true;
		}

		//Check for tearing the bread
		bool isTear = controls.DetectTear ();
		if (isTear) {
			tearCount++;
			tears.text = "Tears made in bread :" + tearCount;
		}

		//End the game if need be
		if (Input.GetMouseButtonDown (0)) 
		{
			if(gameOver)
			{
				Application.LoadLevel ("titleScreen");
			}
		}
	}
}
