using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{
    [SerializeField] int _maxHp = 10;
    [SerializeField] HpGauge _hpGauge;
    int _currentHp;

    void Start()
    {
        _currentHp = _maxHp;
    }
    
    void Update()
    {

    }

    public void TakeDamage()
    {
        _currentHp--;
        _hpGauge.DecreaseValue(_currentHp / (float)_maxHp);
    }
}
