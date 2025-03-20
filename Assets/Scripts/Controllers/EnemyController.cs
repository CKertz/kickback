using Assets.Scripts.Models;
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
    private int enemyId;

    void Start()
    {
        LogNewEnemy();

        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").transform; // Find the player by tag

        if (enemyHealthText != null)
        {
            enemyHealthText.text = "Enemy Health: " + enemyHealth.ToString();
        }
    }

    void Update()
    {
        //temp commenting movement out while testing

        if (player != null)
        {
            // Calculate direction to move towards the player
            Vector2 direction = player.position - transform.position;

            // Move the enemy towards the player
            rb.velocity = direction * moveSpeed;
        }
    }

    private void LogNewEnemy()
    {
        var newEnemy = new Enemy();
        newEnemy.enemyMaxHealth = enemyHealth;
        newEnemy.enemyName = gameObject.name;
        newEnemy.enemyMovementSpeed = moveSpeed;
        newEnemy.isAlive = true;
        var enemyIdCounter = DataManager.Instance.activeEnemyIdCounter;
        DataManager.Instance.activeEnemyList.Add(enemyIdCounter, newEnemy);
        enemyId = DataManager.Instance.activeEnemyIdCounter;
        
        DataManager.Instance.activeEnemyIdCounter++;
        //Debug.Log("enemy logged:");
        //Debug.Log("enemy name: " + newEnemy.enemyName);
        //Debug.Log("enemy movementspeed: " + newEnemy.enemyMovementSpeed);
        //Debug.Log("enemy isalive: " + newEnemy.isAlive);
        //Debug.Log("enemy enemyId: " + enemyId);
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

    public void OnDestroy()
    {
        Debug.Log("enemy destruction starting of enemy id: " + enemyId);
        DataManager.Instance.activeEnemyList.Remove(enemyId);
    }
}
