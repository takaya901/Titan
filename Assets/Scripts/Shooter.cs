using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Input;

public class Shooter : MonoBehaviour
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
        //// レーザー（ray）を飛ばす「起点」と「方向」
        //Ray ray = new Ray(transform.position, transform.forward);
        //RaycastHit hit;

        //if (Physics.Raycast(ray, out hit, 60))
        //{
        //    var hitName = hit.transform.gameObject.tag;

        //    if (hitName == "Enemy")
        //    {
        //        // 照準器の色を「赤」に変える（色は自由に変更してください。）
        //        aimImage.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        //    }
        //    else
        //    {
        //        // 照準器の色を「水色」（色は自由に変更してください。）
        //        aimImage.color = new Color(0.0f, 1.0f, 1.0f, 1.0f);
        //    }
        //}
        //else
        //{
        //    // 照準器の色を「水色」（色は自由に変更してください。）
        //    aimImage.color = new Color(0.0f, 1.0f, 1.0f, 1.0f);
        //}

        var bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().TargetPos = new Vector3(0f, 0f, 20f);
    }
}
