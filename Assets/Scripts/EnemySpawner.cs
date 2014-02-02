using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour 
{
	public Rigidbody zBullet;

	public float bulletForce;
	public float bulletInterval;

	// Use this for initialization
	void Start () 
	{
		StartCoroutine(spawnZBullet());
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	IEnumerator spawnZBullet()
	{
		Rigidbody tempBullet = (Rigidbody) Instantiate(zBullet, transform.position, Quaternion.identity);
		tempBullet.transform.position = new Vector3(Random.Range(-15, 15), Random.Range(-15, 15), 40);
		tempBullet.AddForce(Vector3.back * bulletForce);

		yield return new WaitForSeconds(bulletInterval);
	}
}
