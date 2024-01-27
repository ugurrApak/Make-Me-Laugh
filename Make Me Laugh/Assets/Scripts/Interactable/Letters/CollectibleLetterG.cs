using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleLetterG : MonoBehaviour, IInteractable
{
    public void Interact(Interactor interactor)
    {
        CollectedLetters.collectedLetters.Add((int)Letters.G);
        StartCoroutine(WaitForInteract());
    }

    IEnumerator WaitForInteract()
    {
        yield return new WaitForSeconds(.4f);
        Destroy(gameObject);
    }
}
