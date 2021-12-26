using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet
{
    [SerializeField] GameObject _explosionPrefab;

    /// <summary>
    /// EnemyかEnemyの弾にヒットした時
    /// </summary>
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) { return; }
        // 爆発
        var explosion = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        Destroy(explosion, 2f);
        GetComponent<AudioSource>().Play();
        base.OnTriggerEnter(other);
    }
}
