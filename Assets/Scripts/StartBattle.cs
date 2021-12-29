using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBattle : MonoBehaviour
{
    bool _isWaiting;

    void Start()
    {
        StartCoroutine("Wait");
    }

    void Update()
    {
        if (_isWaiting) return;

        if (Input.GetKeyDown("space") || Input.touchCount > 0) {
            FadeManager.Instance.LoadScene("Battle", 2f);
        }
    }

    IEnumerator Wait()
    {
        _isWaiting = true;
        yield return new WaitForSeconds(5f);
        _isWaiting = false;
    }
}
