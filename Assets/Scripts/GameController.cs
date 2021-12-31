using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    void Start()
    {
        Input.backButtonLeavesApp = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            ShowQuitDialog();
        }

        var enemyNum = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemyNum.Length <= 0) {
            FadeManager.Instance.LoadScene("Clear", 1f);
        }
    }

    /// <summary>
    /// 戻るボタンが押されたら確認のダイアログを表示
    /// </summary>
    void ShowQuitDialog()
    {
        Time.timeScale = 0f;

        CBNativeDialog.Instance.Show(title: "",
            message: "ゲームを終了しますか？",
            positiveButtonTitle: "はい",
            positiveButtonAction: () => { Application.Quit(); },
            negativeButtonTitle: "いいえ",
            negativeButtonAction: () => { Time.timeScale = 1f; });
    }
}
