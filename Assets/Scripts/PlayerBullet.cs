using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet, IDamagable
{
    [SerializeField] GameObject _explosionPrefab;

    /// <summary>
    /// EnemyかEnemyの弾にヒットした時
    /// </summary>
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) { return; }
        other.transform.root.GetComponent<IDamagable>().TakeDamage();  //ヒットした相手の被弾処理
        // 爆発
        var explosion = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        Destroy(explosion, 2f);
        base.OnTriggerEnter(other);
    }

    public void TakeDamage()
    {
    }
}
