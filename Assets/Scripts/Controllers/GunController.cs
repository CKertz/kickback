using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private GameObject bigBulletPrefab;

    [SerializeField]
    private float bulletSpeed;

    [SerializeField]
    private float interval;

    [SerializeField]
    private TextMeshProUGUI missedShotText;

    [SerializeField]
    public float hitChance;

    [SerializeField]
    public float ammoCount;

    [SerializeField]
    private TextMeshProUGUI ammoCountText;


    [Header("Events")]
    public GameEvent onGunFired;

    private List<GameObject> enemiesInRange = new List<GameObject>();

    private bool inRange = false;
    private float timer = 0f;
    private Transform target;
    private bool isBigShotAvailable = false;



    private int missCounter = 0;

    void Start()
    {
        if (missedShotText != null)
        {
            missedShotText.text = "Missed shot count: " + missCounter.ToString();
        }

        if (ammoCountText != null)
        {
            ammoCountText.text = "Ammo count: " + ammoCount.ToString();
        }

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

        if (isBigShotAvailable && Input.GetKeyUp(KeyCode.Space))
        {
            FireBigShot();
        }
    }

    private void FireGun()
    {
        if (target != null && bulletPrefab != null)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            Vector3 bulletSpawnPosition = new Vector3(transform.position.x, transform.position.y + 0.5f);
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPosition, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            
            if (ammoCount <= 0)
            {
                Debug.Log("out of ammo!");
                return;
            }
            else
            {
                ammoCount -= 1;
                ammoCountText.text = "Ammo count: " + ammoCount.ToString();
            }

            if (rb != null)
            {
                rb.velocity = direction * bulletSpeed;
            }
            if (RollHitChance())
            {
               // Debug.Log("sent value is true");
               //used in pf_bullet: bulletController.setHitStatus
                onGunFired.Raise(this, true);

            }
            else
            {
                // Debug.Log("sent value is false");
                //used in pf_bullet: bulletController.setHitStatus
                onGunFired.Raise(this, false);

                CircleCollider2D collider = bullet.GetComponent<CircleCollider2D>();
                collider.isTrigger = false;
                if(missedShotText != null)
                {
                    if(missCounter <= 9)
                    {
                        missCounter++;
                        missedShotText.text = "Missed shot count: " + missCounter.ToString();
                        Debug.Log("shot missed! misscounter incremented to " + missCounter);
                    }
                    else
                    {
                        missedShotText.text = "Missed shot count: " + missCounter.ToString() + ", big shot ready";
                        isBigShotAvailable = true;
                        Debug.Log("misscounter is 10, big shot ready");
                    }
                }
            }

        }
    }

    private void FireBigShot()
    {
        if (isBigShotAvailable)
        {
            GameObject bigShot = Instantiate(bigBulletPrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = bigShot.GetComponent<Rigidbody2D>();
            Vector2 direction = (target.position - transform.position).normalized;

            if (rb != null)
            {
                rb.velocity = direction * 2*bulletSpeed; //doubling the bigshot speed compared to regular because why not
            }

            isBigShotAvailable = false;
            missCounter = 0;
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
