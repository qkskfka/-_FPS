using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyFSM : MonoBehaviour
{
    // 에너미 상태 상수
    enum EnemyState
    {
        Idle,
        Move,
        Attack,
        Return,
        Damaged,
        Die
    }
    // 에너미 상태 변수
    EnemyState m_State;
    // 플레이어 발견 범위
    public float findDistance = 8f;
    // 플레이어 트랜스폼
    Transform player;
    //공격 가능 범위
    public float attackDistance = 2f;
    // 이동 속도
    public float moveSpeed = 6f;
    CharacterController cc;
    // 누적 시간
    float currentTime = 0;
    // 공격 딜레이 시간
    float attackDelay = 2f;
    // 공격력
    public int attackPower = 2;
    Vector3 originPos;
    public float moveDistance = 20f;

    public int hp = 15;
    int maxHP = 15;
    public Slider hpSlider;

    Animator anim;
    
    void Start()
    {
        //초기 에너미 상태
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
        // 만약 플레이어와 적의 거리가 find distance보다 작다면 
        if(Vector3.Distance(transform.position, player.position)
            < findDistance)
        {
            m_State = EnemyState.Move;
            Debug.Log("상태 전환: Idle -> Move");
            anim.SetTrigger("IdleToMove");
        }
    }

    void move()
    {
        if(Vector3.Distance(transform.position, originPos) > moveDistance)
        {
            m_State = EnemyState.Return;
            print("상태 전환 : Move-> Return");
        }
        // 만약 플레이어와 적의 거리가 공격 범위 밖이라면 이동
        else if (Vector3.Distance(transform.position, player.position) 
            > attackDistance)
        {
            Vector3 dir = (player.position - transform.position).normalized;
            cc.Move(dir * moveSpeed * Time.deltaTime);
            transform.forward = dir; // 방향을 바꾸어 플레이어를 향해 뛰어온다.
        }

        else
        {
            m_State = EnemyState.Attack;
            Debug.Log("상태 전환: Move -> Attack");
            // 누적 시간을 공격 딜레이 시간만큼 미리 진행
            currentTime = attackDelay;
        }
    }

    void Attack()
    {
        // 만약 플레이어와 적의 거리가 공격범위 내에 있다면 
        if(Vector3.Distance(transform.position, player.position)
            < attackDistance)
        {
            currentTime += Time.deltaTime;
            if(currentTime > attackDelay)
            {
                player.GetComponent<PlayerMove>().DamageAction(attackPower);
                print("공격!");
                currentTime = 0;
            }
        }
        // 재추격 : Move
        else
        {
            m_State = EnemyState.Move;
            print("상태 전환: Attack -> Move");
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
            print("상태전환: Return -> Idle");
        }
    }
}
