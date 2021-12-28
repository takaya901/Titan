using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Input;

/// <summary>
/// 横方向にランダム移動、ランダムな間隔で攻撃
/// </summary>
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
    bool _isDead;

    bool _isWalking;
    Vector3 _walkDestination;

    void Start()
    {
        _anim = gameObject.GetComponent<Animator>();
        _screaming = GetComponent<AudioSource>();

        // カメラに向くようにY軸のみ回転
        transform.rotation = Utils.LookRotationY(transform.position, Camera.main.transform.position);

        StartCoroutine("Attack");
    }

    void Update()
    {
        if (_isDead) return;
        Walk();

        //Move();
    }

    /// <summary>
    /// ランダムに左右に歩いたり止まったり
    /// </summary>
    void Walk()
    {
        // 目的地を設定
        if (!_isWalking)
        {
            _isWalking = true;
            var targetX = Random.Range(-30f, 30f);
            Debug.Log(targetX);
            _walkDestination = new Vector3(targetX, transform.position.y, transform.position.z);
            transform.rotation = Utils.LookRotationY(transform.position, _walkDestination);
            _anim.SetTrigger("Walk");
            return;
        }

        // 目的地に到達したとき
        if (transform.position == _walkDestination){
            _isWalking = false;
            return;
        }

        if (_anim.GetCurrentAnimatorClipInfo(0)[0].clip.name != "attack(2)")　{
            transform.rotation = Utils.LookRotationY(transform.position, _walkDestination);
        }
        else {
            transform.rotation = Utils.LookRotationY(transform.position, Camera.main.transform.position);
        }
        transform.position = Vector3.MoveTowards(transform.position, _walkDestination, Time.deltaTime);
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

    // ランダムな時間間隔で攻撃
    IEnumerator Attack()
    {
        while (true)
        {
            ThrowStone();
            yield return new WaitForSeconds(Random.Range(3f, 10f));
        }
    }

    void ThrowStone()
    {
        _anim.SetTrigger("Attack");
        transform.rotation = Utils.LookRotationY(transform.position, Camera.main.transform.position);
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
            _isDead = true;
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
