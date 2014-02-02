using UnityEngine;
using System.Collections;

public class SecondaryCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
		camera.enabled = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (GameObject.Find("Main Camera").GetComponent<Camera>().zMode)
		{
			camera.enabled = true;
		}
		else
		{
			camera.enabled = false;
		}
	}
}
