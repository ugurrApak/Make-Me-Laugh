using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FallingStone : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            StartCoroutine(WaitForFall());
        }
    }
    IEnumerator WaitForFall()
    {
        yield return new WaitForSeconds(0.4f);
        gameObject.AddComponent<Rigidbody>();
        yield return new WaitForSeconds(1f);
        Destroy(this);
    }
}
