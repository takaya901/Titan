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

        // カメラに向くようにY軸のみ回転
        var lookRotation = Quaternion.LookRotation(Camera.main.transform.position - transform.position, Vector3.up);
        lookRotation.z = 0;
        lookRotation.x = 0;
        transform.rotation = lookRotation;
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
