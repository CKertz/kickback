using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Transform player; // Reference to the player's transform
    public float moveSpeed;
    public float enemyHealth = 10f;

    //temp placeholder while testing
    [SerializeField]
    private TextMeshProUGUI enemyHealthText;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").transform; // Find the player by tag

        if (enemyHealthText != null)
        {
            enemyHealthText.text = "Enemy Health: " + enemyHealth.ToString();
        }
    }

    void Update()
    {
        if (player != null)
        {
            // Calculate direction to move towards the player
            Vector2 direction = player.position - transform.position;
  //          direction.Normalize(); // Normalize to get a unit vector

            // Move the enemy towards the player
            rb.velocity = direction * moveSpeed;
        }
    }

    public void InflictDamage(int damageCount)
    {
        enemyHealth -= damageCount;
        if (enemyHealth <= 0)
        {
            Debug.Log("health is zero:" + enemyHealth);
            Destroy(gameObject);
            return;
        }
        Debug.Log("enemy health decremented. now:" + enemyHealth);
        if (enemyHealthText != null)
        {
            enemyHealthText.text = "Enemy Health: " + enemyHealth.ToString();
        }


    }
}
