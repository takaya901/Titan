using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireworksManager : MonoBehaviour
{
    [SerializeField] GameObject[] _rockets;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(6f);
        foreach (var rocket in _rockets) {
            rocket.SetActive(true);
        }
    }
}
