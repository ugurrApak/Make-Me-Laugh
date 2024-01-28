using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleLetterM : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject fanArea;
    public void Interact(Interactor interactor)
    {
        CollectedLetters.collectedLetters.Add((int)Letters.M);
        StartCoroutine(WaitForInteract());
        InstantiateFanArea();
    }

    IEnumerator WaitForInteract()
    {
        yield return new WaitForSeconds(.2f);
        Destroy(gameObject);
    }

    void InstantiateFanArea()
    {
        Instantiate(fanArea,transform.position,Quaternion.identity);
    }
}
