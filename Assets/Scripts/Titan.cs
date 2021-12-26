using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Input;

public class Titan : MonoBehaviour
{
    [SerializeField] GameObject _stonePrefab;
    Animator _anim;

    void Start()
    {
        _anim = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if (GetKeyDown("space") || touchCount > 0){
            ThrowStone();
        }
    }

    void ThrowStone()
    {
        _anim.SetTrigger("Attack");
        var initPos = transform.position + new Vector3(0f, transform.localScale.y*0.8f, 0f);
        var stone = Instantiate(_stonePrefab, initPos, Quaternion.identity);
    }
}
