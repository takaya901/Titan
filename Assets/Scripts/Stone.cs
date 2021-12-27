using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NativeUtil;

public class Stone : Bullet, IDamagable
{
    new void Start()
    {
        base.Start();
        TargetPos = Camera.main.transform.position + new Vector3(0f, 0f, 0.5f);
        _audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// プレイヤーにヒットした時
    /// </summary>
    protected override void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) { return; }
        other.GetComponent<IDamagable>().TakeDamage();  //ヒットした相手の被弾処理
        AndroidUtil.Vibrate(100);
        _audioSource.Play();
        base.OnTriggerEnter(other);
    }

    public void TakeDamage()
    {
        Destroy(gameObject);
    }
}
