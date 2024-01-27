using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Letters
{
    M,
    A,
    K,
    E,
    L,
    U,
    G,
    H
}
public class CollectedLetters : MonoBehaviour
{
    public static HashSet<int> collectedLetters = new HashSet<int>();
}
