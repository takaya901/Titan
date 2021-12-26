using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NativeUtil;

public class Stone : MonoBehaviour
{
    [SerializeField] float _flightTime = 1f;
    [SerializeField] Material _transparent;
    Vector3 _endPos;
    AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _endPos = Camera.main.transform.position + new Vector3(0f, 0f, 0.5f);
        StartCoroutine(Throw(_endPos));
    }

    void Update()
    {

    }

    // https://www.gocca.work/unity-parabolic-movement/
    // 現在位置からendPosへの放物運動
    IEnumerator Throw(Vector3 endPos)
    {
        var gravity = -9.8f;
        var speedRate = 1f;
        var startPos = transform.position; // 初期位置
        var diffY = (endPos - startPos).y; // 始点と終点のy成分の差分
        var vn = (diffY - gravity * 0.5f * _flightTime * _flightTime) / _flightTime; // 鉛直方向の初速度vn

        // 放物運動
        for (var t = 0f; t < _flightTime; t += (Time.deltaTime * speedRate))
        {
            var p = Vector3.Lerp(startPos, endPos, t / _flightTime);   //水平方向の座標を求める (x,z座標)
            p.y = startPos.y + vn * t + 0.5f * gravity * t * t; // 鉛直方向の座標 y
            transform.position = p;
            yield return null; //1フレーム経過
        }
        // 終点座標へ補正
        transform.position = endPos;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            gameObject.GetComponent<Renderer>().material = _transparent;
            _audioSource.Play();
            AndroidUtil.Vibrate(100);
            Destroy(gameObject, 1f);
        }
    }
}
