using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    // Ÿ�� ������Ʈ�� Ʈ������
    public Transform target;

    
    void Start()
    {
        
    }

    
    void Update()
    {
        // ī�޶��� ��ġ�� Ÿ�� ��ġ�� ��ġ
        transform.position = target.position;
    }
}
