using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public GameObject bulletPrefab;
    public int playerSpeed = 10;
    private bool facingRight = true;
	private bool facingUp = false;
    public int playerJumpPower = 1250;
    private float moveX;
    public bool isGrounded;
	public Rigidbody2D projectile;
	public float bulletImpulse = 50.0f;
    
	// Update is called once per frame

	void Update ()
	{
		
		if (Input.GetKeyDown (KeyCode.W)) {
			facingUp = true;
		} else if (Input.GetKeyDown (KeyCode.A) || Input.GetKeyDown (KeyCode.D)) {
			facingUp = false;
		}

		if (Input.GetKeyDown (KeyCode.V)) {
			Shoot ();
		}
	    PlayerMove();

	}

    void PlayerMove()
    {
        // controls
        moveX = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            Jump();
        }
        // animation

        // player direction
        if (moveX < 0.0f && facingRight == true)
        {
            FlipPlayer();
        }
        else if (moveX > 0.0f && facingRight == false)
        {
            FlipPlayer();
        }

        // physics
        gameObject.GetComponent<Rigidbody2D>().velocity =
            new Vector2(moveX * playerSpeed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
    }

    void Jump()
    {
        // jumping code
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * playerJumpPower);
        isGrounded = false;
    }

    void FlipPlayer()
    {
        // Set boolean so we remember if we're facing right or not
        facingRight = !facingRight;
        Vector2 localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "ground")
        {
            isGrounded = true;
        }
    }


	void Shoot() {
		

		if (facingRight == true && facingUp == false) {
			var newPosition = new Vector3 (transform.position.x + 1f, transform.position.y);
			var bullet = (GameObject)Instantiate (bulletPrefab, newPosition, Quaternion.identity);
			bullet.GetComponent<Rigidbody2D> ().velocity = bullet.transform.right * 6;
			Destroy (bullet, 2);
		} else if (facingRight == false && facingUp == false) {
			
			var newPosition = new Vector3 (transform.position.x - 1f, transform.position.y);
			var bullet = (GameObject)Instantiate (bulletPrefab, newPosition, Quaternion.identity);
			bullet.GetComponent<Rigidbody2D> ().velocity = bullet.transform.right * -6;
			Destroy (bullet, 2);
		} else  {
			var newPosition = new Vector3 (transform.position.x - 0.0f, transform.position.y+1f);
			var bullet = (GameObject)Instantiate (bulletPrefab, newPosition, Quaternion.identity);
			bullet.GetComponent<Rigidbody2D> ().velocity = bullet.transform.up * 6;
			Destroy (bullet, 2);
		}
		//bullet.AddForce (transform.forward * bulletImpulse, ForceMode2D.Impulse);

	}
}
