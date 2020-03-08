using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public int team;
    public float speed;
    
    private Rigidbody RB;
    
    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody>();
        transform.Rotate(0, 90, 0);
    }

    void Update()
    {
        RB.velocity = RB.transform.forward * speed;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "boundary")
        {
            Destroy(this.gameObject);
        }
    }
}
