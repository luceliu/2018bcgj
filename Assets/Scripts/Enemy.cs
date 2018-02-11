using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int EnemySpeed;
    public int XMoveDirection;
	public Rigidbody2D projectile;
	public float bulletImpulse = 50.0f;
	public float monsterSpeed = 0.2f;
	private GameObject player;
	private Vector3 playerPosition;
	private bool seen;

	void Start () {
		float rand = Random.Range (1.0f, 2.0f);
		InvokeRepeating ("Shoot", 2, rand);
		player = GameObject.FindGameObjectWithTag ("player");
		seen = false;
	}

	void Shoot() {
        SoundManager.Instance.PlaySound(SoundManager.PAPER_FIRE_SOUND);
		Rigidbody2D bullet = (Rigidbody2D)Instantiate (projectile, transform.position, transform.rotation);
		bullet.AddForce (transform.forward * bulletImpulse, ForceMode2D.Impulse);
	}

	// Update is called once per frame
	void Update ()
	{
		var distance = Vector3.Distance (player.transform.position, transform.position);
		if (distance < 20.0f) 
		{
			seen = true;
		} 
		if (seen == true)
		{
			//RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(XMoveDirection, 0));
			//gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (XMoveDirection, 0) * EnemySpeed;
			transform.position = Vector3.MoveTowards(transform.position, player.transform.position , monsterSpeed);
		} 
	}

    void OnCollisionEnter2D(Collision2D col)
    {
		if (col.gameObject.tag == "player") {
			Debug.Log ("I hit the player");
			var player = col.gameObject.GetComponent<Player_Health> ();
			if (player.isTangible == true) {
                SoundManager.Instance.PlaySound(SoundManager.PAPER_HIT_SOUND);
                player.PlayerHit();
            }
        }
        else
        {
            Flip();
        }
    }

    void Flip()
    {
        if (XMoveDirection > 0)
        {
            XMoveDirection = -1;
        }
        else
        {
            XMoveDirection = 1;
        }
    }
}
