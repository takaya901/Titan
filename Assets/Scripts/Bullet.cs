using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NativeUtil;

public class Bullet : MonoBehaviour
{
    [SerializeField] float _flightTime = 4f;
    [SerializeField] protected Material _transparent;
    protected AudioSource _audioSource;

    public Vector3 TargetPos { get; set; }

    protected void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        StartCoroutine(Throw(TargetPos));
    }

    void Update()
    {
    }

    // https://www.gocca.work/unity-parabolic-movement/
    /// <summary>
    /// 現在位置から終着点への放物運動
    /// </summary>
    IEnumerator Throw(Vector3 targetPos)
    {
        var gravity = -9.8f;
        var speedRate = 1f;
        var startPos = transform.position; // 初期位置
        var diffY = (targetPos - startPos).y; // 始点と終点のy成分の差分
        var vn = (diffY - gravity * 0.5f * _flightTime * _flightTime) / _flightTime; // 鉛直方向の初速度vn

        // 放物運動
        for (var t = 0f; t < _flightTime; t += (Time.deltaTime * speedRate))
        {
            var p = Vector3.Lerp(startPos, targetPos, t / _flightTime);   //水平方向の座標を求める (x,z座標)
            p.y = startPos.y + vn * t + 0.5f * gravity * t * t; // 鉛直方向の座標 y
            transform.position = p;
            yield return null; //1フレーム経過
        }
        // 終点座標へ補正
        transform.position = targetPos;
    }

    /// <summary>
    /// プレイヤーにヒットした時
    /// </summary>
    protected virtual void OnTriggerEnter(Collider other)
    {
        gameObject.GetComponent<Renderer>().material = _transparent;
        _audioSource.Play();
        Destroy(gameObject, 1f);
    }

    enum Owner
    {
        Player,
        Enemy
    }
}
