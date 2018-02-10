using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperController : MonoBehaviour {
	public Rigidbody2D projectile;
	//public GameObject monster;
	public float bulletImpulse = 20.0f;
	// Use this for initialization
	private Vector3 playerPosition;

	void Start() {
		GameObject player = GameObject.FindGameObjectWithTag ("player");
		playerPosition = player.transform.position;
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "player")
		{
			Debug.Log("I hit the player");
			var player = col.gameObject.GetComponent<Player_Health>();
			player.PlayerDies();
		}
	}
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.MoveTowards(transform.position, playerPosition, 0.1f);
	}
}
