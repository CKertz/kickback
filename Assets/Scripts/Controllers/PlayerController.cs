using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float recoilForce = 5f;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApplyRecoilForce()
    {
        Vector2 force = transform.right*recoilForce;
        Debug.Log("force calculated: "+force);
        rb.AddForce(-force, ForceMode2D.Impulse);
    }
}
