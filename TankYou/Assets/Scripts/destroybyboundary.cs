using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroybyboundary : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "bullet")
        {
            Destroy(other.gameObject);
        }
    }
}
