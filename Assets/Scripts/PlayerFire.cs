using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    // 피격 이펙트 오브젝트
    public GameObject bulletEffect;
    // 피격 파티클 시스템
    ParticleSystem ps;
    
    void Start()
    {
        ps = bulletEffect.GetComponent<ParticleSystem>();
    }

    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = new Ray(Camera.main.transform.position, 
                Camera.main.transform.forward);
            //레이가 부딫힌 정보 저장
            RaycastHit hitInfo = new RaycastHit();
            //부딪힌 물체가 있으면 피격 이펙트 표시
            if (Physics.Raycast(ray, out hitInfo))
            {
                bulletEffect.transform.position = hitInfo.point;
                ps.Play();
            }
        }
    }
}
