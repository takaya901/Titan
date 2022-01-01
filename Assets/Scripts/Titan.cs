using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Input;

/// <summary>
/// 横方向にランダム移動、ランダムな間隔で攻撃
/// </summary>
public class Titan : MonoBehaviour, IDamagable
{
    [SerializeField] int _hp = 10;
    [SerializeField] float _speed = 10f;
    [SerializeField] float _moveSpeed = 0.1f;
    [SerializeField] MoveType _moveType;
    [SerializeField] GameObject _stonePrefab;
    [SerializeField] BulletType _bulletType;

    Vector2 _touchStart, _touchEnd; //タッチ開始時座標と終了時座標

    Transform _tf;
    Vector3 _cameraPos;
    Animator _anim;
    AudioSource _screaming;
    /// <summary> HPが0になってから死に声再生終わるまでtrue </summary>
    bool _isDead;
    Vector3 _walkDestination;
    const float FIELD_WIDTH = 30f;

    void Start()
    {
        _tf = transform;
        _cameraPos = Camera.main.transform.position;
        _anim = gameObject.GetComponent<Animator>();
        _screaming = GetComponent<AudioSource>();

        _walkDestination = _tf.position;
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
        // 次の目的地を設定
        if (_tf.position == _walkDestination)
        {
            var targetX = Random.Range(-FIELD_WIDTH, FIELD_WIDTH);
            _walkDestination = new Vector3(targetX, _tf.position.y, _tf.position.z);
            _anim.SetTrigger("Walk");
            return;
        }

        // 攻撃アニメーション中は移動しない
        if (_anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Attack") {
            _tf.rotation = Utils.LookRotationY(_tf.position, _cameraPos);
        }
        // 目的地に向かって移動
        else {
            // 振り向き時と攻撃後にゆっくり目的地を向く
            var lookRotation = Utils.LookRotationY(_tf.position, _walkDestination);
            _tf.rotation = Quaternion.Lerp(_tf.rotation, lookRotation, 0.1f);
            _tf.position = Vector3.MoveTowards(_tf.position, _walkDestination, Time.deltaTime * _speed);
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

            _tf.position += diff * _moveSpeed;

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

    /// <summary>
    /// ランダムな時間間隔で攻撃
    /// </summary>
    IEnumerator Attack()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(8f, 10f));
            ThrowStone();
        }
    }

    void ThrowStone()
    {
        if (_isDead) return;

        _anim.SetTrigger("Attack");
        _tf.rotation = Utils.LookRotationY(_tf.position, _cameraPos);
        var initPos = _tf.position + new Vector3(0f, transform.localScale.y*0.8f, 0f);
        var stone = Instantiate(_stonePrefab, initPos, Quaternion.identity);
        stone.GetComponent<Stone>().Type = _bulletType;
    }

    public void TakeDamage(BulletType bulletType)
    {
        if (_isDead) return;    //Die中にHitアニメーション再生しないように
        if (_anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Get_hit") return;  //被弾モーション中は無敵

        Debug.Log(_hp);
        // HPが0になったら死ぬ
        if (--_hp <= 0)
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
