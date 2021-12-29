using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Input;

public class Shooter : MonoBehaviour
{
    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] float _coolTime = 1f;
    bool _isCooling;

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
        if (_isCooling) return; //冷却中は撃てない

        var pos = transform.position + Vector3.forward * 0.5f;  //カメラが隠れないように
        var bullet = Instantiate(_bulletPrefab, pos, Quaternion.identity);

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
        StartCoroutine("Cooling");
    }

    IEnumerator Cooling()
    {
        _isCooling = true;
        yield return new WaitForSeconds(_coolTime);
        _isCooling = false;
    }
}
