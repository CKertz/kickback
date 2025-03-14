using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float recoilForce = 5f;

    [SerializeField]
    private float kickBackMultiplier;

    [SerializeField]
    private float playerHealth;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //has a game event listener on the player for OnGunFired
    public void ApplyRecoilForce()
    {
        Vector2 force = transform.right*recoilForce;
        rb.AddForce(-force*kickBackMultiplier, ForceMode2D.Impulse);
    }

    //has a game event listener on the player for OnBigShotFired
    public void ApplyRecoilForceBigShot()
    {
        Debug.Log("bigshot recoil applied");
        Vector2 force = transform.right * recoilForce * 2;
        rb.AddForce(-force * kickBackMultiplier, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if(playerHealth > 0)
            {
                playerHealth--;
                Debug.Log("player health is now:" + playerHealth);
                //add some sort of invulnerability cooldown
                //i think also if the enemy is sitting on the player it isn't decrementing over time, there's probably a better OnTrigger to use for this instead
            }

            if (playerHealth == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
