using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableStone : MonoBehaviour, IInteractable
{
    public bool Interact(Interactor interactor)
    {
        interactor.GetComponent<PlayerController>();
        transform.SetParent(interactor.transform);
        return true;
    }
}
