using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistonController : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player") {
            if (Input.GetKey(KeyCode.F)) {
                Debug.Log("asdasd");
            }
        }
    }

}
