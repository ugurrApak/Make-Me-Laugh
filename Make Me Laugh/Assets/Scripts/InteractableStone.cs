using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableStone : MonoBehaviour, IInteractable
{
    private bool isParent = false;
    public void Interact(Interactor interactor)
    {
        if (!isParent)
        {
            interactor.GetComponent<PlayerController>().MoveSpeed = 300f;
            transform.SetParent(interactor.transform);
            isParent = true;
        }
        else
        {
            interactor.GetComponent<PlayerController>().MoveSpeed = 500f;
            transform.parent = null;
            isParent = false;
        }
    }
}
