using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotate : MonoBehaviour
{
    // 회전 속도 변수
    public float rotSpeed = 200f;

    // 회전값 변수
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
       
            // 마우스 입력을 받음
            float mouse_X = Input.GetAxis("Mouse X");
            float mouse_Y = Input.GetAxis("Mouse Y");
            // 회전 값 변수에 마우스 입력 값을 누적
            // a = a+b; == a +=b;
            mx += mouse_X * rotSpeed * Time.deltaTime;
            my += mouse_Y * rotSpeed * Time.deltaTime;
            // 회전 값을 -90도에서 +90도로 제한
            my = Mathf.Clamp(my, -90f, 90f);
            // 물체를 회전
            transform.eulerAngles = new Vector3(-my, mx, 0);
        }
    }
}
