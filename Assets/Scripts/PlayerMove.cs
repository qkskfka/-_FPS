using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 7f;
    CharacterController cc;
    //중력 변수
    float gravity = -20f;
    // 수직 속력 변수
    float yVelocity = 0;
    // 점프력 변수
    public float jumpPower = 10f;
    // 점프 상태 변수
    public bool isJumping = false;
    // 플레이어 체력
    public int hp = 10;
    // 최대 체력
    int maxHp = 10;
    // hp 슬라이더
    public Slider hpSlider;

    // 플레이어 피격 함수
    public void DamageAction(int damage)
    {
        hp -= damage;
        
        if(hp < 0)
        {
            hp = 0;
        }
        print(hp);
    }
    
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        //이동 방향 설정
        Vector3 dir = new Vector3(h, 0, v);
        dir = dir.normalized;
        // 메인 카메라를 기준으로 방향 전환
        dir = Camera.main.transform.TransformDirection(dir);
        transform.position += dir * moveSpeed * Time.deltaTime;
        // 점프 중이면서 바닥에 착지했다면
        if(isJumping && cc.collisionFlags == CollisionFlags.Below)
        {
            isJumping = false;
            yVelocity = 0;
        }
        // 점프구현
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            yVelocity = jumpPower;
            isJumping = true;
        }

        // 캐릭터의 수직속도에 중력값을 적용
        yVelocity += gravity * Time.deltaTime;
        dir.y = yVelocity;

        cc.Move(dir * moveSpeed * Time.deltaTime);

        hpSlider.value = (float)hp / (float)maxHp;
    }
}
