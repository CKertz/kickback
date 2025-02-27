using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] 
    public float moveSpeed;

    private bool hitStatus;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            var enemyScript = collision.gameObject.GetComponent<EnemyController>();
            if (gameObject.tag == "bigshot")
            {
                if (enemyScript != null)
                {
                    enemyScript.InflictDamage(3);
                    Destroy(gameObject);
                }
            }

            //Debug.Log("enemy hit");
            if (enemyScript != null && hitStatus)
            {
                enemyScript.InflictDamage(1);
                Destroy(gameObject);
            }

        }
    }

    public void setHitStatus(Component sender, object data)
    {
        if (data is bool)
        {
            bool result = (bool)data;
            //Debug.Log("received value is:"+result);
            if (result)
            {
                hitStatus = true;
            }
            else
            {
                hitStatus = false;
            }
        }
    }
}
