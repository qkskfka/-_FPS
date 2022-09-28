using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyFSM : MonoBehaviour
{
    // ���ʹ� ���� ���
    enum EnemyState
    {
        Idle,
        Move,
        Attack,
        Return,
        Damaged,
        Die
    }
    // ���ʹ� ���� ����
    EnemyState m_State;
    // �÷��̾� �߰� ����
    public float findDistance = 8f;
    // �÷��̾� Ʈ������
    Transform player;
    //���� ���� ����
    public float attackDistance = 2f;
    // �̵� �ӵ�
    public float moveSpeed = 6f;
    CharacterController cc;
    // ���� �ð�
    float currentTime = 0;
    // ���� ������ �ð�
    float attackDelay = 2f;
    // ���ݷ�
    public int attackPower = 2;
    Vector3 originPos;
    public float moveDistance = 20f;

    public int hp = 15;
    int maxHP = 15;
    public Slider hpSlider;

    Animator anim;
    
    void Start()
    {
        //�ʱ� ���ʹ� ����
        m_State = EnemyState.Idle;
        player = GameObject.Find("Player").transform;
        cc = GetComponent<CharacterController>();
        originPos = transform.position;
        anim = transform.GetComponentInChildren<Animator>();
    }

    
    void Update()
    {
        switch (m_State)
        {
            case EnemyState.Idle:
                Idle();
                break;

            case EnemyState.Move:
                move();
                break;

            case EnemyState.Attack:
                Attack();
                break;

            case EnemyState.Return:
                Return();
                break;

        }

        hpSlider.value = (float)hp / (float)maxHP;
    }

    void Idle()
    {
        // ���� �÷��̾�� ���� �Ÿ��� find distance���� �۴ٸ� 
        if(Vector3.Distance(transform.position, player.position)
            < findDistance)
        {
            m_State = EnemyState.Move;
            Debug.Log("���� ��ȯ: Idle -> Move");
            anim.SetTrigger("IdleToMove");
        }
    }

    void move()
    {
        if(Vector3.Distance(transform.position, originPos) > moveDistance)
        {
            m_State = EnemyState.Return;
            print("���� ��ȯ : Move-> Return");
        }
        // ���� �÷��̾�� ���� �Ÿ��� ���� ���� ���̶�� �̵�
        else if (Vector3.Distance(transform.position, player.position) 
            > attackDistance)
        {
            Vector3 dir = (player.position - transform.position).normalized;
            cc.Move(dir * moveSpeed * Time.deltaTime);
            transform.forward = dir; // ������ �ٲپ� �÷��̾ ���� �پ�´�.
        }

        else
        {
            m_State = EnemyState.Attack;
            Debug.Log("���� ��ȯ: Move -> Attack");
            // ���� �ð��� ���� ������ �ð���ŭ �̸� ����
            currentTime = attackDelay;
        }
    }

    void Attack()
    {
        // ���� �÷��̾�� ���� �Ÿ��� ���ݹ��� ���� �ִٸ� 
        if(Vector3.Distance(transform.position, player.position)
            < attackDistance)
        {
            currentTime += Time.deltaTime;
            if(currentTime > attackDelay)
            {
                player.GetComponent<PlayerMove>().DamageAction(attackPower);
                print("����!");
                currentTime = 0;
            }
        }
        // ���߰� : Move
        else
        {
            m_State = EnemyState.Move;
            print("���� ��ȯ: Attack -> Move");
            currentTime = 0;
        }
    }
    void Return()
    {
        if(Vector3.Distance(transform.position, originPos) > 0.1f)
        {
            Vector3 dir = (originPos - transform.position).normalized;
            cc.Move(dir * moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = originPos;
            m_State = EnemyState.Idle;
            print("������ȯ: Return -> Idle");
        }
    }
}
