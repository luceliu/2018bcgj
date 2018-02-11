using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {
    public int health = 3;
    public float enemyGuiOffset = 100f;
    private int currentHealth;
    private Color originalColor;
    private bool isTangible = true;
    public float secondsInvulnerable;
    // Use this for initialization
    void Start()
    {
        originalColor = gameObject.GetComponent<SpriteRenderer>().color;
        currentHealth = health;
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
    void OnGUI()
    {

        Vector2 targetPos;
        targetPos = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        targetPos.y = Screen.height - targetPos.y;
        Debug.Log(targetPos);
        GUI.Box(new Rect(targetPos.x - 30, targetPos.y + enemyGuiOffset, 60, 20), currentHealth + "/" + health);
    }
}
