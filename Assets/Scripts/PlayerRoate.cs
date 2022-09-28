using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRoate : MonoBehaviour
{
    // 회전 속도
    public float rotSpeed = 200f;
    // 회전 값 변수
    float mx = 0;
    
    
    void Start()
    {
        
    }

    
    void Update()
    {
        //마우스 입력 값
        float mouse_X = Input.GetAxis("Mouse X");
        mx += mouse_X * rotSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, mx, 0);
    }
}
