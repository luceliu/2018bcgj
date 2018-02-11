using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanBullet : MonoBehaviour {
	public Rigidbody2D projectile;
	public int bulletspeed = 5;
	public float bulletImpulse = 20.0f;
	// Use this for initialization
	void Start () {
		projectile = GetComponent<Rigidbody2D> ();


		
	}
	
	// Update is called once per frame
	void Update () {
		//transform.position= Vector3.MoveTowards(1) ;
		
	}
}
