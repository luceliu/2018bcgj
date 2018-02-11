using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProBuilder2.Common;

public class PlayerController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public int playerSpeed = 10;
    private bool facingRight = true;
    private bool facingUp = false;
    public int playerJumpPower = 1250;
    private float moveX;
    public bool isGrounded;
    // Reference to the Animator
    private Animator anim;
    private bool running;
    public float bulletSpeed = 10;
    public float bulletDelay;
    private bool canShoot = true;

    void Start()
    {
        anim = GetComponent<Animator>();
        running = false;
    }
    // Update is called once per frame

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

        PlayerMove();
        if (Input.GetAxis("Horizontal") != 0)
        {
            running = true;
        }
        else
        {
            running = false;
        }
        if (running == true)
        {
            anim.SetBool("isRunning", true);
        }
        if (running == false)
        {
            anim.SetBool("isRunning", false);
        }
    }

    void PlayerMove()
    {
        // controls
        moveX = Input.GetAxis("Horizontal");
        if (Input.GetAxis("Vertical") > 0f)
        {
            facingUp = true;
        }
        else
        {
            facingUp = false;
        }

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
        anim.SetTrigger("StartJump");
        SoundManager.Instance.PlaySound(SoundManager.JUMP_SOUND);
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


    void Shoot()
    {
        if (canShoot == true)
        {
            Vector3 bulletInitialPosition;
            Vector2 bulletVelocity;

	      //  canShoot = false;

            if (facingRight == true && facingUp == false)
            {
	            bulletInitialPosition = new Vector3(transform.position.x + 2.5f, transform.position.y);

                bulletVelocity = transform.right * bulletSpeed;
            }
            else if (facingRight == false && facingUp == false)
            {

	            bulletInitialPosition = new Vector3(transform.position.x - 2.5f, transform.position.y);
                bulletVelocity = -transform.right * bulletSpeed;
            }
            else
            {
	            bulletInitialPosition = new Vector3(transform.position.x - 0.0f, transform.position.y + 3f);
                bulletVelocity = transform.up * bulletSpeed;
            }

            var bullet = GameObject.Instantiate(bulletPrefab, bulletInitialPosition, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = bulletVelocity;

            // play sound
            SoundManager.Instance.PlaySound(SoundManager.SHOOTBOOK_SOUND);

            // Don't let player shoot gain for timeDelay()
          //  Invoke("ResetBulletDelay", bulletDelay);
        }
    }

    private void ResetBulletDelay()
    {
        canShoot = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "FinishFlag")
        {
            GameObject.Find("GameController").GetComponent<GameController>().ReachFlagAndEndDream();
        }
    }
}
