using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private float bulletSpeed;

    [SerializeField]
    private float interval;

    [SerializeField]
    public float hitChance;

    [Header("Events")]
    public GameEvent onGunFired;

    private List<GameObject> enemiesInRange = new List<GameObject>();

    private bool inRange = false;
    private float timer = 0f;
    private Transform target;



    private int missCounter = 0;

    void Start()
    {

    }

    void Update()
    {
        if (inRange)
        {
            timer += Time.deltaTime; // Increment the timer while in range

            if (timer >= interval)
            {
                FireGun();

                timer = 0f; // Reset the timer after executing the function
            }
        }
    }

    private void FireGun()
    {
        if (target != null && bulletPrefab != null)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = direction * bulletSpeed;
            }
            if (RollHitChance())
            {
               // Debug.Log("sent value is true");
                onGunFired.Raise(this, true);

            }
            else
            {
               // Debug.Log("sent value is false");

                onGunFired.Raise(this, false);

                CircleCollider2D collider = bullet.GetComponent<CircleCollider2D>();
                collider.isTrigger = false;
                missCounter++;
                Debug.Log("shot missed! misscounter incremented to " + missCounter);
            }

        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            //Debug.Log("enemy found");
            inRange = true;
            if (!enemiesInRange.Contains(other.gameObject))
            {
                //Debug.Log("enemy added" + other.gameObject.name);
                enemiesInRange.Add(other.gameObject);

                if (enemiesInRange.Count > 1)
                {
                    target = GetClosestEnemy().transform;
                    Debug.Log("multiple enemies in range, target selected is" + target.name);
                }
                else
                {
                    target = other.transform;
                }
                //other.gameObject.GetComponent<EnemyController>().InflictDamage();
            }
            //target = other.transform;
        }
    }

    private GameObject GetClosestEnemy()
    {
        GameObject closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemiesInRange)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < closestDistance)
            {
                closestDistance = distanceToEnemy;
                closestEnemy = enemy;
            }
        }
        Debug.Log("cloest enemy " + closestEnemy.name);
        return closestEnemy;
    }

    private bool RollHitChance()
    {
        float random = Random.value;
        Debug.Log("random value: "+ random);
        if(random < (hitChance / 100f))
        {
            Debug.Log("MISS ROLLED");
            return false;
        }
        else
        {
            Debug.Log("HIT ROLLED");
            return true;
        }
        //return random < (hitChance / 100f);
    }
}
