using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpGauge : MonoBehaviour
{
    Image _gauge;
    public float _t;
    float _maxValue;

    void Start()
    {
        _gauge = GetComponent<Image>();
        _maxValue = 1f;
        _t = 1f;
    }

    public void DecreaseValue(float hpRatio)
    {
        //var x = Mathf.Lerp(0f, _maxValue, hpRatio);
        _gauge.fillAmount = hpRatio;
    }
}
