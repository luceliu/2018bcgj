using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DivisonController : MonoBehaviour {
	public Rigidbody2D projectile;
	public float fallSpeed = 20.0f;
	public float spinSpeed = 250.0f;
	public GameObject explosionEffect;

	void Start() {
		GameObject player = GameObject.FindGameObjectWithTag ("player");
		Destroy (gameObject, 2);
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "player")
		{
			var player = col.gameObject.GetComponent<Player_Health> ();
			if (player.isTangible)
			{
				player.PlayerHit();
				Destroy(gameObject);
			}
		}
		else if (col.gameObject.tag != "enemy")
		{
			GameObject explosion = Instantiate (explosionEffect, transform.position, Quaternion.identity);
			Destroy (gameObject);
			Destroy (explosion, 1);
		}
	}
	/*
	void onCollisonEnter2D() {
		GameObject explosion = Instantiate (explosionEffect, transform.position, Quaternion.identity);
		Destroy (gameObject);
		Destroy (explosion, 1);
	} */
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.down * fallSpeed * Time.deltaTime, Space.World);
		transform.Rotate (Vector3.forward, spinSpeed * Time.deltaTime);
	}
}
