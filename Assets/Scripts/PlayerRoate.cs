using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRoate : MonoBehaviour
{
    // ȸ�� �ӵ�
    public float rotSpeed = 200f;
    // ȸ�� �� ����
    float mx = 0;
    
    
    void Start()
    {
        
    }

    
    void Update()
    {
        //���콺 �Է� ��
        float mouse_X = Input.GetAxis("Mouse X");
        mx += mouse_X * rotSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, mx, 0);
    }
}
