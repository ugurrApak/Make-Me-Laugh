using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleLetterL : MonoBehaviour, IInteractable
{
    public void Interact(Interactor interactor)
    {
        CollectedLetters.collectedLetters.Add((int)Letters.L);
        StartCoroutine(WaitForInteract());
    }

    IEnumerator WaitForInteract()
    {
        yield return new WaitForSeconds(.4f);
        Destroy(gameObject);
    }
}
