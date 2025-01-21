using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enermy_FSM : MonoBehaviour
{
    enum EnermyState
    { 
        IDLE,
        MOVE,
        ATTACK,
        RETURN,
        DAMEGED,
        DIE
    }

    Transform player;
    CharacterController CC;
    EnermyState State;
    float Find_Distance = 8f;
    float Attack_Distance = 2f;
    public float Move_Distance = 20f;
    Vector3 OriginPos;

    float Move_Speed = 5f;
    float Cur_Time = 0f;

    public int MaxHp = 30;
    int Hp = 0;
    float Attack_Delay = 2f;
    public int Attack_Dammege = 5;


    // Start is called before the first frame update
    void Start()
    {
        State = EnermyState.IDLE;

        OriginPos = transform.position;

        player = GameObject.Find("Player").transform;

        CC = GetComponent<CharacterController>();

        Hp = MaxHp;
    }

    // Update is called once per frame
    void Update()
    {
        switch (State)
        { 
            case EnermyState.IDLE:
                Idle();
                break;
            case EnermyState.MOVE:
                Move();
                break;
            case EnermyState.RETURN:
                Return();
                break;
            case EnermyState.ATTACK:
                Attack();
                break;
            case EnermyState.DAMEGED:
                //Dameged();
                break;
            case EnermyState.DIE:
                //Die();
                break;
        }
    }

    void Idle()
    {
        if (Vector3.Distance(transform.position, player.position) < Find_Distance)
        {
            print("���º��� : IDLE -> MOVE");
            State = EnermyState.MOVE;
        }
    }

    void Move()
    {
        if (Vector3.Distance(transform.position, OriginPos) > Move_Distance)
        {
            print("���º��� : MOVE -> RETURN");
            State = EnermyState.RETURN;
        }
        else if (Vector3.Distance(transform.position, player.position) > Attack_Distance)
        {
            Vector3 Dir = (player.position - transform.position).normalized;
            CC.Move(Dir * Move_Speed * Time.deltaTime);
        }
        else
        {
            print("���º��� : MOVE -> ATTACK");
            Cur_Time = Attack_Delay;
            State = EnermyState.ATTACK;
        }
    }

    void Return()
    {
        if (Vector3.Distance(transform.position, OriginPos) > 0.1f)
        {
            Vector3 Dir = (OriginPos - transform.position).normalized;
            CC.Move(Dir * Move_Speed * Time.deltaTime);
        }
        else
        {
            transform.position = OriginPos;
            Hp = MaxHp;
            print("���º��� : MOVE -> IDLE");
            State = EnermyState.IDLE;
        }
    }

    void Attack()
    {
        if (Vector3.Distance(transform.position, player.position) < Attack_Distance)
        {
            Cur_Time += Time.deltaTime;
            if (Cur_Time > Attack_Delay)
            {
                Cur_Time = 0f;
                player.GetComponent<Player_Move>().Damaaged(Attack_Dammege);
                print("ATTACK");
            }
        }
        else
        {
            Cur_Time = 0f;
            print("���º��� : ATTACK -> MOVE");
            State = EnermyState.MOVE;
        }
    }

    public void Hit_Enermy(int Dam)
    {
        if(State == EnermyState.DIE || State == EnermyState.RETURN || State == EnermyState.DAMEGED)
        { 
           return; 
        }

        print("���º��� : ����");
        Hp -= Dam;
        if (Hp > 0)
        {
            State = EnermyState.DAMEGED;
            print("���º��� : Hit -> DAMEGED");
            Dameged();
        }
        else 
        {
            State = EnermyState.DIE;
            print("���º��� : Hit -> DIE");
            Die();
        }

    }


    void Dameged()
    {
        StartCoroutine(DamegeProcess());
    }

    
    IEnumerator DamegeProcess()
    {
        //0.5�� ������!!
        yield return new WaitForSeconds(0.5f);

        print("���º��� : DAMEGED -> MOVE");
        State = EnermyState.MOVE;
    }

    void Die()
    {
        StopAllCoroutines();
        StartCoroutine(DieProcess());
    }

    IEnumerator DieProcess()
    {
        //ĳ���� ��Ʈ�ѷ��� ��Ȱ��ȭ ��Ų��.
        CC.enabled = false;

        //2�� ����� �Ҹ�!!
        yield return new WaitForSeconds(2f);

        print(" �Ҹ� ");
        Destroy(gameObject);
    }

}
