using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBattle : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown("space") || Input.touchCount > 0) {
            FadeManager.Instance.LoadScene("Battle", 2f);
        }
    }
}
