using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {
    public int health = 3;
    private Color originalColor;
    private bool isTangible = true;
    public float secondsInvulnerable;
    // Use this for initialization
    void Start()
    {
        originalColor = gameObject.GetComponent<SpriteRenderer>().color;
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void GetHit()
    {
        if (isTangible)
        {
            health--;
            if (health <= 0)
            {
                Die();
            }
            else
            {
                InvulnerabilityStart();
            }
        }
    }

    private void InvulnerabilityStart()
    {
        isTangible = false;
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        Invoke("ResetInvulnerability", secondsInvulnerable);
    }
    public void ResetInvulnerability()
    {
        isTangible = true;
        gameObject.GetComponent<SpriteRenderer>().color = originalColor;
    }
}
