using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleLetterU : MonoBehaviour
{
    public void Interact(Interactor interactor)
    {
        CollectedLetters.collectedLetters.Add((int)Letters.U);
        StartCoroutine(WaitForInteract());
    }

    IEnumerator WaitForInteract()
    {
        yield return new WaitForSeconds(.4f);
        Destroy(gameObject);
    }
}
