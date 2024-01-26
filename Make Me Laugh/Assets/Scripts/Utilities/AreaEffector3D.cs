using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEffector3D : MonoBehaviour
{
    [SerializeField] private float force = 0f;
    [SerializeField] private float forceMagnitude = 0f;
    [SerializeField] private BoxCollider forceArea;


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Done");
        other.GetComponent<Rigidbody>().velocity = Vector3.up * force;
    }

}
