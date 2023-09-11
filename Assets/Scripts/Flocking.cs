using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flocking : MonoBehaviour
{
    public List<GameObject> neighbors;

    [SerializeField] private float cohesionRange, seperationRange;

    private Vector3 centerOfMass, seperationVector;

    private Rigidbody rb;

    private SphereCollider sc;

    private Vector3 averageVelocity;


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
            int cohesionCount = 0;
            int seperationCount = 0;
            seperationVector = Vector3.zero;
            centerOfMass = Vector3.zero;

            for (int i = 0; i < neighbors.Count; i++)
            {
                if (Vector3.Distance(neighbors[i].transform.position, transform.position) > seperationRange)
                {
                    centerOfMass = centerOfMass + neighbors[i].transform.position;
                    cohesionCount++;
                }
                else
                {
                    seperationVector += neighbors[i].transform.position - transform.position;
                    seperationCount++;
                    Debug.Log(seperationVector);
                }

            }
            if(cohesionCount > 0)
            {
                centerOfMass = centerOfMass / cohesionCount;
                rb.AddForce(((centerOfMass - transform.position) / sc.radius));
            }
            if(seperationCount > 0)
            {
                rb.AddForce((seperationVector / seperationCount));
            }


            for(int i = 0; i < neighbors.Count; i++)
            {
                averageVelocity += neighbors[i].GetComponent<Rigidbody>().velocity;
            }
            averageVelocity += rb.velocity;
            averageVelocity /= neighbors.Count + 1;

            rb.velocity += (averageVelocity - rb.velocity)/2;


        }

      



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
