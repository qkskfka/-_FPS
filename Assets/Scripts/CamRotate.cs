using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotate : MonoBehaviour
{
    // ȸ�� �ӵ� ����
    public float rotSpeed = 200f;

    // ȸ���� ����
    float mx = 0;
    float my = 0;

    float current_time = 0;
    float delay_time = 3f;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // print(current_time);
        current_time += Time.deltaTime;
        if (current_time > delay_time)
        {
       
            // ���콺 �Է��� ����
            float mouse_X = Input.GetAxis("Mouse X");
            float mouse_Y = Input.GetAxis("Mouse Y");
            // ȸ�� �� ������ ���콺 �Է� ���� ����
            // a = a+b; == a +=b;
            mx += mouse_X * rotSpeed * Time.deltaTime;
            my += mouse_Y * rotSpeed * Time.deltaTime;
            // ȸ�� ���� -90������ +90���� ����
            my = Mathf.Clamp(my, -90f, 90f);
            // ��ü�� ȸ��
            transform.eulerAngles = new Vector3(-my, mx, 0);
        }
    }
}
