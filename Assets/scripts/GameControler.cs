using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameControler : MonoBehaviour {


	public controls controls;
	public Text count;

	public Slider butterSlider;
	public Slider ripSlider;
	public Text ripCount;

	public Image clock;

	bool gameOver;

	float currTime;
	float endTime;
	
	int tearCount;
	float tearResistance;
	// Use this for initialization
	void Start ()
	{
		currTime = 0f;
		endTime = 20.0f;
		gameOver = false;
		tearCount = 0;
		tearResistance = 10.0f;
		controls.SetTearResistance (tearResistance);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (endTime - currTime > 0) {
			count.text = controls.grid.d_count + "%";
			currTime += Time.deltaTime;
			clock.fillAmount = 1 - (currTime/ endTime);

		}
		else
		{
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
