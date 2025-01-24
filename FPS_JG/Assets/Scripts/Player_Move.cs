using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Move : MonoBehaviour
{
    public GameObject Hit_Effect;
    CharacterController cc;
    Animator Anim;

    public float Move_Speed = 7f;
    public float Jump_Power = 10f;
    public float rotationSpeed = 10f;
    float gravity = -20f;

    
    int Hp = 0;
    public int MaxHp = 100;
    public Slider Hp_Slider;

    public bool IsJump = false;
    public float yVelocity = 0f;
    bool Die = false;


    // Start is called before the first frame update
    void Start()
    {
        Hp = MaxHp;

        cc = GetComponent<CharacterController>();
        Anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerHP <= 0 && !Die)
        {
            Die = true;
            Anim.SetTrigger("DIE");
        }

        Hp_Slider.value = (float)Hp / (float)MaxHp;

        if (GameManager.Gm.State != GameManager.GameState.Run)
        {
            return;
        }


            // 1) �Է�
        float h = Input.GetAxis("Horizontal"); // -1~+1
        float v = Input.GetAxis("Vertical");   // -1~+1
        Vector2 inputDir = new Vector2(h, v);

        // 2) ī�޶� ���� �̵� ����
        Vector3 camForward = Camera.main.transform.forward;
        camForward.y = 0f;
        camForward.Normalize();

        Vector3 camRight = Camera.main.transform.right;
        camRight.y = 0f;
        camRight.Normalize();

        // ���� �̵� ���� (���� ��ǥ��)
        Vector3 moveDir = camForward * v + camRight * h;
        float inputMagnitude = inputDir.magnitude; // 0 ~ 1

        // 3) ���� & �߷�
        if (cc.isGrounded)
        {
            if (IsJump)
            {
                IsJump = false;
                yVelocity = 0f;
            }
            if (Input.GetButtonDown("Jump"))
            {
                yVelocity = Jump_Power;
                IsJump = true;
            }
        }
        yVelocity += gravity * Time.deltaTime;
        moveDir.y = yVelocity;

        // 4) �̵�
        cc.Move(moveDir * Move_Speed * Time.deltaTime);
        

        // ========== �ٽ�: �׻� ī�޶� ������ �ٶ󺸱� ==========
        // ĳ���Ͱ� (y�� ����) ī�޶� �������� ȸ��
        if (camForward.sqrMagnitude > 0.001f)
        {
            Quaternion targetRot = Quaternion.LookRotation(camForward);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
        }

        // ========== 2D Blend Tree �Ķ����(Speed, Angle) ==========
        // (A) Speed: �Է� ����(0=����, 1=�ִ�)
        float speed = Mathf.Clamp01(inputMagnitude);

        // (B) Angle: "ī�޶� ����"�� "�̵� ����" ������ ���� (��ȣ ����)
        //  - ex) ����=0��, ������=+90��, ����=-90��, ��=��180��
        float angle = 0f;
        if (speed > 0.01f)
        {
            // �̵� ����(�����)
            Vector3 planeMoveDir = new Vector3(moveDir.x, 0f, moveDir.z).normalized;
            // ī�޶� �ٶ󺸴� ����
            Vector3 planeCamFwd = camForward; // �̹� y=0 ó����

            angle = Vector3.SignedAngle(planeCamFwd, planeMoveDir, Vector3.up);
            // angle ����: -180 ~ +180
        }

        // �� Animator�� ����
        Anim.SetFloat("MoveMotion", inputDir.magnitude);
        //Anim.SetFloat("Speed", speed);
        //Anim.SetFloat("Angle", angle);

        //Hp_Slider.value = (float)Hp / (float)MaxHp;
    }
    void basemove()
    {
        //float h = Input.GetAxis("Horizontal");
        //float v = Input.GetAxis("Vertical");

        //Vector3 Dir = new Vector3(h, 0, v);
        //Dir = Dir.normalized;
        //Anim.SetFloat("MoveMotion", Dir.magnitude);
        //Dir = Camera.main.transform.TransformDirection(Dir);


        //if (cc.collisionFlags == CollisionFlags.Below)
        //{
        //    if (IsJump)
        //    {
        //        IsJump = false;
        //        yVelocity = 0f;
        //    }
        //}

        //if (Input.GetButtonDown("Jump") && !IsJump)
        //{
        //    yVelocity = Jump_Power;
        //    IsJump = true;
        //}

        //yVelocity += gravity * Time.deltaTime;
        //Dir.y = yVelocity;

        //cc.Move(Dir* Move_Speed* Time.deltaTime);

        //PlayerRotate(v, h, Dir);
    }
    void PlayerRotate(float v, float h, Vector3 Dir)
    {
        //transform.position += Dir * Move_Speed * Time.deltaTime;
        // ��������������������������������������������������������������������������������������������������
        // ĳ���� ȸ�� (TPS: �̵� ������ ���ϵ���)
        //    - �Ʒ��� "�̵� ����"�� �������� ȸ��
        //    - �Ǵ� "ī�޶� ����"�� �������� ȸ���ϰ� �ʹٸ�
        //      Camera.main.transform.forward �� ��ü ����
        // ��������������������������������������������������������������������������������������������������
        // y��(����) ����, ��鿡���� �ٶ� ���� ���

        //   "��Ȯ�� ����(W)��" ���� ���� ȸ��
        //    ��, v > 0 (��) �̸鼭, h�� 0�� ������(�¿찡 ���� ����)

        if (v > 0.01f && Mathf.Abs(h) < 0.01f)
        {
            Vector3 lookDir = new Vector3(Dir.x, 0f, Dir.z);

            if (lookDir.sqrMagnitude > 0.001f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(lookDir);
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    targetRotation,
                    rotationSpeed * Time.deltaTime
                );
            }
        }
    }



    public void Damaaged(int Dam)
    {
        Hp -= Dam;

        if (Hp > 0)
        {
            StartCoroutine(PlayHitEffect());
        }
    }

    IEnumerator PlayHitEffect()
    {
        Hit_Effect.SetActive(true);

        yield return new WaitForSeconds(0.3f);

        Hit_Effect.SetActive(false);
    }

    public int PlayerHP
    {
        get 
        {
            return Hp;
        }       
    }
}
