using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanBullet : MonoBehaviour {
	// Use this for initialization
	void Start ()
	{
        // Bullet should keep track of when to destroy itself
	    Destroy(gameObject, 2);
	}
	
	// Update is called once per frame
	void Update () {
		//transform.position= Vector3.MoveTowards(1) ;
		
	}
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "enemy")
        {
            var enemy = col.GetComponent<EnemyHealth>();
            enemy.GetHit();
        }

        // regardless of what you hit, destroy yourself
        Destroy(gameObject);

    }
}
