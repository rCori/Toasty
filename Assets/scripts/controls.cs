using UnityEngine;
using System.Collections;

public class controls : MonoBehaviour {
	
	Vector3 planeNormal = new Vector3 (0, 1, 0);
	float planeDist = -1.0f;

	public GameObject bread;
	public Grid grid;

	bool mouseIsDown;

	//Keep track of the last xPosition and zPOsition of the knife to see if we are causing tears over time
	private float xMovement;
	private float zMovement;

	//Need a timer for tears
	private float timeLimit;
	private float timer;

	//Did a tear occur?
	private bool isTear;

	void Start()
	{
		grid = new Grid(bread);
		mouseIsDown = false;
		xMovement = 0;
		zMovement = 0;
		//You can only make one new tear per 3 seconds,
		timeLimit = 0.5f;
		timer = 0.0f;
		isTear = false;
	}

	// Update is called once per frame
	void Update () {
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		Plane restriction = new Plane (planeNormal, planeDist);
		float rayDist = 0;
		if (restriction.Raycast (ray, out rayDist))
		{
			if(mouseIsDown)
			{
				xMovement+= Mathf.Abs(transform.position.x - ray.GetPoint(rayDist).x);
				zMovement+= Mathf.Abs(transform.position.z - ray.GetPoint(rayDist).z);
			}
			transform.position = ray.GetPoint(rayDist);
		}

		timer += Time.deltaTime;
		if(timer > timeLimit){
			Debug.Log(xMovement);
			isTear = determineTear(xMovement,zMovement);
			timer = 0.0f;
			xMovement = 0.0f;
			zMovement = 0.0f;
		}
	}

	//Bulletproof
	void OnMouseDown()
	{
		mouseIsDown = true;
	
	}

	void OnMouseUp()
	{
		mouseIsDown = false;
	}

	void OnMouseDrag()
	{
		Vector3 butterPos = new Vector3(this.gameObject.transform.position.x,0.2f,this.gameObject.transform.position.z + 3);
		if (!grid.CheckInCoords(butterPos.x, butterPos.z)) {
			GameObject butter = (GameObject)Instantiate (Resources.Load ("Butter Square"));
			butter.transform.position = butterPos;
			grid.SetInCoords (butterPos.x, butterPos.z);
			grid.UpdateCount ();
		}
	}

	public bool DetectTear()
	{
		if (isTear) {
			isTear = false;
			return true;
		} else {
			return false;
		}
	}


	//Use this internally to do computation on wether there is a tear or not
	bool determineTear(float xMovement, float zMovement)
	{
		if (xMovement >= 3.0 || zMovement >= 3.0) {
				return true;
		} else {
				return false;
		}
	}
}
