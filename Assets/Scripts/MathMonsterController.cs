using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MathMonsterController : MonoBehaviour { 
	public enum OccilationFunction { Sine, Cosine};
	public Rigidbody2D projectile;
	public float bulletImpulse = 50.0f;
	public void Start()
	{
		StartCoroutine (Oscillate (OccilationFunction.Sine, 15f));
		float rand = Random.Range (0.5f, 1.0f);
		InvokeRepeating ("Shoot", 2, rand);
	}
	void Shoot() {
		Rigidbody2D bullet = (Rigidbody2D)Instantiate (projectile, transform.position, transform.rotation);
		bullet.AddForce (transform.forward * bulletImpulse, ForceMode2D.Impulse);
		Destroy (bullet.gameObject, 2);
	}
	private IEnumerator Oscillate (OccilationFunction method, float scalar)
	{
		while (true)
		{
			if (method == OccilationFunction.Sine)
			{
				if (method == OccilationFunction.Sine)
				{
					transform.position = new Vector3 (Mathf.Sin (Time.time) * scalar, transform.position.y, 0);
				} 
				else if (method == OccilationFunction.Cosine)
				{
					transform.position = new Vector3 (Mathf.Cos (Time.time) * scalar, transform.position.y, 0);
				}
				yield return new WaitForEndOfFrame ();
			}
		}
	}
}
