using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FallingStone : MonoBehaviour
{
    private Rigidbody rb;
    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Done");
            //StartCoroutine(WaitForFall());
        }
    }
    IEnumerator WaitForFall()
    {
        yield return new WaitForSeconds(0.4f);
        rb.isKinematic = false;
        rb.useGravity= true;
        yield return new WaitForSeconds(1f);
        Destroy(this);
    }
}
