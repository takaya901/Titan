using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Input;

public class Titan : MonoBehaviour, IDamagable
{
    [SerializeField] int _Hp = 10;
    [SerializeField] float _moveSpeed = 0.1f;
    [SerializeField] MoveType _moveType;
    [SerializeField] GameObject _stonePrefab;

    Vector2 _touchStart;     //タッチ開始時座標
    Vector2 _touchEnd;       //タッチ終了時座標
    Animator _anim;
    AudioSource _screaming;

    void Start()
    {
        _anim = gameObject.GetComponent<Animator>();
        _screaming = GetComponent<AudioSource>();

        // カメラに向くようにY軸のみ回転
        var lookRotation = Quaternion.LookRotation(Camera.main.transform.position - transform.position, Vector3.up);
        lookRotation.z = 0;
        lookRotation.x = 0;
        transform.rotation = lookRotation;
    }

    void Update()
    {
        Move();
        if (GetKeyDown("space") || touchCount > 0){
            ThrowStone();
        }
    }

    /// <summary>
    /// MoveTypeで指定された平面状を移動する
    /// </summary>
    void Move()
    {
        // タッチ開始の座標を保存
        if (GetMouseButtonDown(0)) {
            _touchStart = mousePosition;
        }

        // タッチ中はタッチ位置の移動量分だけモデルの座標を動かす
        if (GetMouseButton(0))
        {
            _touchEnd = mousePosition;

            Vector3 diff = Vector3.zero;

            switch (_moveType)
            {
                case MoveType.Xy:
                    diff = _touchEnd - _touchStart;
                    //diff.x = -diff.x;
                    break;
                //case MoveType.Xz:
                //    //diff = (_touchEnd - _touchStart).ToVector3xz();
                //    diff = -diff;
                //    break;
                //default:
                    //throw new ArgumentOutOfRangeException();
            }

            transform.position += diff * _moveSpeed;

            // 現在の座標を保存
            _touchStart = mousePosition;
        }

        // タッチ終了時に座標をリセット
        if (GetMouseButtonUp(0))
        {
            _touchStart = Vector2.zero;
            _touchEnd = Vector2.zero;
        }
    }

    void ThrowStone()
    {
        _anim.SetTrigger("Attack");
        var initPos = transform.position + new Vector3(0f, transform.localScale.y*0.8f, 0f);
        var stone = Instantiate(_stonePrefab, initPos, Quaternion.identity);
    }

    public void TakeDamage()
    {
        _anim = gameObject.GetComponent<Animator>();

        Debug.Log(_Hp);
        // HPが0になったら死ぬ
        if (--_Hp == 0)
        {
            _anim.SetTrigger("Die");
            _screaming.Play();
            Destroy(gameObject, 3f);
        }
        else {
            _anim.SetTrigger("Hit");
        }
    }

    enum MoveType
    {
        Xy,
        Xz
    }
}
