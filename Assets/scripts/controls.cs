using UnityEngine;
using System.Collections;

public class controls : MonoBehaviour {
	
	Vector3 planeNormal = new Vector3 (0, 1, 0);
	float planeDist = -1.0f;

	public GameObject bread;
	public Grid grid;

	bool mouseIsDown;

	void Start()
	{
		grid = new Grid(bread);
		mouseIsDown = false;
	}

	// Update is called once per frame
	void Update () {
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		Plane restriction = new Plane (planeNormal, planeDist);
		float rayDist = 0;
		if (restriction.Raycast (ray, out rayDist))
		{
			transform.position = ray.GetPoint(rayDist);
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
		Vector3 butterPos = new Vector3(this.gameObject.transform.position.x,1,this.gameObject.transform.position.z + 3);
		if (!grid.CheckInCoords(butterPos.x, butterPos.z)) {
			GameObject butter = (GameObject)Instantiate (Resources.Load ("Butter Square"));
			butter.transform.position = butterPos;
			grid.SetInCoords (butterPos.x, butterPos.z);
			grid.UpdateCount ();
		}
	}

	public bool DetectTear()
	{
		if (mouseIsDown){
			float xMove = Mathf.Abs (Input.GetAxis ("Mouse X"));
			float yMove = Mathf.Abs (Input.GetAxis ("Mouse Y"));
			if(xMove >= 0.50 || yMove >= 0.50){
				return true;
			}else{
				return false;
			}
		}
		else{
			return false;
		}

	}
}
