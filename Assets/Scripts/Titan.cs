using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (Input.GetKeyDown("space"))
        {
            ThrowStone();
        }
    }

    void ThrowStone()
    {
        _anim.SetBool("Attack", true);
        //_anim.SetBool("Attack", false);
        //_anim.SetTrigger("Throw");
        //_anim.SetTrigger("Idle");
        var initPos = transform.position + new Vector3(0f, transform.localScale.y*0.8f, 0f);
        var stone = Instantiate(_stonePrefab, initPos, Quaternion.identity);
    }
}
