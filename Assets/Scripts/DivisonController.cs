using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DivisonController : MonoBehaviour {
	public Rigidbody2D projectile;
	public float fallSpeed = 20.0f;
	public float spinSpeed = 250.0f;
	public GameObject explosionEffect;
	/*
	void Start() {
		GameObject player = GameObject.FindGameObjectWithTag ("player");
	}
*/
	void OnCollisionEnter2D() 
	{
		//TODO: create an explosion when bomb hits the floor  
		GameObject explosion = Instantiate (explosionEffect, transform.position, Quaternion.identity);
		Destroy (gameObject);
		Destroy (explosion, 1);
	}

	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.down * fallSpeed * Time.deltaTime, Space.World);
		transform.Rotate (Vector3.forward, spinSpeed * Time.deltaTime);
	}
}
