using UnityEngine;
using System.Collections.Generic;

public class RhythmCube : MonoBehaviour 
{
	private AudioSource mainMusic;
	private LineRenderer mainLine;

	public List<Vector3> linePoints;
	public float intensity;

	private LineRenderer lineSpectrum;

	// Use this for initialization
	void Start () 
	{
		mainMusic = GameObject.Find("BG Music").GetComponent<AudioSource>();
		mainLine = GetComponent<LineRenderer>();

		lineSpectrum = GetComponent<LineRenderer>();
		lineSpectrum.SetVertexCount(20);
	}
	
	// Update is called once per frame
	void Update () 
	{


		if (mainMusic.isPlaying)
		{
			//float[] spectrum = mainMusic.GetSpectrumData(1024, 0, FFTWindow.Triangle);
			float[] spectrum = mainMusic.GetSpectrumData(1024, 0, FFTWindow.BlackmanHarris);

			int j = 0;
			int k = 0;
			for (var i = 1; i < 1023; i++) 
			{

				if (i % 50 == 0)
				{
					lineSpectrum.SetPosition(k, new Vector3 (j - 1, Mathf.Log(spectrum[i - 1]) + 10, 2));
				}


				Debug.DrawLine (new Vector3 (j - 1, spectrum[i] + 10, 0),
				                new Vector3 (j, spectrum[i + 1] + 10, 0), Color.red);
				Debug.DrawLine (new Vector3 (j - 1, Mathf.Log(spectrum[i - 1]) + 10, 2),
				                new Vector3 (j, Mathf.Log(spectrum[i]) + 10, 2), Color.cyan);


				Debug.DrawLine (new Vector3 (Mathf.Log(j - 1), spectrum[i - 1] - 10, 1),
				                new Vector3 (Mathf.Log(j), spectrum[i] - 10, 1), Color.green);
				Debug.DrawLine (new Vector3 (Mathf.Log(j - 1), Mathf.Log(spectrum[i - 1]), 3),
				                new Vector3 (Mathf.Log(j), Mathf.Log(spectrum[i]), 3), Color.yellow);

				j++;
			}

		}
	}
}
