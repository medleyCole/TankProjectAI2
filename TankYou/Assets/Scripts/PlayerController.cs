using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin,xMax,zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
    //set the rotation at the start to a random angle
    private Vector3 moveRotation;
    public float speed;
    
    public KeyCode left;
    public KeyCode right;
    public KeyCode Up;
    public KeyCode Down;
    public KeyCode Shoot;
    public int team;
    
    public Boundary boundary;
    private Rigidbody RB;

    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;
    private float nextFire;

    private float nextCollision;

    [SerializeField] private Transform player;
    [SerializeField] private Transform respawnPoint;

    void Start()
    {
        RB = GetComponent<Rigidbody>();

        //change thisto only set x/y
        float randomRot = Random.Range(-180, 179);
        moveRotation = new Vector3(0.0f, randomRot, 0.0f);
       // print("player " + player + " rotation set to: " + moveRotation);
        RB.rotation = Quaternion.Euler(moveRotation);

        nextCollision = 0;

        if (RB)
        {
          //  print("have the rigidbody!");
        }
    }

    void Update()
    {
        move();
    }

    void FixedUpdate ()
    {

    }

    //worry about rotation later
    private void move()
    {
        //aply a vector velocity in the direction it's looking
        RB.velocity = RB.transform.forward * speed;
    }

    private void fire_shot()
    {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
    }

    void OnTriggerStay(Collider other)
    {
       if(other.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<PlayerController>().team != team)
            {
                
                //print("pew");
                if (Time.time >= nextFire + Random.Range(0,4))
                {
                    fire_shot();
                }

                //the simulation was a little more interesting with the rotation after the first shot
                //it let a random shot let off flying SOMEWHERE, and let the next shot time be randomly done so the result wasn't mutual destruction (mostly)
                //this way the tanks have--- stupid behaviour. But that made it more fun to watch.
                //also more frustrating if you were to take bets I'd imagine, but hey.
                RB.transform.LookAt(other.gameObject.transform);
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "bullet")
        {
            print("bullet hit");
            if (other.gameObject.GetComponent<Mover>().team != team)
                Destroy(other.gameObject);
            Destroy(this.gameObject);
        }

        if((other.collider.tag == "player" || other.collider.tag == "boundary") && Time.time >= nextCollision)
        {
            RB.rotation *= Quaternion.Euler(0, 180, 0);
            nextCollision = Time.time + .1f;
        }

    }
}

