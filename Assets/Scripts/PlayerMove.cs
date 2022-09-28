using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 7f;
    CharacterController cc;
    //�߷� ����
    float gravity = -20f;
    // ���� �ӷ� ����
    float yVelocity = 0;
    // ������ ����
    public float jumpPower = 10f;
    // ���� ���� ����
    public bool isJumping = false;
    // �÷��̾� ü��
    public int hp = 10;
    // �ִ� ü��
    int maxHp = 10;
    // hp �����̴�
    public Slider hpSlider;

    // �÷��̾� �ǰ� �Լ�
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
        //�̵� ���� ����
        Vector3 dir = new Vector3(h, 0, v);
        dir = dir.normalized;
        // ���� ī�޶� �������� ���� ��ȯ
        dir = Camera.main.transform.TransformDirection(dir);
        transform.position += dir * moveSpeed * Time.deltaTime;
        // ���� ���̸鼭 �ٴڿ� �����ߴٸ�
        if(isJumping && cc.collisionFlags == CollisionFlags.Below)
        {
            isJumping = false;
            yVelocity = 0;
        }
        // ��������
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            yVelocity = jumpPower;
            isJumping = true;
        }

        // ĳ������ �����ӵ��� �߷°��� ����
        yVelocity += gravity * Time.deltaTime;
        dir.y = yVelocity;

        cc.Move(dir * moveSpeed * Time.deltaTime);

        hpSlider.value = (float)hp / (float)maxHp;
    }
}
