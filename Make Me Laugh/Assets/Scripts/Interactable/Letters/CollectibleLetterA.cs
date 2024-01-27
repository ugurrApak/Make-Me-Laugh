using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleLetterA : MonoBehaviour, IInteractable
{
    public void Interact(Interactor interactor)
    {
        CollectedLetters.collectedLetters.Add((int)Letters.A);
        StartCoroutine(WaitForInteract());
    }

    IEnumerator WaitForInteract()
    {
        yield return new WaitForSeconds(.4f);
        Destroy(gameObject);
    }
}
