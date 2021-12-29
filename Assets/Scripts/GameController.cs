using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    void Start()
    {
        Input.backButtonLeavesApp = true;
    }

    void Update()
    {
        var enemyNum = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemyNum.Length <= 0) {
            SceneManager.LoadScene("Clear");
        }
    }
}
