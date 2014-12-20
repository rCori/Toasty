using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameControler : MonoBehaviour {


	public controls controls;
	public Text count;
	public GUIText time;

	public Slider butterSlider;
	public Slider ripSlider;
	public Text ripCount;

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
			count.text = controls.grid.d_count + "%";
			currTime += Time.deltaTime;
			//Implicit type conversions like a scrublord
			time.text = "" + (endTime - currTime);
			finalScore = controls.grid.d_count;

		}
		else
		{
			time.text = "Game Over. Click to end";
			gameOver = true;
		}

		//Check for tearing the bread
		bool isTear = controls.DetectTear ();
		if (isTear) {
			tearCount++;
			ripSlider.value = tearCount;
			ripCount.text = tearCount + "%";
		}

		//End the game if need be
		if (Input.GetMouseButtonDown (0)) 
		{
			if(gameOver)
			{
				Application.LoadLevel ("titleScreen");
			}
		}

		butterSlider.value = controls.grid.d_count;
	}
}
