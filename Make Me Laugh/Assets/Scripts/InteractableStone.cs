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
            interactor.GetComponent<CharacterMovement>().MoveSpeed = 1.4f;
            interactor.GetComponent<CharacterMovement>().RunSpeed = 1.4f;
            transform.SetParent(interactor.transform);
            isParent = true;
        }
        else
        {
            interactor.GetComponent<CharacterMovement>().MoveSpeed = 2f;
            interactor.GetComponent<CharacterMovement>().RunSpeed = 3f;
            transform.parent = null;
            isParent = false;
        }
    }
}
