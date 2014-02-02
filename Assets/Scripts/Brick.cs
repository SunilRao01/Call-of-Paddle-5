using UnityEngine;
using System.Collections;
using System.IO;

public class Brick : MonoBehaviour 
{
	/*
	 *  JS Code to translate:
	 * 
	 *  var bgColor : Color;    
		var blooping : boolean = true;
		 
		function Start () {
		    StartCoroutine(bgColorShifter());
		}
		 
		function bgColorShifter()
		{
		    while (blooping)
		    {
		        bgColor.r = Random.value; // value is already between 0 and 1
		        bgColor.g = Random.value;
		        bgColor.b = Random.value;
		        bgColor.a = 1.0; // I don't think alpha matters    
		        Debug.Log("bgColor: "+bgColor);
		 
		        var t: float = 0f
		        var currentColor = Camera.main.backgroundColor;
		        while( t < 1.0 )
		        {
		            Camera.main.backgroundColor = Color.Lerp(currentColor, bgColor, t );
		            yield null; // Wait one frame
		            t += Time.deltaTime;
		        }
		    }
		 }
	 * 
	 * */



	private bool finished = false;
	private Color bgColor;
	private bool blooping = true; 

	public Renderer child;

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!audio.isPlaying && finished)
		{
			Destroy(gameObject);
		}
	}

	IEnumerator bgColorShifter()
	{
		while (blooping)
		{
			bgColor.r = Random.value; // value is already between 0 and 1
			bgColor.g = Random.value;
			bgColor.b = Random.value;
			bgColor.a -= 0.1f; // I don't think alpha matters    

			float t = 0f;
			var currentColor = renderer.material.color;
			while(t < 1.0)
			{
				renderer.material.color = Color.Lerp(currentColor, bgColor, t );
				child.material.color = Color.Lerp(currentColor, bgColor, t );
				//yield null; // Wait one frame
				yield return new WaitForEndOfFrame();
				t += Time.deltaTime;
			}

			releasePowerup();

			Destroy(gameObject);
		}


	}

	void releasePowerup()
	{

	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.name != "Paddle")
		{
			audio.Play();

			StartCoroutine(bgColorShifter());
			//finished = true;
			//Destroy(gameObject);
		}
	}
}
