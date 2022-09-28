using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    // 타겟 오브젝트의 트랜스폼
    public Transform target;

    
    void Start()
    {
        
    }

    
    void Update()
    {
        // 카메라의 위치를 타겟 위치에 일치
        transform.position = target.position;
    }
}
