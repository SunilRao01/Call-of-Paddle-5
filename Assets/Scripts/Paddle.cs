using UnityEngine;
using System.Collections;

public class Paddle : MonoBehaviour 
{

	public float reflectionForce;
	public Vector3 originalPosition;

	void Start () 
    {
		originalPosition = transform.position;
	}
	
	void Update () 
    {
        
	}
	

	void OnCollisionEnter2D(Collision2D other)
	{
		audio.Play();

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
