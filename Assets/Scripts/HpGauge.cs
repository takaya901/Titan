using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpGauge : MonoBehaviour
{
    Image _gauge;

    void Start()
    {
        _gauge = GetComponent<Image>();
    }

    public void DecreaseValue(float hpRatio)
    {
        //var x = Mathf.Lerp(0f, _maxValue, hpRatio);
        _gauge.fillAmount = hpRatio;
    }
}
