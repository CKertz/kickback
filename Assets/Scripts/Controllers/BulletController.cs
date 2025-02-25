using System.Collections;
using System.Collections.Generic;
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

            //Debug.Log("enemy hit");
            var enemyScript = collision.gameObject.GetComponent<EnemyController>();
            if (enemyScript != null && hitStatus)
            {
                enemyScript.InflictDamage();
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
