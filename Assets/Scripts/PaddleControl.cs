using UnityEngine;
using System.Collections;

public class PaddleControl : MonoBehaviour 
{
	// Variables
	public float movementSpeed;
	
	public float reflectionForce;
	
	private float yPosition;
	
	// Rotation around center
	public float rotationSpeed;


	public Vector3 originalPosition;
	private Vector3 calculatedRotation;




	//TODO: Increase paddle speed when ball increases in movement speed


	void Start () 
	{
		originalPosition = transform.position;
		yPosition = transform.position.y;



	}
	
	void Update () 
	{
		// Handle movement


		calculatedRotation = (Vector3.forward * Input.GetAxis("Horizontal"));
		
		if (Input.GetKey(KeyCode.Space))
		{
			calculatedRotation *= ((GameObject.Find("Ball").GetComponent<Ball>().additionalSpeed.magnitude) / 2);
		}
		
		transform.Rotate(calculatedRotation);
	}
	
	
	void OnCollisionEnter2D(Collision2D other)
	{
		foreach (ContactPoint2D contact in other.contacts)
		{
			if (contact.otherCollider == collider2D)
			{
				float angle = contact.point.x - transform.position.x;
				
				float ballVelocity = Vector2.SqrMagnitude(contact.collider.rigidbody2D.velocity);
				
				contact.collider.rigidbody2D.AddForce(new Vector2(reflectionForce * ballVelocity * angle, 0));
			}
		}
	}
}
