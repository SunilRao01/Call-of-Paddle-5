using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour {

	public bool zMode;

	private Vector3 position2D;
	private Vector3 rotation2D;
	
	private Vector3 position3D;
	private Vector3 rotation3D;

	public Camera secondaryCamera;

	public bool muteSound;

	public float timeInterval;
	private float timer;
	public GUIText timerLabel;

	private int counter = 1;
	//public float timeIntervalDifficulty;
	// Use this for initialization
	void Start () 
	{
		position2D = transform.position;
		rotation2D = transform.rotation.eulerAngles;

		position3D = new Vector3(0, -17, 0);
		rotation3D = new Vector3(-90, 0, 0);

		//StartCoroutine(shiftPerspective());

		timer = timeInterval;
	}

	// Coroutine that slowsly decreases color of object
	public IEnumerator shiftPerspective()
	{
		while (true)
		{
			yield return new WaitForSeconds(0.1f);

			if (timer <= 0)
			{
				timer = timeInterval;

				counter++;
			}
			else
			{
				timer--;
			}
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}

		if (counter % 2 == 0)
		{
			zMode = true;
		}
		else
		{
			zMode = false;
		}

		timerLabel.text = timer.ToString();

		if (muteSound)
		{
			AudioListener.pause = true;
		}

		Transform paddlePosition = GameObject.Find("PaddleCenter").GetComponent<Transform>().Find("Paddle");
		position3D = paddlePosition.position + (-Vector3.up);

		// TODO: Accurately change rotation of camrea according to rotation of paddle (PaddleCenter?)
		rotation3D.z = GameObject.Find("PaddleCenter").GetComponent<Transform>().rotation.eulerAngles.y;
		rotation3D.x = GameObject.Find("PaddleCenter").GetComponent<Transform>().rotation.eulerAngles.x - 90;
		rotation3D.y = 0;

		rotation3D = camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, camera.farClipPlane));

		if (zMode)
		{
			GameObject.Find("Main Camera").GetComponent<Camera>().camera.enabled = false;
		}
		else
		{
			GameObject.Find("Main Camera").GetComponent<Camera>().camera.enabled = true;
		}


		//position3D = new Vector3(paddlePosition.position.x + position3D.x, paddlePosition.position.y + position3D.y, paddlePosition.position.z + position3D.z);

		/*if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			// Switch z mode on
			if (!zMode)
			{
				//transform.position = position3D;
				//transform.rotation = Quaternion.Euler(rotation3D);
				//transform.LookAt(rotation3D);
				//camera.orthographic = false;
				//transform.parent = GameObject.Find("PaddleCenter").GetComponent<Transform>();

				zMode = true;
			}
			// Switch z mode off
			else
			{
				//transform.position = position2D;
				//transform.rotation = Quaternion.Euler(rotation2D);
				//camera.orthographic = true;
				//transform.parent = null;

				zMode = false;
			}
		}*/
	}
	
}
