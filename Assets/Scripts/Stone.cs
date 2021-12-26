using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NativeUtil;

public class Stone : Bullet
{
    new void Start()
    {
        base.Start();
        TargetPos = Camera.main.transform.position + new Vector3(0f, 0f, 0.5f);
    }

    /// <summary>
    /// プレイヤーにヒットした時
    /// </summary>
    protected override void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) { return; }
        AndroidUtil.Vibrate(100);
        base.OnTriggerEnter(other);
    }
}
