using UnityEngine;
using System.Collections;

public class Grid  {

	//Lets shelve this idea for now and see if we can't try something way easier.

	public GameObject d_breadObj;
	public Bounds d_bounds;
	public Vector3 d_size;
	//The height and width of 
	public float d_unitHeight;
	public float d_unitWidth;
	public int[,] d_grid;
	public int d_count;
	public Grid(GameObject i_breadObj)
	{
		d_breadObj = i_breadObj;
		d_bounds = i_breadObj.GetComponent<MeshRenderer>().bounds;
		//I also need size
		d_size = d_bounds.size;
		//Debug.Log("Z size of bread is " + d_size.z);
		//Debug.Log("X size of bread is " + d_size.x);
		d_unitHeight = d_size.z/10;
		//Debug.Log ("unit height of bread is " + d_unitHeight);
		d_unitWidth = d_size.x/10;
		//Debug.Log ("unit width of bread is " + d_unitWidth);
		d_grid = new int[10, 10];

		d_count = 0;
		//More debugging info
		//Debug.Log ("X bounds max - min is " + (d_bounds.max.x - d_bounds.min.x));
		//Debug.Log ("Z bounds max - min is " + (d_bounds.max.z - d_bounds.min.z));
		//That all cheked out to be congruent with size.
	}

	//Take the position in world space and mark that index in the grid
	//This is some broken ass code but we can work with it
	public void SetInCoords(float x, float z)
	{
		int xClamped = CoordToGrid (x, true);
		int zClamped = CoordToGrid (z, false);
		d_grid [xClamped, zClamped] = 1;
	}


	public bool CheckInCoords(float x, float z)
	{
		int xClamped = CoordToGrid (x, true);
		int zClamped = CoordToGrid (z, false);
		if (d_grid [xClamped, zClamped] == 1){
			return true;
		} 
		else 
		{
			return false;
		}
	}

	public void UpdateCount()
	{
		d_count = 0;
		//this is really dumb
		for (int k=0; k < d_grid.GetLength(0); k++) 
		{
			for (int l=0; l < d_grid.GetLength(1); l++) 
			{
				if (d_grid [k, l] == 1) 
				{
					d_count++;
				}
			}
		}
	}

	//Transforms a screen coordinate to valid position ont he grid if possible
	private int CoordToGrid(float i_coord, bool i_isXCoord)
	{
		float position = 0;
		if (i_isXCoord) {
			position = (d_size.x - (i_coord+(0-d_bounds.min.x))) / d_unitWidth;
		} else {
			position = (d_size.z - (i_coord+(0-d_bounds.min.z))) / d_unitHeight;
		}

		int clamped = Mathf.Max ((int)position, 0);
		clamped = Mathf.Min (clamped, 9);

		return clamped;
	}
}
