using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Input;

public class Shooter : MonoBehaviour, IDamagable
{
    [SerializeField] GameObject _bulletPrefab;

    void Start()
    {
        
    }

    void Update()
    {
        if (GetKeyDown("space") || touchCount > 0){
            Shoot();
        }

    }

    void Shoot()
    {
        var bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
        // レーザー（ray）を飛ばす「起点」と「方向」
        Ray ray = new Ray(transform.position, transform.forward);
        var distance = 100f;
        //Debug.DrawLine(ray.origin, ray.direction * distance, Color.red, 5f);

        if (Physics.Raycast(ray, out RaycastHit hit, distance) && hit.collider.CompareTag("Enemy")) {
            Debug.Log("hit");
            bullet.GetComponent<PlayerBullet>().TargetPos = hit.point;
        }
        else {
            Debug.Log("no hit");
            bullet.GetComponent<PlayerBullet>().TargetPos = transform.forward * 30f;
        }
    }

    public void TakeDamage()
    {

    }
}
