using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using Unity.VisualScripting;

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

    NavMeshAgent nav;
    Animator Anim;
    AudioSource Sound;
    Transform player;
    CharacterController CC;
    EnermyState State;
    float Find_Distance = 25f;
    float Attack_Distance = 2f;
    float Move_Distance = 50f;
    Vector3 OriginPos;
    Quaternion OriginRot;

    float Move_Speed = 5f;
    float Cur_Time = 0f;

    public Slider Hp_Slider;
    public int MaxHp = 30;
    int Hp = 0;
    float Attack_Delay = 2f;
    int Attack_Dammege = 10;


    // Start is called before the first frame update
    void Start()
    {
        Anim = transform.GetComponentInChildren<Animator>();
        Sound = GetComponent<AudioSource>();
        Sound.Stop();
        State = EnermyState.IDLE;

        OriginPos = transform.position;
        OriginRot = transform.rotation;

        player = GameObject.Find("Player").transform;

        CC = GetComponent<CharacterController>();
        nav = GetComponent<NavMeshAgent>();

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

        Hp_Slider.value = (float)Hp/ (float)MaxHp;
    }

    void Idle()
    {
        if (Vector3.Distance(transform.position, player.position) < Find_Distance)
        {
            print("상태변경 : IDLE -> MOVE");

            Anim.SetTrigger("IDLEtoMOVE");

            State = EnermyState.MOVE;
        }
    }

    void Move()
    {
        if (Vector3.Distance(transform.position, OriginPos) > Move_Distance)
        {
            print("상태변경 : MOVE -> RETURN");
            State = EnermyState.RETURN;
        }
        else if (Vector3.Distance(transform.position, player.position) > Attack_Distance)
        {
            //Vector3 Dir = (player.position - transform.position).normalized;
            //CC.Move(Dir * Move_Speed * Time.deltaTime);
            //transform.forward = Dir;   
            nav.isStopped = true;
            nav.ResetPath();

            nav.stoppingDistance = Attack_Distance;
            nav.SetDestination(player.position);
        }
        else
        {
            print("상태변경 : MOVE -> ATTACK");
            Cur_Time = Attack_Delay;
            Anim.SetTrigger("MOVEtoATTACKDELAY");
            State = EnermyState.ATTACK;
        }
    }

    void Return()
    {
        if (Vector3.Distance(transform.position, OriginPos) > 0.1f)
        {
            //Vector3 Dir = (OriginPos - transform.position).normalized;
            //CC.Move(Dir * Move_Speed * Time.deltaTime);
            //transform.forward = Dir;
            nav.SetDestination(OriginPos);
            nav.stoppingDistance = 0;
        }
        else
        {
            nav.isStopped = true;
            nav.ResetPath();

            transform.position = OriginPos;
            transform.rotation = OriginRot;

            Hp = MaxHp;

            print("상태변경 : MOVE -> IDLE");
            Anim.SetTrigger("MOVEtoIDLE");

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
                
                //player.GetComponent<Player_Move>().Damaaged(Attack_Dammege);               
                
                print("ATTACK");
                Anim.SetTrigger("STARTATTACK");
            }
        }
        else
        {
            Cur_Time = 0f;
            print("상태변경 : ATTACK -> MOVE");
            Anim.SetTrigger("ATTACKtoMOVE");
            State = EnermyState.MOVE;
        }
    }

    public void AttackAction()
    {
        player.GetComponent<Player_Move>().Damaaged(Attack_Dammege);
    }


    public void Hit_Enermy(int Dam)
    {
        if(State == EnermyState.DIE ||  State == EnermyState.DAMEGED)//State == EnermyState.RETURN ||
        { 
           return; 
        }

        print("상태변경 : 아파");
        Hp -= Dam;

        nav.isStopped = true;
        nav.ResetPath();


        if (Hp > 0)
        {
            State = EnermyState.DAMEGED;
            print("상태변경 : Hit -> DAMEGED");
            Anim.SetTrigger("DAMAGED");
            Dameged();
        }
        else 
        {
            State = EnermyState.DIE;
            print("상태변경 : Hit -> DIE");
            Anim.SetTrigger("DIE");
            Die();
        }

    }


    void Dameged()
    {
        StartCoroutine(DamegeProcess());
    }

    
    IEnumerator DamegeProcess()
    {
        //0.5초 대기시켜!!
        yield return new WaitForSeconds(1f);       
        print("상태변경 : DAMEGED -> MOVE");
        State = EnermyState.MOVE;
    }

    void Die()
    {
        Sound.Play();
        StopAllCoroutines();
        StartCoroutine(DieProcess());
    }

    IEnumerator DieProcess()
    {
        //캐릭터 컨트롤러를 비활성화 시킨다.
        CC.enabled = false;

        //2초 대기후 소멸!!
        yield return new WaitForSeconds(2f);

        print(" 소멸 ");
        Destroy(gameObject);
    }

}
