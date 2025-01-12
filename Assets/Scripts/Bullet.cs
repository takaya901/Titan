﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NativeUtil;

public class Bullet : MonoBehaviour
{
    [SerializeField] float _flightTime = 2f;
    protected AudioSource _audioSource;

    Transform _tf;
    public BulletType Type { get; set; }
    public Vector3 TargetPos { get; set; }

    const float GRAVITY = -9.8f;
    const float SPEED_RATE = 1f;

    protected void Start()
    {
        _tf = transform;
        _audioSource = GetComponent<AudioSource>();
        StartCoroutine(Throw(TargetPos));
    }

    void Update()
    {
        if (_tf.position == TargetPos) {
            Destroy(gameObject);
        }

        _tf.Rotate(new Vector3(-90f, 0f, 0f) * Time.deltaTime, Space.World);
    }

    // https://www.gocca.work/unity-parabolic-movement/
    /// <summary>
    /// 現在位置から終着点への放物運動
    /// </summary>
    IEnumerator Throw(Vector3 targetPos)
    {
        var startPos = _tf.position; // 初期位置
        var diffY = (targetPos - startPos).y; // 始点と終点のy成分の差分
        var vn = (diffY - GRAVITY * 0.5f * _flightTime * _flightTime) / _flightTime; // 鉛直方向の初速度vn

        // 放物運動
        for (var t = 0f; t < _flightTime; t += (Time.deltaTime * SPEED_RATE))
        {
            var p = Vector3.Lerp(startPos, targetPos, t / _flightTime);   //水平方向の座標を求める (x,z座標)
            p.y = startPos.y + vn * t + 0.5f * GRAVITY * t * t; // 鉛直方向の座標 y
            _tf.position = p;
            yield return null; //1フレーム経過
        }
        // 終点座標へ補正
        _tf.position = targetPos;
    }

    /// <summary>
    /// プレイヤーにヒットした時
    /// </summary>
    protected virtual void OnTriggerEnter(Collider other)
    {
        GetComponent<Collider>().enabled = false;
        GetComponent<Renderer>().enabled = false;
        Destroy(gameObject, 1f);
    }
}

public enum BulletType
{
    Player,
    Stone,
    Poison
}
