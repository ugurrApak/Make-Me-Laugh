using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEffector3D : MonoBehaviour
{
    [SerializeField] private float force = 0f;
    [SerializeField] private float forceMagnitude = 0f;
    [SerializeField] private BoxCollider forceArea;

    private void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {

    }
}
