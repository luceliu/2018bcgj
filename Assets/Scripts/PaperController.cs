using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperController : MonoBehaviour {
	public Rigidbody2D projectile;
	//public GameObject monster;
	public float bulletImpulse = 20.0f;
	// Use this for initialization
	private Vector3 playerPosition;
	private GameObject player;

	void Start() {
		player = GameObject.FindGameObjectWithTag ("player");
		playerPosition = player.transform.position;
	   	Destroy(gameObject, 3);
    }

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "player")
		{
            // If you collide into the player, and the player is tangible, hit them and blow up
			var player = col.gameObject.GetComponent<Player_Health>();
		    if (player.isTangible)
		    {
		        player.PlayerHit();
		        Destroy(gameObject);
		    }
		} 
		else if (col.gameObject.tag != "enemy")
		{
            // If you collide into something that isn't yourself, delete yourself
		    Destroy(gameObject);
        } 
	}
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.MoveTowards(transform.position, playerPosition, 0.1f);
	}
}
