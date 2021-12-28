using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlushOnDamaged : MonoBehaviour
{
    Image _img;

    void Start()
    {
        _img = GetComponent<Image>();
        _img.color = Color.clear;
    }

    void Update()
    {
        // 時間が経過するにつれて徐々に透明に戻す
        _img.color = Color.Lerp(_img.color, Color.clear, Time.deltaTime);
    }

    public void Flush(BulletType bulletType)
    {
        switch (bulletType)
        {
            case BulletType.Stone:
                _img.color = new Color(0.5f, 0f, 0f, 0.5f);
                break;
            case BulletType.Poison:
                _img.color = new Color(0.43f, 0f, 1f, 0.5f);
                break;
            default:
                break;
        }
    }
}
