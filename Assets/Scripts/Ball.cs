using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour 
{
    public Vector3 startingDirection;
    public float ballSpeed;
	private Vector2 startingPosition;

	public float difficultySpeed;

	public GUIText livesLabel;
	public int lives = 3;

	public Vector2 additionalSpeed;

	private bool blooping = true;
	private bool deathPhase = false;
	private Color bgColor;
	private Color originalColor;

	public AudioSource respawnSFX;

	private bool respawnDone = false;

	private SpriteRenderer spriteRenderer;
	public float spawnInterval;

	
	void Awake()
	{
		originalColor = renderer.material.color;
		startingPosition = transform.position;
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	// Use this for initialization
	void Start () 
    {
		respawn();

		/*// Set ball to starting position
		transform.position = startingPosition;
		
		// Reset difficulty modifiers
		additionalSpeed.x = 1;
		additionalSpeed.y = 1;
		
		// Reset velocity
		rigidbody2D.velocity = Vector3.zero;
		rigidbody2D.AddForce(startingDirection * (ballSpeed + additionalSpeed.magnitude));
		
		
		// Reset color
		renderer.material.color = originalColor;
		
		
		// You're not dead
		deathPhase = false;*/

	}
	
	// Update is called once per frame
	void Update () 
    {
		if (GameObject.Find("Bricks").GetComponent<Transform>().childCount <= 0)
		{
			Application.LoadLevel("WinScreen");
		}

		livesLabel.text = lives.ToString();

		// When playe reaches 0 lives
		if (lives <= 0)
		{
			Application.LoadLevel("GameOver");
		}

		// When the ball hits the wall
		if (deathPhase)
		{
			Start();
			
			lives--;
			
			deathPhase = false;
		}
	}

	void respawn()
	{
		StartCoroutine(flash());
	}

	IEnumerator flash()
	{
		rigidbody2D.velocity = Vector3.zero;
		transform.position = startingPosition;

		spriteRenderer.material.color = new Color(spriteRenderer.material.color.r, spriteRenderer.material.color.g, spriteRenderer.material.color.b, 0);
		yield return new WaitForSeconds(spawnInterval);
		spriteRenderer.material.color = new Color(spriteRenderer.material.color.r, spriteRenderer.material.color.g, spriteRenderer.material.color.b, 1);
		respawnSFX.Play();
		yield return new WaitForSeconds(spawnInterval);
		spriteRenderer.material.color = new Color(spriteRenderer.material.color.r, spriteRenderer.material.color.g, spriteRenderer.material.color.b, 0);
		yield return new WaitForSeconds(spawnInterval);
		spriteRenderer.material.color = new Color(spriteRenderer.material.color.r, spriteRenderer.material.color.g, spriteRenderer.material.color.b, 1);
		respawnSFX.Play();
		yield return new WaitForSeconds(spawnInterval);
		spriteRenderer.material.color = new Color(spriteRenderer.material.color.r, spriteRenderer.material.color.g, spriteRenderer.material.color.b, 0);
		yield return new WaitForSeconds(spawnInterval);
		spriteRenderer.material.color = new Color(spriteRenderer.material.color.r, spriteRenderer.material.color.g, spriteRenderer.material.color.b, 1);
		respawnSFX.Play();

		// Set ball to starting position
		transform.position = startingPosition;

		// Reset difficulty modifiers
		additionalSpeed = new Vector2(1.1f, 1.1f);

		
		// Reset velocity
		rigidbody2D.velocity = Vector3.zero;
		rigidbody2D.AddForce(startingDirection * (ballSpeed + additionalSpeed.magnitude));
		
		
		// Reset color
		renderer.material.color = originalColor;
		

		// You're not dead
		deathPhase = false;

		StartCoroutine(GameObject.Find("Main Camera").GetComponent<Camera>().shiftPerspective());
	}

	// Coroutine that slowsly decreases color of object
	IEnumerator bgColorShifter()
	{
		while (blooping)
		{
			bgColor.r = Random.value; // value is already between 0 and 1
			bgColor.g = Random.value;
			bgColor.b = Random.value;
			bgColor.a -= 0.01f; // I don't think alpha matters    

			float t = 0f;

			var currentColor = renderer.material.color;

			while(t < 1.0)
			{
				renderer.material.color = Color.Lerp(currentColor, bgColor, t);
				yield return new WaitForEndOfFrame();
				t += Time.deltaTime;
			}
			
			deathPhase = true;
		}
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Wall"))
		{
			deathPhase = true;

			// Reset paddle's position and rotation after death
			GameObject.Find("PaddleCenter").transform.rotation = Quaternion.Euler(Vector3.zero);
			GameObject.Find("PaddleCenter").GetComponentInChildren<Paddle>().transform.position = GameObject.Find("Paddle").GetComponentInChildren<Paddle>().originalPosition;
		}

		if (collision.gameObject.CompareTag("Brick"))
		{
			if (rigidbody2D.velocity.x >= 0 && rigidbody2D.velocity.y >= 0)
			{
				additionalSpeed.x = rigidbody2D.velocity.x + difficultySpeed;
				additionalSpeed.y = rigidbody2D.velocity.y + difficultySpeed;

				rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x + difficultySpeed, rigidbody2D.velocity.y + difficultySpeed);
			}
			if (rigidbody2D.velocity.x >= 0 && rigidbody2D.velocity.y <= 0)
			{
				additionalSpeed.x = rigidbody2D.velocity.x + difficultySpeed;
				additionalSpeed.y = rigidbody2D.velocity.y + difficultySpeed;

				rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x + difficultySpeed, rigidbody2D.velocity.y - difficultySpeed);
			}
			if (rigidbody2D.velocity.x <= 0 && rigidbody2D.velocity.y >= 0)
			{
				additionalSpeed.x = rigidbody2D.velocity.x + difficultySpeed;
				additionalSpeed.y = rigidbody2D.velocity.y + difficultySpeed;

				rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x - difficultySpeed, rigidbody2D.velocity.y + difficultySpeed);
			}
			if (rigidbody2D.velocity.x <= 0 && rigidbody2D.velocity.y <= 0)
			{
				additionalSpeed.x = rigidbody2D.velocity.x + difficultySpeed;
				additionalSpeed.y = rigidbody2D.velocity.y + difficultySpeed;

				rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x - difficultySpeed, rigidbody2D.velocity.y - difficultySpeed);
			}
		
		}
	}
			
}
