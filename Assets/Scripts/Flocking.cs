using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flocking : MonoBehaviour
{
    public List<GameObject> neighbors;

    private float cohesionRange, seperationRange;

    private Vector3 centerOfMass;

    private Rigidbody rb;

    private SphereCollider sc;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        sc = GetComponent<SphereCollider>();
    }


    private void FixedUpdate()
    {
        if(neighbors.Capacity != 0)
        {
            centerOfMass = Vector3.zero;


            for (int i = 0; i < neighbors.Count; i++)
            {
                
                centerOfMass = centerOfMass + neighbors[i].transform.position;

            }

            centerOfMass = centerOfMass / neighbors.Count;
            
            Debug.Log(centerOfMass);
        }

        rb.AddForce(((centerOfMass - transform.position) / sc.radius) * 10);



    }






    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bird"))
        {
            neighbors.Add(other.gameObject);
        }


    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Bird"))
        {
            neighbors.Remove(other.gameObject);
        }
    }

}
