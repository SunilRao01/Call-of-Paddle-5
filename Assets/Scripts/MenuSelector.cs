using UnityEngine;
using System.Collections;

public class MenuSelector : MonoBehaviour 
{
	public bool startMode;
	public bool exitMode;
	public bool optionsMode;

	public float yOffset;

	// Use this for initialization
	void Start () 
	{
		//transform.position = startPosition;
		//guiText.transform.position = startPosition;
		startMode = true;
		exitMode = false;
		optionsMode = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space))
		{
			if (startMode)
			{
				Application.LoadLevel("Game");
			}
			else if (optionsMode)
			{
				// Load Options
			}
			else if (exitMode)
			{
				Application.Quit();
			}
		}

		if (optionsMode)
		{
			if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
			{
				transform.position = new Vector3(transform.position.x, transform.position.y - yOffset, 0);
				exitMode = true;
				startMode = false;
				optionsMode = false;
				audio.Play();

			}
			else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
			{
				transform.position = new Vector3(transform.position.x, transform.position.y + yOffset, 0);
				startMode = true;
				exitMode = false;
				optionsMode = false;
				audio.Play();
			}
		}
		else if (startMode)
		{
			if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
			{
				transform.position = new Vector3(transform.position.x, transform.position.y - yOffset, 0);
				optionsMode = true;
				startMode = false;
				exitMode = false;
				audio.Play();
			}
		}
		else if (exitMode)
		{
			if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
			{
				transform.position = new Vector3(transform.position.x, transform.position.y + yOffset, 0);
				optionsMode = true;
				startMode = false;
				exitMode = false;
				audio.Play();
			}
		}

	}
}
